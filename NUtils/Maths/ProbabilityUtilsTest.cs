//
//  ProbabilityUtilsTest.cs
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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NUtils.Maths {

	[TestFixture()]
	public class ProbabilityUtilsTest {
		[Test()]
		public void TestPickKUniform () {
			List<int> id = new List <int> ();
			int l = 0x2b;
			int T = 0x8000;
			int divp = 0x04;
			for (int i = 0x00; i < l; i++) {
				id.Add (i);
			}
			for (int k = 0x00; k < id.Count; k++) {
				int[] ci = new int[id.Count];
				List<int> li = id.PickKUniform (k).ToList ();
				Assert.AreEqual (k, li.Count);//size check
				for (int j = 0x01; j < k; j++) {
					Assert.Less (li [j - 0x01], li [j]);//order+unique check
				}
				for (int t = 0x00; t < T; t++) {
					foreach (int idi in id.PickKUniform (k)) {
						ci [idi]++;
					}
				}
				int exp = T * k / ci.Length;
				for (int j = 0x00; j < ci.Length; j++) {
					Assert.GreaterOrEqual (ci [j], exp - exp / divp);//counting check
					Assert.LessOrEqual (ci [j], exp + exp / divp);//counting check
				}
			}
		}

		[Test()]
		public void TestPickKUniformPerformance () {
			List<int> id = new List <int> ();
			int l = 0x7cf;
			int T = 0x800;
			int divp = 0x04;
			for (int i = 0x00; i < l; i++) {
				id.Add (i);
			}
			for (int k = 0x00; k < l; k++) {
				Console.Write (k);
				Console.Write ('\t');
				DateTime bg = DateTime.Now;
				for (int t = 0x00; t < T; t++) {
					foreach (int sel in id.PickKUniform (k)) {
					}
				}
				DateTime en = DateTime.Now;
				Console.Write (en - bg);
				Console.Write ('\t');
				HashSet<int> hst = new HashSet<int> ();
				bg = DateTime.Now;
				for (int t = 0x00; t < T; t++) {
					while (hst.Count < k) {
						int r = MathUtils.Next (l);
						hst.Add (id [r]);
					}
					hst.Clear ();
				}
				en = DateTime.Now;
				Console.WriteLine (en - bg);
			}
		}

		[Test()]
		public void TestPickKUniformJump () {
			List<int> id = new List <int> ();
			int l = 0x2b;
			int T = 0x8000;
			int divp = 0x04;
			for (int i = 0x00; i < l; i++) {
				id.Add (i);
			}
			for (int k = 0x00; k < id.Count; k++) {
				int[] ci = new int[id.Count];
				List<int> li = id.PickKUniform (k).ToList ();
				Assert.AreEqual (k, li.Count);//size check
				for (int j = 0x01; j < k; j++) {
					Assert.Less (li [j - 0x01], li [j]);//order+unique check
				}
				for (int t = 0x00; t < T; t++) {
					foreach (int idi in id.PickKUniform (k)) {
						ci [idi]++;
					}
				}
				int exp = T * k / ci.Length;
				for (int j = 0x00; j < ci.Length; j++) {
					Assert.GreaterOrEqual (ci [j], exp - exp / divp);//counting check
					Assert.LessOrEqual (ci [j], exp + exp / divp);//counting check
				}
			}
		}
	}
}

