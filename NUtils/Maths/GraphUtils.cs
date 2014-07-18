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
		public const string KeywordGraph = @"graph";
		public const string KeywordDigraph = @"digraph";
		public const string KeywordEdge = @"--";
		public const string KeywordDiedge = @"->";
		public const string KeywordEnvUp = @"{";
		public const string KeywordEnvDn = @"}";
		public const string KeywordIdent = "\t";
		public const string KeywordSeparator = ";";
		public const string NodePrefix = "n";
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
				writer.Write (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordEdge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.Write (KeywordSeparator);
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
				writer.Write (KeywordSeparator);
			}
			foreach (Tuple<int,int> edge in graph.GetEdges ()) {
				writer.Write (KeywordIdent);
				writer.Write (NodePrefix);
				writer.Write (edge.Item1);
				writer.Write (KeywordDiedge);
				writer.Write (NodePrefix);
				writer.Write (edge.Item2);
				writer.Write (KeywordSeparator);
			}
			writer.WriteLine (KeywordEnvDn);
		}
		#endregion
		#region IGraph and IDigraph functionalities
		#endregion
	}
}

