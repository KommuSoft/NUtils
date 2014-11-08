//
//  Edge.cs
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
using System.Linq;
using NUtils.Abstract;
using NUtils.Collections;

namespace NUtils.Automata {

	/// <summary>
	/// A basic implementation of the <see cref="T:IEdge`2"/> interface.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tag associated with the state.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tag associated with the edge.</typeparam>
	/// <remarks>
	/// <para>The edges have no original state such that they can be "reused" by different
	/// <see cref="T:IState`2"/> instances in order to reduce memory usage.</para>
	/// </remarks>
	public class Edge<TStateTag,TEdgeTag> : TagHashBase<TEdgeTag>, IEdge<TStateTag,TEdgeTag> {

		#region Fields
		/// <summary>
		/// A list of the states to which this edge map.
		/// </summary>
		private readonly HashSet<IState<TStateTag, TEdgeTag>> resultingStates = new HashSet<IState<TStateTag, TEdgeTag>> ();
		#endregion
		#region IEdge implementation
		/// <summary>
		/// Get the list of resulting state(s) after applying the edge.
		/// </summary>
		/// <value>A <see cref="T:IEnumerable`1"/> containing all the <see cref="T:IState`2"/> instances to which this
		/// <see cref="T:IEdge`2"/> refers to.</value>
		/// <remarks>
		/// <para>In case of a deterministic finite state automaton, there is exactly one resulting state,
		/// this can be enforced by deriving from this interface.</para>
		/// </remarks>
		public IEnumerable<IState<TStateTag, TEdgeTag>> ResultingStates {
			get {
				return this.resultingStates.AsEnumerable ();
			}
		}
		#endregion
		#region ICollection implementation
		public int Count {
			get {
				throw new NotImplementedException ();
			}
		}

		public bool IsReadOnly {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`2"/> class.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		public Edge (TEdgeTag tag) : base(tag) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`2"/> class with a give initial list
		/// of states to which this edge points.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		/// <param name="resultingStates">An <see cref="T:IEnumerable`1"/> containing <see cref="T:IState`2"/> instances
		/// representing the states to which this edge points.</param>
		public Edge (TEdgeTag tag, IEnumerable<IState<TStateTag,TEdgeTag>> resultingStates) : this(tag) {
			this.resultingStates.AddAll (resultingStates);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`2"/> class with a give initial list
		/// of states to which this edge points.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		/// <param name="resultingStates">An array containing <see cref="T:IState`2"/> instances
		/// representing the states to which this edge points.</param>
		public Edge (TEdgeTag tag, params IState<TStateTag,TEdgeTag>[] resultingStates) : this(tag,(IEnumerable<IState<TStateTag,TEdgeTag>>) resultingStates) {
		}
		#endregion
		#region ICollection implementation
		public void Add (IState<TStateTag, TEdgeTag> item) {
			throw new NotImplementedException ();
		}

		public void Clear () {
			throw new NotImplementedException ();
		}

		public bool Contains (IState<TStateTag, TEdgeTag> item) {
			throw new NotImplementedException ();
		}

		public void CopyTo (IState<TStateTag, TEdgeTag>[] array, int arrayIndex) {
			throw new NotImplementedException ();
		}

		public bool Remove (IState<TStateTag, TEdgeTag> item) {
			throw new NotImplementedException ();
		}
		#endregion
		#region IEnumerable implementation
		public IEnumerator<IState<TStateTag, TEdgeTag>> GetEnumerator () {
			throw new NotImplementedException ();
		}
		#endregion
		#region IEnumerable implementation
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region ITag implementation
		TEdgeTag ITag<TEdgeTag>.Tag {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

