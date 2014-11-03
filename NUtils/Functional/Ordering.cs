//
//  Ordering.cs
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

namespace NUtils.Functional {

	/// <summary>
	/// An enumeration describing the three possible outcomes of an ordering.
	/// </summary>
	[Flags]
	public enum Ordering : byte {
		/// <summary>
		/// The outcome of the ordering is unknown, for instance the ordering relation is partial.
		/// </summary>
		Unknown = 0x00,
		/// <summary>
		/// The outcome of the ordering is that the two items are equal.
		/// </summary>
		EQ = 0x02,
		/// <summary>
		/// The outcome of the ordering is that the first item is greater than the second item.
		/// </summary>
		GT = 0x04,
		/// <summary>
		/// The outcome of the ordering is that the first item is less than the second item.
		/// </summary>
		LT = 0x01,
		/// <summary>
		/// The outcome of the ordering is that the first item is greater than or equal to the second item.
		/// </summary>
		GE = EQ | GT,
		/// <summary>
		/// The outcome of the ordering is that the first item is less than or equal to the second item.
		/// </summary>
		LE = EQ | LT,
		/// <summary>
		/// The outcome of the ordering is that the first item is not equal to the second item.
		/// </summary>
		NE = GT | LT,
		/// <summary>
		/// A fabricated type, the result of the comparison
		/// is that the first item is less then, equal and
		/// greater than the second item.
		/// </summary>
		/// <remarks>
		/// <para>
		/// This result can't be generated. But can be used to reason
		/// in a method.
		/// </para><para>
		/// Use this construction with caution.
		/// </para>
		/// </remarks>
		All = EQ | GT | LT
	}
}

