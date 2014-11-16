//
//  IDispatcherNondeterministicFiniteAutomaton.cs
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

namespace NUtils.Automata {

	/// <summary>
	/// An interface representing an <see cref="T:INondeterministicFiniteAutomaton`2"/> with an embedded dispatcher
	/// to allocate new state tags.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	/// <remarks>
	/// <para>Most implementations of this interface will require that the tags are unique per state: two
	/// states can't share the same tag.</para>
	/// <para>One can still define statetags oneself, but must be cautious when the dispatcher generates an already
	/// allocated tag.</para>
	/// </remarks>
	public interface IDispatcherNondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : INondeterministicFiniteAutomaton<TStateTag,TEdgeTag>, ICloneable<IDispatcherNondeterministicFiniteAutomaton<TStateTag,TEdgeTag>> {

		/// <summary>
		/// Get a <see cref="T:IDispatcher`1"/> that allocates new state tags.
		/// </summary>
		/// <value>A dispatcher for new state allocation.</value>
		IDispatcher<TStateTag> StateTagDispatcher {
			get;
		}
	}
}

