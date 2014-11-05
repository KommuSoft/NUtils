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
	/// <typeparam name='TKey'>The type of the values of the dictionary.</typeparam>
	public interface IListDictionary<TKey,TValue,TCollection> : IDictionary<TKey,TValue>
	    where TCollection : ICollection<TValue>, new() {
	}
}

