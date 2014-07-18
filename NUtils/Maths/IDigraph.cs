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
using System.Collections.Generic;

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying a directed graph structure.
	/// </summary>
	public interface IDigraph : IGraph {

		/// <summary>
		/// Enumerate the indices of the nodes such that there is a directed edge from the given <paramref name="node"/> to the emited node.
		/// </summary>
		/// <returns>A <see cref="IEnumerable`1"/> of the indices of nodes such that there is a directed edge between the given <paramref name="node"/> and the enumerated node.</returns>
		/// <param name="node">The node for which the neighbors must be calculated.</param>
		IEnumerable<int> GetConnectedNodes (int node);

		/// <summary>
		/// Determines whether there is a directed edge from the first given node (<paramref name="nodea"/>) and the second node (<paramref name="nodeb"/>).
		/// </summary>
		/// <returns><c>true</c> if there is a directed edge from <paramref name="nodea"/> to <paramref name="nodeb"/>; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		bool IsImmediatelyDirectedConnected (int nodea, int nodeb);

		/// <summary>
		/// Determines whether there is a sequence of directed edges between the two given nodes such that there is a directed
		/// path from the given first node (<paramref name="nodea"/>) to the given second node (<paramref name="nodeb"/>).
		/// </summary>
		/// <returns><c>true</c> iff there is a directed path from <paramref name="nodea"/> to <paramref name="nodeb"/>; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		bool IsDirectedConnected (int nodea, int nodeb);
	}
}

