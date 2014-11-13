using System;
using System.Text;
using System.Collections.Generic;

namespace NUtils.Bitwise {

	/// <summary>
	/// A utility class that specifies usefull operations on bit strings, bit tiles, etc.
	/// </summary>
	/// <remarks>
	/// <para>A bit tile is defined as a 64 bit string that is ordered as a 8x8 matrix (left to right, top to down).</para>
	/// </remarks>
	public static class BitUtils {

		#region Constants
		/// <summary>
		/// A constant 64-bit string that represents the identity matrix for a bit tile.
		/// </summary>
		public const ulong ITile = 0x8040201008040201UL;
		/// <summary>
		/// A constant 64-bit string where the last 8 bits are set.
		/// </summary>
		public const ulong L08ULong = 0x00000000000000FFUL;
		/// <summary>
		/// A constant 64-bit string where the last 16 bits are set.
		/// </summary>
		public const ulong L16ULong = 0x000000000000FFFFUL;
		/// <summary>
		/// A constant 64-bit string where the last 32 bits are set.
		/// </summary>
		public const ulong L32ULong = 0x00000000FFFFFFFFUL;
		/// <summary>
		/// A constant 64-bit string where all the bits are set.
		/// </summary>
		public const ulong L64ULong = 0xFFFFFFFFFFFFFFFFUL;
		/// <summary>
		/// A constant 64-bit string where in each group of 8 bits, the last bit is set.
		/// </summary>
		public const ulong I8S8L1ULong = 0x0101010101010101UL;
		#endregion
		#region Static utility functions
		/// <summary>
		/// Calculate the transposed version of the tile: the rows are read as columns and vice versa.
		/// </summary>
		/// <returns>A bit tile that is the transpose of the <paramref name="original"/> bit tile.</returns>
		/// <param name="origin">The original bit tile to transpose.</param>
		public static ulong TransposeTile (ulong origin) {
			return
				((origin & 0x8040201008040201)) |
				((origin & 0x0080402010080402) << 0x07) |
				((origin & 0x0000804020100804) << 0x0e) |
				((origin & 0x0000008040201008) << 0x15) |
				((origin & 0x0000000080402010) << 0x1c) |
				((origin & 0x0000000000804020) << 0x23) |
				((origin & 0x0000000000008040) << 0x2a) |
				((origin & 0x0000000000000080) << 0x31) |
				((origin & 0x4020100804020100) >> 0x07) |
				((origin & 0x2010080402010000) >> 0x0e) |
				((origin & 0x1008040201000000) >> 0x15) |
				((origin & 0x0804020100000000) >> 0x1c) |
				((origin & 0x0402010000000000) >> 0x23) |
				((origin & 0x0201000000000000) >> 0x2a) |
				((origin & 0x0100000000000000) >> 0x31);
		}

		/// <summary>
		/// Copies the row with the given <paramref name="row"/> index to all the other rows in the given <paramref name="origin"/> bit tile.
		/// </summary>
		/// <returns>A bit tile that contains eight rows who all have the same values as the row with the given <paramref name="row"/> index.</returns>
		/// <param name="origin">The original bit tile.</param>
		/// <param name="row">The index of the row to copy.</param>
		public static ulong Copy8Row (ulong origin, int row = 0x00) {
			int shft = row << 0x03;
			ulong mask = ((0xffUL << shft) & origin) >> shft;
			mask |= mask << 0x20;
			mask |= mask << 0x10;
			mask |= mask << 0x08;
			return mask;
		}

		/// <summary>
		/// Compress the values of each row into the first column by performing an AND-operation on the values.
		/// </summary>
		/// <returns>A bit tile where the first column contains the AND-operations performed on every row.</returns>
		/// <param name="origin">The original tile to transform.</param>
		/// <remarks>
		/// <para>The other columns are guaranteed to contain zeros after the operation.</para>
		/// </remarks>
		public static ulong AndCompress8Rows (ulong origin) {
			origin &= (origin & 0xF0F0F0F0F0F0F0F0UL) >> 0x04;
			origin &= (origin & 0x0C0C0C0C0C0C0C0CUL) >> 0x02;
			origin &= (origin & 0x0202020202020202UL) >> 0x01;
			return origin;
		}

