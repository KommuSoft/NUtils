//
//  NondeterministicFiniteAutomatonTest.cs
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
using System.IO;

namespace NUtils.Automata {

	[TestFixture()]
	public class NondeterministicFiniteAutomatonTest {
		[Test()]
		public void TestWriteDotText () {
			NondeterministicFiniteAutomaton<int,char> nfa = new NondeterministicFiniteAutomaton<int,char> (
				new Tuple<int,char,int>[] {
				new Tuple<int,char,int>(0x01,'a',0x02)
			},
				0x01,
				new int[] { 0x02 }
			);
			using (TextWriter tw = new StringWriter()) {
				nfa.WriteDotText (tw);
				tw.Close ();
				Console.Error.WriteLine (tw.ToString ());
			}
		}
	}
}

