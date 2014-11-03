//
//  IOrd.cs
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

namespace NUtils.Functional {

	/// <summary>
	/// An interface representing an ordering relation between two different types of items.
	/// </summary>
	public interface IOrd<in TA,in TB> {

		/// <summary>
		/// Compare the two given items and returns a <see cref="Ordering"/> containing the result of
		/// the ordering.
		/// </summary>
		/// <param name="a">The first item to compare.</param>
		/// <param name="b">The second item to compare.</param>
		Ordering Compare (TA a, TB b);

		Func<TB,Ordering> Compare (TA a);
	}

	/// <summary>
	/// An interface representing an ordering relation bwetween items of the same type.
	/// </summary>
	public interface IOrd <in T> : IOrd<T,T> {

	}
}

