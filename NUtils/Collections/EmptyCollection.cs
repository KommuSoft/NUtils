//
//  EmptyCollection.cs
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
using NUtils.Functional;

namespace NUtils.Collections {

	/// <summary>
	/// An implementation of the <see cref="T:IEmptyCollection`1"/> interface describing a collection that is empty, and never contains any elements.
	/// </summary>
	/// <typeparam name='TElement'>The type of elements contained in this collection (this is a purely virtual type since the collection is empty).</typeparam>
	/// <remarks>
	/// <para>This collection is mainly used if a collection is required (as parameter),
	/// and no items should be entered.</para>
	/// </remarks>
	public class EmptyCollection<TElement> : EnumerableBase<TElement>, IEmptyCollection<TElement> {

		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:EmptyCollection`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:EmptyCollection`1" />.</value>
		/// <remarks>
		/// <para>The value is always zero.</para>
		/// </remarks>
		public int Count {
			get {
				return 0x00;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:EmptyCollection`1" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="T:EmptyCollection`1"/> is read-only; otherwise, <c>false</c>.</value>
		/// <remarks>
		/// <para>The value is always <c>true</c>.</para>
		/// </remarks>
		public bool IsReadOnly {
			get {
				return true;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:EmptyCollection`1"/> class.
		/// </summary>
		public EmptyCollection () {
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Add all the given <paramref name="item"/> to the collection by overriding the single item.
		/// </summary>
		/// <param name="item">A single element that takes the place of the original element.</param>
		/// <exception cref="T:System.NotSupportedException">Always, since the collection is read-only.</exception>
		public void Add (TElement item) {
			throw new NotSupportedException ("The collection is an empty collection and read-only");
		}

		/// <summary>
		/// Removes all items from this <see cref="T:EmptyCollection`1"/> by setting the default value
		/// as single item.
		/// </summary>
		/// <remarks>
		/// <para>Since an empty collection is always empty, no action is performed.</para>
		/// </remarks>
		public void Clear () {
		}

		/// <summary>
		/// Determines whether the current collection contains the given value.
		/// </summary>
		/// <param name="item">The given item to check.</param>
		/// <returns><c>true</c> if the element in the singleton contains the given element; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>Since the collection doesn't contain any elements, the result is always <c>false</c>.</para>
		/// </remarks>
		public bool Contains (TElement item) {
			return false;
		}

		/// <summary>
		/// Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.</exception>
		/// <exception cref="T:ArgumentException">The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		/// <remarks>
		/// <para>Since the collection is empty, no elements in the <paramref name="array"/> are set. Although
		/// exception with respect to <paramref name="array"/> and <paramref name="index"/> aspects will be thrown.</para>
		/// </remarks>
		public void CopyTo (TElement[] array, int index) {
			this.GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Remove the given <paramref name="item"/> from this <see cref="T:EmptyCollection`1"/>.
		/// </summary>
		/// <param name="item">The given item to remove.</param>
		/// <returns><c>true</c> if the method removed something, <c>false</c> otherwise.</returns>
		/// <remarks>
		/// <para>Since the collection doesn't return any element, the result is always <c>false</c>.</para>
		/// </remarks>
		public bool Remove (TElement item) {
			return false;
		}
		#endregion
		#region implemented abstract members of EnumerableBase
		/// <summary>
		/// Gets the enumerator that emits all the given elements this <see cref="T:EmptyCollection`1"/> instance.
		/// </summary>
		/// <returns>A <see cref="T:Enumerator`1"/> that emits all the elements in this singleton.</returns>
		/// <remarks>
		/// <para>The resulting <see cref="T:IEnumerator`1"/> instance emits at most one item.</para>
		/// </remarks>
		public override IEnumerator<TElement> GetEnumerator () {
			yield break;
		}
		#endregion
	}
}

