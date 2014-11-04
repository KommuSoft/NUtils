//
//  IPathNode.cs
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
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// The representation of a single node in a <see cref="T:IPath`1"/>. A node can evaluate a node
	/// in a tree-like structure.
	/// </summary>
	/// <typeparam name='T'>The type of the nodes on which this <see cref="T:IPathNode`1"/> operates.<typeparam>
	public interface IPathNode<T> : IPath<T>, IValidater<T> where T : IComposition<T> {

	}
}

