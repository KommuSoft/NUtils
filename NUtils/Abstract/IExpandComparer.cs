//
//  ExpandComparer.cs
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

namespace NUtils.Abstract {

	/// <summary>
	/// An interface used to compare an instance of the <typeparamref name='TKey'/> type with an instance of
	/// the <see cref="TTarget"/> type. This can be used, for instance to find an element in an ordered array
	/// based on a key.
	/// </summary>
	/// <typeparam name='TKey'>The type of the key, used to retrieve an object.</typeparam>
	/// <typeparam name='TTarget'>The objects on which comparison actually takes place.</typeparam>
	/// <remarks>
	/// <para>Every <see cref="T:Comparer`1"/> is basically a <see cref="T:IExpandComparer`2"/> where the
	/// <typeparamref name='TKey'/> and the <typeparamref name='TTarget'/> type are the same.</para>
	/// </remarks>
	public interface IExpandComparer<TKey,TTarget> {

		/// <summary>
		/// Compares the given key with the given target.
		/// </summary>
		/// <returns>A value larger than zero if the given <paramref name="key"/> is larger than the
		/// given <paramref name="target"/>. Zero if the given <paramref name="key"/> is equal to the given
		/// <paramref name="target"/>. A value less than zero if the given <paramref name="key"/> is smaller
		/// than the given <paramref name="target"/>.</returns>
		/// <param name="key">The given key to compare.</param>
		/// <param name="target">The given target to compare.</param>
		int Compare (TKey key, TTarget target);
	}
}

