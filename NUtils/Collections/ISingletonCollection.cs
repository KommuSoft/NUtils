//
//  ISingletonCollection.cs
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
using System.Collections;
using System.Collections.Generic;

namespace NUtils.Collections {

	/// <summary>
	/// An interface describing a singleton, a singleton is a collection with exactly one element.
	/// </summary>
	/// <typeparam name='TElement'>The type of the element stored in this <see cref="T:ISingletonCollection`1"/></typeparam>
	/// <remarks>
	/// <para>A <see cref="T:ISingletonCollection"/> can be used if a collection is required, and the programmer
	/// wishes to provide a single item without paying the overhead of collection maintenance.</para>
	/// </remarks>
	public interface ISingletonCollection<TElement> : ICollection<TElement> {

		/// <summary>
		/// Gets or sets the single element contained in this <see cref="T:ISingletonCollection`1"/> instance.
		/// </summary>
		/// <value>The single element stored in this singleton.</value>
		TElement Element {
			get;
			set;
		}
	}
}

