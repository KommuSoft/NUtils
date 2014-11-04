//
//  IQuery.cs
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

namespace NUtils.Abstract {

	/// <summary>
	/// An interface describing a query. A query can be processed on a <paramref name="TSource"/>
	/// and can result in zero, one or more answers.
	/// </summary>
	/// <typeparam name='TSource'>The type of instances on which the query can be processed.</typeparam>
	/// <typeparam name='TResult'>The type of the matches that come out of the query.</typeparam>
	public interface IQuery<TSource,TResult> : IValidater<TSource> {

		/// <summary>
		/// Query the specified source and generate a (lazy) list of results.
		/// </summary>
		/// <param name="source">The source to query.</param>
		/// <returns>A <see cref="T:IEnumerable`1"/> of <typeparamref name="TResult"/> instances: matches of the query.</returns>
		IEnumerable<TResult> Query (TSource source);
	}
}

