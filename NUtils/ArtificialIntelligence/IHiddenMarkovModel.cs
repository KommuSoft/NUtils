//
//  IHiddenMarkovModel.cs
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
using NUtils.Maths;
using System.Collections.Generic;
using System.IO;

namespace NUtils.ArtificialIntelligence {
	/// <summary>
	/// An interface specifying a hidden Markov model.
	/// </summary>
	public interface IHiddenMarkovModel : IFiniteDistribution {

		/// <summary>
		/// Get the number of output characters of the hidden Markov model.
		/// </summary>
		/// <value>The number of output characters of the hidden Markov model.</value>
		int OutputSize {
			get;
		}

		/// <summary>
		/// Gets the transition probability of moving from a hidden state with <paramref name="index1"/> to a hidden
		/// state with <paramref name="index2"/>.
		/// </summary>
		/// <returns>The probability of a transition from the hidden state with the first index to the hidden
		/// state with the second index.</returns>
		/// <param name="index1">The index of the initial hidden state.</param>
		/// <param name="index2">The index of the final hidden state.</param>
		double GetTransitionProbability (int index1, int index2);

		/// <summary>
		/// Gets the emission probability of producing the given <paramref name="output"/> given the system
		/// is in the given <paramref name="state"/>.
		/// </summary>
		/// <returns>The probability of a transition from the hidden state with the first index to the hidden
		/// state with the second index.</returns>
		/// <param name="state">The given hidden state.</param>
		/// <param name="output">The given output character.</param>
		double GetEmissionProbability (int state, int output);

		/// <summary>
		/// Train this <see cref="IHiddenMarkovModel"/> by using the given <see cref="T:IFiniteStateMachine`1"/>
		/// model. Since such finite state machine generates infinite sequences of output, training that sequence
		/// would result in an underdetermined system. The sample length must thefore be specified.
		/// </summary>
		/// <returns>The error on the training. In other words how different the original Hidden Markov model is from the
		/// given finite state machine.</returns>
		/// <param name="fsm">The finite state machine from which the <see cref="IHiddenMarkovModel"/> learns.</param>
		/// <param name="initialDistribution"> the initial distribution for the states of the finite state machine.</param>
		/// <param name="sampleLength">The length of the samples, has impact on the trained model.</param>
		double Train (IFiniteStateMachine<int> fsm, IList<int> initialDistribution, int sampleLength);
	}
}

