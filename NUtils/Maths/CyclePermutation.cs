//
//  CyclePermutation.cs
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
using NUtils.Abstract;
using System.Collections.Generic;

namespace NUtils.Maths {
	/// <summary>
	/// A permutation described as a sequence of integers that describe a cycle.
	/// </summary>
	/// <remarks>
	/// <para>
	/// For instance if the cycle is <c>2-&gt;5-&gt;7-&gt;3</c>, then <c>2</c> is
	/// mapped on <c>5</c>, <c>3</c> on <c>2</c>, <c>5</c> on <c>7</c> and <c>7</c>
	/// on <c>3</c>, the other indices map on themselves.
	/// </para>
	/// <para>
	/// <see cref="CyclePermutation"/> instances are permutations that are used
	/// if most of the elements map on themselves. It is not possible to represent
	/// all possible permutations with a <see cref="CyclePermutation"/> instance.
	/// </para>
	/// <para>
	/// An empty cycle represents a permutation that maps all indices to themselves.
	/// </para>
	/// </remarks>
	public class CyclePermutation : INormalize, IValidateable {

		#region Fields
		private int[] cycle;
		#endregion
		#region IValidateable implementation
		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		/// <remarks>
		/// In order to be valid, a <see cref="CyclePermutation"/> should contain every
		/// index at most once and all indices must be larger or equal to zero.
		/// </remarks>
		public bool IsValid {
			get {
				HashSet<int> hs = new HashSet<int> ();
				foreach (int ix in this.cycle) {
					if (ix < 0x00 || !hs.Add (ix)) {
						return false;
					}
				}
				return true;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="CyclePermutation"/> class with a given initial cycle.
		/// </summary>
		/// <param name="cycle">The given cycle that represents the permutation.</param>
		/// <remarks>
		/// <para>
		/// The values are not copied: modifications to the given array will modify the <see cref="CyclePermutation"/>
		/// as well and vice versa.
		/// </para>
		/// <para>
		/// The given values are not checked: it is possible that the <see cref="CyclePermutation"/> is not valid
		/// after initialization.
		/// </para>
		/// </remarks>
		public CyclePermutation (int[] cycle) {
			this.cycle = cycle;
		}
		#endregion
		#region INormalize implementation
		/// <summary>
		/// Normalizes this instance.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Normalization modifies the representation of the instance such that instances with the same "content" all
		/// have the same "representation".
		/// </para>
		/// </remarks>
		public void Normalize () {
			int[] c = this.cycle;
			int nc = c.Length;
			if (nc > 0x00) {
				int min = c [0x00], cur;
				int mi = 0x00;
				for (int i = 0x01; min > 0x00 && i < nc; i++) {
					cur = c [i];
					if (cur < min) {
						min = cur;
						mi = i;
					}
				}
				int gcd = MathUtils.GreatestCommonDivider (nc, mi);
				for (int i = 0x00; i < mi; i++) {//perform shift
					int tmp = c [i];
					int j = i;
					int k = i + mi;
					for (; k < nc; j += mi, k += mi) {
						c [j] = c [k];
					}
					c [j] = tmp;
				}
			}
		}
		#endregion
		#region Equals method
		/// <summary>
		/// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="CyclePermutation"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="CyclePermutation"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="NUtils.Maths.CyclePermutation"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj) {
			CyclePermutation perm = obj as CyclePermutation;
			if (perm != null) {
				perm.Normalize ();
				int[] ca = this.cycle;//California dreamin'
				int[] cb = perm.cycle;
				int na = ca.Length;
				if (na == cb.Length) {
					for (int i = 0x00; i < na; i++) {
						if (ca [i] != cb [i]) {
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="CyclePermutation"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			this.Normalize ();
			int[] c = this.cycle;
			int nc = c.Length;
			int hash = 0x00;
			for (int i = 0x00; i < nc; i++) {
				hash *= 0x03;
				hash ^= c [i].GetHashCode ();
			}
			return hash;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="CyclePermutation"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="CyclePermutation"/>.</returns>
		/// <remarks>
		/// <para>
		/// If the cycle has a length of zero, <c>"Identity"</c> is returned, if the cycle has a length that is larger than
		/// zero, the indices in the cycle are represented with arrows in between and between brackets (e.g. <c>(2-&gt;5-&gt;7-&gt;3)</c>).
		/// </para>
		/// </remarks>
		public override string ToString () {
			int[] c = this.cycle;
			if (c.Length <= 0x00) {
				return "Identity";
			} else {
				return string.Format ("({0})", string.Join ("->", this.cycle));
			}
		}
		#endregion
	}
}

