//
//  NullTagNondeterministicFiniteAutomatonUtils.cs
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

namespace NUtils.Automata {

	/// <summary>
	/// A set of utility methods for <see cref="T:INullTagNondeterministicFiniteAutomaton`2"/> instances.
	/// </summary>
	public static class NullTagNondeterministicFiniteAutomatonUtils {

		#region Combinating automata
		/// <summary>
		/// Concatenate this nondeterministic finite automaton with the given one into a new one such that
		/// the resulting one accepts a sequence of data if and only if it can be subdivded into two parts such
		/// that the first part is accepted by the <paramref name="automaton"/> and the second by the <paramref name="other"/> automaton.
		/// </summary>
		/// <param name="automaton">The first <see cref="T:INullTagNondeterministicFiniteAutomaton`2"/> in the concatenation process.</param>
		/// <param name="other">The second <see cref="T:INondeterministicFiniteAutomaton`2"/> in the concatenation process.</param>
		/// <remarks>
		/// <para>If the second automaton is not effective, this automaton will be cloned (not deeply, with the same <see cref="T:IState`2"/> instances).</para>
		/// <para>Although the return parameter only specifies <see cref="T:INondeterministicFiniteAutomaton`2"/>, the return type is always the same as the <paramref name="automaton"/> type.</para>
		/// <para>Evidently the <see cref="P:NullTagNondeterministicFiniteAutomatonUtils`2.NullEdge"/> is used as "epsilon"-edge.</para>
		/// </remarks>
		public static INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> Concatenate<TStateTag,TEdgeTag> (this INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> automaton, INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> other) {
			return (INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag>)automaton.Concatenate (other, automaton.NullEdge);
		}

		/// <summary>
		/// Disjunct this nondeterministic finite automaton with the given one into a new one such that
		/// the resulting one accepts a sequence of data if and only if this sequence can be accepted by the <paramref name="automaton"/>
		/// or by the <paramref name="other"/> automaton.
		/// </summary>
		/// <param name="automaton">The first <see cref="T:INullTagNondeterministicFiniteAutomaton`2"/> in the disjunction process.</param>
		/// <param name="other">The second <see cref="T:INondeterministicFiniteAutomaton`2"/> in the disjunction process.</param>
		/// <param name="startTag">The tag of an (optional) <see cref="T:IState`2"/> that must be constructed to disjunct this and the given automaton.</param>
		/// <remarks>
		/// <para>If the second automaton is not effective, this automaton will be cloned (not deeply, with the same <see cref="T:IState`2"/> instances).</para>
		/// <para>Although the return parameter only specifies <see cref="T:INondeterministicFiniteAutomaton`2"/>, the return type is always the same as the <paramref name="automaton"/> type.</para>
		/// <para>Evidently the <see cref="P:NullTagNondeterministicFiniteAutomatonUtils`2.NullEdge"/> is used as "epsilon"-edge.</para>
		/// </remarks>
		public static INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> Disjunction<TStateTag,TEdgeTag> (this INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> automaton, INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> other, TStateTag startTag) {
			return (INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag>)automaton.Disjunction (other, automaton.NullEdge, startTag);
		}

		/// <summary>
		/// Calculate the Kleene star of this nondeterministic finite automaton such that a sequence is accepted by the
		/// resulting nondeterministic finite automaton if and only if the sequence can be subdivided in (possibly zero)
		/// subsequences such that every subsequence is accepted by the <paramref name="automaton"/>.
		/// </summary>
		/// <param name="automaton">A <see cref="T:INullTagNondeterministicFiniteAutomaton`2"/> for which the Kleene star automaton is calculated.</param>
		/// <param name="startTag">The tag of an (optional) <see cref="T:IState`2"/> that must be constructed to kleen star this and the given automaton.</param>
		/// <remarks>
		/// <para>Although the return parameter only specifies <see cref="T:INondeterministicFiniteAutomaton`2"/>, the return type is always the same as the <paramref name="automaton"/> type.</para>
		/// <para>Evidently the <see cref="P:NullTagNondeterministicFiniteAutomatonUtils`2.NullEdge"/> is used as "epsilon"-edge.</para>
		/// </remarks>
		public static INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> KleeneStar<TStateTag,TEdgeTag> (this INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> automaton, TStateTag startTag) {
			return (INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag>)automaton.KleeneStar (automaton.NullEdge, startTag);
		}
		#endregion
	}
}

