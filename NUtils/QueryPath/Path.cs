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
	public class Path<T> : PathBase<T> where T : IComposition<T> {

		#region Fields
		private readonly ICollection<IPath<T>> pathElements;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Path`1"/> class.
		/// </summary>
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
		public override IEnumerable<T> Evaluate (T tree) {
			throw new NotImplementedException ();
		}
		#endregion
		#region implemented abstract members of PathBase
		/// <summary>
		/// Generates the NF.
		/// </summary>
		/// <returns>The NF.</returns>
		protected override NondeterministicFiniteAutomaton<int,int> GenerateNFA () {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:Path`1"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:Path`1"/>.</returns>
		public override string ToString () {
			throw new NotImplementedException ();
		}
		#endregion
	}
}

