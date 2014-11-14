//
//  INondeterministicFiniteAutomaton.cs
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
using NUtils.Abstract;
using System.Collections.Generic;

namespace NUtils.Automata {

	/// <summary>
	/// An interface representing a nondeterministic finite automaton.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	/// <remarks>
	/// <para>Most implementations of this interface will require that the tags are unique per state: two
	/// states can't share the same tag.</para>
	/// </remarks>
	public interface INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : ICloneable<INondeterministicFiniteAutomaton<TStateTag,TEdgeTag>> {

		#region Obtaining properties, states and edges
		/// <summary>
		/// Get the number of states in the nondeterminstic finite state automaton.
		/// </summary>
		/// <value>The number of nodes in the nondeterminstic finite state automaton.</value>
		int NumberOfStates {
			get;
		}

		/// <summary>
		/// Get the number of edges in the nondeterministic finite state automaton.
		/// </summary>
		/// <value>The number of edges in the nondeterministic finite state automaton.</value>
		int NumberOfEdges {
			get;
		}

		/// <summary>
		/// Gets the tag of the initial state of this nondeterministic finite state automaton.
		/// </summary>
		/// <value>The tag corresponding with the initial state of this nondeterministic finite state automaton.</value>
		TStateTag InitialStateTag {
			get;
		}

		/// <summary>
		/// Get the initial state of the non-deterministic finite automaton.
		/// </summary>
		/// <value>The initial <see cref="T:IState`2"/> of this non-deterministic finite automaton.</value>
		IState<TStateTag,TEdgeTag> InitalState {
			get;
		}

		/// <summary>
		/// Enumerate all the <see cref="T:IState`2"/> instances in this nondeterministic finite state automaton.
		/// </summary>
		/// <value>An <see cref="T:IEnumerable`1"/> containing all the <see cref="T:IState`2"/> instances in this nondeterministic finite state automaton.</value>
		IEnumerable<IState<TStateTag,TEdgeTag>> States {
			get;
		}

		/// <summary>
		/// Enumerate all the tags associated with the states in this nondeterministic finite state automaton.
		/// </summary>
		/// <value>A <see cref="T:IEnumerable`1"/> containing the tags of all the states in this nondeterministic finite state automaton.</value>
		/// <remarks>
		/// <para>If two states share the same tag, duplicates will be enumerated.</para>
		/// </remarks>
		IEnumerable<TStateTag> StateTags {
			get;
		}

		/// <summary>
		/// Enumerate all the accepting <see cref="T:IState`2"/> instances in this nondeterministic finite state automaton.
		/// </summary>
		/// <value>An <see cref="T:IEnumerable`1"/> containing all the accepting <see cref="T:IState`2"/> instances in this nondeterministic finite state automaton.</value>
		IEnumerable<IState<TStateTag,TEdgeTag>> AcceptingStates {
			get;
		}

		/// <summary>
		/// Enumerate all the tags associated with the accepting states in this nondeterministic finite state automaton.
		/// </summary>
		/// <value>A <see cref="T:IEnumerable`1"/> containing the tags of all the accepting states in this nondeterministic finite state automaton.</value>
		IEnumerable<TStateTag> AcceptingStateTags {
			get;
		}

		/// <summary>
		/// Enumerate all the tags of the associated edges originating form the state(s) with the given <paramref name="statetag"/>
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains the tags of all edges originating from the state(s) associated with the given state tag.</returns>
		/// <param name="statetag">The given state tag.</param>
		IEnumerable<TEdgeTag> GetEdgeTags (TStateTag statetag);
		#endregion
		#region Checking/determining
		/// <summary>
		/// Checks if this non-deterministic finite automaton contains the given <see cref="T:IState`2"/> instance.
		/// </summary>
		/// <returns><c>true</c>, if the given <paramref name="state"/> is part of this non-deterministic finite automaton, <c>false</c> otherwise.</returns>
		/// <param name="state">The given <see cref="T:IState`2"/> to check for.</param>
		bool ContainsState (IState<TStateTag,TEdgeTag> state);

		/// <summary>
		/// Checks if there exists at least one state in this nondeterministic finite state automaton that corresponds with
		/// the given state tag that is accepting.
		/// </summary>
		/// <returns><c>true</c> if this instance is accepting the specified statetag; otherwise, <c>false</c>.</returns>
		/// <param name="statetag">Statetag.</param>
		bool IsAccepting (TStateTag statetag);

		/// <summary>
		/// Check if the given <see cref="T:IState`2"/> is accepted by this non-deterministic finite automaton.
		/// </summary>
		/// <returns><c>true</c> if the given <see cref="T:IState`2"/> instance is accepting; otherwise, <c>false</c>.</returns>
		/// <param name="state">The given state to check for.</param>
		bool IsAccepting (IState<TStateTag,TEdgeTag> state);
		#endregion
		#region Registering states and edges
		/// <summary>
		/// Register the given <paramref name="state"/> including any edges.
		/// </summary>
		/// <param name="state">The given <see cref="T:IState`2"/> that must be added.</param>
		/// <remarks>
		/// <para>If the given <paramref name="state"/> is not effective, nothing happens.</para>
		/// </remarks>
		void RegisterState (IState<TStateTag,TEdgeTag> state);

