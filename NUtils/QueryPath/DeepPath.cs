//
//  DeepPath.cs
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
using System;
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// A path element that describes zero or more additional path random path elements deep. Denoted in
	/// XPath as <c>..</c>.
	/// </summary>
	public class DeepPath<T> : PathBase<T> where T : IComposition<T> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DeepPath`1"/> class.
		/// </summary>
		public DeepPath () {
		}
		#endregion
		#region implemented abstract members of PathBase
		/// <summary>
		/// Evaluate the specified tree.
		/// </summary>
		/// <param name="tree">Tree.</param>
		public override System.Collections.Generic.IEnumerable<T> Evaluate (T tree) {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Generates the NF.
		/// </summary>
		/// <returns>The NF.</returns>
		protected override NondeterministicFiniteAutomaton<int,int> GenerateNFA () {
			throw new NotImplementedException ();
		}

		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:DeepPath`1"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:DeepPath`1"/>.</returns>
		public override string ToString () {
			return "..";
		}
		#endregion
	}
}

