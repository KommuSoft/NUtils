//
//  UniqueQueue.cs
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

namespace NUtils {
	/// <summary>
	/// A basic implementation of the <see cref="T:IUniqueQueue`1"/> interface describing a queue where each
	/// element can only occur once.
	/// </summary>
	/// <typeparam name='TElement'>
	/// The type of elements stored in the queue.
	/// </typeparam>
	public class UniqueQueue<TElement> : IUniqueQueue<TElement> {

		#region Fields
		private readonly Queue<TElement> queue = new Queue<TElement> ();
		private readonly HashSet<TElement> hash = new HashSet<TElement> ();
		#endregion
		#region ICollection implementation
		public int Count {
			get {
				return hash.Count;
			}
		}

		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region Constructor
		public UniqueQueue () {
		}
		#endregion
		#region ISet implementation
		public bool Add (TElement item) {
			this.Enqueue (item);
		}

		public void ExceptWith (IEnumerable<TElement> other) {
			throw new NotImplementedException ();
		}

		public void IntersectWith (IEnumerable<TElement> other) {
			throw new NotImplementedException ();
		}

		public bool IsProperSubsetOf (IEnumerable<TElement> other) {
			return this.hash.IsProperSubsetOf (other);
		}

		public bool IsProperSupersetOf (IEnumerable<TElement> other) {
			return this.hash.IsProperSupersetOf (other);
		}

		public bool IsSubsetOf (IEnumerable<TElement> other) {
			return this.hash.IsSubsetOf (other);
		}

		public bool IsSupersetOf (IEnumerable<TElement> other) {
			return this.hash.IsSupersetOf (other);
		}

		public bool Overlaps (IEnumerable<TElement> other) {
			return this.hash.Overlaps (other);
		}

		public bool SetEquals (IEnumerable<TElement> other) {
			return this.hash.SetEquals (other);
		}

		public void SymmetricExceptWith (IEnumerable<TElement> other) {
			throw new NotImplementedException ();
		}

		public void UnionWith (IEnumerable<TElement> other) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IQueue implementation
		public bool Enqueue (TElement element) {
			if (hash.Add (element)) {
				this.queue.Enqueue (element);
			}
			return false;
		}

		public TElement Dequeue () {
			throw new NotImplementedException ();
		}

		public TElement Peek () {
			throw new NotImplementedException ();
		}
		#endregion
		#region ICollection implementation
		void ICollection<TElement>.Add (TElement item) {
			this.Enqueue (item);
		}

		public void Clear () {
			this.queue.Clear ();
			this.hash.Clear ();
		}

		public bool Contains (TElement item) {
			return this.hash.Contains (item);
		}

		public void CopyTo (TElement[] array, int arrayIndex) {
			throw new NotImplementedException ();
		}

		public bool Remove (TElement item) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IEnumerable implementation
		public IEnumerator<TElement> GetEnumerator () {
			return this.queue.GetEnumerator ();
		}
		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.queue.GetEnumerator ();
		}
		#endregion
	}
}

