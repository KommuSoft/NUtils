//
//  StateUtils.cs
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
	/// Utility functions for <see cref="T:IState`2"/> instances.
	/// </summary>
	public static class StateUtils {

		#region Blankets
		/// <summary>
		/// Calculate the blanket of the given <see cref="T:IState`2"/> for the given <paramref name="blankettag"/>.
		/// </summary>
		/// <returns>The blanket.</returns>
		/// <param name="current">Current.</param>
		/// <param name="blankettag">Blankettag.</param>
		/// <typeparam name="TStateTag">The 1st type parameter.</typeparam>
		/// <typeparam name="TEdgeTag">The 2nd type parameter.</typeparam>
		/// <remarks>
		/// <para>A blanket is the set of states that can be reached by applying zero, one or more times the given tag onto the given state.</para>
		/// <para>Each state is included in its own blanket.</para>
		/// </remarks>
		public static IEnumerable<IState<TStateTag,TEdgeTag>> GetBlanket<TStateTag,TEdgeTag> (this IState<TStateTag,TEdgeTag> current, TEdgeTag blankettag) {
			Queue<IState<TStateTag,TEdgeTag>> todo = new Queue<IState<TStateTag, TEdgeTag>> ();
			yield return current;
		}
		#endregion
	}
}

