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

namespace NUtils {
	/// <summary>
	/// A basic implementation of the <see cref="IPermutation"/> interface.
	/// </summary>
	public class Permutation : IPermutation {

		private readonly int[] indices;
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
	}
}