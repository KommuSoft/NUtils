//
//  ProbabilisticTransitionUtils.cs
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

namespace NUtils.Maths {
	/// <summary>
	/// A set of utility functions for <see cref="IProbabilisticTransition"/> instances.
	/// </summary>
	public static class ProbabilisticTransitionUtils {

		/// <summary>
		/// Converts the given <see cref="T:IEnumerable`1"/> of probabilistic transitions into a <see cref="SparseProbabilisticTransition"/>
		/// instance.
		/// </summary>
		/// <returns>A <see cref="SparseProbabilisticTransition"/> that contains the given list of <see cref="T:Tuple`3"/> items.</returns>
		/// <param name="transitions">A list of probabilistic transitions represented by <see cref="T:Tuple`3"/> instances
		/// containing the intial index, the final index and the probability.</param>
		/// <remarks>
		/// <para>This method can be used for caching purposes.</para>
		/// </remarks>
		public static SparseProbabilisticTransition ToSparseProbabilisticTransition (this IEnumerable<Tuple<int,int,double>> transitions) {
			return new SparseProbabilisticTransition (transitions);
		}
	}
}

