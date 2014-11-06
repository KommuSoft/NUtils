//
//  Register.cs
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
using System.Linq;

namespace NUtils.Collections {

	/// <summary>
	/// A basic implementation of a <see cref="T:IRegister`2"/>. A register is a datastructure that
	/// stores items based on a <typeparamref name="TKey"/> for
	/// fast lookups.
	/// </summary>
	/// <typeparam name='TKey'>The key on which the given items are stored.</typeparam>
	/// <typeparam name='TValue'>The items to store in the register.</typeparam>
	/// <typeparam name='TCollection'>The type of collection used to store values if multiple values map on the same key.</typeparam>
	public class Register<TKey,TValue,TCollection> : ListDictionary<TKey,TValue,TCollection>, IRegister<TKey,TValue>
	where TCollection : ICollection<TValue>, new() {

		
		#region IRegister implementation
		/// <summary>
		/// A function that generates keys given a <typeparamref name="TValue"/>.
		/// </summary>
		/// <value>The generator of keys for this <see cref="T:IRegister`2"/>.</value>
		public Func<TValue, TKey> KeyGenerator {
			get;
			protected set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Register`3"/> class with a given key generator.
		/// </summary>
		/// <param name='keyGenerator'>The key generator associated with this register.</param>">
		public Register (Func<TValue,TKey> keyGenerator) {
			this.KeyGenerator = keyGenerator;
		}
		#endregion
		#region ICollection implementation
		public void Add (TValue item) {
			this.Add (this.KeyGenerator (item), item);
		}

		public bool Contains (TValue item) {
			return this.Contains (this.KeyGenerator (item), item);
		}

		/// <summary>
		/// Copies the values of the <see cref="T:IRegister`2"/> to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.</exception>
		/// <exception cref="T:ArgumentException">The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		public void CopyTo (TValue[] array, int index) {
			((IEnumerable<TValue>)this).GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Removes the given <paramref name="value"/> from the <see cref="T:IRegister`2"/>.
		/// </summary>
		/// <returns><c>true</c> if the register contained the given <paramref name="value"/>; otherwise <c>false</c>.</returns>
		/// <param name="item">The item to remove.</param>
		public bool Remove (TValue item) {
			return this.Remove (this.KeyGenerator (item), item);
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate all values in this <see cref="T:IRegister`2"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance containing <typeparamref name="TValue"/> instances
		/// describing all values in this <see cref="T:IRegister`2"/>.</returns>
		IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator () {
			return this.Values.GetEnumerator ();
		}
		#endregion
	}

	/// <summary>
	/// A basic implementation of a <see cref="T:IRegister`2"/> where the collection is by default <see cref="T:List`1"/>.
	/// A register is a datastructure that stores items based on a <typeparamref name="TKey"/> for fast lookups.
	/// </summary>
	/// <typeparam name='TKey'>The key on which the given items are stored.</typeparam>
	/// <typeparam name='TValue'>The items to store in the register.</typeparam>
	public class Register<TKey,TValue> : Register<TKey,TValue,List<TValue>> {
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Register`3"/> class with a given key generator.
		/// </summary>
		/// <param name='keyGenerator'>The key generator associated with this register.</param>">
		public Register (Func<TValue,TKey> keyGenerator) : base(keyGenerator) {
		}
		#endregion
	}
}

