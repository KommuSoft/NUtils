//
//  IComposition.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2013 Willem Van Onsem
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
using System.Collections.Generic;

namespace NUtils.Designpatterns {

	/// <summary>
	/// An interface that describes the composite pattern. In the composite pattern, every item has children of a certain type <typeparamref name='TChildren'/>.
	/// Furthermore these children must support the <see cref="T:IComposition`1"/> interface as well, with the same type of the subchildren.
	/// </summary>
	/// <typeparam name='TChildren'>
	/// The type of the children of this instance.
	/// </typeparam>
	public interface IComposition<TChildren> where TChildren : IComposition<TChildren> {

		/// <summary>
		/// Enumerate the children of this instance. This is done in a hierarchical manner.
		/// </summary>
		IEnumerable<TChildren> Children ();
	}
}

