//
//  ConcatCollectionView.cs
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
using System.Linq;

namespace NUtils.Collections {

	/// <summary>
	/// An interface that describes a view on multiple <see cref="T:ICollection`1"/> instances, the view concatenates a list of given
	/// <see cref="T:ICollection`1"/> instances. The view is read-only.
	/// </summary>
	/// <typeparam name='TElement'>The type of the elements in the collection.</typeparam>
	/// <remarks>
	/// <para>Most operations like <see cref="M:ICollection`1.Contains"/> need to enumerate over all collections.</para>
	/// <para>This interface is mainly for programmer's convenience.</para>
	/// <para>This interface is a View: if the components on which it is based modify, the view itself
	/// will modify as well.</para>
	/// </remarks>
	public class ConcatCollectionView<TElement> : MulticollectionViewBase<TElement>, IConcatCollectionView<TElement> {

		
		#region implemented abstract members of MulticollectionViewBase
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:MulticollectionViewBase`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:MulticollectionViewBase`1" />.</value>
		public override int Count {
			get {
				return this.ViewCollections.Sum (x => x.Count);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		protected ConcatCollectionView () {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		/// <param name="viewCollections">An <see cref="T:IEnumerable`1"/> of initial <see cref="T:ICollection`1"/> instances on which this view is based.</param>
		protected ConcatCollectionView (IEnumerable<ICollection<TElement>> viewCollections) : base(viewCollections) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:MulticollectionViewBase`1"/> class.
		/// </summary>
		/// <param name="viewCollections">An array of initial <see cref="T:ICollection`1"/> instances on which this view is based.</param>
		protected ConcatCollectionView (params ICollection<TElement>[] viewCollections) : base(viewCollections) {
		}
		#endregion
		#region implemented abstract members of EnumerableBase
		/// <summary>
		/// Enumerate all items in this view.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance containing all the items in this view.</returns>
		public override IEnumerator<TElement> GetEnumerator () {
			foreach (ICollection<TElement> collection in this.ViewCollections) {
				foreach (TElement el in collection) {
					yield return el;
				}
			}
		}
		#endregion
		#region implemented abstract members of MulticollectionViewBase
		/// <summary>
		/// Determines whether the current collection contains a specific value.
		/// </summary>
		/// <param name="item">The given item to check.</param>
		/// <returns><c>true</c> if the view contains the given item; otherwise <c>false</c>.</returns>
		public override bool Contains (TElement item) {
			return this.ViewCollections.Any (x => x.Contains (item));
		}
		#endregion
	}
}

