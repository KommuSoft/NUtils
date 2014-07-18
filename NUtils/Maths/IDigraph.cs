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
	}
}

