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

namespace NUtils.Visual {

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
	}
}

