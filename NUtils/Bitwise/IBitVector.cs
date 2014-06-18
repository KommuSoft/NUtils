//
//  IBitVector.cs
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
using System.Collections.Generic;
using NUtils.Abstract;

namespace NUtils.Bitwise {
	/// <summary>
	/// A vector of bits. This should be implemented in a compact way.
	/// </summary>
	public interface IBitVector : ILength, ICollection<int>, ILowest<int>, ILowerEnumerable<int>, ILocalBitwise<IBitVector> {

		/// <summary>
		/// Gets or sets the value of a single bit in the bitvector.
		/// </summary>
		/// <param name="index">The given index of the bit to get or set.</param>
		/// <value>The bit at <paramref name="index"/>.</value>
		bool this [int index] {
			get;
			set;
		}

		/// <summary>
		/// Gets a value indicating whether all the bits in this <see cref="IBitVector"/> instance are set.
		/// </summary>
		/// <value><c>true</c> if all bits are set; otherwise, <c>false</c>.</value>
		bool AllSet {
			get;
		}

		/// <summary>
		/// Gets 64 bits all packed in one <see cref="ulong"/> of the given <paramref name="block"/> index.
		/// </summary>
		/// <returns>An <see cref="ulong"/> that contains 64 bits of the given <paramref name="block"/> index.</returns>
		/// <param name="block">The given block index.</param>
		ulong GetBlock64 (int block);

		/// <summary>
		/// Resets the bits of this <see cref="IBitVector"/> instance where the corresponding bits
		/// in the given <paramref name="other"/> <see cref="IBitVector"/> are set.
		/// </summary>
		/// <param name="other">A <see cref="IBitVector"/> that specifies which indices to remove.</param>
		void Remove (IBitVector other);
	}
}

