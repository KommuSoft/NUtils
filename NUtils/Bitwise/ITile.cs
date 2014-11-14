//
//  ITile.cs
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

namespace NUtils.Bitwise {

	/// <summary>
	/// A bit tile: an 8x8 Boolean matrix. This can be useful for matrix calculations.
	/// </summary>
	public interface ITile {

		/// <summary>
		/// Get a <see cref="T:ITile"/> that is the transpose of this <see cref="T:ITile"/>.
		/// </summary>
		/// <value>A <see cref="T:ITile"/> that is the transpose of this <see cref="T:ITile"/>.</value>
		/// <remarks>
		/// <para>A transpose means that the <c>i,j</c>-th value of this <see cref="T:ITile"/>
		/// is the <c>j,i</c>-th value of the returning <see cref="T:ITile"/>.</para>
		/// </remarks>
		ITile Transpose {
			get;
		}
	}
}

