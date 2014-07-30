//
//  EnumerableCollection.cs
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

namespace NUtils.Collections {
	/// <summary>
	/// A utility class for the construction/generation of several <see cref="T:IEnumerable`1"/> instances.
	/// </summary>
	public static class EnumerableCollection {

		/// <summary>
		/// Enumerates the integer values between zero (<c>0</c>, inclusive) and the given <paramref name="upper"/>
		/// bound, exclusive, with a delta of one (<c>1</c>).
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that enumerates the integer values between zero (inclusive) and
		/// <paramref name="upper"/> (exclusive).</returns>
		/// <param name="upper">The given upper bound, exclusive.</param>
		/// <remarks>
		/// <para>
		/// The given <see cref="T:IEnumerable`1"/> is guaranteed to be finite, regardless of the value
		/// of <paramref name="upper"/>.
		/// </para>
		/// </remarks>
		public static IEnumerable<int> RangeEnumerable (int upper) {
			return RangeEnumerable (0x00, upper, 0x01);
		}

		/// <summary>
		/// Enumerates the integer values between the given <paramref name="lower"/> bound (inclusive) and the given
		/// <paramref name="upper"/> bound, exclusive, optionally with a given <paramref name="delta"/> per step.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that enumerates the integer values between <paramref name="lower"/>
		/// (inclusive) and <paramref name="upper"/> (exclusive) with steps of <paramref name="delta"/>.</returns>
		/// <param name="lower">The given lower bound, inclusive.</param>
		/// <param name="upper">The given upper bound, exclusive.</param>
		/// <param name="delta">The given difference per step, optional, by default equal to one (<c>1</c>).</param>
		/// <remarks>
		/// <para>
		/// The given <see cref="T:IEnumerable`1"/> is guaranteed to be finite if <paramref name="delta"/> is not equal
		/// to zero (<c>0</c>), regardless of the value of <paramref name="lower"/>, <paramref name="upper"/>, this due
		/// to the overflow of <see cref="int"/> values.
		/// </para>
		/// </remarks>
		public static IEnumerable<int> RangeEnumerable (int lower, int upper, int delta = 0x01) {
			for (int i = lower; i < upper; i += delta) {
				yield return i;
			}
		}
	}
}