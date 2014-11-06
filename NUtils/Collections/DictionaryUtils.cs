//
//  DictionaryUtils.cs
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
	/// A set of utility functions for <see cref="T:IDictionary`2"/> instances.
	/// </summary>
	public static class DictionaryUtils {

		#region KeyValue pairs versus two parameters
		/// <summary>
		/// Determines whether the given <paramref name="value"/> is associated with the given <paramref name="key"/> in the given <paramref name="dictionary"/>.
		/// </summary>
		/// <param name="dictionary">The given dictionary to check for.</param>
		/// <param name="key">The given key to check for.</param>
		/// <param name="value">The given value to check for.</param>
		/// <returns><c>true</c> if the dictionary associates the given value with the given key; otherwise <c>false</c>.</returns>
		/// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
		/// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
		public static bool Contains<TKey,TValue> (this ICollection<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue value) {
			return dictionary.Contains (new KeyValuePair<TKey,TValue> (key, value));
		}

		/// <summary>
		/// Removes the given <paramref name="key"/> and <paramref name="value"/> association from the <paramref name="dictionary"/>.
		/// </summary>
		/// <returns><c>true</c> if the <paramref name="dictionary"/> contains an element with the <paramref name="key"/>; otherwise, <c>false</c>.
		/// This method also returns <c>false</c> if <paramref name="key"/> was not found in the original <paramref name="dictionary"/>.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <param name="value">The value of the element to remove.</param>
		public static bool Remove<TKey,TValue> (this ICollection<KeyValuePair<TKey, TValue>> dictionary, TKey key, TValue value) {
			return dictionary.Remove (new KeyValuePair<TKey,TValue> (key, value));
		}
		#endregion
	}
}

