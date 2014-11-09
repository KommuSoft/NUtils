using NUnit.Framework;

namespace NUtils.Bitwise {

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

		[Test()]
		public void TestToString () {
			Assert.AreEqual ("0", new CompactBitVector (0x01, new ulong[] { 0x00 }).ToString ());
			Assert.AreEqual ("1", new CompactBitVector (0x01, new ulong[] { 0x01 }).ToString ());
			Assert.AreEqual ("00", new CompactBitVector (0x02, new ulong[] { 0x00 }).ToString ());
			Assert.AreEqual ("10", new CompactBitVector (0x02, new ulong[] { 0x01 }).ToString ());
			Assert.AreEqual ("01", new CompactBitVector (0x02, new ulong[] { 0x02 }).ToString ());
			Assert.AreEqual ("11", new CompactBitVector (0x02, new ulong[] { 0x03 }).ToString ());
			Assert.AreEqual ("1001", new CompactBitVector (0x04, new ulong[] { 0x09 }).ToString ());
			Assert.AreEqual ("1001000", new CompactBitVector (0x07, new ulong[] { 0x09 }).ToString ());
		}
	}
}

