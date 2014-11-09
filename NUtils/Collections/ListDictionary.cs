//
//  ListDictionary.cs
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
using System.Linq;
using System.Collections.Generic;
using NUtils.Functional;
using System.Data;
using NUtils.Abstract;

namespace NUtils.Collections {

	/// <summary>
	/// An implementation of the <see cref="T:IListDictionary`2"/> interface. This datastructures
	/// allows to associate multiple <typeparamref name="TValue"/> with the same <typeparamref name="TKey"/>.
	/// </summary>
	/// <typeparam name='TKey'>The type of the keys of the dictionary.</typeparam>
	/// <typeparam name='TValue'>The type of the values of the dictionary.</typeparam>
	/// <typeparam name='TCollection'>The type of collections that store entries in the dictionary (necessary if more
	/// than one value is added).</typeparam>
	public class ListDictionary<TKey,TValue,TCollection> : IListDictionary<TKey,TValue>
	    where TCollection : ICollection<TValue>, new() {

		#region Fields
		/// <summary>
		/// The number of values stored in this <see cref="T:ListDictionary`3"/>.
		/// </summary>
		private int count = 0x00;
		/// <summary>
		/// The inner dictionary that stores the keys and associates them with collections of values.
		/// </summary>
		private readonly Dictionary<TKey,TCollection> innerDictionary = new Dictionary<TKey, TCollection> ();
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Gets the number of elements contained in the <see cref="T:ListDictionary`3" />.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="T:ListDictionary`3" />.</value>
		public int Count {
			get {
				return this.count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the <see cref="T:ListDictionary`3" /> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="T:ListDictionary`3"/> is read-only; otherwise, <c>false</c>.</value>
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region IDictionary implementation
		/// <summary>
		/// Gets an <see cref="T:ICollection`1"/> object containing the keys in the <see cref="T:ListDictionary`1" /> object.
		/// </summary>
		/// <value>An <see cref="T:ICollection`1"/> object containing the keys in the <see cref="T:ListDictionary`1" /> object.</value>
		public ICollection<TKey> Keys {
			get {
				return this.innerDictionary.Keys;
			}
		}

		/// <summary>
		/// Gets an <see cref="T:ICollection`1"/> object containing the values in the <see cref="T:ListDictionary`3" /> object.
		/// </summary>
		/// <value>An <see cref="T:ICollection`1"/> object containing the values in the <see cref="T:ListDictionary`3" /> object.</value>
		public ICollection<TValue> Values {
			get {
				return new ConcatCollectionView<TValue> (this.innerDictionary.Values.Cast<ICollection<TValue>> ());
			}
		}
		#endregion
		#region IDictionary implementation
		/// <summary>
		/// Gets the first value <see cref="T:ListDictionary`3"/> with the specified <paramref name="key"/>.
		/// </summary>
		/// <param name="key">The key of the value to get. If the key does not correspond with
		/// any value, <c>default(TValue)</c> is returned.</param>
		/// <remarks>
		/// <para>The first value associated with the <paramref name="key"/> according to the <typeparamref name="TCollection"/>
		/// is returned.</para>
		/// <para>The value cannot be set, since there are many potential candidates.</para>
		/// </remarks>
		/// <exception cref="ReadOnlyException">If an attempt is made to set the value.</exception>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="key" /> is null.</exception>
		public TValue this [TKey key] {
			get {
				TCollection col;
				if (this.innerDictionary.TryGetValue (key, out col)) {
					return col.FirstOrDefault ();
				} else {
					return default(TValue);
				}
			}
			set {
				throw new ReadOnlyException ();
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ListDictionary`3"/> class.
		/// </summary>
		public ListDictionary () {
		}
		#endregion
		#region IListDictionary implementation
		/// <summary>
		/// Get the list of values associated with the given <paramref name="key"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that lists all the values associated
		/// with the given <paramref name="key"/>. If no value is associated with the key,
		/// the <see cref="T:IEnumerable`1"/> is empty.</returns>
		/// <param name="key">The given key to query the dictionary with.</param>
		/// <exception cref="ArgumentNullException">If the given key is not effective.</exception>
		public IEnumerable<TValue> GetValues (TKey key) {
			TCollection col;
			if (this.innerDictionary.TryGetValue (key, out col)) {
				foreach (TValue val in col) {
					yield return val;
				}
			}
		}

		/// <summary>
		/// Add all the given <paramref name="values"/> to the dictionary.
		/// </summary>
		/// <param name="values">A <see cref="T:IEnumerable`1"/> of <see cref="T:KeyValuePair`2"/> instances that are added to this <see cref="T:IListDictionary`2"/>.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="values" /> is null.</exception>
		/// <remarks>
		/// <para><see cref="T:KeyValuePair`2"/> instances with a key that is not effective are ignored.</para>
		/// <para>If the key already exists, the value is added to the list associated with the key. If the value already exists, the value is
		/// added a second time.</para>
		/// </remarks>
		public void AddAll (IEnumerable<KeyValuePair<TKey, TValue>> values) {
			foreach (KeyValuePair<TKey, TValue> kvp in values) {
				if (kvp.Key != null) {
					this.Add (kvp);
				}
			}
		}
		#endregion
		#region IDictionary implementation
		/// <summary>
		/// Gets the value associated with the specified key.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="T:ListDictionary`3" /> contains an element with the specified key; otherwise, <c>false</c>.</returns>
		/// <param name="key">The key of the value to get.</param>
		/// <param name="value">When this method returns <c>true</c>, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="key" /> is null.</exception>
		public bool TryGetValue (TKey key, out TValue value) {
			TCollection col;
			if (this.innerDictionary.TryGetValue (key, out col)) {
				value = col.First ();
				return true;
			} else {
				value = default(TValue);
				return false;
			}
		}
		#endregion
		#region IDictionary implementation
		/// <summary>
		/// Adds an element with the provided key and value to the <see cref="T:ListDictionary`3" />.
		/// </summary>
		/// <param name="key">The key of the pair to be added.</param>
		/// <param name="value">The value to be associated with the <paramref name="key"/>.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="key" /> is null.</exception>
		public void Add (TKey key, TValue value) {
			TCollection tcol;
			if (!this.innerDictionary.TryGetValue (key, out tcol)) {
				tcol = new TCollection ();
				this.innerDictionary.Add (key, tcol);
			}
			int ldc = tcol.Count;
			tcol.Add (value);
			this.count += tcol.Count - ldc;
		}

		/// <summary>
		/// Determines whether the <see cref="T:ListDictionary`3"/> contains an element with the specified <paramref name="key"/>.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="T:ListDictionary`3" /> contains an element with the key; otherwise, <c>false</c>.</returns>
		/// <param name="key">The key to locate in the <see cref="T:ListDictionary`3" />..</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="key" /> is null.</exception>
		public bool ContainsKey (TKey key) {
			return this.innerDictionary.ContainsKey (key);
		}

		/// <summary>
		/// Removes the element with the specified <paramref name="key"/> from the <see cref="T:ListDictionary`3" />.
		/// </summary>
		/// <returns><c>true</c> if the <see cref="T:ListDictionary`3"/> contains an element with the key; otherwise, <c>false</c>.
		/// This method also returns <c>false</c> if <paramref name="key"/> was not found in the original <see cref="T:ListDictionary`3" />.</returns>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="key" /> is null.</exception>
		public bool Remove (TKey key) {
			TCollection col;
			if (this.innerDictionary.TryGetValue (key, out col)) {
				int del = col.Count;
				try {
					return this.innerDictionary.Remove (key);
				} finally {
					this.count -= del;
				}
			} else {
				return false;
			}
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Adds an object to the end of the <see cref="T:System.Collections.ArrayList" />.
		/// </summary>
		/// <param name="item">The <see cref="T:KeyValuePair`2"/> to be added to the <see cref="T:ListDictionary`3" />.</param>
		public void Add (KeyValuePair<TKey, TValue> item) {
			this.Add (item.Key, item.Value);
		}

		/// <summary>
		/// Removes all items from this <see cref="T:ListDictionary`3"/>.
		/// </summary>
		public void Clear () {
			this.innerDictionary.Clear ();
			this.count = 0x00;
		}

		/// <summary>
		/// Determines whether the current collection contains a specific value.
		/// </summary>
		/// <param name="item">The given <see cref="T:KeyValuePair`2"/> to check.</param>
		/// <returns><c>true</c> if the dictionary associates the given value with the given key; otherwise <c>false</c>.</returns>
		public bool Contains (KeyValuePair<TKey, TValue> item) {
			TCollection col;
			if (this.innerDictionary.TryGetValue (item.Key, out col)) {
				return col.Contains (item.Value);
			}
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
		public void CopyTo (KeyValuePair<TKey, TValue>[] array, int index) {
			this.GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Remove the given key value pair from this <see cref="T:ListDictionary`3"/>.
		/// </summary>
		/// <param name="item">The given <see cref="T:KeyValuePair`2"/> to remove.</param>
		/// <returns><c>true</c> if the method removed something, <c>false</c> otherwise.</returns>
		public bool Remove (KeyValuePair<TKey, TValue> item) {
			TKey key = item.Key;
			TCollection col;
			if (this.innerDictionary.TryGetValue (key, out col)) {
				if (col.Remove (item.Value)) {
					if (!col.Contains ()) {
						this.innerDictionary.Remove (key);
					}
					this.count--;
					return true;
				}
			}
			return false;
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate all pairs of a key and its associative values.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance containing <see cref="T:KeyValuePair`2"/> instances
		/// describing all keys and associative values.</returns>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator () {
			foreach (KeyValuePair<TKey,TCollection> kvp in this.innerDictionary) {
				TKey key = kvp.Key;
				foreach (TValue vl in kvp.Value) {
					yield return new KeyValuePair<TKey,TValue> (key, vl);
				}
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Get a generic <see cref="System.Collections.IEnumerator"/> that enumerates all
		/// <see cref="T:KeyValuePair`2"/> instances.
		/// </summary>
		/// <returns>A <see cref="System.Collections.IEnumerator"/> instance containing <see cref="T:KeyValuePair`2"/> instances
		/// describing all keys and associative values.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
	}

	/// <summary>
	/// An implementation of the <see cref="T:IListDictionary`2"/> interface where the type of collection
	/// used to store values is <see cref="T:List`1"/>, this to make the collection type parameter of
	/// <see cref="T:ListDictionary`3"/> optional. This datastructures allows to associate
	/// multiple <typeparamref name="TValue"/> with the same <typeparamref name="TKey"/>.
	/// </summary>
	/// <typeparam name='TKey'>The type of the keys of the dictionary.</typeparam>
	/// <typeparam name='TValue'>The type of the values of the dictionary.</typeparam>
	public class ListDictionary<TKey,TValue> : ListDictionary<TKey,TValue,List<TValue>> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ListDictionary`2"/> class.
		/// </summary>
		public ListDictionary () {
		}
		#endregion
	}
}

