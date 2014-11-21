//
//  IWeight.cs
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

namespace NUtils.Abstract {

	/// <summary>
	/// An interface describing that an object has a certain weight. This weight can determine for instance
	/// the probability of an item getting selected given.
	/// </summary>
	/// <remarks>
	/// <para>A weight is a real value that is larger than zero (bound included).</para>
	/// <para>One could have used an integer as well, but this causes problems if one wishes to allocate
	/// a weight between two other weights that differ by one.</para>
	/// </remarks>
	public interface IWeight {

		/// <summary>
		/// Get the weight of this <see cref="T:IWeight"/> instance.
		/// </summary>
		/// <value>The weight of this instance.</value>
		/// <remarks>
		/// <para>A weight is a real value that is larger than zero (bound included).</para>
		/// <para>One could have used an integer as well, but this causes problems if one wishes to allocate
		/// a weight between two other weights that differ by one.</para>
		/// </remarks>
		double Weight {
			get;
		}
	}
}

