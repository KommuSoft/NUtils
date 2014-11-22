//
//  ListJumpEnumerator.cs
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
using System.Collections.Generic;
using System;
using System.Diagnostics.Contracts;

namespace NUtils.Collections {

	/// <summary>
	/// An <see cref="T:IJumpEnumerator`1"/> implementation to enumerate with jumps over a <see cref="T:List`1"/> instance.
	/// </summary>
	/// <typeparam name="T">The type of elements of the list that will be enumerated.</typeparam>
	public class ListJumpEnumerator<T> : IJumpEnumerator<T> {

		#region Fields
		/// <summary>
		/// The <see cref="T:List`1"/> instance to enumerate over.
		/// </summary>
		private readonly List<T> list;
		/// <summary>
		/// The current index of the cursor.
		/// </summary>
		private int index = -0x01;
		#endregion
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="T:ListJumpEnumerator`1"/> class with the given list to enumerate over.
		/// </summary>
		/// <param name="list">The <see cref="T:List`1"/> to enumerate over, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given list is not effective.</exception>
		public ListJumpEnumerator (List<T> list) {
			if (list == null) {
				throw new ArgumentNullException ("list", "The given list must be effective.");
			}
			Contract.EndContractBlock ();
			this.list = list;
		}
		#endregion
		#region IJumpEnumerator implementation
		/// <summary>
		/// Move the given number of <paramref name="steps"/> forward in to the enumerator process.
		/// </summary>
		/// <param name="steps">The number of steps to move forward.</param>
		/// <returns><c>true</c> if the jump was successful (if the cursor is standing on an element, or the number of <paramref name="steps"/> is zero or less; otherwise <c>false</c>.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		/// <remarks>
		/// <para>If the number of jumps is zero or less, nothing happens and the answer is always <c>true</c>, the cursor does not return back.</para>
		/// </remarks>
		public bool Jump (int steps) {
			if (steps <= 0x00) {
				return true;
			} else {
				this.index += steps;
				return this.index < list.Count;
			}
		}
		#endregion
		#region IEnumerator implementation
		/// <summary>
		/// Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		public bool MoveNext () {
			return ++this.index < list.Count;
		}

		/// <summary>
		/// Sets the enumerator to its initial position, which is before the first element in the collection.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		public void Reset () {
			this.index = 0x00;
		}

		/// <summary>
		/// Gets the current element in the collection.
		/// </summary>
		/// <value>The current element in the collection.</value>
		/// <exception cref="InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		object System.Collections.IEnumerator.Current {
			get {
				return this.Current;
			}
		}
		#endregion
		#region IDisposable implementation
		/// <summary>
		/// Releases all resource used by the <see cref="T:ListJumpEnumerator`1"/> object.
		/// </summary>
		/// <remarks>
		/// <para>Call <see cref="M:Dispose"/> when you are finished using the <see cref="T:ListJumpEnumerator`1"/>.</para>
		/// <para>The <see cref="M:Dispose"/> method leaves the <see cref="T:ListJumpEnumerator`1"/> in an unusable state.</para>
		/// <para>After calling <see cref="Dispose"/>, you must release all references to the <see cref="T:ListJumpEnumerator`1"/> so the garbage collector can reclaim the memory that the <see cref="T:ListJumpEnumerator`1"/> was occupying.</para>
		/// </remarks>
		public void Dispose () {
		}
		#endregion
		#region IEnumerator implementation
		/// <summary>
		/// Gets the current element in the collection.
		/// </summary>
		/// <value>The current element in the collection.</value>
		/// <exception cref="InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element.</exception>
		public T Current {
			get {
				try {
					return this.list [index];
				} catch (IndexOutOfRangeException ex) {
					throw new InvalidOperationException ("The cursor is placed before or after the range of the list", ex);
				}
			}
		}
		#endregion
	}
}

