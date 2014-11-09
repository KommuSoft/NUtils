//
//  DotTextWriterUtils.cs
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
	/// A set of utility methods defined for the <see cref="T:IDotTextWriter"/> interface.
	/// </summary>
	public static class DotTextWriterUtils {
		#region IEnumerable to params
		/// <summary>
		/// Add a node to the current graph.
		/// </summary>
		/// <param name="writer">The writer to which the node is added.</param>
		/// <param name="identifier">The identifier of the current node.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of optional attributes to be added to the
		/// node that will be added.</param>
		/// <remarks>
		/// <para>The identifier must be effective for the operation to take place.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// <para>If the given <paramref name="writer"/> is not effective, nothing happens.</para>
		/// </remarks>
		public static void AddNode (this IDotTextWriter writer, string identifier, params INodeDotAttribute[] dotAttributes) {
			if (writer != null) {
				writer.AddNode (identifier, (IEnumerable<INodeDotAttribute>)dotAttributes);
			}
		}

		/// <summary>
		/// Add an undirected edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="writer">The writer to which the edge is added.</param>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">An array of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// <para>If the given <paramref name="writer"/> is not effective, nothing happens.</para>
		/// </remarks>
		public static void AddEdge (this IDotTextWriter writer, string fromIdentifier, string toIdentifier, params IEdgeDotAttribute[] dotAttributes) {
			if (writer != null) {
				writer.AddEdge (fromIdentifier, toIdentifier, (IEnumerable<IEdgeDotAttribute>)dotAttributes);
			}
		}

		/// <summary>
		/// Add an directed edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="writer">The writer to which the edge is added.</param>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">An array of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// <para>If the given <paramref name="writer"/> is not effective, nothing happens.</para>
		/// </remarks>
		public static void AddDirectedEdge (this IDotTextWriter writer, string fromIdentifier, string toIdentifier, params IEdgeDotAttribute[] dotAttributes) {
			if (writer != null) {
				writer.AddDirectedEdge (fromIdentifier, toIdentifier, (IEnumerable<IEdgeDotAttribute>)dotAttributes);
			}
		}
		#endregion
	}
}