using NUnit.Framework;
using System;

namespace NUtils {
	[TestFixture()]
	public class CompactBitVectorTest {
		[Test()]
		public void TestLowestBit () {
			int n = 0x10;
			CompactBitVector cbv = new CompactBitVector (n);
			for (int i = 0x00; i < n; i += 0x02) {
				Assert.AreEqual (-0x01, cbv.GetLowest (i));
				cbv.Add (i);
				Assert.AreEqual (i, cbv.GetLowest (i));
			}
			for (int i = 0x00; i < n-0x02; i += 0x02) {
				Assert.AreEqual (i, cbv.GetLowest (i));
				Assert.AreEqual (i + 0x02, cbv.GetLowest (i + 0x01));
			}
			Assert.AreEqual (-0x01, cbv.GetLowest (n));
		}
	}
}

