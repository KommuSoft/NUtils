//
//  IRegister.cs
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
using NUtils.Abstract;

namespace NUtils.Collections {

	/// <summary>
	/// A register is a datastructure that stores items based on a <typeparamref name="TKey"/> for
	/// fast lookups.
	/// </summary>
	/// <typeparam name='TKey'>The key on which the given items are stored.</typeparam>
	/// <typeparam name='TValue'>The items to store in the register</typeparam>
	public interface IRegister<TKey,TValue> : IListDictionary<TKey,TValue>, ICollection<TValue>, ICloneable<IRegister<TKey,TValue>> {

		/// <summary>
		/// A function that generates keys given a <typeparamref name="TValue"/>.
		/// </summary>
		/// <value>The generator of keys for this <see cref="T:IRegister`2"/>.</value>
		Func<TValue,TKey> KeyGenerator {
			get;
		}
	}
}

