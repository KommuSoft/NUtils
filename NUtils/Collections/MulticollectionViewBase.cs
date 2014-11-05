//
//  MulticollectionViewBase.cs
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
	/// A basic implementation of the <see cref="T:IMultiCollectionView`1"/> that provides a list of
	/// the <see cref="T:ICollection`1"/> instances on which it is based.
	/// </summary>
	/// <typeparam name='TElement'>The type of the elements in the collection.</typeparam>
	/// <remarks>
	/// <para>Most operations like <see cref="M:ICollection`1.Contains"/> need to enumerate over all collections.</para>
	/// <para>This interface is mainly for programmer's convenience, and in many cases not that efficient.</para>
	/// <para>This interface is a View: if the components on which it is based modify, the view itself
	/// will modify as well.</para>
	/// </remarks>
	public abstract class MulticollectionViewBase<TElement> : EnumerableBase<TElement>, ICollection<TElement> {

		#region Fields
		/// <summary>
		/// The list of <see cref="T:ICollection`1"/> instances on which this view is based.
		/// </summary>
		protected readonly List<ICollection<TElement>> ViewCollections = new List<ICollection<TElement>> ();
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:MulticollectionViewBase`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:MulticollectionViewBase`1" />.</value>
		public abstract int Count {
			get;
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:MulticollectionViewBase`1" /> is read-only.
		/// </summary>
		/// <value>Since this is a view, always <c>true</c>.</value>
		public bool IsReadOnly {
			get {
				return true;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		protected MulticollectionViewBase () {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		/// <param name="viewCollections">An <see cref="T:IEnumerable`1"/> of initial <see cref="T:ICollection`1"/> instances on which this view is based.</param>
		protected MulticollectionViewBase (IEnumerable<ICollection<TElement>> viewCollections) {
			this.ViewCollections.AddRange (viewCollections);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		/// <param name="viewCollections">An array of initial <see cref="T:ICollection`1"/> instances on which this view is based.</param>
		protected MulticollectionViewBase (params ICollection<TElement>[] viewCollections) : this((IEnumerable<ICollection<TElement>>) viewCollections) {
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Adds an object to the this <see cref="T:MulticollectionView`1"/>, this is not allowed and will always throw an exception.
		/// </summary>
		/// <param name="item">The item to be added to the view.</param>
		/// <exception cref="System.NotSupportedException">Always, this is a view.</exception>
		public void Add (TElement item) {
			throw new NotSupportedException ();
		}

		/// <summary>
		/// Removes all items from this <see cref="T:MulticollectionView`1"/>, this is not allowed and will always throw an exception.
		/// </summary>
		/// <exception cref="System.NotSupportedException">Always, this is a view.</exception>
		public void Clear () {
			throw new NotSupportedException ();
		}

		/// <summary>
		/// Determines whether the current collection contains a specific value.
		/// </summary>
		/// <param name="item">The given item to check.</param>
		/// <returns><c>true</c> if the view contains the given item; otherwise <c>false</c>.</returns>
		public abstract bool Contains (TElement item);

		/// <summary>
		/// Copies the elements of the collection to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.</exception>
		/// <exception cref="T:ArgumentException">The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		public void CopyTo (TElement[] array, int index) {
			this.GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Remove the given item from this view, this is not allowed and will always throw an exception.
		/// </summary>
		/// <param name="item">The given item to remove.</param>
		/// <returns><c>true</c> if the method removed something, <c>false</c> otherwise.</returns>
		/// <exception cref="System.NotSupportedException">Always, this is a view.</exception>
		public bool Remove (TElement item) {
			throw new NotSupportedException ();
		}
		#endregion
	}
}

