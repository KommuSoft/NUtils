//
//  MathUtils.cs
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

	[TestFixture]
	public class MathUtilsTest {

		
		[Test]
		public void TestGreatestCommonDivider () {
			Assert.AreEqual (0x02, MathUtils.GreatestCommonDivider (0x04, 0x06));
		}

		[Test]
		public void TestLeastCommonMultiple () {
			Assert.AreEqual (0x0c, MathUtils.LeastCommonMultiple (0x04, 0x06));
			Assert.AreEqual (0x1f8, MathUtils.LeastCommonMultiple (0x08, 0x09, 0x15));
		}

		private void testApproximation<T> (Func<T,double> freal, Func<T,double> fapprox, IEnumerable<T> values, double tolerance = 1.0e-6d, string xvar = "n") {
			Console.WriteLine ("<listheader><term>{0}</term><description>result</description><description>approximation</description><description>absolute difference</description><description>relative difference</description></listheader>", xvar);
			foreach (T t in values) {
				double e = freal (t);
				double r = fapprox (t);
				Console.WriteLine ("<item><term>{0}</term><description>{1}</description><description>{2}</description><description>{3}</description><description>{4}</description></item>", t, e, r, e - r, (e - r) / e);
				Assert.AreEqual (e, r, tolerance);
			}
		}

		[Test]
		public void TestStirlingLogApproximation () {
			testApproximation<int> (x => Math.Log (MathUtils.Factorial (x)), MathUtils.LogFactorialStirling, Enumerable.Range (0x01, 0x14), 1e-1d);
		}

		[Test]
		public void TestGosperLogApproximation () {
			testApproximation<int> (x => Math.Log (MathUtils.Factorial (x)), MathUtils.LogFactorialGosper, Enumerable.Range (0x01, 0x14), 1e-1d);
		}

		[Test]
		public void TestStirlingDivLogApproximation () {
			IEnumerable<Tuple<int,int>> te = Enumerable.Range (1, 20).SelectMany (x => Enumerable.Range (0, x).Select (y => new Tuple<int,int> (x, y)));
			testApproximation<Tuple<int,int>> (x => Math.Log (MathUtils.Factorial (x.Item1) / MathUtils.Factorial (x.Item1 - x.Item2)), x => MathUtils.LogFactorialDivStirling (x.Item1, x.Item2), te, 1e-1d, "(n,k)");
		}

		[Test]
		public void TestGosperDivLogApproximation () {
			IEnumerable<Tuple<int,int>> te = Enumerable.Range (1, 20).SelectMany (x => Enumerable.Range (0, x).Select (y => new Tuple<int,int> (x, y)));
			testApproximation<Tuple<int,int>> (x => Math.Log (MathUtils.Factorial (x.Item1) / MathUtils.Factorial (x.Item1 - x.Item2)), x => MathUtils.LogFactorialDivGosper (x.Item1, x.Item2), te, 1e-1d, "(n,k)");
		}
	}
}

