//
//  NondeterministicFiniteAutomaton.cs
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
using NUtils.Automata;
using NUtils.Collections;

namespace NUtils {

	/// <summary>
	/// An implementation of the <see cref="T:INondeterministicFiniteAutomaton`2"/> interface that uses a number
	/// of nodes that are linked together.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	/// <typeparam name='TCollection'>The type of collection used to store different states given they share the same tag.</typeparam>
	public class NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,TCollection> : INondeterministicFiniteAutomaton<TStateTag,TEdgeTag>
	    where TCollection : ICollection<IState<TStateTag,TEdgeTag>>, new() {

		#region Fields
		/// <summary>
		/// A <see cref="T:Register`2"/> that maps the <typeparamref name="TStateTag"/> instances on the <see cref="T:IState`2"/> instances.
		/// </summary>
		private readonly Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> stateDictionary = new Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> (x => x.Tag);
		/// <summary>
		/// The initial <see cref="T:IState`2"/> of this non-deterministic finite automaton.
		/// </summary>
		private readonly IState<TStateTag,TEdgeTag> initialState;
		/// <summary>
		/// A <see cref="T:Register`2"/> that maps the <typeparamref name="TStateTag"/> instances on the accepting <see cref="T:IState`2"/> instances of this non-deterministic finite automaton.
		/// </summary>
		private readonly Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> acceptingStateDictionary = new Register<TStateTag,IState<TStateTag,TEdgeTag>,TCollection> (x => x.Tag);
		#endregion
		#region INondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Get the number of states in the nondeterminstic finite state automaton.
		/// </summary>
		/// <value>The number of nodes in the nondeterminstic finite state automaton.</value>
		public int NumberOfStates {
			get {
				return this.stateDictionary.Count;
			}
		}

		/// <summary>
		/// Get the number of edges in the nondeterministic finite state automaton.
		/// </summary>
		/// <value>The number of edges in the nondeterministic finite state automaton.</value>
		public int NumberOfEdges {
			get {
				return this.stateDictionary.Values.Sum (x => x.NumberOfEdges);
			}
		}

		/// <summary>
		/// Gets the tag of the initial state of this nondeterministic finite state automaton.
		/// </summary>
		/// <value>The tag corresponding with the initial state of this nondeterministic finite state automaton.</value>
		public TStateTag InitialStateTag {
			get {
				return this.initialState.Tag;
			}
		}

		/// <summary>
		/// Get the initial state of the non-deterministic finite automaton.
		/// </summary>
		/// <value>The initial <see cref="T:IState`2"/> of this non-deterministic finite automaton.</value>
		public IState<TStateTag, TEdgeTag> InitalState {
			get {
				return this.initialState;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:INondeterministicFiniteAutomaton`3"/> class.
		/// </summary>
		public NondeterministicFiniteAutomaton (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialState, IEnumerable<TStateTag> acceptingStates) {
			this.constructNFA (edges, initialState, acceptingStates);
		}
		#endregion
		#region private methods, for programming convenience
		private void constructNFA (IEnumerable<Tuple<TStateTag, TEdgeTag, TStateTag>> edges, TStateTag initialState, IEnumerable<TStateTag> acceptingStates) {
			IState<TStateTag,TEdgeTag> sf, st;
			foreach (Tuple<TStateTag,TEdgeTag,TStateTag> edge in edges) {
				sf = RegisterState (edge.Item1);
				st = RegisterState (edge.Item3);

			}
		}
		#endregion
		/// <summary>
		/// Register a state for the given <paramref name="sateTag"/>. The new state doesn't contain any
		/// edges.
		/// </summary>
		/// <param name="stateTag">The tag of the new state to create.</param>
		/// <returns>A <see cref="T:IState`2"/> instance that is either an already registered
		/// state with the given <paramref name="stateTag"/> or a new state created.</returns>
		/// <remarks>
		/// <para>If there exists already a state for the given <paramref name="tag"/>, no new state is initialized.</para>
		/// </remarks>
		public IState<TStateTag,TEdgeTag> RegisterState (TStateTag stateTag) {
			IState<TStateTag,TEdgeTag> state;
			if (!this.stateDictionary.TryGetValue (stateTag, out state)) {
				state = new State<TStateTag,TEdgeTag> (stateTag);
				this.stateDictionary.Add (state);
			}
			return state;
		}
		#region INondeterministicFiniteAutomaton implementation
		/// <summary>
		/// Enumerate all the tags associated with the states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the states in this nondeterministic finite state automaton.</returns>
		/// <remarks>
		/// <para>If two states share the same tag, duplicates will be enumerated.</para>
		/// </remarks>
		public IEnumerable<TStateTag> StateTags () {
			return this.stateDictionary.Values.Select (x => x.Tag);
		}

		/// <summary>
		/// Enumerate all the tags associated with the accepting states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the accepting states in this nondeterministic finite state automaton.</returns>
		public IEnumerable<TStateTag> AcceptingStateTags () {
			return this.acceptingStateDictionary.Values.Select (x => x.Tag);
		}

		/// <summary>
		/// Enumerate all the tags of the associated edges originating form the state(s) with the given <paramref name="statetag"/>
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains the tags of all edges originating from the state(s) associated with the given state tag.</returns>
		/// <param name="statetag">The given state tag.</param>
		public IEnumerable<TEdgeTag> GetEdgeTags (TStateTag statetag) {
			return this.stateDictionary.Values.SelectMany (x => x.Edges).Select (x => x.Tag);
		}

		/// <summary>
		/// Checks if there exists at least one state in this nondeterministic finite state automaton that corresponds with
		/// the given state tag that is accepting.
		/// </summary>
		/// <returns><c>true</c> if this instance is accepting the specified statetag; otherwise, <c>false</c>.</returns>
		/// <param name="statetag">Statetag.</param>
		public bool IsAccepting (TStateTag statetag) {
			return acceptingStateDictionary.ContainsKey (statetag);
		}

		/// <summary>
		/// Check if the given <see cref="T:IState`2"/> is accepted by this non-deterministic finite automaton.
		/// </summary>
		/// <returns><c>true</c> if the given <see cref="T:IState`2"/> instance is accepting; otherwise, <c>false</c>.</returns>
		/// <param name="state">The given state to check for.</param>
		public bool IsAccepting (IState<TStateTag, TEdgeTag> state) {
			return (state != null && this.acceptingStateDictionary.Contains (new KeyValuePair<TStateTag,IState<TStateTag,TEdgeTag>> (state.Tag, state)));
		}

		/// <summary>
		/// Checks if this non-deterministic finite automaton contains the given <see cref="T:IState`2"/> instance.
		/// </summary>
		/// <returns><c>true</c>, if the given <paramref name="state"/> is part of this non-deterministic finite automaton, <c>false</c> otherwise.</returns>
		/// <param name="state">The given <see cref="T:IState`2"/> to check for.</param>
		public bool ContainsState (IState<TStateTag, TEdgeTag> state) {
			return (state != null && this.stateDictionary.Contains (new KeyValuePair<TStateTag,IState<TStateTag,TEdgeTag>> (state.Tag, state)));
		}
		#endregion
	}

	/// <summary>
	/// A variant of the <see cref="T:NondeterministicFiniteAutomaton`3"/> class, but with
	/// a default type for the type of collection to hold multiple states for the same tag.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	public class NondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : NondeterministicFiniteAutomaton<TStateTag,TEdgeTag,List<IState<TStateTag,TEdgeTag>>> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:INondeterministicFiniteAutomaton`3"/> class.
		/// </summary>
		public NondeterministicFiniteAutomaton (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges, TStateTag initialState, IEnumerable<TStateTag> acceptingStates) : base(edges, initialState, acceptingStates) {
		}
		#endregion
	}
}

