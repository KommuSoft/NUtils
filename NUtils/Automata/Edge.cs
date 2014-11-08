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
using NUtils.Functional;

namespace NUtils.Automata {

	/// <summary>
	/// A basic implementation of the <see cref="T:IEdge`2"/> interface.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tag associated with the state.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tag associated with the edge.</typeparam>
	/// <typeparam name='TSet'>The type of set that stores the states to which this <see cref="T:Edge`3"/> maps.</typeparam>
	/// <remarks>
	/// <para>The edges have no original state such that they can be "reused" by different
	/// <see cref="T:IState`2"/> instances in order to reduce memory usage.</para>
	/// </remarks>
	public class Edge<TStateTag,TEdgeTag,TSet> : TagHashBase<TEdgeTag>, IEdge<TStateTag,TEdgeTag>
	    where TSet : ISet<IState<TStateTag,TEdgeTag>>, new() {

		#region Fields
		/// <summary>
		/// A list of the states to which this edge map.
		/// </summary>
		private readonly TSet resultingStates = new TSet ();
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
		/// <summary>
		/// Get the number of states to which this <see cref="T:IEdge`2"/> instance maps to.
		/// </summary>
		/// <value>The number of states to which this <see cref="T:IEdge`2"/> instance maps to.</value>
		public int Count {
			get {
				return this.resultingStates.Count;
			}
		}

		/// <summary>
		/// Gets a value indicating whether the set of <see cref="T:IState`2"/> instances this <see cref="T:Edge`3"/> maps to is read only.
		/// </summary>
		/// <value><c>true</c> if that collection is read only; otherwise, <c>false</c>.</value>
		/// <remarks>
		/// <para>The value is always <c>false</c>.</para>
		/// </remarks>
		public bool IsReadOnly {
			get {
				return false;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`3"/> class.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		public Edge (TEdgeTag tag) : base(tag) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`3"/> class with a give initial list
		/// of states to which this edge points.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		/// <param name="resultingStates">An <see cref="T:IEnumerable`1"/> containing <see cref="T:IState`2"/> instances
		/// representing the states to which this edge points.</param>
		public Edge (TEdgeTag tag, IEnumerable<IState<TStateTag,TEdgeTag>> resultingStates) : this(tag) {
			this.resultingStates.AddAll (resultingStates);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`3"/> class with a give initial list
		/// of states to which this edge points.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		/// <param name="resultingStates">An array containing <see cref="T:IState`2"/> instances
		/// representing the states to which this edge points.</param>
		public Edge (TEdgeTag tag, params IState<TStateTag,TEdgeTag>[] resultingStates) : this(tag,(IEnumerable<IState<TStateTag,TEdgeTag>>) resultingStates) {
		}
		#endregion
		#region ICollection implementation
		/// <summary>
		/// Add all the given <paramref name="item"/> to the collection of states to which this edge maps.
		/// </summary>
		/// <param name="item">The given resulting state to add to this <see cref="T:Edge`3"/>.</param>
		public void Add (IState<TStateTag, TEdgeTag> item) {
			this.resultingStates.Add (item);
		}

		/// <summary>
		/// Removes all resulting states from this <see cref="T:Edge`3"/>.
		/// </summary>
		public void Clear () {
			this.resultingStates.Clear ();
		}

		/// <summary>
		/// Determines whether the current <see cref="T:Edge`3"/> maps to the given <see cref="T:IState`2"/>.
		/// </summary>
		/// <param name="state">The given state to check.</param>
		/// <returns><c>true</c> if this <see cref="T:Edge`3"/> maps to the given <paramref name="state"/>; otherwise <c>false</c>.</returns>
		public bool Contains (IState<TStateTag, TEdgeTag> state) {
			return this.resultingStates.Contains (state);
		}

		/// <summary>
		/// Copies the list of <see cref="T:IState`2"/> instances to which this <see cref="T:Edge`3"/> instance maps to a compatible one-dimensional array, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the current collection.</param>
		/// <param name="index">The index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="T:ArgumentNullException"><paramref name="array" /> is null.</exception>
		/// <exception cref="T:ArgumentOutOfRangeException"><paramref name="index" /> is less than the lower bound of <paramref name="array" />. </exception>
		/// <exception cref="T:ArgumentException"><paramref name="index" /> is equal to or greater than the length of <paramref name="array" />.</exception>
		/// <exception cref="T:ArgumentException">The number of elements in the collection is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		public void CopyTo (IState<TStateTag, TEdgeTag>[] array, int index) {
			this.GetEnumerator ().CopyTo (array, index);
		}

		/// <summary>
		/// Remove the given <paramref name="item"/> from the set of <see cref="T:IState`2"/> instances this edge maps to.
		/// </summary>
		/// <param name="item">The state to remove from the set.</param>
		/// <returns><c>true</c> if the method removed something, <c>false</c> otherwise.</returns>
		public bool Remove (IState<TStateTag, TEdgeTag> item) {
			return this.resultingStates.Remove (item);
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Get an enumerator that emits all the <see cref="T:IState`2"/> instances this <see cref="T:Edge`3"/> maps to.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerator`1"/> instance that emits all the <see cref="T:IState`2"/> instance to which this <see cref="T:Edge`3"/> maps to.</returns>
		public IEnumerator<IState<TStateTag, TEdgeTag>> GetEnumerator () {
			return this.resultingStates.GetEnumerator ();
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Get an enumerator that emits all the <see cref="T:IState`2"/> instances this <see cref="T:Edge`3"/> maps to.
		/// </summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerator"/> instance that emits all the <see cref="T:IState`2"/> instance to which this <see cref="T:Edge`3"/> maps to.</returns>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
	}

	/// <summary>
	/// A special case of the <see cref="T:Edge`3"/> class where the set type is set to <see cref="T:HashSet`1"/>.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tag associated with the state.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tag associated with the edge.</typeparam>
	/// <remarks>
	/// <para>The edges have no original state such that they can be "reused" by different
	/// <see cref="T:IState`2"/> instances in order to reduce memory usage.</para>
	/// </remarks>
	public class Edge<TStateTag,TEdgeTag> : Edge<TStateTag,TEdgeTag,HashSet<IState<TStateTag,TEdgeTag>>> {

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
		public Edge (TEdgeTag tag, IEnumerable<IState<TStateTag,TEdgeTag>> resultingStates) : base(tag,resultingStates) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Edge`2"/> class with a give initial list
		/// of states to which this edge points.
		/// </summary>
		/// <param name='tag'>The tag associated with this constructed edge.</param>
		/// <param name="resultingStates">An array containing <see cref="T:IState`2"/> instances
		/// representing the states to which this edge points.</param>
		public Edge (TEdgeTag tag, params IState<TStateTag,TEdgeTag>[] resultingStates) : base(tag,resultingStates) {
		}
		#endregion
	}
}

