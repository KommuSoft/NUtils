//
//  Predicates.cs
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
	/// A utility class to generate predicates.
	/// </summary>
	public static class PredicateUtils {

		/// <summary>
		/// Generate a predicate that checks for each instance if it withing the specified range. Bounds are inclusive.
		/// </summary>
		/// <returns>A <see cref="T:Predicate`1"/> that succeeds if the given value is withing bounds.</returns>
		/// <param name="frm">The lower bound, inclusive.</param>
		/// <param name="to">The upper bound, inclusive.</param>
		/// <typeparam name="T">The type of the objects on which the predicate is applied.</typeparam>
		public static Predicate<T> RangePredicate<T> (T frm, T to) where T : IComparable<T> {
			return (x => frm.CompareTo (x) <= 0x00 && to.CompareTo (x) >= 0x00);
		}
	}
}

