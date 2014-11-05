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
using System.Text;
using NUtils.Abstract;
using NUtils.Maths;
using Microsoft.FSharp.Math;

namespace NUtils.Functional {

	/// <summary>
	/// A utility funcation for <see cref="T:IEnumerable`1"/> lists.
	/// </summary>
	public static class EnumerableUtils {

		#region LINQ extra extensions
		/// <summary>
		/// Order the given <paramref name="source"/> list with the given <paramref name="comparer"/> in descending order.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerable`1"/> with the same items as the given <paramref name="source"/> but ordered
		/// according to the given <paramref name="comparator"/>.</returns>
		/// <param name="source">The source of items that must be ordered.</param>
		/// <param name="comparer">The <see cref="T:IComparer`1"/> that describes how the items must be ordered.</param>
		/// <typeparam name="T">The type of elements that must be ordered.</typeparam>
		/// <remarks>
		/// <para>This method is a wrapper for the <see cref="M:Enumerable.OrderBy``2"/> method where
		/// the identity function is used as key selector.</para>
		/// </remarks>
		public static IEnumerable<T> OrderBy<T> (this IEnumerable<T> source, IComparer<T> comparer) {
			return source.OrderBy (FunctionUtils.Identity<T>, comparer);
		}

		/// <summary>
		/// Order the given <paramref name="source"/> list with the given <paramref name="comparer"/>.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerable`1"/> with the same items as the given <paramref name="source"/> but ordered
		/// according to the given <paramref name="comparator"/>.</returns>
		/// <param name="source">The source of items that must be ordered.</param>
		/// <param name="comparer">The <see cref="T:IComparer`1"/> that describes how the items must be ordered.</param>
		/// <typeparam name="T">The type of elements that must be ordered.</typeparam>
		/// <remarks>
		/// <para>This method is a wrapper for the <see cref="M:Enumerable.OrderBy``2"/> method where
		/// the identity function is used as key selector.</para>
		/// </remarks>
		public static IEnumerable<T> OrderByDescending<T> (this IEnumerable<T> source, IComparer<T> comparer) {
			return source.OrderByDescending (FunctionUtils.Identity<T>, comparer);
		}