		/// <summary>
		/// Register the given <paramref name="state"/> including any edges as an accepting state.
		/// </summary>
		/// <param name="state">The given <see cref="T:IState`2"/> that must be registered as an accepting state.</param>
		/// <returns><c>true</c> if the given <paramref name="state"/> is already registered as a state and the state
		/// is thus accepted as an accepting state; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>If the given <paramref name="state"/> is not effective, nothing happens.</para>
		/// <para>If the given <paramref name="state"/> is not registered as a state of this
		/// finite automaton, nothing happens and <c>false</c> is returned.</para>
		/// </remarks>
		bool RegisterAcceptingState (IState<TStateTag,TEdgeTag> state);

		/// <summary>
		/// Register the first matching registered state with the given <paramref name="stateTag"/> including any edges as an accepting state.
		/// </summary>
		/// <param name="stateTag">The given tag of the state that must be registered as an accepting state.</param>
		/// <returns><c>true</c> if there is a state already registered with the given <paramref name="stateTag"/> as a
		/// state and the state is thus accepted as an accepting state; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>If no state is registered with the given <paramref name="stateTag"/> as a state of this finite automaton,
		/// nothing happens and <c>false</c> is returned.</para>
		/// </remarks>
		bool RegisterAcceptingState (TStateTag stateTag);

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
		IState<TStateTag,TEdgeTag> RegisterState (TStateTag stateTag);

		/// <summary>
		/// Register a state for the given <paramref name="sateTag"/>. The new state doesn't contain any
		/// edges.
		/// </summary>
		/// <param name="fromStateTag">The tag of the state from which the edge originates.</param>
		/// <param name="edgeTag">The tag of the edge that is registered.</param>
		/// <param name="toStateTag">The tag of the state to which the edge maps.</param>
		/// <returns>A <see cref="T:IEdge`2"/> instance that is either an already registered
		/// state with the given <paramref name="stateTag"/> or a new state created.</returns>
		/// <remarks>
		/// <para>If there is no state associated with the <paramref name="fromStateTag"/> or <paramref name="toStateTag"/>,
		/// states are registered (using the <see cref="M:RegisterState"/> method) for these tags.</para>
		/// <para>If there already exists an edge between the two given states with the given tag, no additional
		/// edge is registered.</para>
		/// <para>This operation is not completely specific to the given automaton: if the state is shared with another
		/// automaton, the edge will be added to all the automata.</para>
		/// </remarks>
		IEdge<TStateTag,TEdgeTag> RegisterEdge (TStateTag fromStateTag, TEdgeTag edgeTag, TStateTag toStateTag);
		#endregion
		#region Combinating automata
		/// <summary>
		/// Concatenate this nondeterministic finite automaton with the given one into a new one such that
		/// the resulting one accepts a sequence of data if and only if it can be subdivded into two parts such
		/// that the first part is accepted by this automaton and the second by the <paramref name="other"/> automaton.
		/// </summary>
		/// <param name="other">The second <see cref="T:INondeterministicFiniteAutomaton`2"/> in the concatenation process.</param>
		/// <param name="nullTag">An edge tag used for transitions without the need to consume (or "eat") any characters.</param>
		/// <remarks>
		/// <para>For some implementations, the <paramref name="nullTag"/> might be optional, in that case, any value can be passed.</para>
		/// <para>If the second automaton is not effective, this automaton will be cloned (not deeply, with the same <see cref="T:IState`2"/> instances).</para>
		/// </remarks>
		INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> Concatenate (INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> other, TEdgeTag nullTag);

		/// <summary>
		/// Disjunct this nondeterministic finite automaton with the given one into a new one such that
		/// the resulting one accepts a sequence of data if and only if this sequence can be accepted by this automaton
		/// or by the <paramref name="other"/> automaton.
		/// </summary>
		/// <param name="other">The second <see cref="T:INondeterministicFiniteAutomaton`2"/> in the disjunction process.</param>
		/// <param name="nullTag">An edge tag used for transitions without the need to consume (or "eat") any characters.</param>
		/// <param name="startTag">The tag of an (optional) <see cref="T:IState`2"/> that must be constructed to disjunct this and the given automaton.</param>
		/// <remarks>
		/// <para>For some implementations, the <paramref name="nullTag"/> might be optional, in that case, any value can be passed.</para>
		/// <para>If the second automaton is not effective, this automaton will be cloned (not deeply, with the same <see cref="T:IState`2"/> instances).</para>
		/// </remarks>
		INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> Disjunction (INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> other, TEdgeTag nullTag, TStateTag startTag);

		/// <summary>
		/// Calculate the Kleene star of this nondeterministic finite automaton such that a sequence is accepted by the
		/// resulting nondeterministic finite automaton if and only if the sequence can be subdivided in (possibly zero)
		/// subsequences such that every subsequence is accepted by this nondeterministic finite automaton.
		/// </summary>
		/// <param name="nullTag">An edge tag used for transitions without the need to consume (or "eat") any characters.</param>
		/// <param name="startTag">The tag of an (optional) <see cref="T:IState`2"/> that must be constructed to kleen star this and the given automaton.</param>
		/// <remarks>
		/// <para>For some implementations, the <paramref name="nullTag"/> might be optional, in that case, any value can be passed.</para>
		/// </remarks>
		INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> KleeneStar (TEdgeTag nullTag, TStateTag startTag);
		#endregion
	}
}