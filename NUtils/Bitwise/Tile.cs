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
using System.Collections.Generic;

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
		#region IBitCollection implementation
		/// <summary>
		/// Get the parity of the <see cref="T:IBitCollection"/>
		/// </summary>
		/// <value><c>true</c> if the number of ones is odd; <c>false</c> otherwise.</value>
		public bool Parity {
			get {
				ulong data = this.Data;
				data ^= data >> 0x20;
				data ^= data >> 0x10;
				data ^= data >> 0x08;
				data ^= data >> 0x04;
				data &= 0x0f;
				return ((0x6996UL >> (int)data) & 0x01) == 0x01;
			}
		}

		/// <summary>
		/// Get the number of ones in this <see cref="T:IBitCollection"/>.
		/// </summary>
		/// <value>The number of ones in the bit collection.</value>
		public int NumberOfOnes {
			get {
				ulong data = this.Data;
				ulong mask;
				mask = 0x5555555555555555UL;
				data = ((data >> 0x01) & mask) + (data & mask);
				mask = 0x3333333333333333UL;
				data = ((data >> 0x02) & mask) + (data & mask);
				mask = 0x0707070707070707UL;
				data = ((data >> 0x04) & mask) + (data & mask);
				mask = 0x000F000F000F000FUL;
				data = ((data >> 0x08) & mask) + (data & mask);
				mask = 0x0000001F0000001FUL;
				data = ((data >> 0x10) & mask) + (data & mask);
				mask = 0x000000000000003FUL;
				data = ((data >> 0x20) & mask) + (data & mask);
				return (int)data;
			}
		}

		/// <summary>
		/// Get the number of zeros in this <see cref="T:IBitCollection"/>.
		/// </summary>
		/// <value>The number of zeros in the bit collection.</value>
		public int NumberOfZeros {
			get {
				return 0x40 - this.NumberOfOnes;
			}
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

		/// <summary>
		/// Encode the matrix on a 64-bit <see cref="ulong"/> such that the first eight bits
		/// are the first row, the second eight bits the second row, etc.
		/// </summary>
		/// <value>A 64-bit <see cref="ulong"/> encoded matrix.</value>
		public ulong Raster88 {
			get {
				return this.Data;
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

		/// <summary>
		/// Initializes a new instance of the <see cref="NUtils.Bitwise.Tile"/> struct by providing 8 8-bit rows.
		/// </summary>
		/// <param name="row0">The first row of the tile.</param>
		/// <param name="row1">The second row of the tile.</param>
		/// <param name="row2">The third row of the tile.</param>
		/// <param name="row3">The fourth row of the tile.</param>
		/// <param name="row4">The fifth row of the tile.</param>
		/// <param name="row5">The sixth row of the tile.</param>
		/// <param name="row6">The seventh row of the tile.</param>
		/// <param name="row7">The eight row of the tile.</param>
		public Tile (byte row0, byte row1, byte row2, byte row3, byte row4, byte row5, byte row6, byte row7) : this() {
			ulong d = row7;
			d <<= 0x08;
			d |= row6;
			d <<= 0x08;
			d |= row5;
			d <<= 0x08;
			d |= row4;
			d <<= 0x08;
			d |= row3;
			d <<= 0x08;
			d |= row2;
			d <<= 0x08;
			d |= row1;
			d <<= 0x08;
			d |= row0;
			this.Data = d;
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
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate the boolean values in this <see cref="T:Tile"/> top-to-bottom, left-to-right.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance that enumerates all the elements in
		/// the tile top-to-bottom, left-to-right.</returns>
		/// <remarks>
		/// <para>The result enumerates exactly 64 values.</para>
		/// </remarks>
		public IEnumerator<bool> GetEnumerator () {
			ulong data = this.Data;
			for (int i = 0x00; i < 0x40; i++, data >>= 0x01) {
				yield return ((data & 0x01) == 0x01);
			}
		}
		#endregion
		#region IEnumerable implementation
		/// <summary>
		/// Enumerate the boolean values in this <see cref="T:Tile"/> top-to-bottom, left-to-right.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> instance that enumerates all the elements in
		/// the tile top-to-bottom, left-to-right.</returns>
		/// <remarks>
		/// <para>The result enumerates exactly 64 values.</para>
		/// </remarks>
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator () {
			return this.GetEnumerator ();
		}
		#endregion
		#region IBitwise implementation
		/// <summary>
		/// Computes the AND-operator of this <see cref="T:IBitwise`1"/> and the given <see cref="T:ITile"/> instance.
		/// </summary>
		/// <returns>An <see cref="T:ITile"/> instance where a "bit" is the AND-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the AND-operation with.</param>
		public ITile And (ITile other) {
			return new Tile (this.Data & other.Raster88);
		}

		/// <summary>
		/// Computes the OR-operator of this <see cref="T:IBitwise`1"/> and the given <see cref="T:ITile"/> instance.
		/// </summary>
		/// <returns>An <see cref="T:ITile"/> instance where a "bit" is the OR-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the OR-operation with.</param>
		public ITile Or (ITile other) {
			return new Tile (this.Data | other.Raster88);
		}

		/// <summary>
		/// Computes the XOR-operator of this <see cref="T:IBitwise`1"/> and the given <see cref="T:ITile"/> instance.
		/// </summary>
		/// <returns>An <see cref="T:ITile"/> instance where a "bit" is the XOR-operation applied to the corresponding "bits"
		/// of this instance and the <paramref name="other"/> instance.</returns>
		/// <param name="other">The <see cref="T:IBitwise`1"/> to perform the XOR-operation with.</param>
		public ITile Xor (ITile other) {
			return new Tile (this.Data ^ other.Raster88);
		}

		/// <summary>
		/// Computes the NOT-operator of this <see cref="T:IBitwise`1"/> instance.
		/// </summary>
		/// <returns>An <see cref="T:ITile"/> instance where a "bit" is the NOT-operation applied to the corresponding "bit"
		/// of this instance.</returns>
		public ITile Not () {
			return new Tile (~this.Data);
		}
		#endregion
	}
}

