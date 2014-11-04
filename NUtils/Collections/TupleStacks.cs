//
//  TupleStacks.cs
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
	/// A stack where each element is a tuple containing two elements per location.
	/// The two items are pushed and popped concurrently.
	/// </summary>
	public class Stack<TX1,TX2> : Stack<Tuple<TX1,TX2>> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stack`2"/> that is empty and has a default initial capacity.
		/// </summary>
		public Stack () : base() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stack`2"/> class that contains elements copied from the specified
		/// collection and has sufficient <paramref name="capacity"/> to accommodate the number of elements copied.
		/// </summary>
		/// <param name="collection">The collection to copy elements from.</param>
		public Stack (IEnumerable<Tuple<TX1,TX2>> collection) : base(collection) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Stack`2"/> class that is empty and has the specified initial
		/// capacity or the default initial capacity, whichever is greater.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="T:Stack`2"/> can contain.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.</exception>
		public Stack (int capacity) : base(capacity) {
		}
		#endregion
		#region Additional safe peeks
		/// <summary>
		/// Peek the data on the stack safely.
		/// </summary>
		/// <returns>The tuple that is pushed as last on the stack if the stack is not empty, otherwise <c>null</c>.</returns>
		public Tuple<TX1,TX2> SafePeek () {
			if (this.Count > 0x00) {
				return this.Peek ();
			} else {
				return null;
			}
		}

		/// <summary>
		/// Return safely the first element of the peek of the stack.
		/// </summary>
		/// <returns>The first item of the top tuple of the stack if the stack is not empty, otherwise <c>default(TX1)</c>.</returns>
		public TX1 SafePeek1 () {
			if (this.Count > 0x00) {
				return this.Peek ().Item1;
			} else {
				return default(TX1);
			}
		}

		/// <summary>
		/// Return safely the second element of the peek of the stack.
		/// </summary>
		/// <returns>The second item of the top tuple of the stack if the stack is not empty, otherwise <c>default(TX2)</c>.</returns>
		public TX2 SafePeek2 () {
			if (this.Count > 0x00) {
				return this.Peek ().Item2;
			} else {
				return default(TX2);
			}
		}
		#endregion
		#region Additional pushes
		/// <summary>
		/// Push the specified <paramref name="x1"/> and <paramref name="x2"/> as one tuple onto the stack.
		/// </summary>
		/// <param name="x1">The first item of the tuple to be pushed.</param>
		/// <param name="x2">The second item of the tuple to be pushed.</param>
		public void Push (TX1 x1, TX2 x2) {
			this.Push (new Tuple<TX1,TX2> (x1, x2));
		}

		/// <summary>
		/// Pushes the given <paramref name="x1"/> value on the stack, the element pushed as the second item
		/// is either the <see cref="M:Stack`1.Peek"/> of the second item if the stack is not empty, or
		/// the <c>default(TX2)</c> if the stack contains no elements.
		/// </summary>
		/// <param name="x1">The element to push on the stack as first element.</param>
		public void Push1 (TX1 x1) {
			this.Push (x1, this.SafePeek2 ());
		}

		/// <summary>
		/// Pushes the given <paramref name="x2"/> value on the stack, the element pushed as the first item
		/// is either the <see cref="M:Stack`1.Peek"/> of the first item if the stack is not empty, or
		/// the <c>default(TX1)</c> if the stack contains no elements.
		/// </summary>
		/// <param name="x2">The element to push on the stack as second element.</param>
		public void Push2 (TX2 x2) {
			this.Push (this.SafePeek1 (), x2);
		}
		#endregion
	}
}

