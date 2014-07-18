//
//  EnumerableUtils.cs
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
using System.Linq;

namespace NUtils.Functional {
	/// <summary>
	/// A utility funcation for <see cref="T:IEnumerable`1"/> lists.
	/// </summary>
	public static class EnumerableUtils {

		#region Extension methods
		/// <summary>
		/// Generate an infinite list by repeating the given least infinitely.
		/// </summary>
		/// <returns>An infinite <see cref="T:IEnumerable`1"/> that repeats all the elements in the given source.</returns>
		/// <param name="source">The given list of items.</param>
		/// <typeparam name="TItem">The type of items that will be enumerated.</typeparam>
		/// <remarks>
		/// <para>If the given source is empty, the result is empty as well. The system does not go into an infinite loop.</para>
		/// </remarks>
		public static IEnumerable<TItem> Cycle<TItem> (this IEnumerable<TItem> source) {
			while (true) {
				bool terminate = true;
				foreach (TItem item in source) {
					yield return item;
					terminate = false;
				}
				if (terminate) {
					yield break;
				}
			}
		}

		/// <summary>
		/// Repeat the specified source the given number of times.
		/// </summary>
		/// <param name="source">The original list of items to repeat.</param>
		/// <param name="ntimes">The given number of times to repeat the source.</param>
		/// <typeparam name="TItem">The type of the items to emit.</typeparam>
		public static IEnumerable<TItem> Repeat<TItem> (this IEnumerable<TItem> source, int ntimes) {
			for (int i = 0x00; i < ntimes; i++) {
				foreach (TItem item in source) {
					yield return item;
				}
			}
		}

		/// <summary>
		/// Folds the specified source of items into a single item by applying the given <paramref name="function"/>
		/// over the elements of the given <paramref name="source"/>.
		/// </summary>
		/// <param name="source">The list of items to fold.</param>
		/// <param name="function">The function to apply left to right.</param>
		/// <typeparam name="TA">The type of the elements.</typeparam>
		public static TA Foldl1<TA> (this IEnumerable<TA> source, Func<TA,TA,TA> function) {
			IEnumerator<TA> enumerator = source.GetEnumerator ();
			if (enumerator.MoveNext ()) {
				TA result = enumerator.Current;
				while (enumerator.MoveNext ()) {
					result = function (result, enumerator.Current);
				}
				return result;
			} else {
				return default(TA);
			}
		}

		/// <summary>
		/// Normalizes a stream of key-probability tuples such that the sum of the probabilties is equal to one.
		/// </summary>
		/// <param name="source">The source of key-probabilities.</param>
		/// <typeparam name="T">The type of the keys.</typeparam>
		/// <returns>A stream of tuples such that the sum of the probabilities (second item of the tuples), is
		/// equal to one.</returns>
		/// <exception cref="ArgumentException">If one of the probabilities is smaller than zero.</exception>
		/// <remarks>
		/// <para>The <paramref name="source"/> stream must be finite.</para>
		/// <para>The resulting stream is finite as well.</para>
		/// </remarks>
		public static IEnumerable<Tuple<T,double>> Normalize<T> (this IEnumerable<Tuple<T,double>> source) {
			Queue<Tuple<T,double>> cache = new Queue<Tuple<T,double>> ();
			double sum = 0.0d;
			foreach (Tuple<T,double> tuple in cache) {
				cache.Enqueue (tuple);
				double p = tuple.Item2;
				if (p < 0.0d) {
					throw new ArgumentException ("An unnormalized probability must be larger than or equal to zero.");
				}
				sum += tuple.Item2;
			}
			sum = 1.0d / sum;
			while (cache.Count > 0x00) {
				Tuple<T,double> tup = cache.Dequeue ();
				yield return new Tuple<T,double> (tup.Item1, sum * tup.Item2);
			}
		}

		/// <summary>
		/// Zip two list of values together in a list of tuples such that the <c>i</c>-th tuple contains the
		/// <c>i</c>-th element of the first and second given list.
		/// </summary>
		/// <param name="sourcea">The first given list.</param>
		/// <param name="sourceb">The second given list.</param>
		/// <typeparam name="TA">The type of elements in the first list.</typeparam>
		/// <typeparam name="TB">The type of elements in the second list.</typeparam>
		public static IEnumerable<Tuple<TA,TB>> Zip<TA,TB> (this IEnumerable<TA> sourcea, IEnumerable<TB> sourceb) {
			if (sourcea != null && sourceb != null) {
				IEnumerator<TA> ea = sourcea.GetEnumerator ();
				IEnumerator<TB> eb = sourceb.GetEnumerator ();
				if (ea != null && eb != null) {
					while (ea.MoveNext () && eb.MoveNext ()) {
						yield return new Tuple<TA, TB> (ea.Current, eb.Current);
					}
				}
			}
		}

