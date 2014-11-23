//
//  ILocalBitwise.cs
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

namespace NUtils.Bitwise {

	/// <summary>
	/// An interface specifying that "bitwise" operations can be performed locally on this instance. Local
	/// operations are in most cases faster since for instance no additional memory should be declared.
	/// </summary>
	public interface ILocalBitwise<TBitwise> : IBitwise<TBitwise> where TBitwise : ILocalBitwise<TBitwise> {

		/// <summary>
		/// Applies the AND-operator on this <see cref="T:ILocalBitwise`1"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="TBitwise"/> instance to apply the AND-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:IBitwise`1.And"/> method since for most
		/// instances, no additional "bits" will be declared and the method will be faster.</para>
		/// </remarks>
		void AndLocal (TBitwise other);

		/// <summary>
		/// Applies the OR-operator on this <see cref="T:ILocalBitwise`1"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="TBitwise"/> instance to apply the OR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:IBitwise`1.Or"/> method since for most
		/// instances, no additional "bits" will be declared and the method will be faster.</para>
		/// </remarks>
		void OrLocal (TBitwise other);

		/// <summary>
		/// Applies the XOR-operator on this <see cref="T:ILocalBitwise`1"/> instance and the <paramref name="other"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <param name="other">A <see cref="TBitwise"/> instance to apply the XOR-operation with.</param>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:IBitwise`1.Xor"/> method since for most
		/// instances, no additional "bits" will be declared and the method will be faster.</para>
		/// </remarks>
		void XorLocal (TBitwise other);

		/// <summary>
		/// Applies the NOT-operator on this <see cref="T:ILocalBitwise`1"/> instance.
		/// The result is stored in this instance.
		/// </summary>
		/// <remarks>
		/// <para>The method semantically differs from the <see cref="M:IBitwise`1.Not"/> method since for most
		/// instances, no additional "bits" will be declared and the method will be faster.</para>
		/// </remarks>
		void NotLocal ();

		/// <summary>
		/// Resets the given range of bits.
		/// </summary>
		/// <param name="lower">The lower bound of the range (inclusive).</param>
		/// <param name="upper">The upper bound of the range (inclusive).</param>
		void ResetRange (int lower, int upper);

		/// <summary>
		/// Sets the given range of bits.
		/// </summary>
		/// <param name="lower">The lower bound of the range (inclusive).</param>
		/// <param name="upper">The upper bound of the range (inclusive).</param>
		void SetRange (int lower, int upper);
	}
}

