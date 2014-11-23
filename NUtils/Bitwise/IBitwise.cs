//
//  IBitwise.cs
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
namespace NUtils.Bitwise {

	/// <summary>
	/// An interface specifying that bitwise operations are supported with the given type <typeparamref name='TBitwise'/>
	/// </summary>
	/// <typeparamref name='TBitwise'>
	/// The type of objects with which bitwise operations are performed.
	/// </typeparamref>
	public interface IBitwise<TBitwise> where TBitwise : IBitwise<TBitwise> {

		/// <summary>
		/// Computes the AND-operator of this <see cref="T:IBitwise`1"/> and the given <typeparamref name="TBitwise"/> instance.
		/// </summary>
		/// <returns>An <typeparamref name="TBitwise"/> instance where a "bit" is the AND-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the AND-operation with.</param>
		TBitwise And (TBitwise other);

		/// <summary>
		/// Computes the OR-operator of this <see cref="T:IBitwise`1"/> and the given <typeparamref name="TBitwise"/> instance.
		/// </summary>
		/// <returns>An <typeparamref name="TBitwise"/> instance where a "bit" is the OR-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the OR-operation with.</param>
		TBitwise Or (TBitwise other);

		/// <summary>
		/// Computes the XOR-operator of this <see cref="T:IBitwise`1"/> and the given <typeparamref name="TBitwise"/> instance.
		/// </summary>
		/// <returns>An <typeparamref name="TBitwise"/> instance where a "bit" is the XOR-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the XOR-operation with.</param>
		TBitwise Xor (TBitwise other);

		/// <summary>
		/// Computes the NOT-operator of this <see cref="T:IBitwise`1"/> instance.
		/// </summary>
		/// <returns>An <typeparamref name="TBitwise"/> instance where a "bit" is the NOT-operation applied to the corresponding "bit"
		/// of this instance.</returns>
		TBitwise Not ();
	}
}

