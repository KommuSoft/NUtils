using NUnit.Framework;
using System;

namespace NUtils.Bitwise {

	[TestFixture()]
	public class BitUtilsTest {

		[Test()]
		public void TestGrayIncrement2 () {
			ulong d = 0x00;
			d = BitUtils.GrayIncrement (d, 0x02);
			Assert.AreEqual (0x01, d);
			d = BitUtils.GrayIncrement (d, 0x02);
			Assert.AreEqual (0x03, d);
			d = BitUtils.GrayIncrement (d, 0x02);
			Assert.AreEqual (0x02, d);
			d = BitUtils.GrayIncrement (d, 0x02);
			Assert.AreEqual (0x00, d);
		}

		[Test()]
		public void TestGrayIncrement3 () {
			ulong d = 0x00;
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x01, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x03, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x02, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x06, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x07, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x05, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x04, d);
			d = BitUtils.GrayIncrement (d, 0x03);
			Assert.AreEqual (0x00, d);
		}

		[Test()]
		public void TestGrayIncrement4 () {
			ulong d = 0x00;
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x01, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x03, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x02, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x06, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x07, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x05, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x04, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0c, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0d, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0f, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0e, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0a, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x0b, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x09, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x08, d);
			d = BitUtils.GrayIncrement (d, 0x04);
			Assert.AreEqual (0x00, d);
		}

		[Test()]
		public void TestParallelTileRowSum () {
			ulong da = 0x00, db = 0x00, dc = 0x00, dd = 0x00, de = 0x00, df = 0x00, dg = 0x00, dh = 0x00;
			ulong ca = 0x00, cb = 0x00, cc = 0x00, cd = 0x00, ce = 0x00, cf = 0x00, cg = 0x00;
			for (; da < 0x0100; da += 67) {
				ca = da << 0x38;
				for (; db < 0x0100; db += 59) {
					cb = ca | (db << 0x30);
					for (; dc < 0x0100; dc += 43) {
						cc = cb | (dc << 0x28);
						for (; dd < 0x0100; dd += 67) {
							cd = cc | (dd << 0x20);
							for (; de < 0x0100; de += 59) {
								ce = cd | (de << 0x18);
								for (; df < 0x0100; df += 43) {
									cf = ce | (df << 0x10);
									for (; dg < 0x0100; dg += 67) {
										cg = cf | (dg << 0x08);
										for (; dh < 0x0100; dh += 101) {
											Assert.AreEqual (da + db + dc + dd + de + df + dg + dh, BitUtils.ParallelTileRowSum (cg | dh));
										}
										dh &= 0xff;
									}
									dg &= 0xff;
								}
								df &= 0xff;
							}
							de &= 0xff;
						}
						dd &= 0xff;
					}
					dc &= 0xff;
				}
				db &= 0xff;
			}
		}
	}
}

