//
//  INullPathNode.cs
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
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// An interface that specifies a <see cref="T:INullPathNode`1"/> instance: a node
	/// that is the epsilon in a string: in other words no node. The node doesn't accept any
	/// node, but is useful in an nondeterministic finite automaton.
	/// </summary>
	/// <typeparam name='T'>The type of the nodes on which this <see cref="T:IPathNode`1"/> operates.</typeparam>
	/// <remarks>
	/// <para>A <see cref="T:INullPathNode`1"/> returns <c>false</c> on every <see cref="M:IPathNode`1.Validate"/> call.</para>
	/// <para>An <see cref="T:INullPathNode`1"/> will not match any item in a given tree, the tree will simply not be evaluated.</para>
	/// </remarks>
	public interface INullPathNode<T> : IPathNode<T> where T : IComposition<T> {
	}
}

