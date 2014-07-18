//
//  TransitionBase.cs
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
using System.Text;

namespace NUtils.Maths {
	/// <summary>
	/// A basic implementation of the <see cref="ITransition"/> interface that provides basic implementations
	/// of some methods to ease subclassing.
	/// </summary>
	public abstract class TransitionBase : ITransition {

		#region ILength implementation
		/// <summary>
		/// Gets the number of subelements.
		/// </summary>
		/// <value>The length.</value>
		public abstract int Length {
			get;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="TransitionBase"/> class.
		/// </summary>
		protected TransitionBase () {
		}
		#endregion
		#region ITransition implementation
		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <returns>The target index of the given source <paramref name="index"/>.</returns>
		/// <param name="index">The given index from which the transition originates.</param>
		public abstract int GetTransitionOfIndex (int index);
		#endregion
		#region IEnumerable`1 implementation
		/// <summary>
		/// Gets the enumerator that enumerates the target indices contained in this transition function.
		/// </summary>
		/// <returns>The enumerator that enumerates the target indices contained in this transition function.</returns>
		public abstract IEnumerator<int> GetEnumerator ();
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Gets the enumerator that enumerates the target indices contained in this transition function.
		/// </summary>
		/// <returns>The enumerator that enumerates the target indices contained in this transition function.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region IDigraph implementation
		/// <summary>
		/// Enumerate the indices of the nodes such that there is a directed edge from the given <paramref name="node"/> to the emited node.
		/// </summary>
		/// <returns>A <see cref="IEnumerable`1"/> of the indices of nodes such that there is a directed edge between the given <paramref name="node"/> and the enumerated node.</returns>
		/// <param name="node">The node for which the neighbors must be calculated.</param>
		public virtual IEnumerable<int> GetDirectedConnectedNodes (int node) {
			throw new NotImplementedException ();
		}

		public virtual bool IsImmediatelyDirectedConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}

		public virtual bool IsDirectedConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IGraph implementation
		/// <summary>
		/// Enumerate the edges contained in the graph.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerable`1"/> containing the <see cref="T:Tuple`2"/> instances of the edges.</returns>
		public virtual IEnumerable<Tuple<int, int>> GetEdges () {
			int ei = 0x00;
			foreach (int ej in this) {
				yield return new Tuple<int, int> (ei++, ej);
			}
		}

		/// <summary>
		/// Enumerate the indices of the nodes that have an edge connecting the given <paramref name="node"/> and the returned node.
		/// </summary>
		/// <returns>A <see cref="IEnumerable`1"/> of indices that are connected with the given <paramref name="node"/>.</returns>
		/// <param name="node">The index of the node for which the neighbors must be calculated.</param>
		public virtual IEnumerable<int> GetNeighbors (int node) {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines if there is an edge containing the given <paramref name="node"/> with itself.
		/// </summary>
		/// <returns><c>true</c>, if the given <paramref name="node"/> contains a loop; otherwise, <c>false</c>.</returns>
		/// <param name="node">The given node to check for.</param>
		public virtual bool ContainsLoop (int node) {
			return this.GetTransitionOfIndex (node) == node;
		}

		/// <summary>
		/// Determines whether there is an edge between the two given nodes.
		/// </summary>
		/// <returns><c>true</c> if there is an edge between the two given indices; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>In case of a directed graph, the direction of the graph doesn't matter.</para>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		public virtual bool IsImmediatelyConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Determines whether there is a sequence of edges between the two given nodes such that eventually the two given nodes are connected.
		/// </summary>
		/// <returns><c>true</c> if a sequence of edges contains the two given nodes; otherwise, <c>false</c>.</returns>
		/// <param name="nodea">The first given node.</param>
		/// <param name="nodeb">The second given node.</param>
		/// <remarks>
		/// <para>In case of a directed graph, the direction of the graph doesn't matter.</para>
		/// <para>A node is only connected with itself if there is no loop edge.</para>
		/// </remarks>
		public virtual bool IsConnected (int nodea, int nodeb) {
			throw new NotImplementedException ();
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="ExplicitTransition"/>.</returns>
		public override string ToString () {
			StringBuilder sb = new StringBuilder ("{");
			int i = 0x00;
			foreach (int j in this) {
				sb.AppendFormat (" {0}>{1}", i, j);
				i++;
			}
			sb.Append (" }");
			return sb.ToString ();
		}
		#endregion
	}
}

