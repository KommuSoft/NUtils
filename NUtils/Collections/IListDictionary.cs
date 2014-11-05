//
//  IListDictionary.cs
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
	/// An interface describing a dictionary, but where multiple values can be associated with the same key.
	/// </summary>
	/// <typeparam name='TKey'>The type of the keys of the dictionary.</typeparam>
	/// <typeparam name='TValue'>The type of the values of the dictionary.</typeparam>
	public interface IListDictionary<TKey,TValue,TCollection> : IDictionary<TKey,TValue>
	    where TCollection : ICollection<TValue>, new() {

		/// <summary>
		/// Get the list of values associated with the given <paramref name="key"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that lists all the values associated
		/// with the given <paramref name="key"/>. If no value is associated with the key,
		/// the <see cref="T:IEnumerable`1"/> is empty.</returns>
		/// <param name="key">The given key to query the dictionary with.</param>
		/// <exception cref="ArgumentNullException">If the given key is not effective.</exception>
		IEnumerable<TValue> GetValues (TKey key);

		/// <summary>
		/// Add all the given <paramref name="values"/> to the dictionary.
		/// </summary>
		/// <param name="values">A <see cref="T:IEnumerable`1"/> of <see cref="T:KeyValuePair`2"/> instances that are added to this <see cref="T:IListDictionary`2"/>.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="values" /> is null.</exception>
		/// <remarks>
		/// <para><see cref="T:KeyValuePair`2"/> instances with a key that is not effective are ignored.</para>
		/// <para>If the key already exists, the value is added to the list associated with the key. If the value already exists, the value is
		/// added a second time.</para>
		/// </remarks>
		void AddAll (IEnumerable<KeyValuePair<TKey,TValue>> values);
	}
}

