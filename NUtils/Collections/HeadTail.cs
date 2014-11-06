//
//  HeadTail.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2013 Willem Van Onsem
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
using System.Collections;
using System.Collections.Generic;

namespace NUtils.Collections {

	/// <summary>
	/// An implementation of the <see cref="T:IHeadTail`1"/> interface.
	/// </summary>
	/// <typeparam name='TElement'>The type of elements stored in this <see cref="T:HeadTail`1"/> instance.</typeparam>
	/// <remarks>
	/// <para>This class is sealed to increase performance.</para>
	/// </remarks>
	public sealed class HeadTail<TElement> : EnumerableBase<TElement>, IHeadTail<TElement> {

		#region Fields
		/// <summary>
		/// The head of the head-tail.
		/// </summary>
		private readonly TElement head;
		/// <summary>
		/// The tail of the head-tail.
		/// </summary>
		private readonly IHeadTail<TElement> tail;
		#endregion
		#region IHeadTail implementation
		/// <summary>
		/// Get the head of this <see cref="T:IHeadTail`1"/> instance.
		/// </summary>
		/// <value>The first element of the collection described by this <see cref="T:IHeadTail`1"/>.</value>
		public TElement Head {
			get {
				return this.head;
			}
		}

		/// <summary>
		/// Get the tail of this <see cref="T:IHeadTail`1"/> instance.
		/// </summary>
		/// <value>The rest of the elements in this <see cref="T:IHeadTail`1"/>.</value>
		public IHeadTail<TElement> Tail {
			get {
				return tail;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:HeadTail`1"/> class by unwinding the given <see cref="T:IEnumerator`1"/> instance.
		/// </summary>
		/// <param name="values">The given <see cref="T:IEnumerator`1"/> instance that provides the values that should be stored
		/// in this <see cref="T:HeadTail`1"/> instance.</param>
		private HeadTail (IEnumerator<TElement> values) {
			this.head = values.Current;
			if (values.MoveNext ()) {
				this.tail = new HeadTail<TElement> (values);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:HeadTail`1"/> class with a given head. The tail is assumed to be <c>null</c>.
		/// </summary>
		/// <param name="head">The given head of this <see cref="T:HeadTail`1"/> instance.</param>
		/// <remarks>
		/// <para>The result list contains exactly one item: the head.</para>
		/// </remarks>
		public HeadTail (TElement head) : this(head,null) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:HeadTail`1"/> class with a given <paramref name="head"/> and <paramref name="tail"/>.
		/// </summary>
		/// <param name="head">The given head describing the first element of the new <see cref="T:HeadTail`1"/>.</param>
		/// <param name="tail">The given tail describing the rest of the elements in the new <see cref="T:HeadTail`1"/>.</param>
		public HeadTail (TElement head, IHeadTail<TElement> tail) {
			this.head = head;
			this.tail = tail;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:HeadTail`1"/> class that contains all the elements of the given <paramref name="value"/>.
		/// </summary>
		/// <param name="values">A <see cref="T:IEnumerable`1"/> that contains the list of elements that must be stored in this <see cref="T:HeadTail`1"/> instance.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If the <see cref="T:IEnumerator`1"/> instance derived from <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If <paramref name="values"/> contains zero elements.</exception>
		public HeadTail (IEnumerable<TElement> values) {
			if (values == null) {
				throw new ArgumentNullException ("values", "The values must be effective.");
			}
			IEnumerator<TElement> enumerator = values.GetEnumerator ();
			if (enumerator == null) {
				throw new ArgumentException ("Values produced a noneffective enumerator.", "values");
			}
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("Values must contain at least one element.", "values");
			}
			this.head = enumerator.Current;
			if (enumerator.MoveNext ()) {
				this.tail = new HeadTail<TElement> (enumerator);
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate all elements stored in this <see cref="T:HeadTail`1"/> instance.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance containing all elements stored by this <see cref="T:HeadTail`1"/> instance.</returns>
		/// <remarks>
		/// <para>The resulting <see cref="T:IEnumerable`1"/> is guaranteed to emit at least one element: the head.</para>
		/// <para>The method doesn't use recursion for performance issues.</para>
		/// </remarks>
		public override IEnumerator<TElement> GetEnumerator () {
			IHeadTail<TElement> current = this;
			do {
				yield return current.Head;
				current = current.Tail;
			} while(current != null);
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="T:HeadTail`1"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="T:HeadTail`1"/>.</returns>
		/// <remarks>
		/// <para>The result is a string format according to <c>[ item, item, item ]</c> where
		/// <c>item</c>s are replaced by the textual representation of the items.</para>
		/// </remarks>
		public override string ToString () {
			return string.Format ("[ {0} ]", string.Join (", ", (IEnumerable<TElement>)this));
		}
		#endregion
		#region Equals method
		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:HeadTail`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="T:HeadTail`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:HeadTail`1"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// <para>The given <paramref name="obj"/> doesn't need to be a <see cref="T:HeadTail`1"/> instance, an instance
		/// derived from <see cref="T:IHeadTail`1"/> with the same list of objects is sufficient.</para>
		/// <para>The method checks at every item in the <see cref="T:IHeadTail`1"/> instances if the references are
		/// equal to increase performance.</para>
		/// </remarks>
		public override bool Equals (object obj) {
			if (object.ReferenceEquals (this, obj)) {
				return true;
			}
			if (obj != null && obj is IHeadTail<TElement>) {
				IHeadTail<TElement> htobj = (IHeadTail<TElement>)obj;
				if (!Object.Equals (this.head, htobj.Head)) {
					return false;
				}
				if (this.tail != null) {
					return this.tail.Equals (htobj.Tail);
				} else {
					return (htobj.Tail == null);
				}
			}
			return false;
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="T:HeadTail`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			int hash = 0x03;
			if (this.head != null) {
				hash += 0x07 * this.head.GetHashCode ();
			}
			if (this.tail != null) {
				hash += 0x0b * this.tail.GetHashCode ();
			}
			return hash;
		}
		#endregion
		#region Static utility methods
		/// <summary>
		/// Creates a <see cref="T:HeadTail`1"/> instance based on the values given by the <paramref name="values"/>.
		/// </summary>
		/// <returns>A <see cref="T:HeadTail`1"/> instance that stores all the values in the same order of the given <paramref name="values"/>.</returns>
		/// <param name="values">The given array of values to convert to a <see cref="T:HeadTail`1"/> instance.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If the <see cref="T:IEnumerator`1"/> instance derived from <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If <paramref name="values"/> contains zero elements.</exception>
		public static HeadTail<TElement> FromIEnumerable (params TElement[] values) {
			return FromIEnumerable ((IEnumerable<TElement>)values);
		}

		/// <summary>
		/// Creates a <see cref="T:HeadTail`1"/> instance based on the values given by the <paramref name="values"/>.
		/// </summary>
		/// <returns>A <see cref="T:HeadTail`1"/> instance that stores all the values in the same order of the given <paramref name="values"/>.</returns>
		/// <param name="values">The given list of values to convert to a <see cref="T:HeadTail`1"/> instance.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If the <see cref="T:IEnumerator`1"/> instance derived from <paramref name="values"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If <paramref name="values"/> contains zero elements.</exception>
		public static HeadTail<TElement> FromIEnumerable (IEnumerable<TElement> values) {
			if (values == null) {
				return null;
			}
			IEnumerator<TElement> enumerator = values.GetEnumerator ();
			if (enumerator == null || !enumerator.MoveNext ()) {
				return null;
			}
			return new HeadTail<TElement> (enumerator);
		}
		#endregion
		#region Operators
		/// <summary>
		/// An operator converting the given <paramref name="head"/> and <paramref name="tail"/> into a new <see cref="T:HeadTail`1"/> instance, for programmer convenience.
		/// </summary>
		/// <param name="head">The given head for the new <see cref="T:HeadTail`1"/> instance.</param>
		/// <param name="tail">The given tail for the new <see cref="T:HeadTail`1"/> instance.</param>
		public static HeadTail<TElement> operator & (TElement head, HeadTail<TElement> tail) {
			return new HeadTail<TElement> (head, tail);
		}
		#endregion
	}
}