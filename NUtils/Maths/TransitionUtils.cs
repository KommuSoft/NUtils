//
//  TransitionUtils.cs
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
using NUtils.Bitwise;

namespace NUtils.Maths {
	/// <summary>
	/// A utility class used for functions and methods on <see cref="T:ITransition"/> instances.
	/// </summary>
	public static class TransitionUtils {

		#region Strongly connected groups and walks
		/// <summary>
		/// Enumerate the sets of strongly connected groups: indices that form a cycle.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains the indices of strongly connected groups.</returns>
		/// <remarks>
		/// <para>The strongly connected groups are generated lazily: on demand the next group is generated.</para>
		/// <para>There is no guarantee on the order of the generated groups, nor on the first element of
		/// every group.</para>
		/// <para>Singleton groups are generated as well: indices that have a transition to themselves.</para>
		/// <para>The algorithm is a special case of Tarjans algorithm especially optimized for the transition
		/// functions.</para>
		/// </remarks>
		public static IEnumerable<IEnumerable<int>> GetStronglyConnectedGroups (this ITransition transition) {
			int n = transition.Length;
			CompactBitVector glb = CompactBitVector.All (n);
			CompactBitVector cur = CompactBitVector.All (n);
			int low = 0x00, idx, rem;
			Queue<int> pushQueue = new Queue<int> ();
			do {
				idx = low;
				do {
					cur.Remove (idx);
					pushQueue.Enqueue (idx);
					idx = transition.GetTransitionOfIndex (idx);
				} while(cur.Contains (idx) && glb.Contains (idx));
				if (glb.Contains (idx)) {//we've found a new group
					pushQueue.Enqueue (idx);
					do {
						rem = pushQueue.Dequeue ();
					} while(rem != idx);
					yield return pushQueue;
					pushQueue = new Queue<int> ();
				} else {
					pushQueue.Clear ();
				}
				glb.AndLocal (cur);
				low = glb.GetLowest (low + 0x01);
			} while(low > 0x00);
		}

		/// <summary>
		/// Get the maximim walking distance from any index of the given <see cref="ITransition"/> to
		/// its corresonding strongly connected group.
		/// </summary>
		/// <returns>The maximum walking distance from any index to a strongly connected group.</returns>
		/// <param name="transition">The given <see cref="ITransition"/> for which this distance is calculated.</param>
		/// <seealso cref="M:GetStronlyConnectedPeriod"/>
		public static int GetStronlyConnectedPeriod (this ITransition transition) {
			int n = transition.Length;
			CompactBitVector glb = CompactBitVector.All (n);
			CompactBitVector cur = CompactBitVector.All (n);
			int low = 0x00, idx, rem, siz, period = 0x01;
			Stack<int> stack = new Stack<int> ();
			do {
				idx = low;
				do {
					cur.Remove (idx);
					stack.Push (idx);
					idx = transition.GetTransitionOfIndex (idx);
				} while(cur.Contains (idx) && glb.Contains (idx));
				if (glb.Contains (idx)) {//we've found a new group
					siz = 0x00;
					do {
						rem = stack.Pop ();
						siz++;
					} while(rem != idx);
					Console.WriteLine (siz);
					period = MathUtils.LeastCommonMultiple (period, siz);
				}
				stack.Clear ();
				glb.AndLocal (cur);
				low = glb.GetLowest (low + 0x01);
			} while(low > 0x00);
			return period;
		}

