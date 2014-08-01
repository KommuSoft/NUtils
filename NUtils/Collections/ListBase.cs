//
//  ListBase.cs
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
	/// A basic implementation of <see cref="T:IList`1"/> interface.
	/// </summary>
	/// <typeparam name='TElement'>
	/// The type of elements stored in the <see cref="T:IList`1"/>.
	/// </typeparam>
	public abstract class ListBase<TElement> : CollectionBase<TElement>, IList<TElement> {
		
		#region IList implementation
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <value>The element at the specified index.</value>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index"/> is not a valid index
		/// in the <see cref="T:IList`1"/>.</exception>
		/// <exception cref="T:System.NotSupportedException"> The <see cref="T:ICollection`1"/> is read-only.</exception>
		public abstract TElement this [int index] {
			get;
			set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ListBase`1"/> class.
		/// </summary>
		protected ListBase () {
		}
		#endregion
		#region IList implementation
		/// <summary>
		/// Searches for the specified <paramref name="item"/> and returns the zero-based index of the first occurrence
		/// within the entire <see cref="T:IList`1"/>.
		/// </summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="item"/> within the
		/// entire <see cref="T:IList`1"/>, if found; otherwise, <c>-1</c>.</returns>
		/// <param name="item">The item to locate in the <see cref="T:IList`1"/>.</param>
		public virtual int IndexOf (TElement item) {
			int index = 0x00;
			foreach (TElement e in this) {
				if (Object.Equals (e, item)) {
					return index;
				}
				index++;
			}
			return -0x01;
		}

		/// <summary>
		/// Inserts an element into the <see cref="T:IList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The item to insert.</param>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index"/> is greater than <see cref="P:Count"/>.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:IList`1"/> is read-only.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:IList`1"/> has a fixed size.</exception>
		public abstract void Insert (int index, TElement item);

		/// <summary>
		/// Removes the given item in this <see cref="T:IList`1"/> at the given <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The index of the element to remove.</param>
		/// <exception cref="T:NotSupportedException">The <see cref="T:IList`1"/> is read-only.</exception>
		/// <exception cref="T:NotSupportedException">The <see cref="T:IList`1"/> has a fixed size.</exception>
		public abstract void RemoveAt (int index);
		#endregion
	}
}

