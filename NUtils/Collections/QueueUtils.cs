//
//  QueueUtils.cs
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
	/// A set of utility functions for the <see cref="T:Queue`1"/> class (and hopefully some interface later).
	/// </summary>
	public static class QueueUtils {

		#region All-methods
		/// <summary>
		/// Enqueue all the items in the <paramref name="toEnqueue"/> list to the given <paramref name="queue"/>.
		/// </summary>
		/// <param name="queue">The queue where the items should be enqueued to.</param>
		/// <param name="toEnqueue">A list of items to enqueu to the given <paramref name="queue"/>.</param>
		/// <typeparam name="T">The type of items the <paramref name="queue"/> holds.</typeparam>
		/// <remarks>
		/// <para>If the <paramref name="queue"/> or <paramref name="toEnqueue"/> list is not effective, nothing happens.</para>
		/// </remarks>
		public static void EnqueueAll<T> (this Queue<T> queue, IEnumerable<T> toEnqueue) {
			if (toEnqueue != null && queue != null) {
				foreach (T item in toEnqueue) {
					queue.Enqueue (item);
				}
			}
		}
		#endregion
	}
}

