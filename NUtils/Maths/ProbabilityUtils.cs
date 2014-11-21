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
using System.Diagnostics.Contracts;

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
		#region Picking random from a generic ICollection
		/// <summary>
		/// Pick <paramref name="k"/> unique uniformly items of of the given <paramref name="collection"/> using an algorithm
		/// that runs linear with the size of the collection. This algorithm is only useful if <paramref name="k"/> is proportional
		/// to the size of the <paramref name="collection"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that enumerates <paramref name="k"/> unique elements in the same
		/// order as how the items are enumerated from the <paramref name="collection"/>. The probability of an item
		/// getting selected is uniform.</returns>
		/// <param name="collection">The collection that contains the item to select.</param>
		/// <param name="k">The number of items to enumerate, should be larger than zero.</param>
		/// <typeparam name="T">The type of elements in the <paramref name="collection"/> that will be enumerated.</typeparam>
		/// <exception cref="ArgumentNullException">If the given <paramref name="collection"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If <paramref name="k"/> is greater than the size of the <paramref name="collection"/>.</exception>
		/// <exception cref="ArgumentException">If the <paramref name="collection"/> generates a non-effective <see cref="T:IEnumerator`1"/>.</exception>
		/// <remarks>
		/// <para>If <paramref name="k"/> is less than or equal to zero, no items are enumerated.</para>
		/// <para>If the <paramref name="collection"/> contains an item twice, it can be enumerated twice. The algorithm only guarantees that an item at the same index will not be enumerated twice.</para>
		/// </remarks>
		public static IEnumerable<T> PickKUniform<T> (this ICollection<T> collection, int k) {
			if (collection == null) {
				throw new ArgumentNullException ("collection", "The collection must be effective");
			}
			int n = collection.Count;
			if (k > n) {
				throw new ArgumentException ("The number of items to pick must be less than or equal to the size of the collection.", "k");
			}
			IEnumerator<T> colen = collection.GetEnumerator ();
			if (colen == null) {
				throw new ArgumentException ("The enumerator of the given collection must be effective.", "collection");
			}
			Contract.EndContractBlock ();
			double pi, r;
			int i, l;
			colen.MoveNext ();
			for (; k > 0x00; k--) {
				i = 0x00;
				l = n - k;
				pi = (double)k / n;
				r = MathUtils.NextDouble ();
				while (pi < r) {
					r -= pi;
					colen.MoveNext ();
					pi *= (l - i++);
					pi /= (n - i);
				}
				yield return colen.Current;
				colen.MoveNext ();
				n -= i + 0x01;
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

