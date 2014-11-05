//
//  IMulticollectionView.cs
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
	/// An interface that describes a view on multiple <see cref="T:ICollection`1"/> instances, the exact task
	/// depends on the "sub interface". Views are read-only.
	/// </summary>
	/// <typeparam name='TElement'>The type of the elements in the collection.</typeparam>
	/// <remarks>
	/// <para>Most operations like <see cref="M:ICollection`1.Contains"/> need to enumerate over all collections.</para>
	/// <para>This interface is mainly for programmer's convenience, and in many cases not that efficient.</para>
	/// <para>This interface is a View: if the components on which it is based modify, the view itself
	/// will modify as well.</para>
	/// </remarks>
	public interface IMulticollectionView<TElement> : ICollection<TElement> {
	}
}

