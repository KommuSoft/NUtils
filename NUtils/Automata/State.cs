//
//  State.cs
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
using System.Linq;
using NUtils.Abstract;
using System.Collections.Generic;
using NUtils.Collections;

namespace NUtils.Automata {

	/// <summary>
	/// A basic implementation of the <see cref="T:IState`2"/> interface.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tag associated with the state.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tag associated with the edge.</typeparam>
	/// <remarks>
	/// <para>The states are not inherently accepting or initial since states can be "reused" by another
	/// automata that provides a different purpose for this states.</para>
	/// </remarks>
	public class State<TStateTag,TEdgeTag> : TagHashBase<TStateTag>, IState<TStateTag,TEdgeTag> {

		#region Fields
		/// <summary>
		/// The inner <see cref="T:Register`3"/> that stores the edges based on a tag.
		/// </summary>
		private readonly Register<TEdgeTag,IEdge<TStateTag,TEdgeTag>> edgeMap = new Register<TEdgeTag,IEdge<TStateTag,TEdgeTag>> (x => x.Tag);
		#endregion
		#region IState implementation
		/// <summary>
		/// Get the list of edges that originate from this <see cref="T:IState`2"/>.
		/// </summary>
		/// <value>An <see cref="T:IEnumerable`1"/> of <see cref="T:IEdge`2"/> instance that originate from this
		/// state.</value>
		/// <remarks>
		/// <para>Edges can be reused by several states: the same edge can occur in the list of edges
		/// of many <see cref="T:IState`2"/> instances.</para>
		/// </remarks>
		public IEnumerable<IEdge<TStateTag, TEdgeTag>> Edges {
			get {
				return this.edgeMap.Values;
			}
		}

		/// <summary>
		/// Get the number of edges that originate from this state.
		/// </summary>
		/// <value>The number of edges that originate from this state.</value>
		public int NumberOfEdges {
			get {
				return this.edgeMap.Count;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:State`2"/> class with a given tag.
		/// </summary>
		/// <param name="tag">The given tag to associate with this state.</param>
		public State (TStateTag tag) : base(tag) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:State`2"/> class with a given tag.
		/// </summary>
		/// <param name="tag">The given tag to associate with this state.</param>
		/// <param name="edges">An <see cref="T:IEnumerable`1"/> containing the initial edges stored in this <see cref="T:State`2"/>.</param>
		public State (TStateTag tag, IEnumerable<IEdge<TStateTag,TEdgeTag>> edges) : this(tag) {
			this.edgeMap.AddAll (edges);
		}
		#endregion
		#region IState implementation
		/// <summary>
		/// Get all edges that are associated with the given edge tag.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of <see cref="T:IEdge`1"/> instances that are all
		/// associated with the given <paramref name="edgetag"/>.</returns>
		/// <param name="edgetag">The given tag to match.</param>
		public IEnumerable<IEdge<TStateTag, TEdgeTag>> TaggedEdges (TEdgeTag edgetag) {
			return this.edgeMap.GetValues (edgetag);
		}

		/// <summary>
		/// Add a new edge to this <see cref="T:IState`2"/> such that the edge originates from this state.
		/// </summary>
		/// <param name="edge">The edge to add to this <see cref="T:IState`2"/>.</param>
		public void AddEdge (IEdge<TStateTag, TEdgeTag> edge) {
			this.edgeMap.Add (edge);
		}
		#endregion
	}
}

