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
using System.Text;

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
		#region ITile implementation
		/// <summary>
		/// Get a <see cref="T:ITile"/> that is the transpose of this <see cref="T:ITile"/>.
		/// </summary>
		/// <value>A <see cref="T:ITile"/> that is the transpose of this <see cref="T:ITile"/>.</value>
		/// <remarks>
		/// <para>A transpose means that the <c>i,j</c>-th value of this <see cref="T:ITile"/>
		/// is the <c>j,i</c>-th value of the returning <see cref="T:ITile"/>.</para>
		/// </remarks>
		public ITile Transpose {
			get {
				ulong data = this.Data;
				return new Tile (
					((data & 0x8040201008040201UL)) |
					((data & 0x0080402010080402UL) << 0x07) |
					((data & 0x0000804020100804UL) << 0x0e) |
					((data & 0x0000008040201008UL) << 0x15) |
					((data & 0x0000000080402010UL) << 0x1c) |
					((data & 0x0000000000804020UL) << 0x23) |
					((data & 0x0000000000008040UL) << 0x2a) |
					((data & 0x0000000000000080UL) << 0x31) |
					((data & 0x4020100804020100UL) >> 0x07) |
					((data & 0x2010080402010000UL) >> 0x0e) |
					((data & 0x1008040201000000UL) >> 0x15) |
					((data & 0x0804020100000000UL) >> 0x1c) |
					((data & 0x0402010000000000UL) >> 0x23) |
					((data & 0x0201000000000000UL) >> 0x2a) |
					((data & 0x0100000000000000UL) >> 0x31));
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NUtils.Bitwise.Tile"/> struct with a given <see cref="ulong"/>
		/// that stores all the 64 bits.
		/// </summary>
		/// <param name="data">The <see cref="ulong"/> that contains the 64 bits to store.</param>
		public Tile (ulong data) : this() {
			this.Data = data;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="Tile"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="Tile"/>.</returns>
		public override string ToString () {
			ulong tile = this.Data;
			StringBuilder sb = new StringBuilder ();
			for (int item = 0x00; item < 0x40;) {
				for (; (item&0x07) != 0x00; item++, tile >>= 0x01) {
					sb.Append ((char)(0x30 | (tile & 0x01)));
				}
				sb.AppendLine ();
			}
			return sb.ToString ();
		}
		#endregion
	}
}

