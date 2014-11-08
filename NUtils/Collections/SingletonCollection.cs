//
//  SingletonCollection.cs
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
	/// An implementation of the <see cref="T:ISingletonCollection`1"/> interface. A singleton is a collection with exactly one element.
	/// </summary>
	/// <typeparam name='TElement'>The type of the element stored in this <see cref="T:SingletonCollection`1"/></typeparam>
	/// <remarks>
	/// <para>A <see cref="T:SingletonCollection"/> can be used if a collection is required, and the programmer
	/// wishes to provide a single item without paying the overhead of collection maintenance.</para>
	/// <para><see cref="M:ICollection`1.Add"/> and <see cref="M:ICollection`1.Remove"/> methods are supported. An "empty"
	/// singleton contains the default value for <typeparamref name="TElement"/> and adding an element overrides
	/// the previous one.</para>
	/// <para>If the singleton contains the default element, the collection is said to be empty.</para>
	/// </remarks>
	public class SingletonCollection<TElement> : EnumerableBase<TElement>, ISingletonCollection<TElement> {

		#region Fields
		/// <summary>
		/// The element stored in this singleton.
		/// </summary>
		private TElement element;
		#endregion
		#region ISingletonCollection implementation
		/// <summary>
		/// Gets or sets the single element contained in this <see cref="T:ISingletonCollection`1"/> instance.
		/// </summary>
		/// <value>The single element stored in this singleton.</value>
		public TElement Element {
			get {
				return this.element;
			}
			set {
				this.element = value;
			}
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:SingletonCollection`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:SingletonCollection`1" />.</value>
		/// <remarks>
		/// <para>The value is either zero (0) or one (1).</para>
		/// </remarks>
		public int Count {
			get {
				if (Object.Equals (default(TElement), this.element)) {
					return 0x01;
				} else {
					return 0x00;
				}
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:SigletonCollection`1" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="T:SingletonCollection`1"/> is read-only; otherwise, <c>false</c>.</value>
		/// <remarks>
		/// <para>The value is always <c>false</c>.</para>
		/// </remarks>
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:SingletonCollection`1"/> class with the default value
		/// as element.
		/// </summary>
		public SingletonCollection () : this(default(TElement)) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:SingletonCollection`1"/> class with the initial element
		/// stored in this <see cref="T:SingletonCollection`1"/>.
		/// </summary>
		/// <param name="element">The element contained by the created singleton.</param>
		public SingletonCollection (TElement element) {
			this.element = element;
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Add all the given <paramref name="item"/> to the collection by overriding the single item.
		/// </summary>
		/// <param name="item">A single element that takes the place of the original element.</param>
		public void Add (TElement item) {
			this.element = item;
		}

		/// <summary>
		/// Removes all items from this <see cref="T:SingletonCollection`1"/> by setting the default value
		/// as single item.
		/// </summary>
		public void Clear () {
			this.element = default(TElement);
		}

		/// <summary>
		/// Determines whether the current collection contains the given value.
		/// </summary>
		/// <param name="item">The given item to check.</param>
		/// <returns><c>true</c> if the element in the singleton contains the given element; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>A <see cref="T:SingletonCollection`1"/> can never contain the default value for the <typeparamref name="TElement"/>.</para>
		/// </remarks>
		public bool Contains (TElement item) {
			return Object.Equals (this.element, item) && !Object.Equals (default(TElement), item);
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
		public void CopyTo (TElement[] array, int index) {
			this.GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Remove the given <paramref name="item"/> from this <see cref="T:SingletonCollection`1"/>.
		/// </summary>
		/// <param name="item">The given item to remove.</param>
		/// <returns><c>true</c> if the method removed something, <c>false</c> otherwise.</returns>
		public bool Remove (TElement item) {
			if (Object.Equals (this.element, item)) {
				this.element = default(TElement);
				return true;
			} else {
				return false;
			}
		}
		#endregion
		#region implemented abstract members of EnumerableBase
		/// <summary>
		/// Gets the enumerator that emits all the given elements this <see cref="T:SingletonCollection`1"/> instance.
		/// </summary>
		/// <returns>A <see cref="T:Enumerator`1"/> that emits all the elements in this singleton.</returns>
		/// <remarks>
		/// <para>The resulting <see cref="T:IEnumerator`1"/> instance emits at most one item.</para>
		/// </remarks>
		public override IEnumerator<TElement> GetEnumerator () {
			if (!Object.Equals (default(TElement), this.element)) {
				yield return this.element;
			}
		}
		#endregion
	}
}

