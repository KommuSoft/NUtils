//
//  QueryUtils.cs
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
using NUtils.Abstract;
using System.Collections.Generic;
using NUtils.Functional;

namespace NUtils {

	/// <summary>
	/// A utility class providing additional functionality for <see cref="T:IQuery`2"/> instances.
	/// </summary>
	public static class QueryUtils {

		#region Utility methods
		/// <summary>
		/// Checks if the query succeeds by finding at least one match in the given <paramref name="source"/>.
		/// </summary>
		/// <param name="query">The given query that must be checked.</param>
		/// <param name="source">The given source on which the query runs.</param>
		/// <typeparam name="TSource">The type of the source that is given.</typeparam>
		/// <typeparam name="TResult">The type of results generated by the query.</typeparam>
		/// <remarks>
		/// <para>If the query is performed lazily (what is the case in for instance Prolog), one can save
		/// computation and can see this as a <c>once</c> directive.</para>
		/// </remarks>
		public static bool Find<TSource,TResult> (this IQuery<TSource,TResult> query, TSource source) {
			return query.Query (source).Contains ();
		}
		#endregion
	}
}

