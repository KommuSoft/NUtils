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
	public class Array2Wrapper<T> : IList<IList<T>> {

		#region Fields
		/// <summary>
		/// The given twodimensional array.
		/// </summary>
		private readonly T[,] data;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Array2Wrapper`1"/> class with the given <paramref name="data"/>.
		/// </summary>
		/// <param name="data">The given data over which the wrapper is defined.</param>
		public Array2Wrapper (T[,] data) {
			this.data = data;
		}
		#endregion
		#region Inner class
		private class Row : EnumerableBase<T>, IList<T> {

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
			public T this [int index] {
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
			public int Count {
				get {
					return this.data.GetLength (0x01);
				}
			}

			/// <summary>
			/// Gets a value indicating whether the <see cref="T:ICollection`1" /> is read-only.
			/// </summary>
			/// <value><c>true</c> if the <see cref="T:ICollection`1" /> is read-only;
			/// otherwise, <c>false</c>.</value>
			public bool IsReadOnly {
				get {
					return false;
				}
			}
			#endregion
			#region Constructors
			public Row (T[,] data, int row) {
				this.data = data;
				this.row = row;
			}
			#endregion
			#region IList implementation
			public int IndexOf (T item) {
				throw new NotImplementedException ();//TODO
			}

			public void Insert (int index, T item) {
				throw new InvalidOperationException ();
			}

			public void RemoveAt (int index) {
				throw new InvalidOperationException ();
			}
			#endregion
			#region ICollection implementation
			public void Add (T item) {
				throw new InvalidOperationException ();
			}

			public void Clear () {
				throw new InvalidOperationException ();
			}

			public bool Contains (T item) {
				throw new NotImplementedException ();//TODO
			}

			public void CopyTo (T[] array, int arrayIndex) {
				throw new NotImplementedException ();//TODO
			}

			public bool Remove (T item) {
				throw new InvalidOperationException ();
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

