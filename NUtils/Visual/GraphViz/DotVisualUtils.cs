//
//  DotVisualUtils.cs
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

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// A set of constants describing the GraphViz DOT Graph notation.
	/// </summary>
	public static class DotVisualUtils {

		#region Constants
		/// <summary>
		/// A sequence of characters necessary to separate two tokens.
		/// </summary>
		public const string TokenSeparator = " ";
		/// <summary>
		/// The graph keyword, a keyword used to declare a graph that is not directed.
		/// </summary>
		public const string GraphKeyword = "graph";
		/// <summary>
		/// The digraph keyword, a keyword used to declare a graph that is directed.
		/// </summary>
		public const string DirectedGraphKeyword = "digraph";
		/// <summary>
		/// The separator between two commands.
		/// </summary>
		public const string ScopeSeparator = ";";
		/// <summary>
		/// The character used to open a new scope (i.e. a graph environment).
		/// </summary>
		public const string ScopeOpen = "{";
		/// <summary>
		/// The character used to close the current scope (i.e. a graph environment).
		/// </summary>
		public const string ScopeClose = "}";
		/// <summary>
		/// The character used to open an attribute array (i.e. describe how to draw a node or edge).
		/// </summary>
		public const string AttributeOpen = "[";
		/// <summary>
		/// The character used to close an attribute array (i.e. describe how to draw a node or edge).
		/// </summary>
		public const string AttributeClose = "]";
		/// <summary>
		/// A string that represents the token used to assign a value to an attribute.
		/// </summary>
		public const string AttributeAssignment = "=";
		/// <summary>
		/// The token that is used to open a string.
		/// </summary>
		public const string StringOpen = "\"";
		/// <summary>
		/// The token that is used to close a string.
		/// </summary>
		public const string StringClose = "\"";
		/// <summary>
		/// The token that separates attributes from each other.
		/// </summary>
		public const string AttributeSeparator = ",";
		/// <summary>
		/// The token that specifies that the edge is undirected.
		/// </summary>
		public const string UndirectedEdgeToken = " - ";
		/// <summary>
		/// The token that specifies that the edge is directed.
		/// </summary>
		public const string DirectedEdgeToken = " -> ";
		#endregion
		#region Utility methods
		#endregion
	}
}