		/// <summary>
		/// Zips three lists of values together in a list of tuples such that the <c>i</c>-th tuple contains the
		/// <c>i</c>-th element of the given lists.
		/// </summary>
		/// <param name="sourcea">The first given list.</param>
		/// <param name="sourceb">The second given list.</param>
		/// <param name="sourcec">The third given list.</param>
		/// <typeparam name="TA">The type of elements in the first list.</typeparam>
		/// <typeparam name="TB">The type of elements in the second list.</typeparam>
		/// <typeparam name="TC">The type of elements in the third list.</typeparam>
		public static IEnumerable<Tuple<TA,TB,TC>> Zip<TA,TB,TC> (this IEnumerable<TA> sourcea, IEnumerable<TB> sourceb, IEnumerable<TC> sourcec) {
			if (sourcea != null && sourceb != null && sourcec != null) {
				IEnumerator<TA> ea = sourcea.GetEnumerator ();
				IEnumerator<TB> eb = sourceb.GetEnumerator ();
				IEnumerator<TC> ec = sourcec.GetEnumerator ();
				if (ea != null && eb != null && ec != null) {
					while (ea.MoveNext () && eb.MoveNext () && ec.MoveNext ()) {
						yield return new Tuple<TA, TB, TC> (ea.Current, eb.Current, ec.Current);
					}
				}
			}
		}

		/// <summary>
		/// Zips four lists of values together in a list of tuples such that the <c>i</c>-th tuple contains the
		/// <c>i</c>-th element of the given lists.
		/// </summary>
		/// <param name="sourcea">The first given list.</param>
		/// <param name="sourceb">The second given list.</param>
		/// <param name="sourcec">The third given list.</param>
		/// <param name="sourced">The fourth given list.</param>
		/// <typeparam name="TA">The type of elements in the first list.</typeparam>
		/// <typeparam name="TB">The type of elements in the second list.</typeparam>
		/// <typeparam name="TC">The type of elements in the third list.</typeparam>
		/// <typeparam name="TD">The type of elements in the fourth list.</typeparam>
		public static IEnumerable<Tuple<TA,TB,TC,TD>> Zip<TA,TB,TC,TD> (this IEnumerable<TA> sourcea, IEnumerable<TB> sourceb, IEnumerable<TC> sourcec, IEnumerable<TD> sourced) {
			if (sourcea != null && sourceb != null && sourcec != null && sourced != null) {
				IEnumerator<TA> ea = sourcea.GetEnumerator ();
				IEnumerator<TB> eb = sourceb.GetEnumerator ();
				IEnumerator<TC> ec = sourcec.GetEnumerator ();
				IEnumerator<TD> ed = sourced.GetEnumerator ();
				if (ea != null && eb != null && ec != null && ed != null) {
					while (ea.MoveNext () && eb.MoveNext () && ec.MoveNext () && ed.MoveNext ()) {
						yield return new Tuple<TA, TB, TC, TD> (ea.Current, eb.Current, ec.Current, ed.Current);
					}
				}
			}
		}

		/// <summary>
		/// Zips five lists of values together in a list of tuples such that the <c>i</c>-th tuple contains the
		/// <c>i</c>-th element of the given lists.
		/// </summary>
		/// <param name="sourcea">The first given list.</param>
		/// <param name="sourceb">The second given list.</param>
		/// <param name="sourcec">The third given list.</param>
		/// <param name="sourced">The fourth given list.</param>
		/// <param name="sourcee">The fifth given list.</param>
		/// <typeparam name="TA">The type of elements in the first list.</typeparam>
		/// <typeparam name="TB">The type of elements in the second list.</typeparam>
		/// <typeparam name="TC">The type of elements in the third list.</typeparam>
		/// <typeparam name="TD">The type of elements in the fourth list.</typeparam>
		/// <typeparam name="TE">The type of elements in the fifth list.</typeparam>
		public static IEnumerable<Tuple<TA,TB,TC,TD,TE>> Zip<TA,TB,TC,TD,TE> (this IEnumerable<TA> sourcea, IEnumerable<TB> sourceb, IEnumerable<TC> sourcec, IEnumerable<TD> sourced, IEnumerable<TE> sourcee) {
			if (sourcea != null && sourceb != null && sourcec != null && sourced != null && sourcee != null) {
				IEnumerator<TA> ea = sourcea.GetEnumerator ();
				IEnumerator<TB> eb = sourceb.GetEnumerator ();
				IEnumerator<TC> ec = sourcec.GetEnumerator ();
				IEnumerator<TD> ed = sourced.GetEnumerator ();
				IEnumerator<TE> ee = sourcee.GetEnumerator ();
				if (ea != null && eb != null && ec != null && ed != null && ee != null) {
					while (ea.MoveNext () && eb.MoveNext () && ec.MoveNext () && ed.MoveNext () && ee.MoveNext ()) {
						yield return new Tuple<TA, TB, TC, TD, TE> (ea.Current, eb.Current, ec.Current, ed.Current, ee.Current);
					}
				}
			}
		}

