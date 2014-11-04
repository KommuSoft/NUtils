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

	public sealed class HeadTail<T> : IEnumerable<T> {

		private readonly T head;
		private readonly HeadTail<T> tail;

		public T Head {
			get {
				return this.head;
			}
		}

		public HeadTail<T> Tail {
			get {
				return tail;
			}
		}

		private HeadTail (IEnumerator<T> values) {
			this.head = values.Current;
			if (values.MoveNext ()) {
				this.tail = new HeadTail<T> (values);
			}
		}

		public HeadTail (T head) : this(head,null) {
		}

		public HeadTail (T head, HeadTail<T> tail) {
			this.head = head;
			this.tail = tail;
		}

		public HeadTail (IEnumerable<T> values) {
			if (values == null) {
				throw new ArgumentException ("The values must be effective.", "values");
			}
			IEnumerator<T> enumerator = values.GetEnumerator ();
			if (enumerator == null) {
				throw new ArgumentException ("Values produced a noneffective enumerator.", "values");
			}
			if (!enumerator.MoveNext ()) {
				throw new ArgumentException ("Values must contain at least one element.", "values");
			}
			this.head = enumerator.Current;
			if (enumerator.MoveNext ()) {
				this.tail = new HeadTail<T> (enumerator);
			}
		}
		#region IEnumerable implementation
		IEnumerator IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region IEnumerable implementation
		public IEnumerator<T> GetEnumerator () {
			HeadTail<T> current = this;
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
			return string.Format ("[ {0} ]", string.Join (", ", (IEnumerable<T>)this));
		}
		#endregion
		public override bool Equals (object obj) {
			if (base.Equals (obj)) {
				return true;
			}
			if (obj != null && obj is HeadTail<T>) {
				HeadTail<T> htobj = (HeadTail<T>)obj;
				if (!Object.Equals (this.head, htobj.head)) {
					return false;
				}
				if (this.tail != null) {
					return this.tail.Equals (htobj.tail);
				} else {
					return (htobj.tail == null);
				}
			}
			return false;
		}

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

		public static HeadTail<T> FromIEnumerable (params T[] values) {
			return FromIEnumerable (values);
		}

		public static HeadTail<T> FromIEnumerable (IEnumerable<T> values) {
			if (values == null) {
				return null;
			}
			IEnumerator<T> enumerator = values.GetEnumerator ();
			if (enumerator == null || !enumerator.MoveNext ()) {
				return null;
			}
			return new HeadTail<T> (enumerator);
		}

		public static HeadTail<T> operator & (T head, HeadTail<T> tail) {
			return new HeadTail<T> (head, tail);
		}
	}
}