		/// <summary>
		/// Get the maximim walking distance from any index of the given <see cref="ITransition"/> to
		/// its corresonding strongly connected group.
		/// </summary>
		/// <returns>The maximum walking distance from any index to a strongly connected group.</returns>
		/// <param name="transition">The given <see cref="ITransition"/> for which this distance is calculated.</param>
		/// <seealso cref="M:GetStronlyConnectedPeriod"/>
		public static int GetMaximumStronglyConnectedGroupsDistance (this ITransition transition) {
			int n = transition.Length;
			CompactBitVector glb = CompactBitVector.All (n);
			CompactBitVector cur = CompactBitVector.All (n);
			int[] dists = new int[n];//TODO replace with numbervector?
			int low = 0x00, idx, rem, dist, maxdist = 0x00;
			Stack<int> stack = new Stack<int> ();
			do {
				idx = low;
				do {
					cur.Remove (idx);
					stack.Push (idx);
					idx = transition.GetTransitionOfIndex (idx);
				} while(cur.Contains (idx) && glb.Contains (idx));
				if (glb.Contains (idx)) {//we've found a new group
					do {
						rem = stack.Pop ();
						dists [rem] = 0x00;
					} while(rem != idx);
					dist = 0x00;
				} else {
					dist = dists [idx];
				}
				while (stack.Count > 0x00) {
					dists [stack.Pop ()] = ++dist;
				}
				maxdist = Math.Max (maxdist, dist);
				glb.AndLocal (cur);
				low = glb.GetLowest (low + 0x01);
			} while(low > 0x00);
			return maxdist;
		}

		/// <summary>
		/// Calculate for each index of the transition the distance towards its strongly connected group and the
		/// tour size of that group.
		/// </summary>
		/// <returns>A <see cref="T:Tuple`2"/> containing two arrays. The first array contains the distance
		/// to a strongly connected group for the index of the transition. The second index contains
		/// the tour size the strongly connected group of the index of the transition.</returns>
		/// <param name="transition">The transition to calculate these values for.</param>
		/// <param name="distances">An array containing the distances to the corresponding strongly connected groups.</param>
		/// <param name="tourLengths">An array containing the tour lengths of the corresponding strongly connected groups.</param>
		/// <param name="initialTours">An array containing the first index of the strongly connected group.</param>
		public static void GetStronglyConnectedGroupsDistanceTour (this ITransition transition, out int[] distances, out int[] tourLengths, out int[] initialTours) {
			int n = transition.Length;
			CompactBitVector glb = CompactBitVector.All (n);
			CompactBitVector cur = CompactBitVector.All (n);
			distances = new int[n];
			tourLengths = new int[n];
			initialTours = new int[n];
			int low = 0x00, idx, ini, rem, dist, tour;
			Stack<int> stack = new Stack<int> ();
			Stack<int> ttack = new Stack<int> ();
			do {
				idx = low;
				do {
					cur.Remove (idx);
					stack.Push (idx);
					idx = transition.GetTransitionOfIndex (idx);
				} while(cur.Contains (idx) && glb.Contains (idx));
				if (glb.Contains (idx)) {//we've found a new group
					tour = 0x00;
					ini = idx;
					do {
						tour++;
						rem = stack.Pop ();
						distances [rem] = 0x00;
						initialTours [rem] = rem;
						ttack.Push (rem);
					} while(rem != idx);
					while (ttack.Count > 0x00) {
						tourLengths [ttack.Pop ()] = tour;
					}
					dist = 0x00;
				} else {
					dist = distances [idx];
					tour = tourLengths [idx];
					ini = initialTours [idx];
				}
				while (stack.Count > 0x00) {
					rem = stack.Pop ();
					distances [rem] = ++dist;
					tourLengths [rem] = tour;
					initialTours [rem] = ini;
				}
				glb.AndLocal (cur);
				low = glb.GetLowest (low + 0x01);
			} while(low > 0x00);
		}
		#endregion
		#region Caching
		/// <summary>
		/// Converts the given <see cref="ITransition"/> to a <see cref="ExplicitTransition"/>. The values
		/// are copied.
		/// </summary>
		/// <returns>An <see cref="ExplicitTransition"/> instance that is equivalent to the given transition,
		/// but the values are copied.</returns>
		/// <param name="transition">The transition to convert to an <see cref="ExplicitTransition"/>.</param>
		/// <remarks>
		/// <para>This method is mainly used to cache results of implicitly generated transitions.</para>
		/// </remarks>
		public static ExplicitTransition ToExplicitTransition (this ITransition transition) {
			int n = transition.Length;
			int[] ts = new int[n];
			for (int i = 0x00; i < n; i++) {
				ts [i] = transition.GetTransitionOfIndex (i);
			}
			return new ExplicitTransition (ts);
		}
		#endregion
	}
}