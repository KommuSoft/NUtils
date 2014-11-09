//
//  IDotTextWriter.cs
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

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// An interface describing a <see cref="T:TextWriter"/> that supports the generation
	/// of GraphViz DOT Graph files.
	/// </summary>
	public interface IDotTextWriter {

		/// <summary>
		/// Add a graph to the file with a given name.
		/// </summary>
		/// <param name="type">The type of the graph to be added, optional, by default <see cref="DotGraphType.DirectedGraph"/>.</param>
		/// <param name="name">The name of the graph, optional, by default not effective.</param>
		void AddGraph (DotGraphType type = DotGraphType.DirectedGraph, string name = null);

		/// <summary>
		/// Add a node to the current graph.
		/// </summary>
		/// <param name="identifier">The identifier of the current node.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of optional <see cref="INodeDotAttribute"/>
		/// attributes to be added to the node that will be added.</param>
		/// <remarks>
		/// <para>The identifier must be effective for the operation to take place.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// </remarks>
		void AddNode (string identifier, IEnumerable<INodeDotAttribute> dotAttributes);

		/// <summary>
		/// Add an undirected edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// </remarks>
		void AddEdge (string fromIdentifier, string toIdentifier, IEnumerable<IEdgeDotAttribute> dotAttributes);

		/// <summary>
		/// Add an directed edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// </remarks>
		void AddDirectedEdge (string fromIdentifier, string toIdentifier, IEnumerable<IEdgeDotAttribute> dotAttributes);
	}
}

