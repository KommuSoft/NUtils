//
//  IProbabilisticTransition.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2014 Willem Van Onsem
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using NUtils.Abstract;

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying a probabilistic transition function: for each initial index
	/// there are one or more indices to which a transition is possible and each of these transitions
	/// has a probability.
	/// </summary>
	/// <remarks>
	/// <para>
	/// For each index, the sum of probabilities of transition from that index must sum up to one
	/// and must all be larger than or equal to zero.
	/// </para>
	/// </remarks>
	public interface IProbabilisticTransition : ILength, IEnumerable<Tuple<int,int,double>> {

		/// <summary>
		/// Gets the target indices of transitions from the given <paramref name="index"/> annotated
		/// with the probability of the transition.
		/// </summary>
		/// <returns>A list of tuples containing the target index and the probability of a transition
		/// from the given <paramref name="index"/> to the target index. Transitions with a probability
		/// of zero can be left out.
		/// </returns>
		/// <remarks>
		/// <para>
		/// All returned probabilities for a given index are larger than or equal to zero and sum
		/// up to one.
		/// </para>
		/// </remarks>
		IEnumerable<Tuple<int,double>> GetTransitionOfIndex (int index);

		/// <summary>
		/// Gets the probability of the transition from the given index <paramref name="frm"/> to the
		/// given index <paramref name="to"/>
		/// </summary>
		/// <returns>The transition probability from <paramref name="frm"/> to <paramref name="to"/>.</returns>
		/// <param name="frm">The intial index of the transition.</param>
		/// <param name="to">The final index of the transition.</param>
		/// <remarks>
		/// <para>
		/// If no transition exists from the given index <paramref name="frm"/> to the given index
		/// <paramref name="to"/>, zero (<c>0.0</c>) is returned.
		/// </para>
		/// </remarks>
		double GetTransitionProbability (int frm, int to);
	}
}