		/// <summary>
		/// Compress the values of each row into the first column by performing an OR-operation on the values.
		/// </summary>
		/// <returns>A bit tile where the first column contains the OR-operations performed on every row.</returns>
		/// <param name="origin">The original tile to transform.</param>
		/// <remarks>
		/// <para>The content of the other columns is modified as well, the behavior is unknown. One can mask
		/// these columns out, due to performance reasons, this is not done automatically.</para>
		/// </remarks>
		public static ulong OrCompress8Rows (ulong origin) {
			origin |= (origin & 0xF0F0F0F0F0F0F0F0UL) >> 0x04;
			origin |= (origin & 0x0C0C0C0C0C0C0C0CUL) >> 0x02;
			origin |= (origin & 0x0202020202020202UL) >> 0x01;
			return origin;
		}

		/// <summary>
		/// Calculate the bitwise or operation per row such
		/// that given a bit in the <c>i</c>-th row is true,
		/// the lowest column of the <c>i</c>-th returning tile
		/// is true.
		/// </summary>
		/// <returns>A tile where the first column is the bitwise or operator of all the columns at the specified row.</returns>
		/// <param name="origin">The tile on which the operation is performed.</param>
		public static ulong Or8Rows (ulong origin) {
			ulong mask = origin & 0x0101010101010101UL;
			mask |= (origin & 0x0202020202020202UL) >> 0x01;
			mask |= (origin & 0x0404040404040404UL) >> 0x02;
			mask |= (origin & 0x0808080808080808UL) >> 0x03;
			mask |= (origin & 0x1010101010101010UL) >> 0x04;
			mask |= (origin & 0x2020202020202020UL) >> 0x05;
			mask |= (origin & 0x4040404040404040UL) >> 0x06;
			mask |= (origin & 0x8080808080808080UL) >> 0x07;
			return mask;
		}

		/// <summary>
		/// Given a tile, the given <paramref name="col"/> column, is copied to all rows of the resulting tile.
		/// </summary>
		/// <returns>A tile where the given <paramref name="col"/> of the <paramref name="origin"/> tile is copied to all other rows.</returns>
		/// <param name="origin">The original tile that contains the data that must be copied.</param>
		/// <param name="col">The index of the column that must be copied.</param>
		public static ulong Copy8Col (ulong origin, int col = 0x00) {
			ulong mask = ((0x0101010101010101UL << col) & origin) >> col;
			mask |= mask << 0x04;
			mask |= mask << 0x02;
			mask |= mask << 0x01;
			return mask;
		}

		/// <summary>
		/// A coroutine that packs 64 booleans into a single <see cref="ulong"/>. In case the booleans
		/// are exhausted and the length is not modulo 64, the booleans are padded with <c>false</c> values.
		/// </summary>
		/// <returns>An <see cref="IEnumerable`1"/> of <see cref="ulong"/> instances, such that each <see cref="ulong"/>
		/// represents the value of 64 <see cref="bool"/> values.</returns>
		/// <param name="data">A <see cref="IEnumerable`1"/> instance of <see cref="bool"/> values that will be grouped together.</param>
		/// <remarks>
		/// <para>The result is calculated lazily, thus <see cref="IEnumerable`1"/> instances without an end are supported.</para>
		/// </remarks>
		public static IEnumerable<ulong> PackUlong (IEnumerable<bool> data) {
			IEnumerator<bool> en = data.GetEnumerator ();
			ulong pack;
			bool nxt = en.MoveNext ();
			while (nxt) {
				pack = 0x00;
				for (int i = 0x00; i < 0x40 && nxt; i++) {
					if (en.Current) {
						pack |= 0x01UL << i;
					}
					nxt = en.MoveNext ();
				}
				yield return pack;
			}
		}

		public static IEnumerable<ulong> TileUlong (IEnumerable<bool> data, int n) {
			int nl = (n + 0x07) >> 0x03;
			IEnumerator<bool> en = data.GetEnumerator ();
			ulong[] pack = new ulong[nl];
			bool nxt = en.MoveNext ();
			int l = 0x00;
			while (nxt) {
				for (int i = 0x00; i < 0x08 && nxt; i++) {
					int ii = 0x08 * i;
					for (int j = 0x00; j < nl && nxt; j++) {
						int e = Math.Min (0x08, n - 0x08 * j);
						for (int jj = 0x00; jj < e && nxt; jj++) {
							if (en.Current) {
								pack [j] |= 0x01UL << (ii + jj);
							}
							nxt = en.MoveNext ();
						}
					}
				}
				for (int i = 0x00; i < nl; i++) {
					yield return pack [i];
					pack [i] = 0x00;
				}
				l++;
			}
			for (; l < nl; l++) {
				for (int i = 0x00; i < nl; i++) {
					yield return 0x00;
				}
			}
		}

