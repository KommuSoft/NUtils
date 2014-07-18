//
//  IGraph.cs
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
using NUtils.Abstract;

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying a graph structure.
	/// </summary>
	/// <remarks>
	/// <para>A graph is a collection of nodes and a collection of edges between two nodes.</para>
	/// </remarks>
	public interface IGraph : ILength {

		/// <summary>
		/// Enumerate the edges contained in the graph.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerable`1"/> containing the <see cref="T:Tuple`2"/> instances of the edges.</returns>
		IEnumerable<Tuple<int,int>> GetEdges ();

		/// <summary>
		/// Enumerate the indices of the nodes that have an edge connecting the given <paramref name="node"/> and the returned node.
		/// </summary>
		/// <returns>A <see cref="IEnumerable`1"/> of indices that are connected with the given <paramref name="node"/>.</returns>
		/// <param name="node">The index of the node for which the neighbors must be calculated.</param>
		IEnumerable<int> GetNeighbors (int node);

		/// <summary>
		/// Determines if there is an edge containing the given <paramref name="node"/> with itself.
		/// </summary>
		/// <returns><c>true</c>, if the given <paramref name="node"/> contains a loop; otherwise, <c>false</c>.</returns>
		/// <param name="node">The given node to check for.</param>
		bool ContainsLoop (int node);

		/// <summary>
		/// Determines whether there is an edge between the two given nodes.
		/// </summary>
		/// <returns><c>true</c> if there is an edge between the two given indices; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>In case of a directed graph, the direction of the graph doesn't matter.</para>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		bool IsImmediatelyConnected (int nodea, int nodeb);

		/// <summary>
		/// Determines whether there is a sequence of edges between the two given nodes such that eventually the two given nodes are connected.
		/// </summary>
		/// <returns><c>true</c> if a sequence of edges contains the two given nodes; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>In case of a directed graph, the direction of the graph doesn't matter.</para>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		bool IsConnected (int nodea, int nodeb);
	}
}

