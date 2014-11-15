//
//  NullTagNondeterministicFiniteAutomaton.cs
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
using NUtils.Abstract;

namespace NUtils.Automata {

	/// <summary>
	/// An implementation of the <see cref="T:INullTagNondeterministicFiniteAutomaton`2"/> interface representing a
	/// nondeterministic finite automaton that has a definition for a special <typeparamref name="TEdgeTag"/>
	/// that represents the "epsilon"-transition: a transition that doesn't eat any item of the sequence.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	/// <typeparam name='TCollection'>The type of collection used to store different states given they share the same tag.</typeparam>
	public class NullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> : NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection>, INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag>
	    where TCollection : ICollection<IState<TStateTag,TEdgeTag>>, new() {

		#region INullTagNondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Get the tag used for an "epsilon"-edge.
		/// </summary>
		/// <value>A <typeparamref name="TEdgeTag"/> that represents the tag assigned to edges that don't "eat" a sequence item.</value>
		public TEdgeTag NullEdge {
			get;
			private set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/> class by copying the states from the given <paramref name="origin"/>.
		/// </summary>
		/// <param name="origin">The given <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/> from which the data is copied, must be effective.</param>
		/// <remarks>
		/// <para>The <see cref="T:IState`2"/> instances in the given <paramref name="origin"/> are not cloned: they are the same and modifications
		/// to the states in the original automaton can have effects in the new automaton and vice-versa.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">The given automaton must be effective.</exception>
		protected NullTagNondeterministicFiniteAutomaton (NullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> origin) : base(origin) {
			origin.NullEdge = origin.NullEdge;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/> class based on tags with a given list of <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <param name="nullEdge">The tag associated with the epsilon edge: an edge that doesn't "eat" any item in the verifying sequence.</param>
		/// <remarks>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NullTagNondeterministicFiniteAutomaton (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags, TEdgeTag nullEdge) : base(edges,initialStateTag,acceptingStateTags) {
			this.NullEdge = nullEdge;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/> class based on tags with a given list of <paramref name="states"/>, <paramref name="edges"/>, the tag of the initial state (<paramref name="initialStateTag"/>) and a list of accepting states (<paramref name="acceptingStateTags"/>).
		/// </summary>
		/// <param name="states">The list of <see cref="T:IState`2"/> instances to be added to the new <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="edges">The list of <see cref="T:Tuple`3"/> instances representing edges to be added to the new <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/>.</param>
		/// <param name="initialStateTag">The tag of the state that is the initial state.</param>
		/// <param name="acceptingStateTags">The list of tags representing the accepting states.</param>
		/// <param name="nullEdge">The tag associated with the epsilon edge: an edge that doesn't "eat" any item in the verifying sequence.</param>
		/// <remarks>
		/// <para>If the given list of <paramref name="states"/> is not effective, then no states are added.</para>
		/// <para>Non-effective <paramref name="states"/> will not be added to the list of states.</para>
		/// <para>Edges with tags that are not associated with any state will result in the creation of new <see cref="T:IState`2"/> instances in this <see cref="T:NullTagNondeterministicFiniteAutomaton`3"/>.</para>
		/// <para>If the given <paramref name="initialStateTag"/> does not correspond with any registered state, a <see cref="T:IState`2"/> instance will be created.</para>
		/// <para>For each tag in the list of <paramref name="acceptingStateTags"/>, the "first" <see cref="T:IState`2"/> that matches the tag will be picked, if no <see cref="T:IState`2"/> instance matches the given tag, the tag is ignored.</para>
		/// </remarks>
		public NullTagNondeterministicFiniteAutomaton (IEnumerable<IState<TStateTag,TEdgeTag>> states, IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialStateTag, IEnumerable<TStateTag> acceptingStateTags, TEdgeTag nullEdge) : base(states,edges,initialStateTag,acceptingStateTags) {
			this.NullEdge = nullEdge;
		}
		#endregion
		#region ICloneable implementation
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		INullTagNondeterministicFiniteAutomaton<TStateTag, TEdgeTag> ICloneable<INullTagNondeterministicFiniteAutomaton<TStateTag, TEdgeTag>>.Clone () {
			return new NullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> (this);
		}
		#endregion
		#region Clone method
		/// <summary>
		/// Generate a clone of this instance: a different instance with the same data.
		/// </summary>
		/// <returns>A new object that is a copy of this instance</returns>
		/// <remarks>
		/// <para>The resulting clone is - unless specified otherwise - not deep.</para>
		/// </remarks>
		public override NondeterministicFiniteAutomaton<TStateTag, TEdgeTag,TCollection> Clone () {
			return new NullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> (this);
		}
		#endregion
	}
}