		public static void PrintRow (StringBuilder sb, ulong tile, int row, int span = 0x08) {
			ulong mask = (tile >> (row << 0x03)) & 0xff;
			for (int i = 0x00; i < span; i++, mask >>= 0x01) {
				sb.Append ((char)(0x30 | (mask & 0x01)));
			}
		}

		public static void PrintBitString (StringBuilder sb, ulong bs, int span = 0x40) {
			ulong c = bs;
			for (int i = 0x00; i < span; i++, c >>= 0x01) {
				sb.Append ((char)(0x30 | (c & 0x01)));
			}
		}

		public static string PrintTile (ulong tile) {
			StringBuilder sb = new StringBuilder ();
			for (int i = 0x00; i < 0x08; i++) {
				PrintRow (sb, tile, i);
				sb.AppendLine ();
			}
			return sb.ToString ();
		}

		public static ulong Compose8Col (ulong col0, ulong col1, ulong col2, ulong col3, ulong col4, ulong col5, ulong col6, ulong col7) {
			return col0 |
				(col1) << 0x01 |
				(col2) << 0x02 |
				(col3) << 0x03 |
				(col4) << 0x04 |
				(col5) << 0x05 |
				(col6) << 0x06 |
				(col7) << 0x07;
		}

		public static ulong TileSerialColumn (ulong tile, int index = 0x00) {
			ulong res = (tile >> (0x00 + index)) & 0x01;
			res |= (tile >> (0x07 + index)) & 0x02;
			res |= (tile >> (0x0e + index)) & 0x04;
			res |= (tile >> (0x15 + index)) & 0x08;
			res |= (tile >> (0x1c + index)) & 0x10;
			res |= (tile >> (0x23 + index)) & 0x20;
			res |= (tile >> (0x2a + index)) & 0x40;
			res |= (tile >> (0x31 + index)) & 0x80;
			return res;
		}

		public static int CountBits (uint val) {
			val = val - ((val >> 0x01) & 0x55555555);
			val = (val & 0x33333333) + ((val >> 0x02) & 0x33333333);
			return (int)((((val + (val >> 0x04)) & 0x0F0F0F0F) * 0x01010101) >> 0x18);
		}

		public static int CountBits (ulong val) {
			return CountBits ((uint)(val & 0xFFFFFFFF)) + CountBits ((uint)(val >> 0x20));
		}

		public static ulong MaskTileColumn (ulong source, int index) {
			return source & (I8S8L1ULong << index);
		}

		public static ulong MaskTileRow (ulong source, int index) {
			return source & (L08ULong << (index << 0x03));
		}

		/// <summary>
		/// Get the index of the lowest set bit of the given value.
		/// </summary>
		/// <returns>The index of the lowest bit that is set of the given value.</returns>
		/// <param name="value">The given value to check for.</param>
		public static int LowestBitIndex (ulong value) {
			if (value != 0x00) {
				int index = 0x00;
				while ((value&0x01) == 0x00) {
					value >>= 0x01;
					index++;
				}
				return index;
			} else {
				return -0x01;
			}
		}

		/// <summary>
		/// Return a bitmask where only the lowest bit of the given value is set. If the given value
		/// is zero (<c>0</c>), zero (<c>0</c> is returned as well).
		/// </summary>
		/// <returns>A bit mask where only the lowest set bit of the given <paramref name="value"/> is set.</returns>
		/// <param name="value">The given value to calculate the mask for.</param>
		public static ulong LowestBitMask (ulong value) {
			return (value & ((~value) + 0x01));
		}

		/// <summary>
		/// Get the parity of the given bit.
		/// </summary>
		/// <returns>One (<c>1</c>) if the number of set bits is odd, otherwise zero (<c>0</c>).</returns>
		/// <param name="value">The value to check the parity for.</param>
		public static ulong GetParity (ulong value) {
			value ^= value >> 0x20;
			value ^= value >> 0x10;
			value ^= value >> 0x08;
			value ^= value >> 0x04;
			value &= 0x0f;
			return (0x6996UL >> (int)value) & 0x01;
		}

