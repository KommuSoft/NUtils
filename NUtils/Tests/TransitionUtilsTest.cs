//
//  TransitionUtilsTest.cs
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
using System.Linq;
using System;
using System.Collections.Generic;

namespace NUtils.Maths {
	[TestFixture()]
	public class TransitionUtilsTest {
		[Test()]
		public void TestGetStronglyConnectedGroups () {
			ExplicitTransition et = new ExplicitTransition (0x04, 0x00, 0x06, 0x02, 0x01, 0x06, 0x05, 0x07);
			int[][] grps = et.GetStronglyConnectedGroups ().Select (x => x.ToArray ()).ToArray ();
			Assert.AreEqual (3, grps.Length);
			Assert.AreEqual (3, grps [0x00].Length);
			Assert.AreEqual (4, grps [0x00] [0x00]);
			Assert.AreEqual (1, grps [0x00] [0x01]);
			Assert.AreEqual (0, grps [0x00] [0x02]);
			Assert.AreEqual (2, grps [0x01].Length);
			Assert.AreEqual (5, grps [0x01] [0x00]);
			Assert.AreEqual (6, grps [0x01] [0x01]);
			Assert.AreEqual (1, grps [0x02].Length);
			Assert.AreEqual (7, grps [0x02] [0x00]);
		}
	}
}