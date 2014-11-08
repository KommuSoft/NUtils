//
//  FiniteAutomatonUtils.cs
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

namespace NUtils.Automata {

	/// <summary>
	/// A set of utility methods for finite state machines.
	/// </summary>
	public static class FiniteAutomatonUtils {

		#region Register in bulk
		/// <summary>
		/// Register the list of edges with a list of tuples containing the tags of the initial and final state
		/// as well as the tag for the edge to create.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="edges">A <see cref="T:IEnumerable`1"/> of <see cref="T:Tuple`3"/> instance containing
		/// the tag of the initial state, the tag of the edge to create and the tag of the final state.</param>
		/// <remarks>
		/// <para>If there is no state associated with the <paramref name="fromStateTag"/> or <paramref name="toStateTag"/>,
		/// states are registered (using the <see cref="M:RegisterState"/> method) for these tags.</para>
		/// <para>If there already exists an edge between the two given states with the given tag, no additional
		/// edge is registered.</para>
		/// <para>If the given list of <paramref name="edges"/> or the <paramref name="nfa"/> is not effective, nothing happens.</para>
		/// <para>This operation is not completely specific to the given automaton: if the state is shared with another
		/// automaton, the edge will be added to all the automata.</para>
		/// </remarks>
		public static void RegisterEdges<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>> edges) {
			if (edges != null && nfa != null) {
				foreach (Tuple<TStateTag,TEdgeTag,TStateTag> edge in edges) {
					nfa.RegisterEdge (edge.Item1, edge.Item2, edge.Item3);
				}
			}
		}

		/// <summary>
		/// Register the array of edges with a list of tuples containing the tags of the initial and final state
		/// as well as the tag for the edge to create.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="edges">An array of <see cref="T:Tuple`3"/> instance containing
		/// the tag of the initial state, the tag of the edge to create and the tag of the final state.</param>
		/// <remarks>
		/// <para>If there is no state associated with the <paramref name="fromStateTag"/> or <paramref name="toStateTag"/>,
		/// states are registered (using the <see cref="M:RegisterState"/> method) for these tags.</para>
		/// <para>If there already exists an edge between the two given states with the given tag, no additional
		/// edge is registered.</para>
		/// <para>If the given list of <paramref name="edges"/> or the <paramref name="nfa"/> is not effective, nothing happens.</para>
		/// <para>This operation is not completely specific to the given automaton: if the state is shared with another
		/// automaton, the edge will be added to all the automata.</para>
		/// </remarks>
		public static void RegisterEdges<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, params Tuple<TStateTag,TEdgeTag,TStateTag>[] edges) {
			RegisterEdges<TStateTag,TEdgeTag> (nfa, (IEnumerable<Tuple<TStateTag,TEdgeTag,TStateTag>>)edges);
		}

		/// <summary>
		/// Register the list of given <paramref name="states"/> including any edges.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="states">A <see cref="T:IEnumerable`1"/> of <see cref="T:IState`2"/> instances that must be added.</param>
		/// <remarks>
		/// <para>If the given <paramref name="nfa"/> or <paramref name="states"/> are not effective, nothing happens.</para>
		/// </remarks>
		public static void RegisterStates<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, IEnumerable<IState<TStateTag,TEdgeTag>> states) {
			if (nfa != null && states != null) {
				foreach (IState<TStateTag,TEdgeTag> state in states) {
					nfa.RegisterState (state);
				}
			}
		}

		/// <summary>
		/// Register the array of given <paramref name="states"/> including any edges.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="states">An array of <see cref="T:IState`2"/> instances that must be added.</param>
		/// <remarks>
		/// <para>If the given <paramref name="nfa"/> or <paramref name="states"/> are not effective, nothing happens.</para>
		/// </remarks>
		public static void RegisterStates<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, params IState<TStateTag,TEdgeTag>[] states) {
			RegisterStates<TStateTag,TEdgeTag> (nfa, (IEnumerable<IState<TStateTag,TEdgeTag>>)states);
		}

		/// <summary>
		/// Register the list of given <paramref name="states"/> including any edges as accepting states.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="states">A <see cref="T:IEnumerable`1"/> of <see cref="T:IState`2"/> instances that must be registered
		/// as accepting states.</param>
		/// <remarks>
		/// <para>If the given <paramref name="nfa"/> or <paramref name="states"/> are not effective, nothing happens.</para>
		/// <para>Only <see cref="T:IState`2"/> instances that were registered as states first, are registered as accepting states.</para>
		/// </remarks>
		public static void RegisterAcceptingStates<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, IEnumerable<IState<TStateTag,TEdgeTag>> states) {
			if (nfa != null && states != null) {
				foreach (IState<TStateTag,TEdgeTag> state in states) {
					nfa.RegisterAcceptingState (state);
				}
			}
		}

		/// <summary>
		/// Register the array of given <paramref name="states"/> including any edges as accepting states.
		/// </summary>
		/// <param name="nfa">The finite state automaton to which the edges must be added.</param>
		/// <param name="states">An array of <see cref="T:IState`2"/> instances that must be registered as as accepting states.</param>
		/// <remarks>
		/// <para>If the given <paramref name="nfa"/> or <paramref name="states"/> are not effective, nothing happens.</para>
		/// <para>Only <see cref="T:IState`2"/> instances that were registered as states first, are registered as accepting states.</para>
		/// </remarks>
		public static void RegisterAcceptingStates<TStateTag,TEdgeTag> (this INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> nfa, params IState<TStateTag,TEdgeTag>[] states) {
			RegisterAcceptingStates<TStateTag,TEdgeTag> (nfa, (IEnumerable<IState<TStateTag,TEdgeTag>>)states);
		}
		#endregion
	}
}

