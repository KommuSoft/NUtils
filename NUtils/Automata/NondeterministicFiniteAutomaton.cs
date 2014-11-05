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
using NUtils.Abstract;
using NUtils.Automata;

namespace NUtils {

	/// <summary>
	/// An implementation of the <see cref="T:INondeterministicFiniteAutomaton`2"/> interface that uses a number
	/// of nodes that are linked together.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	public class NondeterministicFiniteAutomaton<TStateTag,TEdgeTag> : INondeterministicFiniteAutomaton<TStateTag,TEdgeTag> {

		#region Fields
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NondeterministicFiniteAutomaton`2"/> class.
		/// </summary>
		public NondeterministicFiniteAutomaton () {
		}
		#endregion
		#region INondeterministicFiniteAutomaton implementation
		public IEnumerable<TStateTag> StateTags () {
			throw new NotImplementedException ();
		}

		public IEnumerable<TStateTag> AcceptingStateTags () {
			throw new NotImplementedException ();
		}

		public IEnumerable<TEdgeTag> GetEdgeTags (TStateTag statetag) {
			throw new NotImplementedException ();
		}

		public bool IsAccepting (TStateTag statetag) {
			throw new NotImplementedException ();
		}

		public int NumberOfStates {
			get {
				throw new NotImplementedException ();
			}
		}

		public int NumberOfEdges {
			get {
				throw new NotImplementedException ();
			}
		}

		public TStateTag InitialStateTag {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

