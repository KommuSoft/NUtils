using NUnit.Framework;
using System;

namespace NUtils {
	[TestFixture()]
	public class BitUtilsTest {
		[Test()]
		public void TestAndCompress8Rows () {
			ulong ra = 0x00, rb, rc, rd, re, rf, rg, rh, i, ri, a, b, c, d, e, f, g, h;
			for (a = 0x00; a < 0xff; a++) {
				rb = 0x00;
				for (b = 0x00; b < 0xff; b++) {
					rc = 0x00;
					for (c = 0x00; c < 0xff; c++) {
						rd = 0x00;
						for (d = 0x00; d < 0xff; d++) {
							re = 0x00;
							for (e = 0x00; e < 0xff; e++) {
								rf = 0x00;
								for (f = 0x00; f < 0xff; f++) {
									rg = 0x00;
									for (g = 0x00; g < 0xff; g++) {
										rh = 0x00;
										for (h = 0x00; h < 0xff; h++) {
											i = (a << 0x38) | (b << 0x30) | (c << 0x28) | (d << 0x20) | (e << 0x18) | (f << 0x10) | (g << 0x08) | h;
											ri = (ra << 0x38) | (rb << 0x30) | (rc << 0x28) | (rd << 0x20) | (re << 0x18) | (rf << 0x10) | (rg << 0x08) | rh;
											Assert.AreEqual (ri, BitUtils.AndCompress8Rows (i), i.ToString ("X"));
										}
										rh = 0x01;
										i = (a << 0x38) | (b << 0x30) | (c << 0x28) | (d << 0x20) | (e << 0x18) | (f << 0x10) | (g << 0x08) | h;
										ri = (ra << 0x38) | (rb << 0x30) | (rc << 0x28) | (rd << 0x20) | (re << 0x18) | (rf << 0x10) | (rg << 0x08) | rh;
										Assert.AreEqual (ri, BitUtils.AndCompress8Rows (i), i.ToString ("X"));
									}
									rg = 0x01;
								}
								rf = 0x01;
							}
							re = 0x01;
						}
						rd = 0x01;
					}
					rc = 0x01;
				}
				rb = 0x01;
			}
			ra = 0x01;
		}
	}
}

