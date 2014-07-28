//
//  GraphUtils.cs
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
using System.IO;

namespace NUtils.Maths {
	/// <summary>
	/// A collection of utility methods for graphs and graph-like structures.
	/// </summary>
	public static class GraphUtils {

		#region Constants
		/// <summary>
		/// The keyword to start the description of a graph in the DOT language.
		/// </summary>
		public const string KeywordGraph = @"graph";
		/// <summary>
		/// The keyword to start the description of a directed in the DOT language.
		/// </summary>
		public const string KeywordDigraph = @"digraph";
		/// <summary>
		/// The keyword to label a node or edge in the DOT language.
		/// </summary>
		public const string KeywordLabel = @"label";
		/// <summary>
		/// The undirected edge operator in the DOT language.
		/// </summary>
		public const string KeywordEdge = @"--";
		/// <summary>
		/// The directed edge operator in the DOT language.
		/// </summary>
		public const string KeywordDiedge = @"->";
		/// <summary>
		/// The delimeter in the DOT language to open an environment.
		/// </summary>
		public const string KeywordEnvUp = @"{";
		/// <summary>
		/// The delimter in the DOT language to close an environment.
		/// </summary>
		public const string KeywordEnvDn = @"}";
		/// <summary>
		/// The delimeter in the DOT language to open an optional environment.
		/// </summary>
		public const string KeywordOptUp = @"[";
		/// <summary>
		/// The delimter in the DOT language to close an optional environment.
		/// </summary>
		public const string KeywordOptDn = @"]";
		/// <summary>
		/// The delimeter in the DOT language to associate a key with a value.
		/// </summary>
		public const string KeywordKeyVal = @"=";
		/// <summary>
		/// The delimeter in the DOT language to begin and end a string.
		/// </summary>
		public const string KeywordString = "\"";
		/// <summary>
		/// The indentation part in the DOT language (when an environment is opened).
		/// </summary>
		public const string KeywordIdent = "\t";
		/// <summary>
		/// The default separator of items in the DOT language.
		/// </summary>
		public const string KeywordSeparator = ";";
		/// <summary>
		/// A prefix to generate a node identifier.
		/// </summary>
		public const string NodePrefix = "n";
		/// <summary>
		/// A prefix to generate an edge identifier.
		/// </summary>
		public const string EdgePrefix = "e";
		#endregion
		#region Default label functions
		/// <summary>
		/// The function used by default to label a node for the Graphviz visualizer.
		/// </summary>
		/// <returns>The generated label for the node.</returns>
		/// <param name="node">The index of the given node to label.</param>
		/// <remarks>
		/// <para>The label function simply returns <c>"n"</c> followed by the node's index.</para>
		/// </remarks>
		public static string DefaultNodeLabelFunction (int node) {
			return string.Format ("n{0}", node);
		}

