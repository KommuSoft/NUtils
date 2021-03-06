//
//  DotTextWriter.cs
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
using System.CodeDom.Compiler;
using System.IO;
using NUtils.Functional;
using System.Collections.Generic;

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// An implementation of the <see cref="T:IDotTextWriter"/> interface. A <see cref="T:TextWriter"/> that supports the generation
	/// of GraphViz DOT Graph files.
	/// </summary>
	public class DotTextWriter : IndentedTextWriter, IDotTextWriter {

		#region Fields
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DotTextWriter"/> class with a given <see cref="T:TextWriter"/>
		/// to write to.
		/// </summary>
		/// <param name="writer">The <see cref="T:TextWriter"/> to write data to.</param>
		public DotTextWriter (TextWriter writer) : base(writer) {
		}
		#endregion
		#region IDotTextWriter implementation
		/// <summary>
		/// Add a graph to the file with a given name.
		/// </summary>
		/// <param name="type">The type of the graph to be added, optional, by default <see cref="DotGraphType.DirectedGraph"/>.</param>
		/// <param name="name">The name of the graph, optional, by default not effective.</param>
		public void AddGraph (DotGraphType type = DotGraphType.DirectedGraph, string name = null) {
			//TODO: cleanup
			switch (type) {
			case DotGraphType.DirectedGraph:
				this.Write (DotVisualUtils.DirectedGraphKeyword);
				break;
			case DotGraphType.Graph:
				this.Write (DotVisualUtils.GraphKeyword);
				break;
			}
			this.Write (DotVisualUtils.TokenSeparator);
			this.WriteLine (DotVisualUtils.ScopeOpen);
			this.Indent++;
		}
		#endregion
		#region private method, for programmers convenience
		/// <summary>
		/// Write the given list of attributes, if the list is effective and contains at least one element.
		/// </summary>
		/// <param name="dotAttributes">An <see cref="T:IEnumerable`1"/> of <see cref="T:IDotAttribute"/> instances
		/// that specify how a certain element should be drawn.</param>
		private void WriteAttributeList (IEnumerable<IDotAttribute> dotAttributes) {
			if (dotAttributes != null && dotAttributes.Contains ()) {
				this.Write (string.Format ("{0}{1}{2}", DotVisualUtils.AttributeOpen, string.Join (DotVisualUtils.AttributeSeparator, dotAttributes), DotVisualUtils.AttributeClose));
			}
		}
		#endregion
		#region IDotTextWriter implementation
		/// <summary>
		/// Add a node to the current graph.
		/// </summary>
		/// <param name="identifier">The identifier of the current node.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of optional attributes to be added to the
		/// node that will be added.</param>
		/// <remarks>
		/// <para>The identifier must be effective for the operation to take place.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// </remarks>
		public void AddNode (string identifier, IEnumerable<INodeDotAttribute> dotAttributes) {
			if (identifier != null) {
				this.Write (identifier);
				this.WriteAttributeList (dotAttributes);
				this.WriteLine (DotVisualUtils.ScopeSeparator);
			}
		}

		/// <summary>
		/// Add an undirected edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If one of the identifiers (<paramref name="fromIdentifier"/> or <paramref name="toIdentifier"/>), the edge is not added.</para>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// </remarks>
		public void AddEdge (string fromIdentifier, string toIdentifier, IEnumerable<IEdgeDotAttribute> dotAttributes) {
			if (fromIdentifier != null && toIdentifier != null) {
				this.Write (fromIdentifier);
				this.Write (DotVisualUtils.UndirectedEdgeToken);
				this.Write (toIdentifier);
				this.WriteAttributeList (dotAttributes);
				this.WriteLine (DotVisualUtils.ScopeSeparator);
			}
		}

		/// <summary>
		/// Add an directed edge from a node with the given <paramref name="fromIdentifier"/> to a node
		/// with the given <paramref name="toIdentifier"/>.
		/// </summary>
		/// <param name="fromIdentifier">The identifier of the first node of the edge.</param>
		/// <param name="toIdentifier">The identifier of the second node of the edge.</param>
		/// <param name="dotAttributes">A <see cref="T:IEnumerable`1"/> of <see cref="T:IEdgeDotAttribute"/> instances
		/// that alter the way the edge is displayed.</param>
		/// <remarks>
		/// <para>If one of the identifiers (<paramref name="fromIdentifier"/> or <paramref name="toIdentifier"/>), the edge is not added.</para>
		/// <para>If there are no nodes defined with the given identifier, additional nodes will be added to the graph,
		/// this is the behavior of GraphViz DOT graphs.</para>
		/// <para>If the given list of attributes is not effective, no attributes are added to the node.</para>
		/// </remarks>
		public void AddDirectedEdge (string fromIdentifier, string toIdentifier, IEnumerable<IEdgeDotAttribute> dotAttributes) {
			if (fromIdentifier != null && toIdentifier != null) {
				this.Write (fromIdentifier);
				this.Write (DotVisualUtils.DirectedEdgeToken);
				this.Write (toIdentifier);
				this.WriteAttributeList (dotAttributes);
				this.WriteLine (DotVisualUtils.ScopeSeparator);
			}
		}
		#endregion
		#region Close override method
		/// <summary>
		/// Closes the current writer and releases any system resources associated with the writer, missing unidents are added.
		/// </summary>
		/// <remarks>
		/// <para>Scopes that are open at that time will be closed, to generate a valid GraphViz DOT graph file.</para>
		/// </remarks>
		public override void Close () {
			int n = this.Indent;
			for (; n > 0x00;) {
				this.Indent = --n;
				this.WriteLine (DotVisualUtils.ScopeClose);
			}
			base.Close ();
		}
		#endregion
	}
}

