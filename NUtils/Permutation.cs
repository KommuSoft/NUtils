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
using System;
using System.Text;

namespace NUtils {
	/// <summary>
	/// A basic implementation of the <see cref="IPermutation"/> interface.
	/// </summary>
	public class Permutation : IPermutation, IValidateable {

		#region Fields
		private readonly int[] indices;
		#endregion
		#region IValidateable implementation
		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		public bool IsValid {
			get {
				int[] idc = this.indices;
				int n = idc.Length;
				CompactBitVector cbv = new CompactBitVector (n);
				for (int i = 0x00; i < n; i++) {
					cbv.Add (idc [i]);
				}
				return cbv.AllSet;
			}
		}
		#endregion
		#region IPermutation implementation
		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <param name="index">The given index.</param>
		public int this [int index] {
			get {
				return this.indices [index];
			}
		}
		#endregion
		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The length.</value>
		public int Length {
			get {
				return this.indices.Length;
			}
		}
		#endregion
		/// <summary>
		/// Initializes a new instance of the <see cref="Permutation"/> class.
		/// </summary>
		/// <param name="n">N.</param>
		public Permutation (int n) {
			this.indices = new int[n];
			this.Reset ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Permutation"/> class with a given initial permutation.
		/// </summary>
		/// <param name="n">The number of indices.</param>
		/// <param name="indices">The initial indices.</param>
		/// <remarks>
		/// <para>The values of the given indices are not copied: modifications made to the <paramref name="indices"/>
		/// array will have effect on the permutations as well.</para>
		/// <para>Consistency is not checked: it is possible that the described permutation is not possible. The user
		/// should check this.</para>
		/// </remarks>
		public Permutation (params int[] indices) {
			this.indices = indices;
		}
		#region IPermutable implementation
		/// <summary>
		/// Swaps the content associated with the two given indices.
		/// </summary>
		/// <param name="i">The first index to swap.</param>
		/// <param name="j">The second index to swap.</param>
		public void Swap (int i, int j) {
			int[] idx = this.indices;
			int tmp = idx [i];
			idx [i] = idx [j];
			idx [j] = tmp;
		}
		#endregion
		#region IResetable implementation
		/// <summary>
		/// Sets the instance back to its original state.
		/// </summary>
		public void Reset () {
			int[] idx = this.indices;
			int n = idx.Length;
			for (int i = 0x00; i < n; i++) {
				idx [i] = i;
			}
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="NUtils.Permutation"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="NUtils.Permutation"/>.</returns>
		public override string ToString () {
			StringBuilder sb = new StringBuilder ("{");
			int[] d = this.indices;
			int dl = d.Length;
			for (int i = 0x00; i < dl; i++) {
				sb.AppendFormat (" {0}->{1}", i, d [i]);
			}
			sb.Append (" }");
			return sb.ToString ();
		}
		#endregion
	}
}