//
//  TupleUtils.cs
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

namespace NUtils {
	/// <summary>
	/// A utility class that defines operations on tuples. For instance filtering the elements.
	/// </summary>
	public static class TupleUtils {

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`2"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2> (this Tuple<T1,T2> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`3"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3> (this Tuple<T1,T2,T3> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`4"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3,T4> (this Tuple<T1,T2,T3,T4> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`5"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth element of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3,T4,T5> (this Tuple<T1,T2,T3,T4,T5> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`6"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth element of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth element of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3,T4,T5,T6> (this Tuple<T1,T2,T3,T4,T5,T6> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`7"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth element of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth element of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth element of the tuple.</typeparam>
		/// <typeparam name="T7">The type of the seventh element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3,T4,T5,T6,T7> (this Tuple<T1,T2,T3,T4,T5,T6,T7> tuple) {
			return tuple.Item1;
		}

		/// <summary>
		/// A function that returns the first item of a given <see cref="T:Tuple`8"/>.
		/// </summary>
		/// <param name="tuple">The tuple to return the first element from.</param>
		/// <returns>The first element of the given tuple.</returns>
		/// <typeparam name="T1">The type of the first element of the tuple.</typeparam>
		/// <typeparam name="T2">The type of the second element of the tuple.</typeparam>
		/// <typeparam name="T3">The type of the third element of the tuple.</typeparam>
		/// <typeparam name="T4">The type of the fourth element of the tuple.</typeparam>
		/// <typeparam name="T5">The type of the fifth element of the tuple.</typeparam>
		/// <typeparam name="T6">The type of the sixth element of the tuple.</typeparam>
		/// <typeparam name="T7">The type of the seventh element of the tuple.</typeparam>
		/// <typeparam name="T8">The type of the eight element of the tuple.</typeparam>
		/// <remarks>
		/// <para>This method is mainly implement to extend LINQ functionality.</para>
		/// </remarks>
		public static T1 First<T1,T2,T3,T4,T5,T6,T7,T8> (this Tuple<T1,T2,T3,T4,T5,T6,T7,T8> tuple) {
			return tuple.Item1;
		}
	}
}

