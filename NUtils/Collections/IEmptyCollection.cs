//
//  IEmptyCollection.cs
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
	/// An interface describing a collection that is empty, and never contains any elements.
	/// </summary>
	/// <typeparam name='TElement'>The type of elements contained in this collection (this is a purely virtual type since the collection is empty).</typeparam>
	/// <remarks>
	/// <para>This collection is mainly used if a collection is required (as parameter),
	/// and no items should be entered.</para>
	/// </remarks>
	public interface IEmptyCollection<TElement> : ICollection<TElement> {
	}
}

