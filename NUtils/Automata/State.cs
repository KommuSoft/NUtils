//
//  State.cs
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
	/// A basic implementation of the <see cref="T:IState`2"/> interface.
	/// </summary>
	/// <typeparam name='TStateTag'>The type of the tag associated with the state.</typeparam>
	/// <typeparam name='TEdgeTag'>The type of the tag associated with the edge.</typeparam>
	/// <remarks>
	/// <para>The states are not inherently accepting or initial since states can be "reused" by another
	/// automata that provides a different purpose for this states.</para>
	/// </remarks>
	public class State<TStateTag,TEdgeTag> : TagBase<TStateTag>, IState<TStateTag,TEdgeTag> {

		#region Fields
		private readonly IDictionary<TEdgeTag,IEdge<TStateTag,TEdgeTag>> edgeMap = new Dictionary<TEdgeTag,IEdge<TStateTag,TEdgeTag>> ();
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:State`2"/> class with a given tag.
		/// </summary>
		/// <param name="tag">The given tag to associate with this state.</param>
		public State (TStateTag tag) : base(tag) {
		}
		#endregion
		#region IState implementation
		public IEnumerable<IEdge<TStateTag, TEdgeTag>> TaggedEdges (TEdgeTag edgetag) {
			throw new NotImplementedException ();
		}

		public IEnumerable<IEdge<TStateTag, TEdgeTag>> Edges {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

