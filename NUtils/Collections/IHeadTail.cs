//
//  IHeadTail.cs
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
	/// An interface describing a head-tail pair as is common in functional programming.
	/// </summary>
	/// <typeparam name='TElement'>The type of elements stored in this <see cref="T:IHeadTail`1"/> instance.</typeparam>
	/// <remarks>
	/// <para><see cref="T:IHeadTail`1"/> instances are useful for fast parsing of list constructions in a LR parser.</para>
	/// <para><see cref="T:IHeadTail`1"/> instances are in fact collections, but most operations on these items are computationally intensive since elements have linear access time.</para>
	/// </remarks>
	public interface IHeadTail<out TElement> : IEnumerable<TElement> {

		/// <summary>
		/// Get the head of this <see cref="T:IHeadTail`1"/> instance.
		/// </summary>
		/// <value>The first element of the collection described by this <see cref="T:IHeadTail`1"/>.</value>
		/// <remarks>
		/// <para>The head can be non-effective, but in that case, the list is assumed to contain a non-effective element.</para>
		/// </remarks>
		TElement Head {
			get;
		}

		/// <summary>
		/// Get the tail of this <see cref="T:IHeadTail`1"/> instance.
		/// </summary>
		/// <value>The rest of the elements in this <see cref="T:IHeadTail`1"/>.</value>
		IHeadTail<TElement> Tail {
			get;
		}
	}
}

