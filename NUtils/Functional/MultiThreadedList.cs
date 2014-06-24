//
//  MultiThreadedList.cs
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

namespace NUtils.Functional {
	/// <summary>
	/// A <see cref="T:ICollection`1"/> that supports 
	/// </summary>
	/// <typeparam name='TData'>
	/// The type of the items stored in the collection.
	/// </typeparam>
	public class MultiThreadedList<TData> : ICollection<TData> {

		private int count = 0x00;
		private MultiThreadedListItem first;
		private MultiThreadedListItem last;
		#region ICollection implementation
		/// <summary>
		/// Gets the number of items the <see cref="T:ICollection`1"/> instance contains.
		/// </summary>
		/// <value>The number of items.</value>
		public int Count {
			get {
				return this.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance is read only.
		/// </summary>
		/// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region ICollection implementation
		/// <Docs>The item to add to the current collection.</Docs>
		/// <para>Adds an item to the current collection.</para>
		/// <remarks>To be added.</remarks>
		/// <exception cref="System.NotSupportedException">The current collection is read-only.</exception>
		/// <summary>
		/// Add the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public void Add (TData item) {
			MultiThreadedListItem mtli = new MultiThreadedListItem (item);
			this.count++;
			if (this.last == null) {
				this.first = this.last = mtli;
			} else {
				this.last.Next = mtli;
				this.last = mtli;
			}
		}

		/// <summary>
		/// Remove all the data from the entire collection.
		/// </summary>
		public void Clear () {
			this.first = null;
			this.last = null;
			this.count = 0x00;
		}

		/// <Docs>The object to locate in the current collection.</Docs>
		/// <para>Determines whether the current collection contains a specific value.</para>
		/// <summary>
		/// Contains the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Contains (TData item) {
			foreach (TData cur in this) {
				if (Object.Equals (cur, item)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Copies the content of this <see cref="T:MultiThreadedList`1"/> to the given array.
		/// </summary>
		/// <param name="array">The given array to copy the elements to.</param>
		/// <param name="arrayIndex">The index of the array on which copying begins.</param>
		public void CopyTo (TData[] array, int arrayIndex) {
			int n = array.Length;
			IEnumerator<TData> enumerator = this.GetEnumerator ();
			for (int i = arrayIndex; i < n && enumerator.MoveNext (); i++) {
				array [i] = enumerator.Current;
			}
		}

		/// <Docs>The item to remove from the current collection.</Docs>
		/// <para>Removes the first occurrence of an item from the current collection.</para>
		/// <summary>
		/// Remove the specified item.
		/// </summary>
		/// <param name="item">Item.</param>
		public bool Remove (TData item) {
			MultiThreadedListItem cur = this.first, prv = null;
			while (cur != null) {
				if (Object.Equals (cur.Data, item)) {
					MultiThreadedListItem nxt = cur.Next;
					if (prv != null) {
						prv.Next = nxt;
					} else {
						this.first = nxt;
					}
					if (nxt == null) {
						this.last = prv;
					}
					this.count--;
					return true;
				}
				prv = cur;
				cur = cur.Next;
			}
			return false;
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets an enumerator to enumerate over the entire collection.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerator`1"/> instance that iterates over all the items stored in the collection.</returns>
		/// <remarks>
		/// <para>The <see cref="T:MultiThreadedList`1"/> allows multiple iterations at once and while an <see cref="T:IEnumerator`1"/> is iterating
		/// adding additional elements is allowed that are immediately visible to the other active <see cref="T:IEnumerator`1"/> instances.</para>
		/// </remarks>
		public IEnumerator<TData> GetEnumerator () {
			MultiThreadedListItem cur = this.first;
			while (cur != null) {
				yield return cur.Data;
				cur = cur.Next;
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets an <see cref="System.Collections.IEnumerator"/> to enumerate over all the items stored in the collection.
		/// </summary>
		/// <returns>An <see cref="System.Collections.IEnumerator"/> instance to enumerate over all the items stored in the collection.</returns>
		/// <remarks>
		/// <para>The <see cref="T:MultiThreadedList`1"/> allows multiple iterations at once and while an <see cref="T:IEnumerator`1"/> is iterating
		/// adding additional elements is allowed that are immediately visible to the other active <see cref="T:IEnumerator`1"/> instances.</para>
		/// </remarks>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		private class MultiThreadedListItem {

			public TData Data;
			public MultiThreadedListItem Next;

			public MultiThreadedListItem (TData data) {
				this.Data = data;
			}
		}
	}
}

