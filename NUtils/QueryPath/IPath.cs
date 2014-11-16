//
//  IPath.cs
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
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// A path in a tree-structured datastructure, in this case represented by a <see cref="T:IComposition`1"/>.
	/// </summary>
	public interface IPath<T> where T : IComposition<T> {



		/// <summary>
		/// Evaluate the specified tree using this <see cref="T:IPath`1"/> instance and return all possible matches.
		/// </summary>
		/// <param name="tree">The given tree to evaluate.</param>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains all possible nodes in the tree that match
		/// the path specifications. This will in many cases be evaluated lazily.</returns>
		IEnumerable<T> Evaluate (T tree);

		/// <summary>
		/// Compiles this <see cref="T:IPath`1"/> instance into an NFA to evaluate trees.
		/// </summary>
		/// <remarks>
		/// <para>In case this method has not been called before the <see cref="M:Evaluate"/> method
		/// has been called, the method will be called automatically.</para>
		/// <para>After compilation, the path cannot be modified any further.</para>
		/// <para>Some instances don't compile first: for instance <see cref="T:IPathNode`1"/> instances. In that
		/// case the compile method does nothing.</para>
		/// </remarks>
		void Compile ();
	}
}

