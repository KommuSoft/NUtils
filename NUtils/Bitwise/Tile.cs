//
//  Tile.cs
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
	/// A structure implementing the <see cref="T:ITile"/> interface. The structure works
	/// on one <see cref="ulong"/> to make the data compact and the computations very efficient.
	/// </summary>
	public struct Tile : ITile {

		#region Fields
		/// <summary>
		/// Get the <see cref="ulong"/> that stores the data.
		/// </summary>
		/// <value>An <see cref="ulong"/> that stores the data of the entire tile.</value>
		/// <remarks>
		/// <para>The data is stored by linearisation: row-by-row. The lowest 8 bits of
		/// the data thus represent the first row.</para>
		/// </remarks>
		public ulong Data {
			get;
			private set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NUtils.Bitwise.Tile"/> struct with a given <see cref="ulong"/>
		/// that stores all the 64 bits.
		/// </summary>
		/// <param name="data">The <see cref="ulong"/> that contains the 64 bits to store.</param>
		public Tile (ulong data) {
			this.Data = data;
		}
		#endregion
	}
}

