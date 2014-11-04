//
//  StandardFunctions.cs
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
	/// A library of default functions that can be used if a predicate is required.
	/// </summary>
	public static class StandardFunctions {

		#region Predicates
		/// <summary>
		/// Returns a predicate that returns true on all input (regardless of its value).
		/// </summary>
		/// <returns>A predicate that returns true on all input.</returns>
		/// <typeparam name="T">The type of items over which the predicate is defined.</typeparam>
		public static Predicate<T> AllPredicate<T> () {
			return x => true;
		}

		/// <summary>
		/// Returns the predicate that returns false for all input (regardless of its value).
		/// </summary>
		/// <returns>A predicate that returns false on all input.</returns>
		/// <typeparam name="T">The type of items over which the predicate is defined.</typeparam>
		public static Predicate<T> NonePredicate<T> () {
			return x => false;
		}

		/// <summary>
		/// A predicate that checks if the given value is effective.
		/// </summary>
		/// <returns>A predicate that returns <c>true</c> if the given parameter is effective, <c>false</c> otherwise.</returns>
		/// <typeparam name="T">The type of items over which the predicate is defined.</typeparam>
		public static Predicate<T> NotNull<T> ()
		where T : class {
			return x => x != null;
		}
		#endregion
	}
}