		/// <summary>
		/// Add the eight rows of the tile into a binary value.
		/// </summary>
		/// <returns>The sum of the eight rows of the tile as a binary value.</returns>
		/// <param name="tile">The tile for which the rows must be summed.</param>
		/// <remarks>
		/// <para>
		/// Summation is done in 9 basic instructions. This is faster than an ordinary summation
		/// that takes 22 basic instructions.
		/// </para>
		/// </remarks>
		public static ulong ParallelTileRowSum (ulong tile) {
			tile = ((tile >> 0x08) & 0x00FF00FF00FF00FFul) + (tile & 0x00FF00FF00FF00FFul);
			tile += tile >> 0x10;
			tile += tile >> 0x20;
			tile &= 0x0fff;
			return tile;
		}

		/// <summary>
		/// Yield all the indices of bits that are set of the given <see cref="byte"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of indices of the bits that are set in the given <paramref name="data"/>.</returns>
		/// <param name="data">The given data to analyze</param>
		/// <param name="maxIndex">An optional parameter that determines the maximum index to be returned.</param>
		public static IEnumerable<int> GetSetIndices (byte data, int maxIndex = 0x08) {
			return GetSetIndices ((ulong)data, maxIndex);
		}

		/// <summary>
		/// Yield all the indices of bits that are set of the given <see cref="ushort"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of indices of the bits that are set in the given <paramref name="data"/>.</returns>
		/// <param name="data">The given data to analyze</param>
		/// <param name="maxIndex">An optional parameter that determines the maximum index to be returned.</param>
		public static IEnumerable<int> GetSetIndices (ushort data, int maxIndex = 0x08) {
			return GetSetIndices ((ulong)data, maxIndex);
		}

		/// <summary>
		/// Yield all the indices of bits that are set of the given <see cref="uint"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of indices of the bits that are set in the given <paramref name="data"/>.</returns>
		/// <param name="data">The given data to analyze</param>
		/// <param name="maxIndex">An optional parameter that determines the maximum index to be returned.</param>
		public static IEnumerable<int> GetSetIndices (uint data, int maxIndex = 0x08) {
			return GetSetIndices ((ulong)data, maxIndex);
		}

		/// <summary>
		/// Yield all the indices of bits that are set of the given <see cref="ulong"/>.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> of indices of the bits that are set in the given <paramref name="data"/>.</returns>
		/// <param name="data">The given data to analyze</param>
		/// <param name="maxIndex">An optional parameter that determines the maximum index to be returned.</param>
		public static IEnumerable<int> GetSetIndices (ulong data, int maxIndex = 0x40) {
			data &= 0xFFFFFFFFFFFFFFFFUL >> (0x40 - maxIndex);
			for (int i = 0x00; data != 0x00; i++, data >>= 0x01) {
				if ((data & 0x01) == 0x01) {
					yield return i;
				}
			}
		}

		/// <summary>
		/// Enumerate the rows of the given tile.
		/// </summary>
		/// <returns>A <see cref="T:IEnumerable`1"/> containing the several rows in the tile.</returns>
		/// <param name="tile">The tile to get the rows from.</param>
		/// <remarks>The number of emitted items is always equal to eight (<c>8</c>).</remarks>
		public static IEnumerable<ulong> GetRows (ulong tile) {
			for (int i = 0x08; i > 0x00; i--, tile >>= 0x08) {
				yield return tile & 0xff;
			}
		}

		/// <summary>
		/// Spread the lowest eight bits of the given <paramref name="res"/> value over the different rows of the resulting tile.
		/// In other words if the <c>i</c>-th bit is one, all bits in the <c>i</c>-th row of the resulting tile will be set,
		/// if the bit is unset, all bits in the row are unset.
		/// </summary>
		/// <param name="res">The given byte to spread over the tile.</param>
		public static ulong Spread (ulong res) {
			res = (res & 0x01) |
				((res & 0x02) << 0x07) |
				((res & 0x04) << 0x0e) |
				((res & 0x08) << 0x15) |
				((res & 0x10) << 0x1c) |
				((res & 0x20) << 0x23) |
				((res & 0x40) << 0x2a) |
				((res & 0x80) << 0x31);
			res |= res << 0x01;
			res |= res << 0x02;
			res |= res << 0x04;
			return res;
		}
		#endregion
		#region Gray encoding
		public static ulong GrayIncrement (ulong original, int bits = 0x40) {
			ulong last = 0x01UL << (bits - 0x01);
			if (GetParity (original) == 0x00) {
				return original ^ 0x01UL;
			} else {
				ulong lbm = (original & ((~original) + 0x01));
				if (lbm < last) {
					original ^= (lbm << 0x01);
					return original;
				} else {
					return 0x00;
				}
			}
		}
		#endregion
	}
}
