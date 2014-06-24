//
//  LinqCache.cs
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
	/// A class that is used to cache the results of some LINQ-query. This is done
	/// if the query is computationally expensive and the result will be used many
	/// times.
	/// </summary>
	/// <typeparam name='TData'>
	/// The type of the data the cache contains and is enumerated by the original LINQ query.
	/// </typeparam>
	/// <remarks>
	/// <para>Infinite <see cref="T:IEnumerable`1"/> instances are supported since the data
	/// is cached lazely.</para>
	/// <para>The cached version supports multiple enumerations at once.</para>
	/// </remarks>
	public class LinqCache<TData> : IEnumerable<TData> {

		private readonly IEnumerator<TData> enumerator;
		private readonly ICollection<TData> cache;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:LinqCache`1"/> class with a given
		/// <see cref="T:IEnumerable`1"/> that generates a sequence of results that need to be cached.
		/// </summary>
		/// <param name="source">Source.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="source"/> is not effective.</exception>
		/// <exception cref="ArgumentException">If the given <paramref name="source"/> does not generate a valid <see cref="T:IEnumerator`1"/>
		/// when calling the <see cref="T:IEnumerable`1.GetEnumerator"/> method.</exception>
		public LinqCache (IEnumerable<TData> source) {
			if (source != null) {
				throw new ArgumentNullException ("The given source must be effective.");
			} else if (source is IList<TData>) {
				this.cache = source as ICollection<TData>;
				this.enumerator = null;
			} else {
				this.enumerator = source.GetEnumerator ();
				this.cache = new MultiThreadedList<TData> ();
			}
		}

		/// <summary>
		/// Enrolls the <see cref="T:IEnumerator`1"/> further such that additional data is cached.
		/// </summary>
		/// <returns><c>true</c>, if next item can be emmitted, <c>false</c> otherwise.</returns>
		/// <param name="data">The item that was emitted.</param>
		private bool emitNext (out TData data) {
			IEnumerator<TData> enumerator = this.enumerator;
			if (enumerator != null && enumerator.MoveNext ()) {
				TData res = enumerator.Current;
				this.cache.Add (res);
				data = res;
				return true;
			} else {
				data = default(TData);
				return false;
			}
		}
		#region IEnumerable implementation
		/// <summary>
		/// Generate an <see cref="T:IEnumerator`1"/> that will enumerate all the items of the original
		/// given <see cref="T:IEnumerable`1"/>, the items are caches such that, when called a second time, the enumeration
		/// is done faster.
		/// </summary>
		/// <returns>The <see cref="T:IEnumerator`1"/> that enumerates all the items of the original
		/// given <see cref="T:IEnumerable`1"/>.</returns>
		public IEnumerator<TData> GetEnumerator () {
			foreach (TData data in this.cache) {
				yield return data;
			}
			TData edata;
			while (this.emitNext (out edata)) {
				yield return edata;
			}//TODO: interleaved rechecking on cache data?
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Generate an <see cref="System.Collections.IEnumerator"/> that will enumerate all the items of the original
		/// given <see cref="T:IEnumerable`1"/>, the items are caches such that, when called a second time, the enumeration
		/// is done faster.
		/// </summary>
		/// <returns>The <see cref="System.Collections.IEnumerator"/> that enumerates all the items of the original
		/// given <see cref="T:IEnumerable`1"/>.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
	}
}

