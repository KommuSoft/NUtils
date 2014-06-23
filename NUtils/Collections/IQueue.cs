//
//  IQueue.cs
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
	/// An interface specifying a queue. A queue is a collection that works First-In-First-Out (FIFO).
	/// </summary>
	/// <typeparam name='TElement'>
	/// The type of elements stored in the queue.
	/// </typeparam>
	public interface IQueue<TElement> : ICollection<TElement> {

		/// <summary>
		/// Add an element at the back of the queue.
		/// </summary>
		/// <param name="element">The element to add.</param>
		bool Enqueue (TElement element);

		/// <summary>
		/// Retrieve the first element of the queue and removes it from the collection.
		/// </summary>
		/// <returns>
		/// The first element of the queue. If the queue is empty, a <see cref="InvalidOperationException"/> exception is thrown.
		/// </returns>
		/// <exception cref="InvalidOperationException">If the queue is empty.</exception>
		TElement Dequeue ();

		/// <summary>
		/// Retrieve the head of the queue without removing that element.
		/// </summary>
		/// <returns>
		/// The first element of the queue. If the queue is empty, a <see cref="InvalidOperationException"/> exception is thrown.
		/// </returns>
		/// <exception cref="InvalidOperationException">If the queue is empty.</exception>
		TElement Peek ();
	}
}

