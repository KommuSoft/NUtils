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
	public interface INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> {

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
		/// Enumerate all the tags associated with the states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the states in this nondeterministic finite state automaton.</returns>
		/// <remarks>
		/// <para>If two states share the same tag, duplicates will be enumerated.</para>
		/// </remarks>
		IEnumerable<TStateTag> StateTags ();

		/// <summary>
		/// Enumerate all the tags associated with the accepting states in this nondeterministic finite state automaton.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the tags of all the accepting states in this nondeterministic finite state automaton.</returns>
		IEnumerable<TStateTag> AcceptingStateTags ();

		/// <summary>
		/// Enumerate all the tags of the associated edges originating form the state(s) with the given <paramref name="statetag"/>
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains the tags of all edges originating from the state(s) associated with the given state tag.</returns>
		/// <param name="statetag">The given state tag.</param>
		IEnumerable<TEdgeTag> GetEdgeTags (TStateTag statetag);

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
	}
}