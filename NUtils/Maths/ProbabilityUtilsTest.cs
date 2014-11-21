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

namespace NUtils.Maths {

	[TestFixture()]
	public class ProbabilityUtilsTest {
		[Test()]
		public void PickKUniform () {
			List<int> fibonnaccis = new List <int> ();
			int i0 = 0x00, i1 = 0x01, i2 = 0x01;
			for (; i2 < int.MaxValue/0x02;) {
				fibonnaccis.Add (i2);
				i0 = i1;
				i1 = i2;
				i2 += i0;
			}
			for (int k = 0x00; k < fibonnaccis.Count; k++) {

			}
		}
	}
}

