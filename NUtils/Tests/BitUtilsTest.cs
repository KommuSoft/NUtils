using NUnit.Framework;
using System;
using NUtils.Bitwise;

namespace NUtils {
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
	}
}

