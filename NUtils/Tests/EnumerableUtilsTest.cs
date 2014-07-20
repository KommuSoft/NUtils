//
//  EnumerableUtilsTest.cs
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

namespace NUtils.Functional {
	[TestFixture()]
	public class EnumerableUtilsTest {
		[Test ()]
		public void TestTakeWhile () {
			testEqual (EnumerableUtils.TakeWhile (new int[] { 1, 2, 3, 4, 1, 2, 3, 4 }, x => x < 3), new int[] { 1, 2 });
			testEqual (EnumerableUtils.TakeWhile (new int[] { 1, 2, 3 }, x => x < 9), new int [] { 1, 2, 3 });
			testEqual (EnumerableUtils.TakeWhile (new int[] { 1, 2, 3 }, x => x < 0), new int [] { });
		}

		[Test ()]
		public void TestDropWhile () {
			testEqual (EnumerableUtils.DropWhile (new int[] { 1, 2, 3, 4, 5, 1, 2, 3 }, x => x < 3), new int [] { 3, 4, 5, 1, 2, 3 });
			testEqual (EnumerableUtils.DropWhile (new int[] { 1, 2, 3 }, x => x < 9), new int [] { });
			testEqual (EnumerableUtils.DropWhile (new int[] { 1, 2, 3 }, x => x < 0), new int [] { 1, 2, 3 });
		}

		[Test ()]
		public void TestTake () {
			testEqual (new char[] { 'H', 'e', 'l', 'l', 'o' }, EnumerableUtils.Take ("Hello World!", 5));
			testEqual (new int[] { 1, 2, 3 }, EnumerableUtils.Take (new int[] { 1, 2, 3, 4, 5 }, 3));
			testEqual (new int[] { 1, 2 }, EnumerableUtils.Take (new int[] { 1, 2 }, 3));
			testEqual (new int[] { }, EnumerableUtils.Take (new int[] { }, 3));
			testEqual (new int[] { }, EnumerableUtils.Take (new int[] { 1, 2 }, -1));
			testEqual (new int[] { }, EnumerableUtils.Take (new int[] { 1, 2 }, 0));
		}

		[Test ()]
		public void TestDrop () {
			testEqual (new char[] { 'W', 'o', 'r', 'l', 'd', '!' }, EnumerableUtils.Drop ("Hello World!", 6));
			testEqual (new int[] { 4, 5 }, EnumerableUtils.Drop (new int[] { 1, 2, 3, 4, 5 }, 3));
			testEqual (new int[] { }, EnumerableUtils.Drop (new int[] { 1, 2 }, 3));
			testEqual (new int[] { }, EnumerableUtils.Drop (new int[] { }, 3));
			testEqual (new int[] { 1, 2 }, EnumerableUtils.Drop (new int[] { 1, 2 }, -1));
			testEqual (new int[] { 1, 2 }, EnumerableUtils.Drop (new int[] { 1, 2 }, 0));
		}

		private bool testEqual<T> (IEnumerable<T> xs, IEnumerable<T> ys) {
			IEnumerator<T> xse = xs.GetEnumerator ();
			IEnumerator<T> yse = ys.GetEnumerator ();
			bool xseb = xse.MoveNext ();
			bool yseb = yse.MoveNext ();
			while (xseb && yseb) {
				if (!Object.Equals (xse.Current, yse.Current)) {
					Assert.Fail ("Not the same element: {0} versus {1}.", xse.Current, yse.Current);
					return false;
				}
				xseb = xse.MoveNext ();
				yseb = yse.MoveNext ();
			}
			if (xseb != yseb) {
				Assert.Fail ("Unequal length.");
				return false;
			}
			return true;
		}
	}
}

