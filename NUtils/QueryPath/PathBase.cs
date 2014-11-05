//
//  PathBase.cs
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
using System.Collections.Generic;

namespace NUtils.QueryPath {

	/// <summary>
	/// A basic implementation of the <see cref="T:IPath`1"/> interface, for programmer's convenience.
	/// </summary>
	public abstract class PathBase<T> : IPath<T> where T : IComposition<T> {

		#region Fields
		/// <summary>
		/// An internal NFA that processes the XPath queries.
		/// </summary>
		protected NondeterministicFiniteAutomaton<int,int> InnerNfa = null;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathBase`1"/> class.
		/// </summary>
		protected PathBase () {
		}
		#endregion
		#region IPath implementation
		/// <summary>
		/// Evaluate the specified tree using this <see cref="T:IPath`1"/> instance and return all possible matches.
		/// </summary>
		/// <param name="tree">The given tree to evaluate.</param>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains all possible nodes in the tree that match
		/// the path specifications. This will in many cases be evaluated lazily.</returns>
		public abstract IEnumerable<T> Evaluate (T tree);

		/// <summary>
		/// Compiles this <see cref="T:IPath`1"/> instance into an NFA to evaluate trees.
		/// </summary>
		/// <remarks>
		/// <para>In case this method has not been called before the <see cref="M:Evaluate"/> method
		/// has been called, the method will be called automatically.</para>
		/// <para>After compilation, the path cannot be modified any further.</para>
		/// <para>This method checks if the NFA is already generated, if not, <see cref="M:GenerateNFA"/> is called.</para>
		/// </remarks>
		public virtual void Compile () {
			if (this.InnerNfa == null) {
				this.InnerNfa = GenerateNFA ();
			}
		}
		#endregion
		#region Introduced protected methods
		/// <summary>
		/// A protected method that generates an NFA based on the <see cref="T:IPath`1"/> instance. This
		/// NFA will be used for processing.
		/// </summary>
		/// <returns>A <see cref="NondeterminsticFiniteAutomaton"/> used for parsing trees.</returns>
		/// <remarks>
		/// <para>The result must guaranteed to be effective.</para>
		/// <para>This method is only called once.</para>
		/// </remarks>
		protected abstract NondeterministicFiniteAutomaton<int,int> GenerateNFA ();
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:PathBase`1"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:PathBase`1"/>.</returns>
		public abstract override string ToString ();
		#endregion
	}
}