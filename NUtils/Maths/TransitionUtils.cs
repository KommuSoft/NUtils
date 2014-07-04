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
	}
}

