//
//  INullTagINondeterministicFiniteAutomaton.cs
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
	/// An interface representing a nondeterministic finite automaton that has a definition for
	/// a special <typeparamref name="TEdgeTag"/> that represents the "epsilon"-transition: a transition
	/// that doesn't eat any item of the sequence.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	public interface INullTagNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> {

		/// <summary>
		/// Get the tag used for an "epsilon"-edge.
		/// </summary>
		/// <value>A <typeparamref name="TEdgeTag"/> that represents the tag assigned to edges that don't "eat" a sequence item.</value>
		TEdgeTag NullEdge {
			get;
		}
	}
}

