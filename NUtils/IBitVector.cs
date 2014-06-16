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
using System;
using System.Collections.Generic;

namespace NUtils {
	/// <summary>
	/// A vector of bits. This should be implemented in a compact way.
	/// </summary>
	public interface IBitVector : ILength, ICollection<int> {

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
		/// Computes the AND-operator of this <see cref="IBitVector"/> and the given <see cref="IBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="IBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the AND-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="IBitVector"/> to perform the AND-operation with.</param>
		IBitVector And (IBitVector other);

		/// <summary>
		/// Computes the OR-operator of this <see cref="IBitVector"/> and the given <see cref="IBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="IBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the OR-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="IBitVector"/> to perform the OR-operation with.</param>
		IBitVector Or (IBitVector other);

		/// <summary>
		/// Computes the XOR-operator of this <see cref="IBitVector"/> and the given <see cref="IBitVector"/>.
		/// </summary>
		/// <returns>An <see cref="IBitVector"/> instance with length the maximum of both vectors
		/// where bit <c>i</c> is the XOR-operation applied to the <c>i</c>-th bit
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="IBitVector"/> to perform the XOR-operation with.</param>
		IBitVector Xor (IBitVector other);

		/// <summary>
		/// Computes the NOT-operator of this <see cref="IBitVector"/> instance.
		/// </summary>
		/// <returns>An <see cref="IBitVector"/> instance with the same length such that the <c>i</c>-th bit
		/// is the negation of the <c>i</c>-th bit of this <see cref="IBitVector"/> instance.</returns>
		IBitVector Not ();

		/// <summary>
		/// Applies the AND-operator on this <see cref="IBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="IBitVector"/> instance to apply the AND-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:And"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		void AndLocal (IBitVector other);

		/// <summary>
		/// Applies the OR-operator on this <see cref="IBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="IBitVector"/> instance to apply the OR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Or"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		void OrLocal (IBitVector other);

		/// <summary>
		/// Applies the XOR-operator on this <see cref="IBitVector"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="IBitVector"/> instance to apply the XOR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Xor"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		void XorLocal (IBitVector other);

		/// <summary>
		/// Applies the NOT-operator on this <see cref="IBitVector"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:Not"/> method since the length of the
		/// vector is not modified. Furthermore this method will use less memory.</para>
		/// </remarks>
		void NotLocal ();

		/// <summary>
		/// Gets 64 bits all packed in one <see cref="ulong"/> of the given <paramref name="block"/> index.
		/// </summary>
		/// <returns>An <see cref="ulong"/> that contains 64 bits of the given <paramref name="block"/> index.</returns>
		/// <param name="block">The given block index.</param>
		ulong GetBlock64 (int block);

		/// <summary>
		/// Calculates the index of the lowest bit that is set with an index greater than or equal to the given
		/// <paramref name="lower"/> bound.
		/// </summary>
		/// <returns>The lowest index larger or equal than <paramref name="lower"/> of the lowest bit that is true.</returns>
		/// <param name="lower">The given lower bound on the index.</param>
		/// <remarks>
		/// <para>If no bit is set with an index larger than or equal to the given <paramref name="lower"/> bound, <c>-1</c> is returned.</para>
		/// </remarks>
		int GetLowest (int lower = 0x00);
	}
}

