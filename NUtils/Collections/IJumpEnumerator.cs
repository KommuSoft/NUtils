//
//  IJumpEnumerator.cs
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
	/// An interface describing a <see cref="T:IEnumerator`1"/> that but with a <see cref="M:Jump"/> method
	/// that allows to move through the <see cref="T:IEnumerable`1"/> several steps at a time.
	/// </summary>
	/// <typeparam name='T'>The type of elements enumerated by the <see cref="T:IJumpEnumerator`1"/></typeparam>
	public interface IJumpEnumerator<out T> : IEnumerator<T> {

		/// <summary>
		/// Move the given number of <paramref name="steps"/> forward in to the enumerator process.
		/// </summary>
		/// <param name="steps">The number of steps to move forward.</param>
		/// <returns><c>true</c> if the jump was successful (if the cursor is standing on an element, or the number of <paramref name="steps"/> is zero or less; otherwise <c>false</c>.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		/// <remarks>
		/// <para>If the number of jumps is zero or less, nothing happens and the answer is always <c>true</c>, the cursor does not return back.</para>
		/// </remarks>
		bool Jump (int steps);
	}
}

