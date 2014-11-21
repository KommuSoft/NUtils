//
//  IJumpEnumerable.cs
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
	/// An interface specifying that the instance can generate a <see cref="T:IJumpEnumerator`1"/> instance, a <see cref="T:IEnumerator`1"/>
	/// that can skip several items at once.
	/// </summary>
	/// <typeparam name='T'></typeparam>
	public interface IJumpEnumerable<out T> : IEnumerable<T> {

		/// <summary>
		/// Get a <see cref="T:IJumpEnumerator"/> to enumerate over this instance but in such a way
		/// that several items can be skipped at once (or at least faster than linear).
		/// </summary>
		/// <returns>A <see cref="T:IJumpEnumerator"/> that can enumerate over this <see cref="T:IJumpEnumerable`1"/>.</returns>
		IJumpEnumerator<T> GetJumpEnumerator ();
	}
}