		/// <summary>
		/// The function used by default to label a edge for the Graphviz visualizer.
		/// </summary>
		/// <returns>The generated label for the given edge.</returns>
		/// <param name="node1">The index of the first node of the given edge to label.</param>
		/// <param name="node2">The index of the second node of the given edge to label.</param>
		/// <remarks>
		/// <para>The label function simply returns an empty string for every edge.</para>
		/// </remarks>
		public static string DefaultEdgeLabelFunction (int node1, int node2) {
			return string.Empty;
		}
		#endregion
		#region Graphviz functions
		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <remarks>
		/// <para>The <see cref="M:DefaultNodeLabelFunction"/> is used to label the nodes, the method returns the index
		/// the node prefixed with <c>"n"</c>.</para>
		/// <para>The <see cref="M:DefaultEdgeLabelFunction"/> is used to label the edges, the method returns the empty
		/// string for each edge.</para>
		/// </remarks>
		public static void WriteDotStream (this IGraph graph, TextWriter writer) {
			WriteDotStream (graph, writer, DefaultNodeLabelFunction, DefaultEdgeLabelFunction);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <param name="nodeLabelFunction">A function that generates the labels for the nodes.</param>
		/// <remarks>
		/// <para>The <see cref="M:DefaultEdgeLabelFunction"/> is used to label the edges, the method returns the empty
		/// string for each edge.</para>
		/// </remarks>
		public static void WriteDotStream (this IGraph graph, TextWriter writer, Func<int,string> nodeLabelFunction) {
			WriteDotStream (graph, writer, nodeLabelFunction, DefaultEdgeLabelFunction);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <param name="nodeLabelFunction">A function that generates the labels for the nodes.</param>
		/// <param name="edgeLabelFunction">A function that generates the labels for the edges.</param>
		public static void WriteDotStream (this IGraph graph, TextWriter writer, Func<int,string> nodeLabelFunction, Func<int,int,string> edgeLabelFunction) {
			writer.Write (KeywordGraph);
			writer.WriteLine (KeywordEnvUp);
			int l = graph.Length;
			for (int node = 0x00; node < l; node++) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (node);
				writer.Write (KeywordOptUp);
				writer.Write (KeywordLabel);
				writer.Write (KeywordKeyVal);
				writer.Write (KeywordString);
				writer.Write (nodeLabelFunction (node));
				writer.Write (KeywordString);
				writer.Write (KeywordOptDn);
				writer.WriteLine (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordEdge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.Write (KeywordOptUp);
				writer.Write (KeywordLabel);
				writer.Write (KeywordKeyVal);
				writer.Write (KeywordString);
				writer.Write (edgeLabelFunction (edge.Item1, edge.Item2));
				writer.Write (KeywordString);
				writer.Write (KeywordOptDn);
				writer.WriteLine (KeywordSeparator);
			}
			writer.WriteLine (KeywordEnvDn);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <remarks>
		/// <para>The <see cref="M:DefaultNodeLabelFunction"/> is used to label the nodes, the method returns the index
		/// the node prefixed with <c>"n"</c>.</para>
		/// <para>The <see cref="M:DefaultEdgeLabelFunction"/> is used to label the edges, the method returns the empty
		/// string for each edge.</para>
		/// </remarks>
		public static void WriteDotStream (this IDigraph graph, TextWriter writer) {
			WriteDotStream (graph, writer, DefaultNodeLabelFunction, DefaultEdgeLabelFunction);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <param name="nodeLabelFunction">A function that generates the labels for the nodes.</param>
		/// <remarks>
		/// <para>The <see cref="M:DefaultEdgeLabelFunction"/> is used to label the edges, the method returns the empty
		/// string for each edge.</para>
		/// </remarks>
		public static void WriteDotStream (this IDigraph graph, TextWriter writer, Func<int,string> nodeLabelFunction) {
			WriteDotStream (graph, writer, nodeLabelFunction, DefaultEdgeLabelFunction);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		/// <param name="nodeLabelFunction">A function that generates the labels for the nodes.</param>
		/// <param name="edgeLabelFunction">A function that generates the labels for the edges.</param>
		public static void WriteDotStream (this IDigraph graph, TextWriter writer, Func<int,string> nodeLabelFunction, Func<int,int,string> edgeLabelFunction) {
			writer.Write (KeywordDigraph);
			writer.WriteLine (KeywordEnvUp);
			int l = graph.Length;
			for (int node = 0x00; node < l; node++) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (node);
				writer.Write (KeywordOptUp);
				writer.Write (KeywordLabel);
				writer.Write (KeywordKeyVal);
				writer.Write (KeywordString);
				writer.Write (nodeLabelFunction (node));
				writer.Write (KeywordString);
				writer.Write (KeywordOptDn);
				writer.WriteLine (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordDiedge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.Write (KeywordOptUp);
				writer.Write (KeywordLabel);
				writer.Write (KeywordKeyVal);
				writer.Write (KeywordString);
				writer.Write (edgeLabelFunction (edge.Item1, edge.Item2));
				writer.Write (KeywordString);
				writer.Write (KeywordOptDn);
				writer.WriteLine (KeywordSeparator);
			}
			writer.WriteLine (KeywordEnvDn);
		}
		#endregion
		#region IGraph and IDigraph functionalities (basic implementations, if no more efficient impelementation is available)
		#endregion
	}
}