		/// <summary>
		/// Zips the given tuple of key-value pairs. When a key occurs a second time, the given function is applied to the cached value and
		/// the new value.
		/// </summary>
		/// <returns>A stream of tuples such that every key occurs only once containing the zip of all values corresponding to that key.</returns>
		/// <param name="source">A list of tuples to zip by key.</param>
		/// <param name="f">F.</param>
		/// <typeparam name="TKey">The type of the key.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <remarks>
		/// <para>The order of the original tuples of the <paramref name="source"/> is not maintained.</para>
		/// </remarks>
		public static IEnumerable<Tuple<TKey,TValue>> ZipKey<TKey,TValue> (this IEnumerable<Tuple<TKey,TValue>> source, Func<TValue,TValue,TValue> f) {
			Dictionary<TKey,TValue> cache = new Dictionary<TKey, TValue> ();
			TKey key;
			TValue val, cur;
			foreach (Tuple<TKey,TValue> tuple in source) {
				key = tuple.Item1;
				val = tuple.Item2;
				if (cache.TryGetValue (key, out cur)) {
					cache [key] = f (cur, val);
				} else {
					cache.Add (key, val);
				}
			}
			foreach (KeyValuePair<TKey,TValue> kvp in cache) {
				yield return new Tuple<TKey,TValue> (kvp.Key, kvp.Value);
			}
		}

		/// <summary>
		/// Caches the items of the specified source such that enumerating over it multiple times will increase performance.
		/// </summary>
		/// <param name="source">A source of items to be cached and enumerated eventually.</param>
		/// <typeparam name="T">The type of elements produced by the source.</typeparam>
		/// <remarks>
		/// <para>This is merely done if the given <paramref name="source"/> is an expensive LINQ-query that should
		/// be executed several times and would always yield the same result.</para>
		/// <para>Infinite <see cref="T:IEnumerable`1"/> instances are supported since the data
		/// is cached lazely.</para>
		/// <para>The cached version supports multiple enumerations at once.</para>
		/// </remarks>
		public static IEnumerable<T> Cache<T> (this IEnumerable<T> source) {
			return new LinqCache<T> (source);
		}

		/// <summary>
		/// Flattens the given two-dimensional source to a one-dimensional list.
		/// </summary>
		/// <param name="source">The given two-dimensional list to flatten.</param>
		/// <typeparam name="T">The type of items to enumerate.</typeparam>
		public static IEnumerable<T> Flatten <T> (this IEnumerable<IEnumerable<T>> source) {
			foreach (IEnumerable<T> list in source) {
				foreach (T item in list) {
					yield return item;
				}
			}
		}

		/// <summary>
		/// Returns the <paramref name="ith"/> element of the given <paramref name="source"/>, in case
		/// such element does not exists, the <c>default(T)</c> is returned.
		/// </summary>
		/// <returns>The <paramref name="ith"/> elementen of the given <see cref="T:IEnumerable`1"/> if
		/// that element exists; otherwise <c>default(T)</c>.</returns>.
		/// <param name="source">The given source to determine the <c>i</c>-th element from.</param>
		/// <param name="ith">An optional parameter describing the requested index, by default zero (<c>0</c>).</param>
		/// <typeparam name="T">The type of the elements of the given <paramref name="source"/>.</typeparam>
		/// <remarks>
		/// <para>The method is optimized for <see cref="T:IList`1"/> instances, in that case
		/// the index of the element is accessed directly.</para>
		/// <para>In case the given <paramref name="source"/> is not effective, or the given
		/// index is out of range, <c>default(T)</c> is returned.</para>
		/// </remarks>
		public static T IthOrDefault<T> (this IEnumerable<T> source, int ith = 0x00) {
			if (ith >= 0x00 && source != null) {
				if (source is IList<T>) {
					IList<T> list = (IList<T>)source;
					if (ith < list.Count) {
						return list [ith];
					}
				} else {
					return source.Skip (ith).FirstOrDefault ();
				}
			}
			return default(T);
		}

		/// <summary>
		/// Determines the index of a specific item in the given <see cref="T:IEnumerable`1" />.
		/// </summary>
		/// <returns>The (first) index of <paramref name="item"/> if found in the list; otherwise, <c>-1</c>.</returns>
		/// <param name="source">The given <see cref="T:IEnumerable`1"/> to search for the given <paramref name="item"/>.</param>
		/// <param name="item">The object to locate in the <see cref="T:IEnumerable`1" />.</param>
		/// <typeparam name="T">The type of the elements of the given <paramref name="source"/>.</typeparam>
		/// <remarks>
		/// <para>The method is optimized for <see cref="T:IList`1"/> instances, in the <see cref="IndexOf"/> method
		/// is called immediately (since some <see cref="T:IList`1"/> implementations constraint the list
		/// so searching can be done faster).</para>
		/// <para>In case the given <paramref name="source"/> is not effective, <c>-1</c> is returned as well.</para>
		/// </remarks>
		public static int IndexOf<T> (this IEnumerable<T> source, T item) {
			if (source != null) {
				if (source is IList<T>) {
					IList<T> list = (IList<T>)source;
					return list.IndexOf (item);
				} else {
					int index = 0x00;
					foreach (T elem in source) {
						if (Object.Equals (source, elem)) {
							return index;
						}
					}
				}
			}
			return -0x01;
		}
		#endregion
	}
}