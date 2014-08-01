//
//  MultidimensionalArrayWrapper.cs
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
	/// A wrapper that wraps a twodimensional array to a <see cref="T:IList`1"/> of <see cref="T:IList`1"/> instances.
	/// </summary>
	/// <typeparam name='T'>The type of elements over which the array is defined.</typeparam>
	public class Array2Wrapper<T> : ListBase<IList<T>>,IMultiList2<T> {

		#region Fields
		/// <summary>
		/// The given twodimensional array.
		/// </summary>
		private readonly T[,] data;
		#endregion
		#region IList implementation
		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <value>The element at the specified index.</value>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index
		/// in the <see cref="T:IList`1" />.</exception>
		/// <exception cref="T:NotSupportedException">Always thrown, if the property is set.</exception>
		public override IList<T> this [int index] {
			get {
				return new Row (this.data, index);
			}
			set {
				throw new NotSupportedException ();
			}
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:ICollection`1" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:ICollection`1" />.</value>
		public override int Count {
			get {
				return this.data.GetLength (0x00);
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:ICollection`1" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="T:ICollection`1" /> is read-only;
		/// otherwise, <c>false</c>.</value>
		/// <remarks>
		/// <para>The elements on this level are readonly, the value is always <c>true</c>.</para>
		/// </remarks>
		public override bool IsReadOnly {
			get {
				return true;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Array2Wrapper`1"/> class with the given <paramref name="data"/>.
		/// </summary>
		/// <param name="data">The given data over which the wrapper is defined.</param>
		public Array2Wrapper (T[,] data) {
			this.data = data;
		}
		#endregion
		#region IList implementation
		/// <summary>
		/// Inserts an element into the <see cref="T:IList`1"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
		/// <param name="item">The item to insert.</param>
		/// <exception cref="T:NotSupportedException">Always, since the collection is read-only.</exception>
		public override void Insert (int index, IList<T> item) {
			throw new NotSupportedException ();
		}

		/// <summary>
		/// Removes the given item in this <see cref="T:IList`1"/> at the given <paramref name="index"/>.
		/// </summary>
		/// <param name="index">The index of the element to remove.</param>
		/// <exception cref="T:NotSupportedException">Always, since the collection is read-only.</exception>
		public override void RemoveAt (int index) {
			throw new NotSupportedException ();
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Adds an object to the end of the <see cref="T:ICollection`1"/>.
		/// </summary>
		/// <returns>The <see cref="T:ICollection`1"/> index at which the <paramref name="value"/> has been added.</returns>
		/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:ICollection`1" />.</param>
		/// <exception cref="T:NotSupportedException">Always, since the collection is read-only.</exception>
		public override void Add (IList<T> item) {
			throw new NotSupportedException ();
		}

		/// <summary>
		/// Removes all items from the <see cref="T:System.Collections.IList" />. This implementation always throws <see cref="T:System.NotSupportedException" />.
		/// </summary>
		/// <exception cref="T:NotSupportedException">Always, since the collection is read-only.</exception>
		public override void Clear () {
			throw new NotSupportedException ();
		}

		/// <summary>
		/// Removes the first occurrence of a specific object from the <see cref="T:IList`1" />.
		/// </summary>
		/// <param name="item">The object to remove from the <see cref="T:IList`1" />.</param>
		/// <exception cref="ArgumentException"><paramref name="item" /> is of a type that is not
		/// assignable to the <see cref="T:IList`1" />.</exception>
		/// <exception cref="NotSupportedException">Always thrown.</exception>
		/// <remarks>Since the collection has a fixed size, no elements can be removed.</remarks>
		public override bool Remove (IList<T> item) {
			throw new NotSupportedException ();
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Get the <see cref="T:IEnumerator`1"/> that enumerates all the items stored in this <see cref="T:IEumerable`1"/>
		/// instance.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator`1"/> emmitting all items stored in this instance.</returns>
		public override IEnumerator<IList<T>> GetEnumerator () {
			T[,] data = this.data;
			int n = data.GetLength (0x00);
			for (int i = 0x00; i < n; i++) {
				yield return new Row (this.data, i);
			}
		}
		#endregion
		#region Inner class
		private class Row : ListBase<T>, IList<T> {

			#region Fields
			/// <summary>
			/// A reference to the data which must be accessed.
			/// </summary>
			private readonly T[,] data;
			/// <summary>
			/// The index of the row which is accessed.
			/// </summary>
			private int row;
			#endregion
			#region IList implementation
			/// <summary>
			/// Gets or sets the element at the specified index.
			/// </summary>
			/// <value>The element at the specified index.</value>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is not a valid index
			/// in the <see cref="T:IList`1" />.</exception>
			public override T this [int index] {
				get {
					return this.data [row, index];
				}
				set {
					this.data [row, index] = value;
				}
			}
			#endregion
			#region ICollection implementation
			/// <summary>
			/// Gets the number of elements contained in the <see cref="T:ICollection`1" />.
			/// </summary>
			/// <value>The number of elements contained in the <see cref="T:ICollection`1" />.</value>
			public override int Count {
				get {
					return this.data.GetLength (0x01);
				}
			}
			#endregion
			#region Constructors
			/// <summary>
			/// Initializes a new instance of the <see cref="T:Array2Wrapper`1+Row"/> class with a given reference
			/// to the array and a given index of the <paramref name="row"/>.
			/// </summary>
			/// <param name="data">The given array to wrap.</param>
			/// <param name="row">The index of the row to wrap.</param>
			public Row (T[,] data, int row) {
				this.data = data;
				this.row = row;
			}
			#endregion
			#region IList implementation
			/// <summary>
			/// Inserts an element into the <see cref="T:IList`1"/> at the specified index.
			/// </summary>
			/// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
			/// <param name="item">The item to insert.</param>
			/// <exception cref="T:NotSupportedException">Always, since the collection is fixed-size.</exception>
			public override void Insert (int index, T item) {
				throw new NotSupportedException ();
			}

			/// <summary>
			/// Removes the given item in this <see cref="T:IList`1"/> at the given <paramref name="index"/>.
			/// </summary>
			/// <param name="index">The index of the element to remove.</param>
			/// <exception cref="T:NotSupportedException">Always, since the collection is fixed-size.</exception>
			public override void RemoveAt (int index) {
				throw new NotSupportedException ();
			}
			#endregion
			#region ICollection implementation
			/// <summary>
			/// Adds an object to the end of the <see cref="T:ICollection`1"/>.
			/// </summary>
			/// <returns>The <see cref="T:ICollection`1"/> index at which the <paramref name="value"/> has been added.</returns>
			/// <param name="item">The <see cref="T:System.Object" /> to be added to the end of the <see cref="T:ICollection`1" />.</param>
			/// <exception cref="T:NotSupportedException">Always, since the collection is fixed-size.</exception>
			public override void Add (T item) {
				throw new NotSupportedException ();
			}

			/// <summary>
			/// Removes all items from the <see cref="T:System.Collections.IList" />. This implementation always throws <see cref="T:System.NotSupportedException" />.
			/// </summary>
			/// <exception cref="T:NotSupportedException">Always, since the collection is fixed-size.</exception>
			public override void Clear () {
				throw new NotSupportedException ();
			}

			/// <summary>
			/// Removes the first occurrence of a specific object from the <see cref="T:IList`1" />.
			/// </summary>
			/// <param name="item">The object to remove from the <see cref="T:IList`1" />.</param>
			/// <exception cref="ArgumentException"><paramref name="item" /> is of a type that is not
			/// assignable to the <see cref="T:IList`1" />.</exception>
			/// <exception cref="T:NotSupportedException">Always, since the collection is fixed-size.</exception>
			/// <remarks>Since the collection has a fixed size, no elements can be removed.</remarks>
			public override bool Remove (T item) {
				throw new NotSupportedException ();
			}
			#endregion
			#region IEnumerable implementation
			/// <summary>
			/// Get the <see cref="T:IEnumerator`1"/> that enumerates all the items stored in this <see cref="T:IEumerable`1"/>
			/// instance.
			/// </summary>
			/// <returns>A <see cref="T:IEnumerator`1"/> emmitting all items stored in this instance.</returns>
			public override IEnumerator<T> GetEnumerator () {
				T[,] data = this.data;
				int r = this.row;
				int n = data.GetLength (0x01);
				for (int i = 0x00; i < n; i++) {
					yield return data [r, i];
				}
			}
			#endregion
		}
		#endregion
	}
}

