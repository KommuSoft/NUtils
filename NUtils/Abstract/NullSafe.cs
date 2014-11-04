//
//  NullSafe.cs
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
using System.Linq;
using System.Diagnostics.Contracts;
using System.Collections.Generic;
using NUtils.Functional;

namespace NUtils.Abstract {

	/// <summary>
	/// A utility library that can be used to make null checks way easier to handle.
	/// </summary>
	public static class NullSafe {

		/// <summary>
		/// Invokes the given <paramref name="function"/> with the given <paramref name="data"/> safely:
		/// if the given data is <c>null</c>, null is returned as well, otherwise the function is invoked.
		/// </summary>
		/// <returns>The result of the function invocation if the the given data is effective, <c>null</c> otherwise.</returns>
		/// <param name="data">The given data to check and invoke the <paramref name="function"/> with.</param>
		/// <param name="function">The given function to invoke.</param>
		/// <typeparam name="TX">The type of data that is provided.</typeparam>
		/// <typeparam name="TResult">The type of the result of the function: the type of data that must be returned.</typeparam>
		/// <remarks>
		/// <para>For performance issues, the <paramref name="function"/> is assumed to be effective, no check is done.</para>
		/// </remarks>
		public static TResult OrNull <TX,TResult> (this TX data, Func<TX,TResult> function)
			where TX : class
			where TResult : class {
			Contract.Requires (function != null);
			if (data != null) {
				return function (data);
			} else {
				return null;
			}
		}

		/// <summary>
		/// Invokes the given <paramref name="predicate"/> with the given <paramref name="data"/> safely:
		/// if the given data is <c>null</c>, <c>false</c> is returned as well, otherwise the predicate is invoked
		/// and the result is returned.
		/// </summary>
		/// <returns>The result of the predicate invocation if the the given data is effective, <c>false</c> otherwise.</returns>
		/// <param name="data">The given data to check and invoke the <paramref name="predicate"/> with.</param>
		/// <param name="predicate">The given predicate to invoke.</param>
		/// <typeparam name="TX">The type of data that is provided.</typeparam>
		/// <remarks>
		/// <para>For performance issues, the <paramref name="predicate"/> is assumed to be effective, no check is done.</para>
		/// </remarks>
		public static bool OrFalse <TX,TResult> (this TX data, Predicate<TX> predicate)
			where TX : class {
			Contract.Requires (predicate != null);
			if (data != null) {
				return predicate (data);
			} else {
				return false;
			}
		}

		/// <summary>
		/// Invokes the given <paramref name="action"/> with the given <paramref name="data"/> safely:
		/// if the given data is <c>null</c>, nothing is done, otherwise the action is performed.
		/// </summary>
		/// <param name="data">The given data to check and invoke the <paramref name="action"/> with.</param>
		/// <param name="action">The given action that must be executed.</param>
		/// <typeparam name="TX">The type of data that is provided.</typeparam>
		/// <remarks>
		/// <para>For performance issues, the <paramref name="action"/> is assumed to be effective, no check is done.</para>
		/// </remarks>
		public static void IfEffective <TX> (this TX data, Action<TX> action)
			where TX : class {
			Contract.Requires (action != null);
			if (data != null) {
				action (data);
			}
		}

		/// <summary>
		/// If the given <paramref name="value"/> is not effective, <paramref name="deflt"/> is returned,
		/// otherwise the <paramref name="value"/> is returned.
		/// </summary>
		/// <returns><paramref name="value"/> if effective, otherwise <paramref name="deflt"/>.</returns>
		/// <param name="value">The value to check and possible return.</param>
		/// <param name="deflt">The value to return if the given <paramref name="value"/> is null.</param>
		/// <typeparam name="T">The type of elements over which the function is defined.</typeparam>
		/// <remarks>
		/// <para>It is still possible that the result is not effective if both <paramref name="value"/>
		/// and <paramref name="deflt"/> are null.</para>
		/// </remarks>
		public static T IfNull<T> (this T value, T deflt)
		where T : class {
			if (value != null) {
				return value;
			} else {
				return deflt;
			}
		}

		/// <summary>
		/// If the given <paramref name="value"/> is not effective, the algorithm searches
		/// for the first effective value in <paramref name="deflts"/>, otherwise the <paramref name="value"/> is returned.
		/// </summary>
		/// <returns><paramref name="value"/> if effective, otherwise the first effective value of
		/// <paramref name="deflts"/> if exists, otherwise <c>null</c>.</returns>
		/// <param name="value">The value to check and possible return.</param>
		/// <param name="deflts">An list of default values that must be tried all until a non-effective
		/// value is found, or the end of the list is reached.</param>
		/// <typeparam name="T">The type of elements over which the function is defined.</typeparam>
		/// <remarks>
		/// <para>If the list of default values is not effective as well, null is returned.</para>
		/// <para>It is still possible that the result is not effective if both <paramref name="value"/>
		/// and all items in <paramref name="deflt"/> are <c>null</c> (or the list itself is <c>null</c>).</para>
		/// </remarks>
		public static T IfNull<T> (this T value, IEnumerable<T> deflts)
			where T : class {
			if (value != null || deflts == null) {
				return value;
			} else {
				return Enumerable.FirstOrDefault (deflts, StandardFunctions.NotNull<T> ().Invoke);
			}
		}

		/// <summary>
		/// If the given <paramref name="value"/> is not effective, the algorithm searches
		/// for the first effective value in <paramref name="deflts"/>, otherwise the <paramref name="value"/> is returned.
		/// </summary>
		/// <returns><paramref name="value"/> if effective, otherwise the first effective value of
		/// <paramref name="deflts"/> if exists, otherwise <c>null</c>.</returns>
		/// <param name="value">The value to check and possible return.</param>
		/// <param name="deflts">An array of default values that must be tried all until a non-effective
		/// value is found, or the end of the array is reached.</param>
		/// <typeparam name="T">The type of elements over which the function is defined.</typeparam>
		/// <remarks>
		/// <para>If the array of default values is not effective as well, null is returned.</para>
		/// <para>It is still possible that the result is not effective if both <paramref name="value"/>
		/// and all items in <paramref name="deflt"/> are <c>null</c> (or the list itself is <c>null</c>).</para>
		/// </remarks>
		public static T IfNull<T> (this T value, params T[] deflts)
			where T : class {
			return IfNull (value, (IEnumerable<T>)deflts);
		}

		/// <summary>
		/// Generate a list of all effectives of the given list of <paramref name="values"/>.
		/// </summary>
		/// <returns>A list containing the given <paramref name="values"/> that are effective.</returns>
		/// <param name="values">The given list of values to be filtered.</param>
		/// <typeparam name="T">The type of values of in the list.</typeparam>
		/// <remarks>
		/// <para>If the given <paramref name="values"/> is not effective, an empty list is returned.</para>
		/// <para>The filter operation is carried out lazily, infinite lists are supported as long
		/// as no infinite sequences of <c>null</c> values are part of the given <paramref name="values"/>.</para>
		/// </remarks>
		public static IEnumerable<T> Effectives<T> (this IEnumerable<T> values)
		where T : class {
			if (values != null) {
				foreach (T t in values) {
					if (t != null) {
						yield return t;
					}
				}
			}
		}
	}
}

