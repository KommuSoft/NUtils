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

namespace NUtils.Maths {
	[TestFixture()]
	public class MathUtilsTest {

		
		[Test()]
		public void TestGreatestCommonDivider () {
			Assert.AreEqual (0x02, MathUtils.GreatestCommonDivider (0x04, 0x06));
		}

		[Test()]
		public void TestLeastCommonMultiple () {
			Assert.AreEqual (0x0c, MathUtils.LeastCommonMultiple (0x04, 0x06));
			Assert.AreEqual (0x1f8, MathUtils.LeastCommonMultiple (0x08, 0x09, 0x15));
		}
	}
}