		/// <summary>
		/// Converts the given <see cref="T:IEnumerable`1"/> to a new constructed <see cref="T:LinkedList`1"/>
		/// </summary>
		/// <returns>A <see cref="T:LinkedList`1"/> that contains all the elements of the given <paramref name="source"/>
		/// in the same order.</returns>
		/// <param name="source">The list of items to convert to a <see cref="T:LinkedList`1"/>.</param>
		/// <typeparam name="T">The type of items that will be enumerated.</typeparam>
		/// <remarks>
		/// <para>
		/// The given <see cref="T:IEnumerable`1"/> must be finite.
		/// </para><para>
		/// If the given <paramref name="source"/> is not effective, the result will not be effective as well.
		/// </para>
		/// </remarks>
		public static LinkedList<T> ToLinkedList<T> (this IEnumerable<T> source) {
			LinkedList<T> result = null;
			if (source != null) {
				result = new LinkedList<T> ();
				foreach (T si in source) {
					result.AddLast (si);
				}
			}
			return result;
		}
		#endregion
		#region Basic Extension methods
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
		/// <para>The method is optimized for <see cref="T:IList`1"/> instances, in the <see cref="M:IList`1.IndexOf"/> method
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
						if (Object.Equals (item, elem)) {
							return index;
						}
						index++;
					}
				}
			}
			return -0x01;
		}

		/// <summary>
		/// Enumerate the cross-product of the two given <see cref="T:IEnumerable`1"/> instances.
		/// </summary>
		/// <param name="source1">The first given list of instances.</param>
		/// <param name="source2">The second given list of instances.</param>
		/// <typeparam name="T1">The type of elements emitted by the first list.</typeparam>
		/// <typeparam name="T2">The type of elements emitted by the second list.</typeparam>
		public static IEnumerable<Tuple<T1,T2>> Cross<T1,T2> (this IEnumerable<T1> source1, IEnumerable<T2> source2) {
			foreach (T1 t1 in source1) {
				foreach (T2 t2 in source2) {
					yield return new Tuple<T1,T2> (t1, t2);
				}
			}
		}

		/// <summary>
		/// Checks if the given <paramref name="source"/> contains at least one element (this element can be non-effective).
		/// </summary>
		/// <param name="source">The given list to check for.</param>
		/// <typeparam name="T">The type of the elements in the given list.</typeparam>
		/// <returns><c>true</c> if the given <paramref name="source"/> contains at least one element; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>If the <paramref name="source"/> is not effective, or the corresponding <see cref="T:IEnumerator`1"/>, <c>false</c> is returned.</para>
		/// <para>If the <see cref="M:IEnumerator`1.MoveNext"/> method throws an error, the error is thrown as well.</para>
		/// </remarks>
		public static bool Contains<T> (this IEnumerable<T> source) {
			if (source != null) {
				IEnumerator<T> enu = source.GetEnumerator ();
				if (enu != null) {
					return enu.MoveNext ();
				}
			}
			return false;
		}
		#endregion
		#region Data.List (Haskell)
		/// <summary>
		/// The prepend operator of Haskell (denoted by a colon <code>:</code> ). The equivalent of the <code>( head : tail )</code> operator.
		/// </summary>
		/// <param name="head">
		/// The head of the list we are generating.
		/// </param>
		/// <param name="tail">
		/// The tail of the list we are generating.
		/// </param>
		/// <returns>
		/// A lazy generated list, who starts with the head, followed by the elements of the tail.
		/// </returns>
		public static IEnumerable<T> Prepend<T> (T head, IEnumerable<T> tail) {
			yield return head;
			foreach (T x in tail) {
				yield return x;
			}
		}

		/// <summary>
		/// The prepend operator of Haskell (denoted by a colon). The equivalent of the <code>( head1 : head2 : tail )</code> operator.
		/// </summary>
		/// <param name="head1">
		/// The first head of the list we are generating.
		/// </param>
		/// <param name="head2">
		/// The second head of the list we are generating.
		/// </param>
		/// <param name="tail">
		/// The tail of the list we are generating.
		/// </param>
		/// <returns>
		/// A lazy generated list, who starts with the two heads, followed by the elements of the tail.
		/// </returns>
		/// <remarks>
		/// There is no real equivalent of this method in Haskell since the colon operator is right associative. Therefore one can see <code>(h1:h2:t)</code>
		/// as <code>(h1:(h2:t))</code>. However since this notation is not (yet) possible in Mono, we have defined additional methods for better support.
		/// </remarks>
		public static IEnumerable<T> Prepend<T> (T head1, T head2, IEnumerable<T> tail) {
			yield return head1;
			yield return head2;
			foreach (T x in tail) {
				yield return x;
			}
		}
		#region BasicFunctions
		/// <summary>
		/// Appends two lists. The equivalent of the <code>xs++ys</code> operator in Haskell.
		/// </summary>
		/// <param name="xs">
		/// The first list.
		/// </param>
		/// <param name="ys">
		/// The seconds list.
		/// </param>
		/// <returns>
		/// A lazy generated list who contains elements of the first list <paramref name="xs"/> followed by the elements of the second list <paramref name="ys"/>.
		/// </returns>
		/// <remarks>
		/// If the first list is infinite, the result is the first list.
		/// </remarks>
		public static IEnumerable<T> Append<T,SX> (this IEnumerable<T> xs, IEnumerable<SX> ys) where SX : T {
			foreach (T x in xs) {
				yield return x;
			}
			foreach (T y in ys) {
				yield return y;
			}
		}

		/// <summary>
		/// Extract the first element of a list, which must be non-empty.
		/// </summary>
		/// <param name="xs">
		/// The list to extract the first element from.
		/// </param>
		/// <returns>
		/// The first element of the given list.
		/// </returns>
		public static T Head<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			ie.MoveNext ();
			return ie.Current;
		}

		/// <summary>
		/// Extract the last element of a list, which must be finite and non-empty.
		/// </summary>
		/// <param name="xs">
		/// The list to extract the last element from.
		/// </param>
		/// <returns>
		/// The last element of the given list.
		/// </returns>
		public static T Last<T> (IEnumerable<T> xs) {
			T t = default(T);
			foreach (T x in xs) {
				t = x;
			}
			return t;
		}

		/// <summary>
		/// Extract the last element of a list, which must be finite and non-empty.
		/// </summary>
		/// <param name="xs">
		/// The list to extract the last element from.
		/// </param>
		/// <returns>
		/// The last element of the given list.
		/// </returns>
		public static IEnumerable<T> Tail<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			ie.MoveNext ();
			while (ie.MoveNext ()) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// Return all the elements of a list except the last one. The list must be non-empty.
		/// </summary>
		/// <param name="xs">
		/// The lazy list to calculate the sublist from.
		/// </param>
		/// <returns>
		/// A lazy generated list who contains all the elements of <paramref name="xs"/> except the last element.
		/// </returns>
		public static IEnumerable<T> Init<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			ie.MoveNext ();
			T t = ie.Current;
			while (ie.MoveNext ()) {
				yield return t;
				t = ie.Current;
			}
		}

		/// <summary>
		/// Test whether a list is empty.
		/// </summary>
		/// <param name="xs">
		/// The list to test emptyness on.
		/// </param>
		/// <returns>
		/// True if the list is empty, otherwise true. This property is checked active (not lazy).
		/// </returns>
		/// <remarks>
		/// This method is more powerfull than checking if the length of the list is larger than zero since infinite
		/// lists will never return their length, and this method only checks the first element.
		/// </remarks>
		public static bool Null<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			return !ie.MoveNext ();
		}

		/// <summary>
		/// Returns the length of a finite list as an integer.
		/// </summary>
		/// <param name="xs">
		/// The list to get the length from.
		/// </param>
		/// <returns>
		/// The length of the list. This length is generated active (not lazy).
		/// </returns>
		/// <remarks>
		/// In order to achieve the length of the list, the list must be finite. This method calculates the length in O(n).
		/// </remarks>
		public static int Length<T> (this IEnumerable<T> xs) {
			return xs.Count ();
		}

		/// <summary>
		/// Checks if the given list is larger than the given length.
		/// </summary>
		/// <param name="xs">
		/// The list to check the constraint on.
		/// </param>
		/// <param name="length">
		/// The given length. In order to pass this test, the length of the list must be larger than this.
		/// </param>
		/// <returns>
		/// True if the given list <paramref name="xs"/> is larger than the given length <paramref name="length"/>, otherwise false.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This method is usefull for inifinite lists when we can't extract the length of the list but still need a size constraint.
		/// </para><para>
		/// This method is not implemented in the <code>Data.List</code> library.
		/// </para>
		/// </remarks>
		public static bool LengthLarger<T> (this IEnumerable<T> xs, int length) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			for (int i = 0; i < length && ie.MoveNext (); i++)
				;
			return ie.MoveNext ();
		}
		#endregion
		#region ListTransformations
		/// <summary>
		/// Obtain a list where the given function <paramref name="f"/> is applied on any element of the given list <paramref name="xs"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list of original values.
		/// </param>
		/// <param name="f">
		/// The function that must be applied on any element of the original list.
		/// </param>
		/// <returns>
		/// A lazy generated list where each element is the result of applying <paramref name="f"/> on the corresponding element of the given list <paramref name="xs"/>.
		/// </returns>
		public static IEnumerable<TF> Map<T,TF> (this IEnumerable<T> xs, Func<T,TF> f) {
			foreach (T x in xs) {
				yield return f (x);
			}
		}

		/// <summary>
		/// Obtain a list where the elements of the given list are enumerated on the reversed order.
		/// </summary>
		/// <param name="xs">
		/// The given list to be reversed.
		/// </param>
		/// <returns>
		/// The elements of <paramref name="xs"/> in reversed order. <paramref name="xs"/> must be finite.
		/// </returns>
		public static IEnumerable<T> Reverse<T> (IEnumerable<T> xs) {
			Stack<T> sx = new Stack<T> ();
			foreach (T x in xs) {
				sx.Push (x);
			}
			while (sx.Count > 0) {
				yield return sx.Pop ();
			}
		}

		/// <summary>
		/// The intersperse function takes an element and a list and ``intersperses'' that element between the elements of the list.
		/// </summary>
		/// <param name="xs">
		/// The list of that must be interspersed.
		/// </param>
		/// <param name="inter">
		/// The element that must be placed between each element of the original list and it's successor.
		/// </param>
		/// <returns>
		/// A lazy generated list who contains alternately an element of the given list <paramref name="xs"/> and the intersperse element <paramref name="inter"/>.
		/// </returns>
		/// <example>
		/// The following code:
		/// <code>
		/// "abc".Intersperse(",");
		/// </code>
		/// Will generate the following result:
		/// <code>"a,b,c"</code>
		/// </example>
		public static IEnumerable<T> Intersperse<T> (this IEnumerable<T> xs, T inter) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			if (ie.MoveNext ()) {
				yield return ie.Current;
				while (ie.MoveNext ()) {
					yield return inter;
					yield return ie.Current;
				}
			}
		}

		/// <summary>
		/// Obtain a list of list where a fragment <paramref name="xs"/> is inserted between each element of <paramref name="xss"/> and it's successor.
		/// </summary>
		/// <param name="xss">
		/// A list with elements who will form the base of the result.
		/// </param>
		/// <param name="xs">
		/// A fragement who is inserted between each each element of <paramref name="xss"/> and it's sucessor.
		/// </param>
		/// <returns>
		/// A lazy generated list who contains alretately an element of the given list <paramref name="xss"/> and the given fragment <paramref name="xs"/>.
		/// </returns>
		/// <remarks>
		/// <code>xss.Intercalculate(xs)</code> is the equivalent of <code>xss.Intersperse(xs).Concat()</code>.
		/// </remarks>
		public static IEnumerable<T> Intercalculate<T> (this IEnumerable<IEnumerable<T>> xss, IEnumerable<T> xs) {
			return xss.Intersperse (xs).Concat ();
		}

		/// <summary>
		/// Generate the transpose of the given matrix. This means the rows and columns are swapped.
		/// </summary>
		/// <param name="matrix">
		/// The given matrix to be transposed.
		/// </param>
		/// <returns>
		/// A lazy generated 2d list where an element <i>(i,j)</i> of the resulting list is the element <i>(j,i)</i> of the original list.
		/// </returns>
		public static IEnumerable<IEnumerable<T>> Transpose<T> (this IEnumerable<IEnumerable<T>> matrix) {
			IEnumerable<IEnumerable<T>> xss = matrix;
			while (!xss.Head ().Null ()) {
				IEnumerable<T> xxs = xss.Head ();
				T x = xxs.Head ();
				xss = xss.Tail ();
				yield return Prepend (x, xss.Map (a => a.Head ()));
				IEnumerable<T> xs = xxs.Tail ();
				xss = Prepend (xs, xss.Map (a => a.Tail ()));
			}
		}

		/// <summary>
		/// Generate a list of all subsequences of the given argument.
		/// </summary>
		/// <param name="xs">
		/// A given sequence of items to generate all subsequences from.
		/// </param>
		/// <returns>
		/// The list of subsequences of the argument <paramref name="xs"/>.
		/// </returns>
		/// <example>
		/// The code <code>"abc".Subsequences()</code> will result in a list with the following elements: <code>"","a","b","ab","c","ac","bc","abc"</code>
		/// </example>
		public static IEnumerable<IEnumerable<T>> Subsequences<T> (this IEnumerable<T> xs) {
			return Prepend (new T[0x00], xs.NonEmptySubsequences ());
		}

		/// <summary>
		/// Generates a list of all subsequences of the given argument, except the empty subsequence.
		/// </summary>
		/// <param name="list">
		/// A given sequence of items to generate all subsequences from.
		/// </param>
		/// <returns>
		/// The list of subsequences of the argument <paramref name="xs"/>
		/// </returns>
		/// <remarks>
		/// This method is no part of the original <code>Data.List</code> Haskell library.
		/// </remarks><example>
		/// The code <code>"abc".NonEmptySubsequences()</code> will result in a list with the following elements: <code>"a","b","ab","c","ac","bc","abc"</code>
		/// </example>
		public static IEnumerable<IEnumerable<T>> NonEmptySubsequences<T> (this IEnumerable<T> list) {
			if (!list.Null ()) {
				T x = list.Head ();
				yield return new T[] { x };
				IEnumerable<T> xs = list.Tail ();
				Func<IEnumerable<T>,IEnumerable<IEnumerable<T>>,IEnumerable<IEnumerable<T>>> f = (a, b) => Prepend (a, Prepend (x, a), b);
				foreach (IEnumerable<T> r in Foldr(f,new T[0x00][],NonEmptySubsequences(xs))) {
					yield return r;
				}
			}
		}

		/// <summary>
		/// The permutations function returns the list of all permutations of the argument.
		/// </summary>
		/// <param name="list">
		/// The list of items to generate a list of all permutations from.
		/// </param>
		/// <returns>
		/// A list of lists where each item is a permutation of the list. The list has <i>n!</i> elements with
		/// <i>n</i> the length of the original list.
		/// </returns>
		/// <example>
		/// A simple example is the generation of permutations of <c>"abc"</c>: the permutations are generated by:
		/// <code>"abc".Permutations();</code> will result in: <c>["abc","bac","cba","bca","cab","acb"]</c>.
		/// </example>
		public static IEnumerable<IEnumerable<T>> Permutations<T> (this IEnumerable<T> list) {
			return Prepend (list, perms (list, new T[0x00]));
		}

		private static IEnumerable<IEnumerable<T>> perms<T> (IEnumerable<T> tl, IEnumerable<T> il) {
			if (!tl.Null ()) {
				T t = tl.Head ();
				IEnumerable<T> ts = tl.Tail ();
				return Foldr ((x, y) => interleave (t, ts, b => b, x, y).Item2, perms (ts, Prepend (t, il)), Permutations (il));
			} else {
				return new T[0x00][];
			}
		}

		private static Tuple<IEnumerable<T>,IEnumerable<TF>> interleave<T,TF> (T t, IEnumerable<T> ts, Func<IEnumerable<T>,TF> f, IEnumerable<T> xs, IEnumerable<TF> r) {
			if (xs.Null ()) {
				return new Tuple<IEnumerable<T>,IEnumerable<TF>> (ts, r);
			} else {
				Tuple<IEnumerable<T>,IEnumerable<TF>> tmp = interleave (t, ts, x => f (Prepend (xs.Head (), x)), xs.Tail (), r);
				IEnumerable<T> yus = Prepend (xs.Head (), tmp.Item1);
				return new Tuple<IEnumerable<T>,IEnumerable<TF>> (yus, Prepend (f (Prepend (t, yus)), tmp.Item2));
			}
		}
		#endregion
		#region ReducingLists
		/// <summary>
		/// Applies a binary operator, a starting value (typically the left-identity of the operator), and a list, reduces the list using
		/// the binary operator from left to right. The list must be finite.
		/// </summary>
		/// <param name="f">
		/// The binary operator used by the operation.
		/// </param>
		/// <param name="z">
		/// The starting value of the operation.
		/// </param>
		/// <param name="ys">
		/// The list of values to evaluate the operation on.
		/// </param>
		/// <returns>
		/// The binary operator evaluated left to right on the list formed by the starting value <paramref name="z"/> and the given
		/// list <paramref name="ys"/>.
		/// </returns>
		/// <remarks>
		/// This operation is performed in <i>O(n)</i> with <i>n</i> the length of the given list <paramref name="ys"/>.
		/// </remarks>
		public static TX Foldl<TX,Y> (Func<TX,Y,TX> f, TX z, IEnumerable<Y> ys) {
			TX t = z;
			foreach (Y y in ys) {
				t = f (t, y);
			}
			return t;
		}

		/// <summary>
		/// A variant of <see cref="M:Foldl`2"/> that has no starting value argument, and thus must be applied to non-empty lists.
		/// </summary>
		/// <param name="f">
		/// The binary operator used by the operation.
		/// </param>
		/// <param name="xs">
		/// The list of values to evaluate the operation on.
		/// </param>
		/// <returns>
		/// The binary operator evaluated left to right on the given list <paramref name="ys"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This operation is performed in <i>O(n)</i> with <i>n</i> the length of the given list <paramref name="ys"/>.
		/// </para><para>
		/// This operation <code>DataList.Foldl1(f,list)</code> is equivalent to <code>DataList.Foldl(f,list.Head(),list.Tail())</code>.
		/// </para>
		/// </remarks>
		public static T Foldl1<T> (Func<T,T,T> f, IEnumerable<T> xs) {
			return Foldl (f, xs.Head (), xs.Tail ());
		}

		/// <summary>
		/// Applies a binary operator, a starting value (typically the right-identity of the operator), and a list, reduces the list using
		/// the binary operator from right to left. The list must be finite.
		/// </summary>
		/// <param name="f">
		/// The binary operator used in this in this operation.
		/// </param>
		/// <param name="z">
		/// The starting value of the operation.
		/// </param>
		/// <param name="xs">
		/// The list of values to evaluate the operation on.
		/// </param>
		/// <returns>
		/// The binary operator evaluated right to left on the list formed by the starting value <paramref name="z"/> and the given
		/// list <paramref name="xs"/>.
		/// </returns>
		/// <remarks>
		/// This operation is performed in <i>O(n)</i> with <i>n</i> the length of the given list <paramref name="xs"/>.
		/// </remarks>
		public static TF Foldr<T,TF> (Func<T,TF,TF> f, TF z, IEnumerable<T> xs) {
			Stack<T> sx = new Stack<T> ();
			foreach (T x in xs) {
				sx.Push (x);
			}
			TF t = z;
			while (sx.Count > 0x00) {
				t = f (sx.Pop (), t);
			}
			return t;
		}

		/// <summary>
		/// A variant of <see cref="M:Foldr`2"/> that has no starting value argument, and thus must be applied to non-empty lists.
		/// </summary>
		/// <param name="f">
		/// The binary operator used by the operation.
		/// </param>
		/// <param name="xs">
		/// The list of values to evaluate the operation on.
		/// </param>
		/// <returns>
		/// The binary operator evaluated right to left on the given list <paramref name="xs"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// This operation is performed in <i>O(n)</i> with <i>n</i> the length of the given list <paramref name="xs"/>.
		/// </para><para>
		/// This operation <code>DataList.Foldr1(f,list)</code> is equivalent to <code>DataList.Foldr(f,list.Last(),list.Init())</code>.
		/// </para>
		/// </remarks>
		public static T Foldr1<T> (Func<T,T,T> f, IEnumerable<T> xs) {
			return Foldr (f, xs.Last (), xs.Init ());
		}
		#region SpecialFolds
		/// <summary>
		/// Concatenates a list of lists.
		/// </summary>
		/// <param name="xss">
		/// A lists of lists who must be concatenated.
		/// </param>
		/// <returns>
		/// A lazy generated list who contains the elements of the elements of the lists in <paramref name="xss"/>.
		/// </returns>
		public static IEnumerable<T> Concat<T> (this IEnumerable<IEnumerable<T>> xss) {
			foreach (IEnumerable<T> xs in xss) {
				foreach (T x in xs) {
					yield return x;
				}
			}
		}

		/// <summary>
		/// Maps a function over the given list <paramref name="xs"/> and concatenates the results.
		/// </summary>
		/// <param name="xs">
		/// The given list of original values.
		/// </param>
		/// <param name="f">
		/// The given function who must be performed on the original elements <paramref name="xs"/>
		/// </param>
		/// <returns>
		/// A lazy generated list containing the elements after the given function <paramref name="f"/> is aplied to the
		/// original values <paramref name="xs"/>.
		/// </returns>
		public static IEnumerable<TF> ConcatMap<T,TF> (this IEnumerable<T> xs, Func<T,IEnumerable<TF>> f) {
			foreach (T x in xs) {
				foreach (TF y in f(x)) {
					yield return y;
				}
			}
		}

		/// <summary>
		/// Returns the conjunction of a Boolean list. For the result to be True, the list must be finite; False however,
		/// results from a False value at a finite index of a finite or infinite list.
		/// </summary>
		/// <param name="list">
		/// A list of booleans where the AND operator is applied to.
		/// </param>
		/// <returns>
		/// True if the list contains only True values and is finite, False if the list contains a False at a finite index.
		/// </returns>
		public static bool And (this IEnumerable<bool> list) {
			foreach (bool x in list) {
				if (!x) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Returns the disjunction of a Boolean list. For the result to be False, the list must be finite; True, however,
		/// results from a True value at a finite index of a finite or infinite list.
		/// </summary>
		/// <param name="list">
		/// A list of booleans where the OR operator is applied to.
		/// </param>
		/// <returns>
		/// True if the list contains a True at a finite index. False if the list contains only False values and is finite.
		/// </returns>
		public static bool Or (this IEnumerable<bool> list) {
			foreach (bool x in list) {
				if (x) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Applies a given predicate to the given list. If any of the object succeeds on the predicate, true is returned. For the
		/// result to be False, the list must be finite. True however, results from a True value for the predicate applied to an
		/// element at a finite index of a finite or infinite list.
		/// </summary>
		/// <param name="list">
		/// The given list of elements.
		/// </param>
		/// <param name="predicate">
		/// A function who checks elements in <paramref name="list"/> for a certain property.
		/// </param>
		/// <returns>
		/// True if at least one of the elements in the list succeeds on the given predicate <paramref name="predicate"/>, otherwise False.
		/// </returns>
		public static bool Any<T> (IEnumerable<T> list, Predicate<T> predicate) {
			foreach (T x in list) {
				if (predicate (x)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Applies the given predicate to the given list. If all of the objects succceed on the predicate, true is returned. For the
		/// result to be True, the list must be finite, False however, results from a False value for the predicate applied to an
		/// element at a finite index of a finite or infinite list.
		/// </summary>
		/// <param name="list">
		/// The given list of elements.
		/// </param>
		/// <param name="predicate">
		/// A function who checks elements in <paramref name="list"/> for a certain property.
		/// </param>
		/// <returns>
		/// True if all of the elements in the list suceed on the given predicate <paramref name="predicate"/>, otherwise False.
		/// </returns>
		public static bool All<T> (IEnumerable<T> list, Predicate<T> predicate) {
			foreach (T x in list) {
				if (!predicate (x)) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Computes the sum of a finite list of numbers.
		/// </summary>
		/// <param name="list">
		/// The list to compute the sum from.
		/// </param>
		/// <returns>
		/// The sum of the elements of the given list <paramref name="list"/>.
		/// </returns>
		public static int Sum (this IEnumerable<int> list) {
			return Foldl1 ((a, b) => a + b, list);
		}

		/// <summary>
		/// Computes the sum of a finite list of numbers.
		/// </summary>
		/// <param name="list">
		/// The list to compute the sum from.
		/// </param>
		/// <returns>
		/// The sum of the elements of the given list <paramref name="list"/>.
		/// </returns>
		public static T Sum<T> (this IEnumerable<T> list) where T : INumeric<T> {
			return Foldl1 ((a, b) => a.Add (a, b), list);
		}

		/// <summary>
		/// Computes the product of a finite list of numbers.
		/// </summary>
		/// <param name="list">
		/// The list to compute the product from.
		/// </param>
		/// <returns>
		/// The product of the elements of the given list <paramref name="list"/>.
		/// </returns>
		public static T Product<T> (this IEnumerable<T> list) where T : INumeric<T> {
			return Foldl1 ((a, b) => a.Multiply (a, b), list);
		}

		/// <summary>
		/// Computes the maximum value from a list which must be non-empty, finite and of a comparable type. It is a special case of
		/// <see cref="M:EnumerableUtils.MaximumBy`1"/> which allows the programmer to supply their own comparison function.
		/// </summary>
		/// <param name="list">
		/// The given list of values to calculate the maximum from.
		/// </param>
		/// <returns>
		/// The maximum element of the given list <paramref name="list"/>.
		/// </returns>
		public static T Maximum<T> (this IEnumerable<T> list) where T : IComparable<T> {
			return Foldl1 ((a, b) => MathUtils.Maximum (a, b), list);
		}

		/// <summary>
		/// Computes the minimum value from a list which must be non-empty, finite and of a comparable type. It is a special case of
		/// <see cref="M:EnumerableUtils.MinimumBy`1"/> which allows the programmer to supply their own comparison function.
		/// </summary>
		/// <param name="list">
		/// The given list of values to calculate the minimum from.
		/// </param>
		/// <returns>
		/// The minimum element of the given list <paramref name="list"/>.
		/// </returns>
		public static T Minimum<T> (this IEnumerable<T> list) where T : IComparable<T> {
			return Foldl1 ((a, b) => MathUtils.Minimum (a, b), list);
		}
		#endregion
		#endregion
		#region BuildingLists
		#region Scans
		/// <summary>
		/// A method similar to <see cref="M:EnumerableUtils.Foldl`2"/> but returns the list of successive reduced values from the left.
		/// </summary>
		/// <param name="f">
		/// The given function to evaluate the following element.
		/// </param>
		/// <param name="z">
		/// The given start value.
		/// </param>
		/// <param name="ys">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the <see cref="M:EnumerableUtils.Foldl`2"/> of the sublist of 0 to <i>i-1</i>.
		/// </returns>
		/// <remarks>
		/// Note that <code>DataList.Scanl(f,z,ys).Last()</code> is equal to <code>DataList.Foldl(f,z,ys)</code>
		/// </remarks>
		public static IEnumerable<T> Scanl<T,TF> (Func<T,TF,T> f, T z, IEnumerable<TF> ys) {
			T t = z;
			yield return t;
			foreach (TF y in ys) {
				t = f (t, y);
				yield return t;
			}
		}

		/// <summary>
		/// A variant of <see cref="M:Scanl`2"/> that has no starting value.
		/// </summary>
		/// <param name="f">
		/// The given function to evaluate the following element.
		/// </param>
		/// <param name="xs">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the <see cref="M:Foldl1`2"/> on the sublist of 0 to <i>i-1</i>.
		/// </returns>
		public static IEnumerable<T> Scanl1<T> (Func<T,T,T> f, IEnumerable<T> xs) {
			return Scanl (f, xs.Head (), xs.Tail ());
		}

		/// <summary>
		/// The right-to-left dual of <see cref="M:Scanl`2"/>.
		/// </summary>
		/// <param name="f">
		/// The given function to evaluate the following element.
		/// </param>
		/// <param name="z">
		/// The given start value.
		/// </param>
		/// <param name="ys">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the <see cref="M:Foldr`2"/> of the sublist of <i>i</i> to <i>n</i>.
		/// </returns>
		/// <remarks>
		/// Note that <code>DataList.Scanr(f,z,ys).Head()</code> is equal to <code>DataList.Foldr(f,z,ys)</code>
		/// </remarks>
		public static IEnumerable<T> Scanr<T,TF> (Func<T,TF,T> f, T z, IEnumerable<TF> ys) {
			Stack<TF> sy = new Stack<TF> ();
			foreach (TF y in ys) {
				sy.Push (y);
			}
			T t = z;
			yield return t;
			foreach (TF y in ys) {
				t = f (t, sy.Pop ());
				yield return t;
			}
		}

		/// <summary>
		/// A variant of <see cref="M:Scanr`2"/> that has no starting value argument.
		/// </summary>
		/// <param name="f">
		/// The given function to evaluate the following element.
		/// </param>
		/// <param name="xs">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the <see cref="M:Foldr1`2"/> of the sublist of <i>i</i> to <i>n</i>.
		/// </returns>
		public static IEnumerable<T> Scanr1<T> (Func<T,T,T> f, IEnumerable<T> xs) {
			return Scanr (f, xs.Last (), xs.Init ());
		}
		#endregion
		#region AccumulatingMaps
		/// <summary>
		/// A method who behaves like a combination of <see cref="M:Map`2"/> and <see cref="M:Foldl`2"/>; it applies a function to
		/// each element of a list, passing an accumulating parameter from left to right, and returning the final value of this accumulator together
		/// with the new list.
		/// </summary>
		/// <param name="f">
		/// The given function to calculate both the folding and the mapping.
		/// </param>
		/// <param name="z">
		/// The starting value of the accumulator.
		/// </param>
		/// <param name="xs">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A tuple where the first element is the result of the folding, this element is immediatly generated. The second part is the mapping part,
		/// who is generated lazy.
		/// </returns>
		public static Tuple<Acc,IEnumerable<Y>> MapAccumL<T,Y,Acc> (Func<Acc,T,Tuple<Acc,Y>> f, Acc z, IEnumerable<T> xs) {
			return new Tuple<Acc, IEnumerable<Y>> (Foldl ((a, b) => f (a, b).Item1, z, xs), mapAccumLList (f, z, xs));
		}

		private static IEnumerable<Y> mapAccumLList<T,Y,Acc> (Func<Acc,T,Tuple<Acc,Y>> f, Acc z, IEnumerable<T> xs) {
			Acc t = z;
			Tuple<Acc,Y> res;
			foreach (T x in xs) {
				res = f (t, x);
				yield return res.Item2;
				t = res.Item1;
			}
		}

		/// <summary>
		/// A method who behaves like a combination of <see cref="M:Map`2"/> and <see cref="M:Foldr`2"/>; it applies a function to
		/// each element of a list, passing an accumulating parameter from right to left, and returning a final value of this accumulator together
		/// with the new list.
		/// </summary>
		/// <param name="f">
		/// The given function to calculate both the folding and the mapping.
		/// </param>
		/// <param name="z">
		/// The starting value of the accumulator.
		/// </param>
		/// <param name="xs">
		/// A list of elements to perform the function <paramref name="f"/> on.
		/// </param>
		/// <returns>
		/// A tuple where the first element is the result of the folding, this element is immediatly generated. The second part is the mapping part,
		/// who is generated lazy.
		/// </returns>
		public static Tuple<Acc,IEnumerable<Y>> MapAccumR<T,Y,Acc> (Func<Acc,T,Tuple<Acc,Y>> f, Acc z, IEnumerable<T> xs) {
			return new Tuple<Acc, IEnumerable<Y>> (Foldl ((a, b) => f (a, b).Item1, z, xs), mapAccumRList (f, z, xs));
		}

		private static IEnumerable<Y> mapAccumRList<T,Y,Acc> (Func<Acc,T,Tuple<Acc,Y>> f, Acc z, IEnumerable<T> xs) {
			Stack<T> sx = new Stack<T> ();
			foreach (T x in xs) {
				sx.Push (x);
			}
			Acc t = z;
			Tuple<Acc,Y> res;
			while (sx.Count > 0x00) {
				res = f (t, sx.Pop ());
				yield return res.Item2;
				t = res.Item1;
			}
		}
		#endregion
		#region InfiniteLists
		/// <summary>
		/// Returns an infinte list of repeated applications of <paramref name="f"/> to <paramref name="x"/>.
		/// </summary>
		/// <param name="f">
		/// The given function on which we apply infinite iteration.
		/// </param>
		/// <param name="x">
		/// The initial value of the list.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the <i>i</i>-th iteration of <paramref name="f"/> on <paramref name="x"/>.
		/// </returns>
		public static IEnumerable<T> Iterate<T> (Func<T,T> f, T x) {
			T t = x;
			while (true) {
				yield return t;
				t = f (t);
			}
		}

		/// <summary>
		/// Generate an infinite list with <paramref name="x"/> the value of every element.
		/// </summary>
		/// <param name="x">
		/// The value of each element in the generated list.
		/// </param>
		/// <returns>
		/// A lazy generated list where each element is equal to <paramref name="x"/>.
		/// </returns>
		public static IEnumerable<T> Repeat<T> (T x) {
			while (true) {
				yield return x;
			}
		}

		/// <summary>
		/// Generate a list of length <paramref name="n"/> with <paramref name="x"/> the value of every element. It is an
		/// instance of the more general <see cref="M:EnumerableUtils.GenericReplicate"/>, in which <paramref name="n"/> may be
		/// of any integral type.
		/// </summary>
		/// <param name="n">
		/// The length of the generated list.
		/// </param>
		/// <param name="x">
		/// The value of each element in the generated list.
		/// </param>
		/// <returns>
		/// A lazy generated list with length <paramref name="n"/> where each element is equal to <paramref name="x"/>.
		/// </returns>
		public static IEnumerable<T> Replicate<T> (int n, T x) {
			for (int i = 0; i < n; i++) {
				yield return x;
			}
		}

		/// <summary>
		/// Ties a finite list into a circular one, or equivalently, the infinite repition of the original list. It is the
		/// identity on infinite lists.
		/// </summary>
		/// <param name="list">
		/// The list to be repeated infinitly.
		/// </param>
		/// <returns>
		/// A lazy generated list who keeps repeating the given list.
		/// </returns>
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
		#endregion
		#region Unfolding
		/// <summary>
		/// The "dual" method of the <see cref="EnumerableUtils.Foldr"/>: while <see cref="EnumerableUtils.Foldr"/> reduces a list to a
		/// summary value, unfoldr builds a list from a seed value. The function takes the element and returns Nothing if
		/// it is done producing the list or returns a <c>Tuple&lt;A,B&gt;(a,b)</c> in which case, a is a prepended to the list and b is
		/// used in the next element in a recursive call.
		/// </summary>
		/// <param name="f">
		/// The function that must be applied iteratively on to the offsetvalue <paramref name="offset"/>, until Nothing is reached.
		/// </param>
		/// <param name="offset">
		/// The offset value of the iterative function application process.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the application of <paramref name="f"/> onto <paramref name="offset"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Note that <code>DataList.Unfoldr(x => new Tuple&lt;X,Y&gt;(x,f(x)),offset)</code> is equivalent to <code>DataList.Iterate(f,x)</code>.
		/// </para><para>
		/// As the name suggests, in some cases Unfoldr can undo <see cref="EnumerableUtils.Foldr"/> operations:
		/// <code>DataList.Unfoldr(f2,DataList.Foldr(f,z,xs))</code> is then equivalent to <code>xs</code>. This happens when the following constraints hold:
		/// <list type="bullet">
		/// <item><code>f2(f(x,y))</code> is equal to <code>new Tuple&lt;X,Y&gt;(x,y)</code></item>
		/// <item><code>f2(z)</code> is equal to <code>new Maybe&lt;Tuple&lt;X,Y&gt;&gt;()</code> (this is Nothing).</item>
		/// </list>
		/// </para>
		/// </remarks>
		/// <example>
		/// A simple use of Unfoldr:
		/// <code>DataList.Unfoldr(a => ((a == 0) ? new Maybe&lt;Tuple&lt;int,int&gt;&gt;() : new Tuple&lt;int,int&gt;(a,a-1)),10)</code> will result in
		/// <code>[10,9,8,7,6,5,4,3,2,1]</code>.
		/// </example>
		public static IEnumerable<A> Unfoldr<A,B> (Func<B,Maybe<Tuple<A,B>>> f, B offset) {
			Maybe<Tuple<A,B>> mtab = f (offset);
			while (!mtab.Nothing) {
				yield return mtab.Value.Item1;
				mtab = f (mtab.Value.Item2);
			}
		}
		#endregion
		#endregion
		#region Sublists
		#region ExtractingSublists
		/// <summary>
		/// Generate a prefix of <paramref name="xs"/> of length <paramref name="n"/>, or <paramref name="xs"/> if <paramref name="n"/> is
		/// larger than the length of <paramref name="xs"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to generate the prefix from.
		/// </param>
		/// <param name="n">
		/// The length of the prefix to generate.
		/// </param>
		/// <returns>
		/// A lazy generated list containing the first <paramref name="n"/> items of <paramref name="xs"/>, or the entire list if the list is shorter
		/// than <paramref name="n"/>.
		/// </returns>
		/// <remarks>
		/// It is an instance of the more general <see cref="GenericTake"/> in which <paramref name="n"/> may be of any integral type.
		/// </remarks>
		public static IEnumerable<T> Take<T> (this IEnumerable<T> xs, int n) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			for (int i = 0; i < n && ie.MoveNext (); i++) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// Generate a suffix of <paramref name="xs"/> after the first <paramref name="n"/> elements, or an empty list if the length of <paramref name="xs"/>
		/// is larger than <paramref name="n"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to generate the suffix from.
		/// </param>
		/// <param name="n">
		/// The number of elements to drop before the suffix to generate.
		/// </param>
		/// <returns>
		/// A lazy generated list containing the elements after the first <paramref name="n"/> items of <paramref name="xs"/>,
		/// or an empty list if the list is shorter than <paramref name="n"/>.
		/// </returns>
		/// <remarks>
		/// It is an instance of the more general <see cref="GenericDrop"/> in which <paramref name="n"/> may be of any integral type.
		/// </remarks>
		public static IEnumerable<T> Drop<T> (this IEnumerable<T> xs, int n) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			for (int i = 0; i < n && ie.MoveNext (); i++)
				;
			while (ie.MoveNext ()) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// Generates a tuple where the first element is <paramref name="xs"/>'s prefix of length <paramref name="n"/> and the second element
		/// is the remainder of the list.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to generate the prefix and suffix from.
		/// </param>
		/// <param name="n">
		/// The length of the prefix.
		/// </param>
		/// <returns>
		/// A tuple containing the prefix and suffix of the given list <paramref name="xs"/> where the prefix is of length <paramref name="n"/>.
		/// </returns>
		public static Tuple<IEnumerable<T>,IEnumerable<T>> SplitAt<T> (this IEnumerable<T> xs, int n) {
			return new Tuple<IEnumerable<T>, IEnumerable<T>> (xs.Take (n), xs.Drop (n));
		}

		/// <summary>
		/// Applies a predicate <paramref name="p"/> on a list <paramref name="xs"/>, and returns the longest prefix (possibly empty) of <paramref name="xs"/>
		/// of elements satisfying <paramref name="p"/>.
		/// </summary>
		/// <param name="xs">
		/// The list of elements to generate the conditional prefix from.
		/// </param>
		/// <param name="p">
		/// The predicate to test the elements on.
		/// </param>
		/// <returns>
		/// A lazy generated list where all elements are satisfied by the predicate <paramref name="p"/>.
		/// </returns>
		public static IEnumerable<T> TakeWhile<T> (this IEnumerable<T> xs, Predicate<T> p) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			while (ie.MoveNext () && p (ie.Current)) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// Generates the suffix remaining after <see cref="EnumerableUtils.TakeWhile"/> performed on <paramref name="xs"/> and <paramref name="p"/>.
		/// </summary>
		/// <param name="xs">
		/// The list of elements to generate the conditional suffix from.
		/// </param>
		/// <param name="p">
		/// The predicate to test the elements on.
		/// </param>
		/// <returns>
		/// A lazy generated suffix where the first element is the first element of <paramref name="xs"/> who doesn't satisfies <paramref name="p"/>.
		/// </returns>
		public static IEnumerable<T> DropWhile<T> (this IEnumerable<T> xs, Predicate<T> p) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			bool nxt = ie.MoveNext ();
			while (nxt && p (ie.Current))
				nxt = ie.MoveNext ();
			while (nxt) {
				yield return ie.Current;
				nxt = ie.MoveNext ();
			}
		}

		/// <summary>
		/// Drops the largest suffix of the given list <paramref name="xs"/> in which the given predicate <paramref name="p"/> holds for all elements.
		/// </summary>
		/// <param name="xs">
		/// The given list to drop the suffix from.
		/// </param>
		/// <param name="p">
		/// The predicate that holds for all elements of the dropped suffix.
		/// </param>
		/// <returns>
		/// A lazy generated list containing all elements from <paramref name="xs"/> except the longest suffix of elements in which the given
		/// predicate <paramref name="p"/> holds.
		/// </returns>
		public static IEnumerable<T> DropWhileEnd<T> (this IEnumerable<T> xs, Predicate<T> p) {
			Queue<T> qx = new Queue<T> ();
			IEnumerator<T> ie = xs.GetEnumerator ();
			bool nxt = ie.MoveNext ();
			while (nxt) {
				while (qx.Count > 0x00) {
					yield return qx.Dequeue ();
				}
				if (!p (ie.Current)) {
					do {
						yield return ie.Current;
						nxt = ie.MoveNext ();
					} while(nxt && !p (ie.Current));
				}
				while (nxt && p (ie.Current)) {
					qx.Enqueue (ie.Current);
					nxt = ie.MoveNext ();
				}

			}
		}

		/// <summary>
		/// Applies a predicate to a list <paramref name="xs"/> and returns a tuple where the first element is the longest prefix (possibly empty)
		/// of <paramref name="xs"/> of elements that satisfy <paramref name="p"/> and second element is the remainder of the list.
		/// </summary>
		/// <param name="xs">
		/// The given list to span.
		/// </param>
		/// <param name="p">
		/// The predicate that holds for all the elements in the first list.
		/// </param>
		/// <returns>
		/// A tuple containing the longest prefix satisfying <paramref name="p"/> in the first element and the remainder of the list in the second.
		/// </returns>
		public static Tuple<IEnumerable<T>,IEnumerable<T>> Span<T> (this IEnumerable<T> xs, Predicate<T> p) {
			return new Tuple<IEnumerable<T>,IEnumerable<T>> (xs.TakeWhile (p), xs.DropWhile (p));
		}

		/// <summary>
		/// Applies a predicate to a list <paramref name="xs"/> and returns a tuple where the first element is the longest prefix (possibly empty)
		/// of <paramref name="xs"/> of elements that do not satisfy <paramref name="p"/> and second element is the remainder of the list.
		/// </summary>
		/// <param name="xs">
		/// The given list to span.
		/// </param>
		/// <param name="p">
		/// The predicate that does not holds for each element in the first list.
		/// </param>
		/// <returns>
		/// A tuple containing the longest prefix not satisfying <paramref name="p"/> in the first element and the remainder of the list in the second.
		/// </returns>
		public static Tuple<IEnumerable<T>,IEnumerable<T>> Break<T> (this IEnumerable<T> xs, Predicate<T> p) {
			return new Tuple<IEnumerable<T>,IEnumerable<T>> (xs.TakeWhile (a => !p (a)), xs.DropWhile (a => !p (a)));
		}

		/// <summary>
		/// Drops the given <paramref name="prefix"/> from the given <paramref name="list"/>. It returns Nothing if the list did not start with the
		/// <paramref name="prefix"/> given, or the list after the prefix encapsulated if it does.
		/// </summary>
		/// <param name="prefix">
		/// The given prefix to drop from the list.
		/// </param>
		/// <param name="list">
		/// The given list to drop the prefix from.
		/// </param>
		/// <returns>
		/// Nothing if the given <paramref name="list"/> does not contain the given <paramref name="prefix"/>, otherwise the list after the <paramref name="prefix"/>.
		/// </returns>
		public static Maybe<IEnumerable<T>> StripPrefix<T> (IEnumerable<T> prefix, IEnumerable<T> list) {
			IEnumerator<T> ie = list.GetEnumerator ();
			int i = 0;
			foreach (T px in prefix) {
				if (!ie.MoveNext () || px.Equals (ie.Current)) {
					return new Maybe<IEnumerable<T>> ();
				}
				i++;
			}
			return new Maybe<IEnumerable<T>> (list.Drop (i));
		}

		/// <summary>
		/// Takes the given <paramref name="list"/> and returns a list of list such that the concatenation of the result is equal to the argument. Moreover, each
		/// sublist in the result contains only equals elements.
		/// </summary>
		/// <param name="xs">
		/// The given list to partition in lists where all elements are equal.
		/// </param>
		/// <returns>
		/// A lazy generated list of lists, where the concatenation of the elements is the original list and all the elements are equal.
		/// </returns>
		/// <example>
		/// The grouping of <code>"Mississippi".Group();</code> is equal to <code>["M","i","ss","i","ss","i","pp","i"]</code>.
		/// </example>
		/// <remarks>
		/// This method is a special case of <see cref="EnumerableUtils.GroupBy"/> wich allows the programmer to supply their own equality test.
		/// </remarks>
		public static IEnumerable<IEnumerable<T>> Group<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			List<T> xl = new List<T> ();
			bool nxt = ie.MoveNext ();
			while (nxt) {
				T old = ie.Current;
				xl = new List<T> ();
				do {
					xl.Add (ie.Current);
					nxt = ie.MoveNext ();
				} while(nxt && old.Equals (ie.Current));
				yield return xl;
			}
		}

		/// <summary>
		/// Generates a list of all initial segments of the given list <paramref name="xs"/>, shortest first.
		/// </summary>
		/// <param name="xs">
		/// The given list to generate the initial segments from.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the prefix of the given list <paramref name="xs"/> with length <i>i</i>.
		/// </returns>
		/// <example>
		/// The initials of <code>"abc".Inits();</code> is equal to <code>["","a","ab","abc"]</code>.
		/// </example>
		public static IEnumerable<IEnumerable<T>> Inits<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			yield return new T[0x00];
			for (int i = 1; ie.MoveNext (); i++) {
				yield return xs.Take (i);
			}
		}

		/// <summary>
		/// Generates a list of all final segments of the given list <paramref name="xs"/>, longest first.
		/// </summary>
		/// <param name="xs">
		/// The given list to generate the final segments from.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the suffix of the given list <paramref name="xs"/> who starts at index <i>i</i>.
		/// </returns>
		/// <example>
		/// The initials of <code>"abc".Tails();</code> is equal to <code>["abc","bc","c",""]</code>.
		/// </example>
		public static IEnumerable<IEnumerable<T>> Tails<T> (this IEnumerable<T> xs) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			for (int i = 0; ie.MoveNext (); i++) {
				yield return xs.Drop (i);
			}
			yield return new T[0x00];
		}
		#endregion
		#region Predicates
		/// <summary>
		/// A method taking two lists and returns True iff the first list <paramref name="prefix"/> is a prefix of the second <paramref name="list"/>.
		/// </summary>
		/// <param name="prefix">
		/// The given prefix to test from.
		/// </param>
		/// <param name="list">
		/// The given list tot test te prefix on.
		/// </param>
		/// <returns>
		/// True if the given list <paramref name="prefix"/> is a prefix of the given list <paramref name="list"/>, otherwise False.
		/// </returns>
		public static bool IsPrefixOf<T> (IEnumerable<T> prefix, IEnumerable<T> list) {
			IEnumerator<T> ie = list.GetEnumerator ();
			foreach (T x in prefix) {
				if (!ie.MoveNext () || !x.Equals (ie.Current)) {
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// A function taking two lists and returns True iff the first list is a suffix of the second, both lists must be finite.
		/// </summary>
		/// <param name="suffix">
		/// The suffix to test for.
		/// </param>
		/// <param name="list">
		/// The given list where the suffix must be equal to the given suffix <paramref name="suffix"/>.
		/// </param>
		/// <returns>
		/// True if the given suffix <paramref name="suffix"/> is a suffix of the given list <paramref name="list"/>, otherwise False.
		/// </returns>
		public static bool IsSuffixOf<T> (IEnumerable<T> suffix, IEnumerable<T> list) {
			List<T> sfx = new List<T> (suffix);
			int i, f = sfx.Count;
			if (f == 0x00) {
				return true;
			}
			IEnumerator<T> ie = list.GetEnumerator ();

			T[] other = new T[f];
			bool full = false;
			do {
				i = 0x00;
				for (; i < f && ie.MoveNext (); i++) {
					other [i] = ie.Current;
				}
				full = true;
			} while(i >= f);
			if (full) {
				foreach (T x in sfx) {
					if (!x.Equals (other [i++])) {
						return false;
					}
					i %= f;
				}
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// A method taking two lists and returns True iff the first list is contained, wholly intact, anywhere within the second.
		/// </summary>
		/// <param name="infix">
		/// The sequence to check if it is contained by the given list <paramref name="list"/>.
		/// </param>
		/// <param name="list">
		/// The given list to check for an infix.
		/// </param>
		/// <returns>
		/// True if the given infix <paramref name="infix"/> is contained, wholly intact anywhere in <paramref name="list"/>, otherwise False.
		/// </returns>
		/// <example>
		/// The following two samples show the usage of the <c>IsInfixOf</c> method:
		/// <code>DataList.IsInfixOf("Mono","I really like Mono.") == true</code> and <code>DataList.IsInfixOf("IMn","I really like Mono.") == false</code>.
		/// </example>
		public static bool IsInfixOf<T> (IEnumerable<T> infix, IEnumerable<T> list) {
			List<T> w = new List<T> (infix);
			List<T> s = new List<T> (list);
			int wc = w.Count;
			int[] t = new int[wc];
			kmpTable (w, t);
			int i = 0x00, m = 0x00;
			while (m + i < s.Count) {
				if (w [i].Equals (s [m + i])) {
					i++;
					if (i >= wc - 1) {//check if this comparison is correct
						return true;
					}
				} else {
					m += i - t [i];
					i = Math.Max (0x00, t [i]);
				}
			}
			return false;
		}

		/// <summary>
		/// A method generating all the starting indices of the given infix of the list.
		/// </summary>
		/// <param name="infix">
		/// The given infix to test the to test.
		/// </param>
		/// <param name="list">
		/// The given list of elements where we want to search for the given <paramref name="infix"/>.
		/// </param>
		/// <returns>
		/// A lazy generated list where each integer is the startposition of the given <paramref name="infix"/> in the given <paramref name="list"/>.
		/// </returns>
		public static IEnumerable<int> InfixIndices<T> (IEnumerable<T> infix, IEnumerable<T> list) {
			List<T> w = new List<T> (infix);
			List<T> s = new List<T> (list);
			int wc = w.Count;
			int[] t = new int[wc];
			kmpTable (w, t);
			int i = 0x00, m = 0x00;
			while (m + i < s.Count) {
				if (w [i].Equals (s [m + i])) {
					if (i >= wc - 0x01) {
						yield return m;
						m += i - t [i];
						i = Math.Max (0x00, t [i]);
					} else {
						i++;
					}
				} else {
					m += i - t [i];
					i = Math.Max (0x00, t [i]);
				}
			}
		}

		private static void kmpTable<T> (List<T> w, int[] t) {
			int pos = 0x02;
			int cnd = 0x00;
			t [0x00] = -0x01;
			int wl = t.Length;
			while (pos < wl) {
				if (w [pos - 0x01].Equals (w [cnd])) {
					t [pos++] = ++cnd;
				} else if (cnd > 0x00) {
					cnd = t [cnd];
				} else {
					pos++;
				}
			}
		}
		#endregion
		#endregion
		#region SearchingLists
		#region SearchingByEquality
		/// <summary>
		/// The lest membership predicate. For the result to be False, the list must be finite; True, however, results from an element equal to
		/// <paramref name="x"/> found at a finite index of a finite or infinite list.
		/// </summary>
		/// <param name="xs">
		/// The given list to check membership on.
		/// </param>
		/// <param name="x">
		/// The element to search in the given list.
		/// </param>
		/// <returns>
		/// True if the given list <paramref name="xs"/> contains the given element <paramref name="x"/>, in case the element is not part of the list
		/// and the list is finite, this method will return False.
		/// </returns>
		public static bool Elem<T> (this IEnumerable<T> xs, T x) {
			foreach (T xi in xs) {
				if (xi.Equals (x)) {
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// The negation of <see cref="EnumerableUtils.Elem"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list to check membership on.
		/// </param>
		/// <param name="x">
		/// The element to search in the given list.
		/// </param>
		/// <returns>
		/// False if the give  list <paramref name="xs"/> contains the given element <paramref name="x"/>, in case the element is not part of the list
		/// and the list is finite, this method will return True.
		/// </returns>
		public static bool NotElem<T> (this IEnumerable<T> xs, T x) {
			return !Elem (xs, x);
		}
		#endregion
		/// <summary>
		/// Look up a key in association list.
		/// </summary>
		/// <param name="xys">
		/// The given list to look up a key.
		/// </param>
		/// <param name="x">
		/// The key to look up.
		/// </param>
		/// <returns>
		/// The corresponding value from the given tuple list <paramref name="xys"/> if the key exists. Otherwise Nothing is returned.
		/// </returns>
		public static Maybe<Y> Lookup<T,Y> (this IEnumerable<Tuple<T,Y>> xys, T x) {
			foreach (Tuple<T,Y> xy in xys) {
				if (x.Equals (xy.Item1)) {
					return new Maybe<Y> (xy.Item2);
				}
			}
			return new Maybe<Y> ();
		}
		#region SearchingWithAPredicate
		/// <summary>
		/// Takes a predicate <paramref name="p"/> and a list <paramref name="xs"/> and returns the first element in the list matching the predicate,
		/// or Nothing if there is no such element.
		/// </summary>
		/// <param name="xs">
		/// The given list to find the first matching element from.
		/// </param>
		/// <param name="p">
		/// The given predicate to test the elements on.
		/// </param>
		/// <returns>
		/// The first element in the list who satisfies the predicate <paramref name="p"/>. If such element doesn't exist, Nothing is returned.
		/// </returns>
		public static Maybe<T> Find<T> (this IEnumerable<T> xs, Predicate<T> p) {
			foreach (T x in xs) {
				if (p (x)) {
					return x;
				}
			}
			return new Maybe<T> ();
		}

		/// <summary>
		/// Generates a list of all the element of the given list <paramref name="xs"/> who satisfy the given predicate <paramref name="p"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements who must be filtered.
		/// </param>
		/// <param name="p">
		/// The given predicate to test the elements of the given list on.
		/// </param>
		/// <returns>
		/// A lazy generated list where all the elements are satisfying the given predicate <paramref name="p"/>. The order is the same as in the
		/// original list <paramref name="xs"/>.
		/// </returns>
		public static IEnumerable<T> Filter<T> (this IEnumerable<T> xs, Predicate<T> p) {
			foreach (T x in xs) {
				if (p (x)) {
					yield return x;
				}
			}
		}

		/// <summary>
		/// Generates a tuple where the first element is a list containing all the elements from the given list <paramref name="xs"/> who satisfy
		/// the given predicate <paramref name="p"/>, the second element is the list of elements who do not satisfy this predicate.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements who must be partitioned by the predicate.
		/// </param>
		/// <param name="p">
		/// The predicate who partitions the original list into two partitions.
		/// </param>
		/// <returns>
		/// A tuple where the first element is a list of all the elements from <paramref name="xs"/> who satisfy <paramref name="p"/>. The second element
		/// contains all elements who do not satisfy this predicate.
		/// </returns>
		public static Tuple<IEnumerable<T>,IEnumerable<T>> Partition<T> (this IEnumerable<T> xs, Predicate<T> p) {
			return new Tuple<IEnumerable<T>,IEnumerable<T>> (xs.Filter (p), xs.Filter (x => !p (x)));
		}
		#endregion
		#endregion
		#region IndexingLists
		/// <summary>
		/// List index (subscript) operator, starting from 0. It is an instance of the more general <see cref="M:GenericIndex`2"/> which takes an index of any
		/// integral type.
		/// </summary>
		/// <param name="xs">
		/// The given list elements where we want to extract the element at place <paramref name="index"/> from.
		/// </param>
		/// <param name="index">
		/// The given index of the element we want to extract.
		/// </param>
		/// <returns>
		/// The element from <paramref name="xs"/> at index <paramref name="index"/>.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException">If the index is smaller than zero, or larger or equal to the length of the given list <paramref name="xs"/>.</exception>
		public static T IndexOperator<T> (this IEnumerable<T> xs, int index) {
			if (index < 0x00) {
				throw new IndexOutOfRangeException ("Index must be larger or equal to zero.");
			}
			IEnumerator<T> ie = xs.GetEnumerator ();
			for (; index > 0x00 && ie.MoveNext (); index--)
				;
			if (index == 0x00) {
				return ie.Current;
			}
			throw new IndexOutOfRangeException ("Index must be smaller than the length of the list.");
		}

		/// <summary>
		/// Returns the index of the first element in the given list <paramref name="xs"/> which is equal to the query element <paramref name="x"/>,
		/// or Nothing if there is no such element.
		/// </summary>
		/// <param name="xs">
		/// The list to search for the index of the given element <paramref name="x"/>.
		/// </param>
		/// <param name="x">
		/// The element to look for in the given list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// The index of the first element in the given list <paramref name="xs"/> equal to the given element <paramref name="x"/>. Nothing if there is
		/// no such element.
		/// </returns>
		public static Maybe<int> ElemIndex<T> (this IEnumerable<T> xs, T x) {
			int index = 0x00;
			foreach (T xi in xs) {
				if (xi.Equals (x)) {
					return new Maybe<int> (index);
				}
				index++;
			}
			return new Maybe<int> ();
		}

		/// <summary>
		/// An extension of <see cref="M:EnumerableUtils.ElementIndex`1"/>, by returning the indices of all elements equal to the query element <paramref name="x"/>,
		/// in ascending order.
		/// </summary>
		/// <param name="xs">
		/// The list to search for the index of the given element <paramref name="x"/>.
		/// </param>
		/// <param name="x">
		/// The element to look for in the given list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// A lazy generated list of indices where the corresponding elements of the <paramref name="xs"/> are equal to <paramref name="x"/>.
		/// </returns>
		public static IEnumerable<int> ElemIndices<T> (this IEnumerable<T> xs, T x) {
			int index = 0x00;
			foreach (T xi in xs) {
				if (xi.Equals (x)) {
					yield return index;
				}
				index++;
			}
		}

		/// <summary>
		/// Takes a predicate <paramref name="p"/> and a list <paramref name="xs"/> and retursn the index of the first element in the list satisfying the predicate,
		/// or Nothing if there is no such element.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to search for an index.
		/// </param>
		/// <param name="p">
		/// The predicate to test the elements from the given list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// The first index of the first element of the given list <paramref name="xs"/> who satisfies the given predicate <paramref name="p"/>, or Nothing if such
		/// element doesn't exists.
		/// </returns>
		public static Maybe<int> FindIndex<T> (this IEnumerable<T> xs, Predicate<T> p) {
			int index = 0x00;
			foreach (T x in xs) {
				if (p (x)) {
					return new Maybe<int> (index);
				}
				index++;
			}
			return new Maybe<int> ();
		}

		/// <summary>
		/// An extension of <see cref="M:EnumerableUtils.FindIndex`1"/>, by returning the indices of all elements satisfying the given predicate <paramref name="p"/>, in
		/// ascending order.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to search for indices.
		/// </param>
		/// <param name="p">
		/// The predicate to test the elements from the given list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// A lazy generated list of all indices of <paramref name="xs"/> who match the given predicate <paramref name="p"/>.
		/// </returns>
		public static IEnumerable<int> FindIndices<T> (this IEnumerable<T> xs, Predicate<T> p) {
			int index = 0x00;
			foreach (T x in xs) {
				if (p (x)) {
					yield return index;
				}
				index++;
			}
		}
		#endregion
		#regionZippingAndUnzippingLists
		/// <summary>
		/// A method taking two lists and returns a list of corresponding pairs. If one input list is short, excess elements of the longer list are discarded.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T>> Zip<S,T> (this IEnumerable<S> ss, IEnumerable<T> ts) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext ()) {
				yield return new Tuple<S,T> (ies.Current, iet.Current);
			}
		}

		/// <summary>
		/// A method taking three lists and returns a list of corresponding triples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U>> Zip3<S,T,U> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext ()) {
				yield return new Tuple<S,T,U> (ies.Current, iet.Current, ieu.Current);
			}
		}

		/// <summary>
		/// A method taking four lists and returns a list of corresponding quadruples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <param name="vs">
		/// The fourth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U,V>> Zip4<S,T,U,V> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			IEnumerator<V> iev = vs.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext () && iev.MoveNext ()) {
				yield return new Tuple<S,T,U,V> (ies.Current, iet.Current, ieu.Current, iev.Current);
			}
		}

		/// <summary>
		/// A method taking five lists and returns a list of corresponding five-tuples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <param name="vs">
		/// The fourth list to zip.
		/// </param>
		/// <param name="ws">
		/// The fifth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U,V,W>> Zip5<S,T,U,V,W> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs, IEnumerable<W> ws) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			IEnumerator<V> iev = vs.GetEnumerator ();
			IEnumerator<W> iew = ws.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext () && iev.MoveNext () && iew.MoveNext ()) {
				yield return new Tuple<S,T,U,V,W> (ies.Current, iet.Current, ieu.Current, iev.Current, iew.Current);
			}
		}

		/// <summary>
		/// A method taking six lists and returns a list of corresponding six-tuples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <param name="vs">
		/// The fourth list to zip.
		/// </param>
		/// <param name="ws">
		/// The fifth list to zip.
		/// </param>
		/// <param name="xs">
		/// The sixth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U,V,W,X>> Zip6<S,T,U,V,W,X> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs, IEnumerable<W> ws, IEnumerable<X> xs) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			IEnumerator<V> iev = vs.GetEnumerator ();
			IEnumerator<W> iew = ws.GetEnumerator ();
			IEnumerator<X> iex = xs.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext () && iev.MoveNext () && iew.MoveNext () && iex.MoveNext ()) {
				yield return new Tuple<S,T,U,V,W,X> (ies.Current, iet.Current, ieu.Current, iev.Current, iew.Current, iex.Current);
			}
		}

		/// <summary>
		/// A method taking seven lists and returns a list of corresponding seven-tuples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <param name="vs">
		/// The fourth list to zip.
		/// </param>
		/// <param name="ws">
		/// The fifth list to zip.
		/// </param>
		/// <param name="xs">
		/// The sixth list to zip.
		/// </param>
		/// <param name="ys">
		/// The seventh list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U,V,W,X,Y>> Zip7<S,T,U,V,W,X,Y> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs, IEnumerable<W> ws, IEnumerable<X> xs, IEnumerable<Y> ys) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			IEnumerator<V> iev = vs.GetEnumerator ();
			IEnumerator<W> iew = ws.GetEnumerator ();
			IEnumerator<X> iex = xs.GetEnumerator ();
			IEnumerator<Y> iey = ys.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext () && iev.MoveNext () && iew.MoveNext () && iex.MoveNext () && iey.MoveNext ()) {
				yield return new Tuple<S,T,U,V,W,X,Y> (ies.Current, iet.Current, ieu.Current, iev.Current, iew.Current, iex.Current, iey.Current);
			}
		}

		/// <summary>
		/// A method taking eight lists and returns a list of corresponding eight-tuples, analogues to <see cref="EnumerableUtils.Zip"/>.
		/// </summary>
		/// <param name="ss">
		/// The first list to zip.
		/// </param>
		/// <param name="ts">
		/// The second list to zip.
		/// </param>
		/// <param name="us">
		/// The third list to zip.
		/// </param>
		/// <param name="vs">
		/// The fourth list to zip.
		/// </param>
		/// <param name="ws">
		/// The fifth list to zip.
		/// </param>
		/// <param name="xs">
		/// The sixth list to zip.
		/// </param>
		/// <param name="ys">
		/// The seventh list to zip.
		/// </param>
		/// <param name="zs">
		/// The eight list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list of tuples who contains a element of each list.
		/// </returns>
		public static IEnumerable<Tuple<S,T,U,V,W,X,Y,Z>> Zip8<S,T,U,V,W,X,Y,Z> (IEnumerable<S> ss, IEnumerable<T> ts, IEnumerable<U> us, IEnumerable<V> vs, IEnumerable<W> ws, IEnumerable<X> xs, IEnumerable<Y> ys, IEnumerable<Z> zs) {
			IEnumerator<S> ies = ss.GetEnumerator ();
			IEnumerator<T> iet = ts.GetEnumerator ();
			IEnumerator<U> ieu = us.GetEnumerator ();
			IEnumerator<V> iev = vs.GetEnumerator ();
			IEnumerator<W> iew = ws.GetEnumerator ();
			IEnumerator<X> iex = xs.GetEnumerator ();
			IEnumerator<Y> iey = ys.GetEnumerator ();
			IEnumerator<Z> iez = zs.GetEnumerator ();
			while (ies.MoveNext () && iet.MoveNext () && ieu.MoveNext () && iev.MoveNext () && iew.MoveNext () && iex.MoveNext () && iey.MoveNext () && iez.MoveNext ()) {
				yield return new Tuple<S,T,U,V,W,X,Y,Z> (ies.Current, iet.Current, ieu.Current, iev.Current, iew.Current, iex.Current, iey.Current, iez.Current);
			}
		}

		/// <summary>
		/// A generaliziation of <see cref="EnumerableUtils.Zip"/> by zipping with the function <paramref name="f"/> given as the first argument,
		/// instead of a tupling function.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the given function <paramref name="f"/> applied to the <i>i</i>-th elements of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith<A,B,C> (Func<B,C,A> f, IEnumerable<B> bs, IEnumerable<C> cs) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext ()) {
				yield return f (ieb.Current, iec.Current);
			}
		}

		/// <summary>
		/// Takes a function <paramref name="f"/> which combines three elements, as well as three lists and returns a list of their point-wise combination,
		/// analogous to <see cref="EnumerableUtils.ZipWith"/>.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <param name="ds">
		/// The third list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the point-wise combination of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith3<A,B,C,D> (Func<B,C,D,A> f, IEnumerable<B> bs, IEnumerable<C> cs, IEnumerable<D> ds) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			IEnumerator<D> ied = ds.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext () && ied.MoveNext ()) {
				yield return f (ieb.Current, iec.Current, ied.Current);
			}
		}

		/// <summary>
		/// Takes a function <paramref name="f"/> which combines four elements, as well as four lists and returns a list of their point-wise combination,
		/// analogous to <see cref="EnumerableUtils.ZipWith"/>.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <param name="ds">
		/// The third list to zip.
		/// </param>
		/// <param name="es">
		/// The fourth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the point-wise combination of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith4<A,B,C,D,E> (Func<B,C,D,E,A> f, IEnumerable<B> bs, IEnumerable<C> cs, IEnumerable<D> ds, IEnumerable<E> es) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			IEnumerator<D> ied = ds.GetEnumerator ();
			IEnumerator<E> iee = es.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext () && ied.MoveNext () && iee.MoveNext ()) {
				yield return f (ieb.Current, iec.Current, ied.Current, iee.Current);
			}
		}

		/// <summary>
		/// Takes a function <paramref name="f"/> which combines five elements, as well as five lists and returns a list of their point-wise combination,
		/// analogous to <see cref="EnumerableUtils.ZipWith"/>.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <param name="ds">
		/// The third list to zip.
		/// </param>
		/// <param name="es">
		/// The fourth list to zip.
		/// </param>
		/// <param name="fs">
		/// The fifth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the point-wise combination of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith5<A,B,C,D,E,F> (Func<B,C,D,E,F,A> f, IEnumerable<B> bs, IEnumerable<C> cs, IEnumerable<D> ds, IEnumerable<E> es, IEnumerable<F> fs) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			IEnumerator<D> ied = ds.GetEnumerator ();
			IEnumerator<E> iee = es.GetEnumerator ();
			IEnumerator<F> ief = fs.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext () && ied.MoveNext () && iee.MoveNext () && ief.MoveNext ()) {
				yield return f (ieb.Current, iec.Current, ied.Current, iee.Current, ief.Current);
			}
		}

		/// <summary>
		/// Takes a function <paramref name="f"/> which combines six elements, as well as six lists and returns a list of their point-wise combination,
		/// analogous to <see cref="EnumerableUtils.ZipWith"/>.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <param name="ds">
		/// The third list to zip.
		/// </param>
		/// <param name="es">
		/// The fourth list to zip.
		/// </param>
		/// <param name="fs">
		/// The fifth list to zip.
		/// </param>
		/// <param name="gs">
		/// The sixth list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the point-wise combination of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith6<A,B,C,D,E,F,G> (Func<B,C,D,E,F,G,A> f, IEnumerable<B> bs, IEnumerable<C> cs, IEnumerable<D> ds, IEnumerable<E> es, IEnumerable<F> fs, IEnumerable<G> gs) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			IEnumerator<D> ied = ds.GetEnumerator ();
			IEnumerator<E> iee = es.GetEnumerator ();
			IEnumerator<F> ief = fs.GetEnumerator ();
			IEnumerator<G> ieg = gs.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext () && ied.MoveNext () && iee.MoveNext () && ief.MoveNext () && ieg.MoveNext ()) {
				yield return f (ieb.Current, iec.Current, ied.Current, iee.Current, ief.Current, ieg.Current);
			}
		}

		/// <summary>
		/// Takes a function <paramref name="f"/> which combines seven elements, as well as seven lists and returns a list of their point-wise combination,
		/// analogous to <see cref="EnumerableUtils.ZipWith"/>.
		/// </summary>
		/// <param name="f">
		/// The function to perform on the items of the the given lists.
		/// </param>
		/// <param name="bs">
		/// The first list to zip.
		/// </param>
		/// <param name="cs">
		/// The second list to zip.
		/// </param>
		/// <param name="ds">
		/// The third list to zip.
		/// </param>
		/// <param name="es">
		/// The fourth list to zip.
		/// </param>
		/// <param name="fs">
		/// The fifth list to zip.
		/// </param>
		/// <param name="gs">
		/// The sixth list to zip.
		/// </param>
		/// <param name="hs">
		/// The seventh list to zip.
		/// </param>
		/// <returns>
		/// A lazy generated list where the <i>i</i>-th element is the point-wise combination of the given lists.
		/// </returns>
		public static IEnumerable<A> ZipWith7<A,B,C,D,E,F,G,H> (Func<B,C,D,E,F,G,H,A> f, IEnumerable<B> bs, IEnumerable<C> cs, IEnumerable<D> ds, IEnumerable<E> es, IEnumerable<F> fs, IEnumerable<G> gs, IEnumerable<H> hs) {
			IEnumerator<B> ieb = bs.GetEnumerator ();
			IEnumerator<C> iec = cs.GetEnumerator ();
			IEnumerator<D> ied = ds.GetEnumerator ();
			IEnumerator<E> iee = es.GetEnumerator ();
			IEnumerator<F> ief = fs.GetEnumerator ();
			IEnumerator<G> ieg = gs.GetEnumerator ();
			IEnumerator<H> ieh = hs.GetEnumerator ();
			while (ieb.MoveNext () && iec.MoveNext () && ied.MoveNext () && iee.MoveNext () && ief.MoveNext () && ieg.MoveNext () && ieh.MoveNext ()) {
				yield return f (ieb.Current, iec.Current, ied.Current, iee.Current, ief.Current, ieg.Current, ieh.Current);
			}
		}

		/// <summary>
		/// Transforms a list of pairs <paramref name="tuples"/> into a list of first components and a list of second components.
		/// </summary>
		/// <param name="tuples">
		/// The given list of pairs.
		/// </param>
		/// <returns>
		/// A tuple where the first item is a lazy generated list of the first components of the given list <paramref name="tuples"/>, the second item is a lazy generated list of the second components.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>> Unzip<S,T> (this IEnumerable<Tuple<S,T>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2));
		}

		/// <summary>
		/// Transforms a list of triples and returns three lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>> Unzip3<S,T,U> (this IEnumerable<Tuple<S,T,U>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3));
		}

		/// <summary>
		/// Transforms a list of quadruples and returns four lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>> Unzip4<S,T,U,V> (this IEnumerable<Tuple<S,T,U,V>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3),
				tuples.Map (a => a.Item4));
		}

		/// <summary>
		/// Transforms a list of five-tuples and returns five lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>> Unzip5<S,T,U,V,W> (this IEnumerable<Tuple<S,T,U,V,W>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3),
				tuples.Map (a => a.Item4),
				tuples.Map (a => a.Item5));
		}

		/// <summary>
		/// Transforms a list of six-tuples and returns six lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>> Unzip6<S,T,U,V,W,X> (this IEnumerable<Tuple<S,T,U,V,W,X>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3),
				tuples.Map (a => a.Item4),
				tuples.Map (a => a.Item5),
				tuples.Map (a => a.Item6));
		}

		/// <summary>
		/// Transforms a list of seven-tuples and returns seven lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>,IEnumerable<Y>> Unzip7<S,T,U,V,W,X,Y> (this IEnumerable<Tuple<S,T,U,V,W,X,Y>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>,IEnumerable<Y>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3),
				tuples.Map (a => a.Item4),
				tuples.Map (a => a.Item5),
				tuples.Map (a => a.Item6),
				tuples.Map (a => a.Item7));
		}

		/// <summary>
		/// Transforms a list of eight-tuples and returns eight lists, analoguous to <see cref="EnumerableUtils.Unzip"/>.
		/// </summary>
		/// <param name="tuples">
		/// The given list of tuples.
		/// </param>
		/// <returns>
		/// A tuple of lazy generated lists, where the <i>i</i>-th item is a lazy generated list containing the <i>i</i>-th components of the given list <paramref name="tuples"/>.
		/// </returns>
		public static Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>,IEnumerable<Y>,IEnumerable<Z>> Unzip8<S,T,U,V,W,X,Y,Z> (this IEnumerable<Tuple<S,T,U,V,W,X,Y,Z>> tuples) {
			return new Tuple<IEnumerable<S>,IEnumerable<T>,IEnumerable<U>,IEnumerable<V>,IEnumerable<W>,IEnumerable<X>,IEnumerable<Y>,IEnumerable<Z>> (
				tuples.Map (a => a.Item1),
				tuples.Map (a => a.Item2),
				tuples.Map (a => a.Item3),
				tuples.Map (a => a.Item4),
				tuples.Map (a => a.Item5),
				tuples.Map (a => a.Item6),
				tuples.Map (a => a.Item7),
				tuples.Map (a => a.Rest));
		}
		#endregion
		#region SpecialLists
		#region FunctionOnStrings
		/// <summary>
		/// Breaks a string up int a list of strings at newline characters. The resulting strings do not contain newlines.
		/// </summary>
		/// <param name="text">
		/// The text to split the string into a list of lines.
		/// </param>
		/// <returns>
		/// A lazy generated list of strings where between each two successing string, their was a newline string.
		/// </returns>
		public static IEnumerable<string> Lines (this IEnumerable<char> text) {
			StringBuilder sb = new StringBuilder ();
			IEnumerable<int> indices = InfixIndices (Environment.NewLine, text);
			int last = 0, cur, nnl = Environment.NewLine.Length;
			IEnumerator<char> ie = text.GetEnumerator ();
			foreach (int index in indices) {
				for (; last < index && ie.MoveNext (); last++) {
					sb.Append (ie.Current);
				}
				yield return sb.ToString ();
				sb.Clear ();
				cur = last;
				last += nnl;
				for (; cur < last && ie.MoveNext (); cur++)
					;
			}
			if (ie.MoveNext ()) {
				do {
					sb.Append (ie.Current);
				} while(ie.MoveNext ());
				yield return sb.ToString ();
			}

		}

		/// <summary>
		/// Breaks a string up into a list of words, which were delimited by white space.
		/// </summary>
		/// <param name="text">
		/// The given text to break up.
		/// </param>
		/// <returns>
		/// A lazy generated list of words.
		/// </returns>
		public static IEnumerable<string> Words (this IEnumerable<char> text) {
			StringBuilder sb = new StringBuilder ();
			foreach (char c in text) {
				if (char.IsWhiteSpace (c)) {
					if (sb.Length > 0) {
						yield return sb.ToString ();
						sb.Clear ();
					}
				} else {
					sb.Append (c);
				}
			}
		}

		/// <summary>
		/// The inverse operation of <see cref="EnumerableUtils.Lines"/>. It joins lines, after appending a terminating newline to each.
		/// </summary>
		/// <param name="lines">
		/// The list of strings who must be joint into a string.
		/// </param>
		/// <returns>
		/// A lazy generated string who joins the given list of strings <paramref name="lines"/> and appends a terminating newline after each string.
		/// </returns>
		public static IEnumerable<char> Unlines (this IEnumerable<IEnumerable<char>> lines) {
			foreach (IEnumerable<char> ie in lines) {
				foreach (char c in ie) {
					yield return c;
				}
				foreach (char c in Environment.NewLine) {
					yield return c;
				}
			}
		}

		/// <summary>
		/// The inverse operation of <see cref="EnumerableUtils.Words"/> it joins words with separating spaces.
		/// </summary>
		/// <param name="words">
		/// The list of words to join into the new string.
		/// </param>
		/// <returns>
		/// A lazy generated string who joins the given words <paramref name="words"/> with separating spaces.
		/// </returns>
		public static IEnumerable<char> Unwords (IEnumerable<IEnumerable<char>> words) {
			IEnumerator<IEnumerable<char>> ie = words.GetEnumerator ();
			if (ie.MoveNext ()) {
				foreach (char c in ie.Current) {
					yield return c;
				}
				while (ie.MoveNext ()) {
					yield return ' ';
					foreach (char c in ie.Current) {
						yield return c;
					}
				}
			}
		}
		#endregion
		#region SetOperations
		/// <summary>
		/// This function removes duplicate elements from a list. In particular, it keeps only the first occurrence of
		/// each element (the name "nub" means 'essence').
		/// </summary>
		/// <param name="xs">
		/// The given list of elements.
		/// </param>
		/// <returns>
		/// A lazy generated list of elements where each element will only occur once.
		/// </returns>
		/// <remarks>
		/// <para>
		/// It is a special case of <see cref="EnumerableUtils.NubBy"/> which allows the programmer to supply their own equality test.
		/// </para><para>
		/// The time complexity of this function is <i>O(n^2)</i> with <i>n</i> the length of the original list.
		/// </para>
		/// </remarks>
		public static IEnumerable<T> Nub<T> (this IEnumerable<T> xs) {
			HashSet<T> hs = new HashSet<T> ();
			foreach (T x in xs) {
				if (hs.Add (x)) {
					yield return x;
				}
			}
		}

		/// <summary>
		/// Removes the first occurence of <paramref name="x"/> from the list argument <paramref name="xs"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to delete the first occurence of <paramref name="x"/> from.
		/// </param>
		/// <param name="x">
		/// The given element to remove from <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// A lazy generated list where the first occurence of <paramref name="x"/> from the given list <paramref name="xs"/> is removed.
		/// </returns>
		/// <example>
		/// When we delete <code>'a'</code> from <code>"banana"</code> this results in <code>"banana".Delete('a').ToArray() == "bnana";</code>.
		/// </example>
		public static IEnumerable<T> Delete<T> (this IEnumerable<T> xs, T x) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			while (ie.MoveNext () && !x.Equals (ie.Current)) {
				yield return ie.Current;
			}
			while (ie.MoveNext ()) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// The list difference (non-associative). In Haskell denoted by <c>\\</c>. The first occurence of each element of <paramref name="ys"/>
		/// in turn (if any) has been removed from <paramref name="xs"/>.
		/// </summary>
		/// <param name="xs">
		/// The original given list where we want to delete all the first occurences in <paramref name="ys"/> from.
		/// </param>
		/// <param name="ys">
		/// The list of elements we want to delete from the first list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// A lazy generated list with all the elements of <paramref name="xs"/> except the first occurences of elements who are in <paramref name="ys"/>.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The following code will simly return <c>ys</c> again:<code>DataList.Difference(DataList.Append(xs,ys),xs) == ys.</code>
		/// </para><para>
		/// The second list <paramref name="ys"/> must be finite.
		/// </para>
		/// </remarks>
		public static IEnumerable<T> Difference<T> (IEnumerable<T> xs, IEnumerable<T> ys) {
			HashSet<T> toRemove = new HashSet<T> (ys);
			foreach (T x in xs) {
				if (!toRemove.Remove (x)) {
					yield return x;
				}
			}
		}

		/// <summary>
		/// The union function returns the list union of two lists. Duplicates, and elements of the first list, are removed from the second list, but
		/// if the first list contains duplicates, so will the result. It is a special case of <see cref="EnumerableUtils.UnionBy"/> which allows the programmer
		/// to supply their own equality test.
		/// </summary>
		/// <param name="xs">
		/// The first list to calculate the union from.
		/// </param>
		/// <param name="ys">
		/// The second list to calculate the uinion from.
		/// </param>
		/// <returns>
		/// A lazy generated list who starts with all elements in <paramref name="xs"/> (duplicates included), followed by the second list <paramref name="ys"/>
		/// where the elements of the first list together with optional duplicates are removed.
		/// </returns>
		/// <remarks>
		/// Both lists can be empty or infinite.
		/// </remarks>
		/// <example>
		/// <code>DataList.Union("dog","cow").ToArray();</code> will return the union and thus: <c>"dogcw"</c>.
		/// </example>
		public static IEnumerable<T> Union<T> (IEnumerable<T> xs, IEnumerable<T> ys) {
			HashSet<T> hs = new HashSet<T> ();
			foreach (T x in xs) {
				yield return x;
				hs.Add (x);
			}
			foreach (T y in ys) {
				if (hs.Add (y)) {
					yield return y;
				}
			}
		}

		/// <summary>
		/// Takes the list intersection of two lists. If the first list contains duplicates, so will the result. It is a special case of <see cref="EnumerableUtils.IntersectBy"/>,
		/// which allows the programmer to supply their own equality test.
		/// </summary>
		/// <param name="xs">
		/// The first list of the intersection.
		/// </param>
		/// <param name="ys">
		/// The second list of the intersection.
		/// </param>
		/// <returns>
		/// A lazy generated list containing all the elements of <paramref name="xs"/> who are also part of <paramref name="ys"/>.
		/// </returns>
		/// <remarks>
		/// The first list is allowed to be infinite or empty. The second list can be empty. Only if all the elements in the first list are members of the second list,
		/// the second list can be infinite (this makes the intersection of course quite useless).
		/// </remarks>
		/// <example>
		/// If we intersect <c>[1,2,3,4]</c> with <c>[2,4,6,8]</c>, the result of <code>DataList.Intersect(new int[] {1,2,3,4},new int[2,4,6,8]);</code> is <c>[2,4]</c>.
		/// However if we introduce a duplicate in the first list, this duplicate will also be enumerated: the result of
		/// <code>DataList.Intersect(new int[] {1,2,2,3,4},new int[2,4,6,8]);</code> is thus <c>[2,2,4]</c>.
		/// </example>
		public static IEnumerable<T> Intersect<T> (IEnumerable<T> xs, IEnumerable<T> ys) {
			HashSet<T> hs = new HashSet<T> ();
			IEnumerator<T> ie = ys.GetEnumerator ();
			foreach (T x in xs) {
				if (!hs.Contains (x)) {
					while (ie.MoveNext ()) {
						T t = ie.Current;
						if (t.Equals (x)) {
							yield return x;
							hs.Add (t);
							break;
						} else {
							hs.Add (t);
						}
					}
				} else {
					yield return x;
				}
			}
		}

		/// <summary>
		/// This method implements a stable sorting algorithm. It is a special case of <see cref="EnumerableUtils.SortBy"/>, which
		/// allows the programmer to supply their own comparison function.
		/// </summary>
		/// <param name="list">
		/// The list to be sorted.
		/// </param>
		/// <returns>
		/// A list containing all the elements of the given list <paramref name="list"/> but the elements are ordered according
		/// to their built-in comparison function. If two objects are equal, they remain in the same order.
		/// </returns>
		public static IEnumerable<T> Sort<T> (this IEnumerable<T> list) where T : IComparable<T> {
			List<T> la = new List<T> (list);
			List<T> lb = new List<T> (list);
			int n = la.Count;
			innerMergeSort (la, lb, 0x00, n);
			return lb;
		}

		private static void innerMergeSort<T> (List<T> read, List<T> write, int frm, int to) where T : IComparable<T> {
			if (to - frm > 0x02) {
				int mid = (to + frm) >> 0x01;
				innerMergeSort (write, read, frm, mid);
				innerMergeSort (write, read, mid, to);
				int i = frm, j = mid, k = frm;
				for (; i < mid && j < to;) {
					if (read [i].CompareTo (read [j]) <= 0x00) {
						write [k++] = read [i++];
					} else {
						write [k++] = read [j++];
					}
				}
				for (; i < mid;) {
					write [k++] = read [i++];
				}
				for (; j < to;) {
					write [k++] = read [j++];
				}

			} else if (to - frm >= 0x01) {
				if (read [frm].CompareTo (read [to - 1]) > 0x00) {
					write [frm] = read [to - 1];
					write [to - 1] = read [frm];
				}
			}
		}
		#endregion
		#region OrderedLists
		/// <summary>
		/// The Insert method takes an element <paramref name="x"/> and a list <paramref name="xs"/> and inserts the element into the list at the last position
		/// where it is still less than or equal to the next element. In particular, if the list is sorted before the call, the result will also be sorted. It is
		/// a special case of <see cref="EnumerableUtils.InsertBy"/>, which allows the programmer to supply their own comparison function.
		/// </summary>
		/// <param name="xs">
		/// The given list of items to insert the given element in.
		/// </param>
		/// <param name="x">
		/// The given element to insert in the list.
		/// </param>
		/// <returns>
		/// A lazy generated list where the given element <paramref name="x"/> is inserted at the last possible position in <paramref name="xs"/> where each next
		/// element is larger or equal to each next element.
		/// </returns>
		public static IEnumerable<T> Insert<T> (this IEnumerable<T> xs, T x) where T : IComparable<T> {
			IEnumerator<T> ie = xs.GetEnumerator ();
			bool nxt = ie.MoveNext ();
			while (nxt && ie.Current != null && ie.Current.CompareTo (x) <= 0x00) {
				yield return ie.Current;
				nxt = ie.MoveNext ();
			}
			yield return x;
			if (nxt) {
				do {
					yield return ie.Current;
				} while(ie.MoveNext ());
			}
		}
		#endregion
		#endregion
		#region GeneralizedFunctions
		#region TheByOperations
		#region UserSuppliedEqualityReplacingTheOrdContext
		/// <summary>
		/// A method behaving just like <see cref="EnumerableUtils.Nub"/>, except it uses a user-supplied equality predicate instead of the
		/// overloaded <see cref="Object.Equals"/> function.
		/// </summary>
		/// <param name="xs">
		/// The given list to sort out the duplicates.
		/// </param>
		/// <param name="equalf">
		/// The given predicate to check if two objects are equal. The predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <returns>
		/// A lazy generated list containing all the given elements without the duplicates.
		/// </returns>
		public static IEnumerable<T> NubBy<T> (this IEnumerable<T> xs, EqualityFunction<T> equalf) {
			List<T> res = new List<T> ();
			foreach (T x in xs) {
				bool newv = true;
				foreach (T x2 in res) {
					if (equalf (x, x2)) {
						newv = false;
						break;
					}
				}
				if (newv) {
					yield return x;
					res.Add (x);
				}
			}
		}

		/// <summary>
		/// A method behaving like <see cref="EnumerableUtils.Delete"/>, but takes a user-supplied equality predicate.
		/// </summary>
		/// <param name="xs">
		/// The given list to delete the first occurence of <paramref name="x"/> from.
		/// </param>
		/// <param name="equalf">
		/// The given predicate to check if two objects are equal. The predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <param name="x">
		/// The given object to delete the first occurence of.
		/// </param>
		/// <returns>
		/// A lazy generated list with all the elements of the given list <paramref name="xs"/> with the first occurence of <paramref name="x"/>.
		/// missing.
		/// </returns>
		public static IEnumerable<T> DeleteBy<T> (this IEnumerable<T> xs, EqualityFunction<T> equalf, T x) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			while (ie.MoveNext () && !equalf (x, ie.Current)) {
				yield return ie.Current;
			}
			while (ie.MoveNext ()) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// A method taking a predicate and two lists and returns the first list with the first occurence of each element of the second list removed.
		/// </summary>
		/// <param name="xs">
		/// The given list to remove the first occurences of the elements from <paramref name="deletelist"/>.
		/// </param>
		/// <param name="equalf">
		/// The given predicate to check if two objects are equal. The predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <param name="deletelist">
		/// A list of items to remove the first occurences of the first list <paramref name="xs"/> from.
		/// </param>
		/// <returns>
		/// A lazy generated list with all the elements of the given list <paramref name="xs"/> without the first occurences of all element from <paramref name="deletelist"/>.
		/// </returns>
		public static IEnumerable<T> DeleteFirstsBy<T> (this IEnumerable<T> xs, EqualityFunction<T> equalf, IEnumerable<T> deletelist) {
			LinkedList<T> ll = new LinkedList<T> (deletelist);
			foreach (T x in xs) {
				LinkedListNode<T> llf = ll.First;
				bool allowed = true;
				while (llf != null) {
					if (equalf (x, llf.Value)) {
						ll.Remove (llf);
						allowed = false;
						break;
					}
					llf = llf.Next;
				}
				if (allowed) {
					yield return x;
				}
			}
		}

		/// <summary>
		/// The UnionBy function is the non-overloaded version of <see cref="EnumerableUtils.Union"/>.
		/// </summary>
		/// <param name="xs">
		/// The first list to calculate the union from.
		/// </param>
		/// <param name="equalf">
		/// The equality predicate to test the equality of two objects. The predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <param name="ys">
		/// The second list to calculate the union from.
		/// </param>
		/// <returns>
		/// A lazy generated list containing all the elements of the first list <paramref name="xs"/> followed by the second list <paramref name="ys"/> without any
		/// duplicates or elements who are part of the first list <paramref name="xs"/>.
		/// </returns>
		public static IEnumerable<T> UnionBy<T> (IEnumerable<T> xs, EqualityFunction<T> equalf, IEnumerable<T> ys) {
			List<T> ll = new List<T> ();
			foreach (T x in xs) {
				yield return x;
				bool newv = true;
				foreach (T x2 in ll) {
					if (equalf (x, x2)) {
						newv = false;
						break;
					}
				}
				if (newv) {
					ll.Add (x);
				}
			}
			foreach (T x in ys) {
				bool newv = true;
				foreach (T x2 in ll) {
					if (equalf (x, x2)) {
						newv = false;
						break;
					}
				}
				if (newv) {
					yield return x;
					ll.Add (x);
				}
			}
		}

		/// <summary>
		/// The IntersectBy function is the non-overloaded version of <see cref="Intersect"/>.
		/// </summary>
		/// <param name="xs">
		/// The first list to calculate the intersection from.
		/// </param>
		/// <param name="equalf">
		/// The equality predicate to test the equality between two objects. This predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <param name="ys">
		/// The second list to calculate the intersection from.
		/// </param>
		/// <returns>
		/// A lazy generated list who enumerates elements from the first list, if they are also part of the second list.
		/// </returns>
		public static IEnumerable<T> IntersectBy<T> (IEnumerable<T> xs, EqualityFunction<T> equalf, IEnumerable<T> ys) {
			IEnumerator<T> ie = ys.GetEnumerator ();
			List<T> ll = new List<T> ();
			foreach (T x in xs) {
				bool yscontains = false;
				foreach (T x2 in ll) {
					if (equalf (x, x2)) {
						yscontains = true;
						break;
					}
				}
				if (!yscontains) {
					while (ie.MoveNext ()) {
						if (equalf (x, ie.Current)) {
							yield return x;
							ll.Add (ie.Current);
							break;
						} else {
							ll.Add (ie.Current);
						}
					}
				} else {
					yield return x;
				}
			}
		}

		/// <summary>
		/// The GroupBy function is the non-overloaded version of <see cref="EnumerableUtils.Group"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list to group into equality partitions.
		/// </param>
		/// <param name="equalf">
		/// The equality predicate to test the equality between two objects. This predicate must be reflexive, symmetric and transitive.
		/// </param>
		/// <returns>
		/// A lazy generated list of elements where elements in the same item are equal to each other. Furthermore concatenating the groups
		/// results in the original list.
		/// </returns>
		public static IEnumerable<IEnumerable<T>> GroupBy<T> (this IEnumerable<T> xs, EqualityFunction<T> equalf) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			List<T> xl = new List<T> ();
			bool nxt = ie.MoveNext ();
			while (nxt) {
				T old = ie.Current;
				xl = new List<T> ();
				do {
					xl.Add (ie.Current);
					nxt = ie.MoveNext ();
				} while(nxt && equalf (old, ie.Current));
				yield return xl;
			}
		}
		#endregion
		#region UserSuppliedComparisonReplacingAnOrdContext
		/// <summary>
		/// The non-overloaded version of <see cref="EnumerableUtils.Sort"/>.
		/// </summary>
		/// <param name="list">
		/// The list of elements to be sorted.
		/// </param>
		/// <param name="ord">
		/// The order function who determines the order of the list. This function is reflexive with EQ output,
		/// and must be transitive under LT and GT.
		/// </param>
		/// <returns>
		/// A list containing all the elements of the given list <paramref name="list"/> but the elements are ordered according
		/// to the given comparison function <paramref name="ord"/>. If two objects are equal, they remain in the same order.
		/// </returns>
		public static IEnumerable<T> SortBy<T> (this IEnumerable<T> list, IOrd<T> ord) where T : IComparable<T> {
			List<T> la = new List<T> (list);
			List<T> lb = new List<T> (list);
			int n = la.Count;
			innerMergeSortBy (ord, la, lb, 0x00, n);
			return lb;
		}

		private static void innerMergeSortBy<T> (IOrd<T> ord, List<T> read, List<T> write, int frm, int to) where T : IComparable<T> {
			if (to - frm > 0x02) {
				int mid = (to + frm) >> 0x01;
				innerMergeSort (write, read, frm, mid);
				innerMergeSort (write, read, mid, to);
				int i = frm, j = mid, k = frm;
				for (; i < mid && j < to;) {
					if (ord.Compare (read [i], read [j]) == Ordering.GT) {
						write [k++] = read [j++];
					} else {
						write [k++] = read [i++];
					}
				}
				for (; i < mid;) {
					write [k++] = read [i++];
				}
				for (; j < to;) {
					write [k++] = read [j++];
				}

			} else if (to - frm >= 0x01) {
				if (ord.Compare (read [frm], read [to - 1]) == Ordering.GT) {
					write [frm] = read [to - 1];
					write [to - 1] = read [frm];
				}
			}
		}

		/// <summary>
		/// The non-overloaded version of <see cref="EnumerableUtils.Insert"/>.
		/// </summary>
		/// <param name="xs">
		/// The given list to insert the item in.
		/// </param>
		/// <param name="ord">
		/// The order function who determines the order of the list. This function is reflexive with EQ output,
		/// and must be transitive under LT and GT.
		/// </param>
		/// <param name="x">
		/// The item to insert in the resulting list.
		/// </param>
		/// <returns>
		/// A lazy generated list containing all the elements from <paramref name="xs"/> with the given item <paramref name="x"/> at the right
		/// place according to <paramref name="ord"/>.
		/// </returns>
		public static IEnumerable<T> InsertBy<T> (this IEnumerable<T> xs, IOrd<T> ord, T x) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			bool nxt = ie.MoveNext ();
			while (nxt && ord.Compare (ie.Current, x) != Ordering.LT) {
				yield return ie.Current;
				nxt = ie.MoveNext ();
			}
			yield return x;
			if (nxt) {
				do {
					yield return ie.Current;
				} while(ie.MoveNext ());
			}
		}

		/// <summary>
		/// The MaximumBy function takes a comparison function and a list and returns the greatest element of the list by the comparison function.
		/// The list must be finite and non-empty.
		/// </summary>
		/// <param name="xs">
		/// The list of elements to calculate the maximum from.
		/// </param>
		/// <param name="ord">
		/// A function determining the order between two objects.
		/// </param>
		/// <returns>
		/// The maximum element of the given list <paramref name="xs"/> by the given ordering function <paramref name="ord"/>.
		/// </returns>
		public static T MaximumBy<T> (this IEnumerable<T> xs, IOrd<T> ord) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			ie.MoveNext ();
			T x = ie.Current;
			while (ie.MoveNext ()) {
				if (ord.Compare (ie.Current, x) == Ordering.GT) {
					x = ie.Current;
				}
			}
			return x;
		}

		/// <summary>
		/// The MinimumBy function takes a comparison function and a list and returns the least element of the list by the comparison function.
		/// The list must be finite and non-empty.
		/// </summary>
		/// <param name="xs">
		/// The list of elements to calculate the minimum from.
		/// </param>
		/// <param name="ord">
		/// A function determining the order between two objects.
		/// </param>
		/// <returns>
		/// The minimum element of the given list <paramref name="xs"/> by the given ordering function <paramref name="ord"/>.
		/// </returns>
		public static T MinimumBy<T> (this IEnumerable<T> xs, IOrd<T> ord) {
			IEnumerator<T> ie = xs.GetEnumerator ();
			ie.MoveNext ();
			T x = ie.Current;
			while (ie.MoveNext ()) {
				if (ord.Compare (ie.Current, x) == Ordering.LT) {
					x = ie.Current;
				}
			}
			return x;
		}
		#endregion
		#endregion
		#region TheGenericOperations
		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.Length"/>. In particular, instead of returning an <see cref="int"/>,
		/// it returns any type which is an instance of <see cref="INumeric"/>. It is, however, less efficient than length.
		/// </summary>
		/// <param name="xs">
		/// The list of elements to calculate the length from.
		/// </param>
		/// <param name="number">
		/// An instance of the type of number the programmer want to be returned (for instance <see cref="BigInteger"/>).
		/// </param>
		/// <returns>
		/// A numeric type who stores the length of the given list.
		/// </returns>
		public static I GenericLength<I,T> (this IEnumerable<T> xs, I number) where I : INumeric<I> {
			I len = number.Zero;
			foreach (T x in xs) {
				len = len.Add (len, number.Zero);
			}
			return len;
		}

		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.Take"/>, which accepts a <see cref="IIntegral{I}"/> value as the number
		/// of elements to take.
		/// </summary>
		/// <param name="xs">
		/// The original list of elements to generate a sublist from.
		/// </param>
		/// <param name="integer">
		/// The number of items to take.
		/// </param>
		/// <returns>
		/// A lazy generated list containing the first <paramref name="integer"/> elements of the given list <paramref name="xs"/>.
		/// </returns>
		public static IEnumerable<T> GenericTake<T,I> (this IEnumerable<T> xs, I integer) where I : IIntegral<I> {
			IEnumerator<T> ie = xs.GetEnumerator ();
			while (ie.MoveNext () && integer.Compare (integer, integer.Zero) > 0x00) {
				yield return ie.Current;
				integer = integer.Subtract (integer, integer.One);
			}
		}

		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.Drop"/>, which accepts a <see cref="IIntegral{I}"/> value as the number
		/// of elements to take.
		/// </summary>
		/// <param name="xs">
		/// The original list of elements to generate a sublist from.
		/// </param>
		/// <param name="integer">
		/// The number of elements to drop.
		/// </param>
		/// <returns>
		/// A lazy generated list containing the element from the given list <paramref name="xs"/> after the first <paramref name="integer"/> items.
		/// </returns>
		public static IEnumerable<T> GenericDrop<T,I> (this IEnumerable<T> xs, I integer) where I : IIntegral<I> {
			IEnumerator<T> ie = xs.GetEnumerator ();
			while (ie.MoveNext () && integer.Compare (integer, integer.Zero) > 0x00) {
				integer = integer.Subtract (integer, integer.One);
			}
			while (ie.MoveNext ()) {
				yield return ie.Current;
			}
		}

		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.SplitAt"/>, which accepts any <see cref="IIntegral{I}"/> value as the position
		/// at which to split.
		/// </summary>
		/// <param name="xs">
		/// The given list to generate two sublists from.
		/// </param>
		/// <param name="integer">
		/// The position where to split the given list into two pieces.
		/// </param>
		/// <returns>
		/// A tuple where the first item is a lazy generated list containg the first <paramref name="integer"/> elements, the second item is the rest of the list.
		/// </returns>
		public static Tuple<IEnumerable<T>,IEnumerable<T>> GenericSplitAt<T,I> (this IEnumerable<T> xs, I integer) where I : IIntegral<I> {
			return new Tuple<IEnumerable<T>,IEnumerable<T>> (xs.GenericTake (integer), xs.GenericDrop (integer));
		}

		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.IndexOperator"/>. Which accepts any <see cref="IIntegral{I}"/> value as the position at which to split.
		/// </summary>
		/// <param name="xs">
		/// The given list of elements to extract the <paramref name="integer"/>-th element from.
		/// </param>
		/// <param name="integer">
		/// The position of the element to extract from the given list <paramref name="xs"/>.
		/// </param>
		/// <returns>
		/// The <i>i</i>-th element from the given list <paramref name="xs"/> where <i>i</i> is the value of the given integer <paramref name="integer"/>.
		/// </returns>
		/// <exception cref="IndexOutOfRangeException">If the index is smaller than zero, or larger or equal to the length of the given list <paramref name="xs"/>.</exception>
		public static T GenericIndex<T,I> (this IEnumerable<T> xs, I integer) where I : IIntegral<I> {
			if (integer.Compare (integer, integer.Zero) < 0x00) {
				throw new IndexOutOfRangeException ("The index must be larger or equal to zero.");
			} else {
				IEnumerator<T> ie = xs.GetEnumerator ();
				while (ie.MoveNext () && integer.Compare (integer, integer.Zero) > 0x00) {
					integer = integer.Subtract (integer, integer.One);
				}
				if (integer.Compare (integer, integer.Zero) <= 0x00) {
					return ie.Current;
				} else {
					throw new IndexOutOfRangeException ("The index must be smaller than the length of the given list.");
				}
			}
		}

		/// <summary>
		/// The overloaded version of <see cref="EnumerableUtils.Replicate"/>, which accepts any <see cref="IIntegral{I}"/> value as
		/// the number of repititions to make.
		/// </summary>
		/// <param name="integer">
		/// The number of replications to make.
		/// </param>
		/// <param name="x">
		/// The element to generate replications from.
		/// </param>
		/// <returns>
		/// A lazy generated list containing <paramref name="integer"/> copies of the given value <paramref name="x"/>.
		/// </returns>
		public static IEnumerable<T> GenericReplicate<T,I> (I integer, T x) where I : IIntegral<I> {
			for (; integer.Compare (integer, integer.Zero) > 0x00; integer.Subtract (integer, integer.One)) {
				yield return x;
			}
		}
		#endregion
		#endregion
		#endregion
	}
}