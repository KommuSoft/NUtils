//
//  Path.cs
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
using Microsoft.FSharp.Control;
using System.Collections;
using System.Collections.Generic;
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// A basic implementation of a <see cref="T:IPath`1"/>. This version consists of one or more
	/// <see cref="T:IPathNode`1"/> instance that form a path. One can use Kleene stars, deep paths
	/// and disjunctions as well.
	/// </summary>
	public class Path<T> : IPath<T> where T : IComposition<T> {

		#region Fields
		private readonly ICollection<IPath<T>> pathElements;
		#endregion
		#region Constructors
		public Path () {
		}
		#endregion
		#region IPath implementation
		/// <summary>
		/// Evaluate the specified tree using this <see cref="T:IPath`1"/> instance and return all possible matches.
		/// </summary>
		/// <param name="tree">The given tree to evaluate.</param>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains all possible nodes in the tree that match
		/// the path specifications. This will in many cases be evaluated lazily.</returns>
		public IEnumerable<T> Evaluate (T tree) {
			throw new NotImplementedException ();
		}
		#endregion
	}
}

