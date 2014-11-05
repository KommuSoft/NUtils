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
using NUtils.Abstract;

namespace NUtils {

	/// <summary>
	/// An implementation of the <see cref="INondeterministicFiniteAutomaton"/> interface that uses a number
	/// of nodes that are linked together.
	/// </summary>
	/// <typeparam name='TNodeTag'>The type of the tags that are assigned to the nodes.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tags that are assigned to the edges.</typeparam>
	public class NondeterministicFiniteAutomaton<TNodeTag,TEdgeTag> {

		#region Inner classes
		private class Node : TagBase<TNodeTag> {

		}

		private class Edge : TagBase<TEdgeTag> {

		}
		#endregion
		#region Constructors
		public NondeterministicFiniteAutomaton () {
		}
		#endregion
	}
}

