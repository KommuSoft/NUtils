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
		#region Graphviz functions
		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		public static void WriteDotStream (this IGraph graph, TextWriter writer) {
			writer.Write (KeywordGraph);
			writer.WriteLine (KeywordEnvUp);
			int l = graph.Length;
			for (int node = 0x00; node < l; node++) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (node);
				writer.WriteLine (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordEdge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.WriteLine (KeywordSeparator);
			}
			writer.WriteLine (KeywordEnvDn);
		}

		/// <summary>
		/// Writes a dot-notation of the given <paramref name="graph"/> to the given <paramref name="writer"/>.
		/// </summary>
		/// <param name="graph">The given graph to write out.</param>
		/// <param name="writer">The writer to write the graph structure to.</param>
		public static void WriteDotStream (this IDigraph graph, TextWriter writer) {
			writer.Write (KeywordDigraph);
			writer.WriteLine (KeywordEnvUp);
			int l = graph.Length;
			for (int node = 0x00; node < l; node++) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (node);
				writer.WriteLine (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordDiedge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.WriteLine (KeywordSeparator);
			}
			writer.WriteLine (KeywordEnvDn);
		}
		#endregion
		#region IGraph and IDigraph functionalities (basic implementations, if no more efficient impelementation is available)
		#endregion
	}
}

