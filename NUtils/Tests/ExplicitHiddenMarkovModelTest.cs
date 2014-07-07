//
//  ExplicitHiddenMarkovModelTest.cs
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
using NUtils.Maths;

namespace NUtils.ArtificialIntelligence {
	[TestFixture()]
	public class ExplicitHiddenMarkovModelTest {

		private static readonly ExplicitTransition et0 = new ExplicitTransition (0x04, 0x00, 0x06, 0x02, 0x01, 0x06, 0x05, 0x07);
		private static readonly int[] ex0 = new int[] { 0x01, 0x01, 0x02, 0x00, 0x00, 0x02, 0x02, 0x01 };
		private static readonly ReferencedFiniteStateMachine<int,ExplicitTransition> rfsm0 = new ReferencedFiniteStateMachine<int,ExplicitTransition> (et0, ex0);
		private static readonly ExplicitHiddenMarkovModel ehm = new ExplicitHiddenMarkovModel (new double[] { 0.8d, 0.2d }, new double[,] {
			{0.8d,0.2d},
			{0.3d,0.7d}
		}, new double[,] {
			{0.2d,0.4d,0.4d},
			{0.5d,0.4d,0.1d}
		});

		[Test()]
		public void TestGenerateInfluenceMatrix () {
			ehm.GenerateInfluenceMatrix (rfsm0, 0x00);
		}
	}
}

