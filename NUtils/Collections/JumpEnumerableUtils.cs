//
//  JumpEnumerableUtils.cs
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
using System.Diagnostics.Contracts;
using System.Collections.Generic;

namespace NUtils.Collections {

	/// <summary>
	/// A set of utility functions for collections to support <see cref="T:IJumpEnumerable`1"/> and <see cref="T:IJumpEnumerator`1"/> interfaces.
	/// </summary>
	public static class JumpEnumerableUtils {

		#region JumpEnumerator construction
		/// <summary>
		/// Get a <see cref="T:IJumpEnumerator"/> to enumerate over this instance but in such a way
		/// that several items can be skipped at once (or at least faster than linear).
		/// </summary>
		/// <param name="list">The given list to generate a <see cref="T:IJumpEnumerator`1"/> from.</param>
		/// <returns>A <see cref="T:IJumpEnumerator"/> that can enumerate over this <see cref="T:IJumpEnumerable`1"/>.</returns>
		/// <typeparam name='T'>The type of elements stored in the <paramref name="list"/> and will be enumerated.</typeparam>
		/// <exception cref="ArgumentNullException">If the given <paramref name="list"/> is not effective.</exception>
		public static ListJumpEnumerator<T> GetJumpEnumerator<T> (this List<T> list) {
			if (list == null) {
				throw new ArgumentNullException ("list", "the list must be effective");
			}
			Contract.EndContractBlock ();
			return new ListJumpEnumerator<T> (list);
		}
		#endregion
	}
}

