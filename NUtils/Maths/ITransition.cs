//
//  ITransition.cs
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

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying a transition function on indices. Such function is guaranteed to
	/// be injective but not surjective.
	/// </summary>
	public interface ITransition : ILength, IDigraph, IEnumerable<int> {

		/// <summary>
		/// Gets the index on which the given index maps.
		/// </summary>
		/// <returns>The target index of the given source <paramref name="index"/>.</returns>
		/// <param name="index">The given index from which the transition originates.</param>
		int GetTransitionOfIndex (int index);
	}
}