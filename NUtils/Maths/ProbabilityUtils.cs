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
	/// A set of utility functions related to probabilistic concepts and instances related to distributions,...
	/// </summary>
	public static class ProbabilityUtils {

		#region Tests
		/// <summary>
		/// Determines if the given list of probabilities is a valid distribution given the <paramref name="epsilon"/> value.
		/// </summary>
		/// <returns><c>true</c> if all probabilistic values are larger than or equal to zero and sum up to one
		/// (given a certain tolerance); otherwise, <c>false</c>.</returns>
		/// <param name="probabilities">The given list of probabilistic values to check.</param>
		/// <remarks>
		/// <para>If the given <paramref name="probabilities"/> is not effective, the condition returns <c>false</c>.</para>
		/// <para>The epsilon value is set on <c>1e-6d</c>.</para>
		/// </remarks>
		public static bool IsValidDistribution (this IEnumerable<double> probabilities) {
			return IsValidDistribution (probabilities, 1e-6d);
		}

		/// <summary>
		/// Determines if the given list of probabilities is a valid distribution given the <paramref name="epsilon"/> value.
		/// </summary>
		/// <returns><c>true</c> if all probabilistic values are larger than or equal to zero and sum up to one
		/// (given the <paramref name="epsilon"/> maximum difference); otherwise, <c>false</c>.</returns>
		/// <param name="probabilities">The given list of probabilistic values to check.</param>
		/// <param name="epsilon">The maximum difference of the sum to one.</param>
		/// <remarks>
		/// <para>If the given <paramref name="probabilities"/> is not effective, the condition returns <c>false</c>.</para>
		/// </remarks>
		public static bool IsValidDistribution (this IEnumerable<double> probabilities, double epsilon) {
			if (probabilities != null) {
				double sum = 0.0d;
				foreach (double p in probabilities) {
					if (p < 0.0d) {
						return false;
					}
					sum += p;
				}
				return MathUtils.EqualEpsilon (1.0d, sum, epsilon);
			} else {
				return false;
			}
		}
		#endregion
		#region Caching
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
		#endregion
	}
}

