using System;
using System.Text;
using System.Collections.Generic;

namespace NUtils {
	public static class NumericalUtils {

		public const ulong IdentityTile = 0x8040201008040201;

		public static ulong Transpose (ulong origin) {
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

		public static ulong Copy8Row (ulong origin, int row = 0x00) {
			int shft = row << 0x03;
			ulong mask = ((0xffUL << shft) & origin) >> shft;
			mask |= mask << 0x20;
			mask |= mask << 0x10;
			mask |= mask << 0x08;
			return mask;
		}

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

		public static ulong Copy8Col (ulong origin, int col = 0x00) {
			ulong mask = ((0x0101010101010101UL << col) & origin) >> col;
			mask |= mask << 0x04;
			mask |= mask << 0x02;
			mask |= mask << 0x01;
			return mask;
		}

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

		public static int CountBits (uint val) {
			val = val - ((val >> 0x01) & 0x55555555);
			val = (val & 0x33333333) + ((val >> 0x02) & 0x33333333);
			return (int)((((val + (val >> 0x04)) & 0x0F0F0F0F) * 0x01010101) >> 0x18);
		}

		public static int CountBits (ulong val) {
			return CountBits ((uint)(val & 0xFFFFFFFF)) + CountBits ((uint)(val >> 0x20));
		}
	}
}
