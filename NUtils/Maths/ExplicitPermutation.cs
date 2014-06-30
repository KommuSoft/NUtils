//
//  Permutation.cs
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
using NUtils.Abstract;
using NUtils.Bitwise;
using System.Collections.Generic;

namespace NUtils.Maths {
	/// <summary>
	/// A basic implementation of the <see cref="IPermutation"/> interface that stores the permutation explicitly as
	/// an array. This is useful for fast item access but takes much memory.
	/// </summary>
	public class ExplicitPermutation : ExplicitTransition, IPermutation, IValidateable {

		#region IValidateable implementation
		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid {
			get {
				int[] idc = this.Indices;
				int n = idc.Length;
				CompactBitVector cbv = new CompactBitVector (n);
				for (int i = 0x00; i < n; i++) {
					cbv.Add (idc [i]);
				}
				return cbv.AllSet;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitPermutation"/> class.
		/// </summary>
		/// <param name="n">N.</param>
		public ExplicitPermutation (int n) : base(n) {
			this.Reset ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitPermutation"/> class with a given list of initial permutations.
		/// </summary>
		/// <param name="indices">The initial indices.</param>
		public ExplicitPermutation (IEnumerable<int> indices) : base(indices) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplicitPermutation"/> class with a given initial permutation.
		/// </summary>
		/// <param name="indices">The initial indices.</param>
		/// <remarks>
		/// <para>The values of the given indices are not copied: modifications made to the <paramref name="indices"/>
		/// array will have effect on the permutations as well.</para>
		/// <para>Consistency is not checked: it is possible that the described permutation is not possible. The user
		/// should check this.</para>
		/// </remarks>
		public ExplicitPermutation (params int[] indices) : base(indices) {
		}
		#endregion
		#region IPermutable implementation
		/// <summary>
		/// Swaps the content associated with the two given indices.
		/// </summary>
		/// <param name="i">The first index to swap.</param>
		/// <param name="j">The second index to swap.</param>
		public void Swap (int i, int j) {
			int[] idx = this.Indices;
			int tmp = idx [i];
			idx [i] = idx [j];
			idx [j] = tmp;
		}

		/// <summary>
		/// Swaps the content of the indices according to the given permutation.
		/// </summary>
		/// <param name="permutation">The given permutation that specifies how the content should be permutated.</param>
		public void Swap (IPermutation permutation) {
			int[] ia = this.Indices;
			int l = ia.Length, lp = permutation.Length, f;
			for (int i = 0x00; i < l; i++) {
				f = ia [i];
				if (f < lp) {
					ia [i] = permutation.GetTransitionOfIndex (f);
				}
			}
		}

		/// <summary>
		/// Calculate a permuatation that is the oposite function of this <see cref="IPermutation"/>
		/// </summary>
		/// <returns>
		/// A <see cref="IPermutation"/> that represents the opposite permutation. In other words
		/// applying the resulting permutation on this permuation results in an identity permutation.
		/// </returns>
		public IPermutation Reverse () {
			int[] ia = this.Indices;
			int na = ia.Length;
			int[] ib = new int[na];
			for (int i = 0x00; i < na; i++) {
				ib [ia [i]] = i;
			}
			return new ExplicitPermutation (ib);
		}

		/// <summary>
		/// Calculates the reverse permutation and stores it in this <see cref="IPermutation"/> instance.
		/// </summary>
		public void LocalReverse () {
			int[] ia = this.Indices;
			int na = ia.Length;
			int[] ib = new int[na];
			for (int i = 0x00; i < na; i++) {
				ib [ia [i]] = i;
			}
			for (int i = 0x00; i < na; i++) {
				ia [i] = ib [i];
			}
		}
		#endregion
		#region IResetable implementation
		/// <summary>
		/// Sets the instance back to its original state.
		/// </summary>
		public void Reset () {
			int[] idx = this.Indices;
			int n = idx.Length;
			for (int i = 0x00; i < n; i++) {
				idx [i] = i;
			}
		}
		#endregion
		#region Static generators
		/// <summary>
		/// Calculate the identity permutation for a given number of elements.
		/// </summary>
		/// <returns>A <see cref="ExplicitPermutation"/> that represents the identity operation for the given number of elements.</returns>
		/// <param name="n">The given number of elements.</param>
		public static ExplicitPermutation Identity (int n) {
			int[] idx = new int[n];
			for (int i = 0x00; i < n; i++) {
				idx [i] = i;
			}
			return new ExplicitPermutation (idx);
		}
		#endregion
	}
}