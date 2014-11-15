//
//  NullPathNode.cs
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
	/// An implementation of the <see cref="T:INullPathNode`1"/> interface: a <see cref="T:IPathNode`1"/>
	/// that is the epsilon character of the alphabet: not a node, mainly used for epsilon-edges in a nondeterminstic
	/// finite automaton.
	/// </summary>
	/// <remarks>
	/// <para>Every <see cref="T:NullPathNode`1"/> instance is equivalent with every other
	/// <see cref="T:INUllPathNode`1"/> instance, as long as <typeparamref name="T"/> is equivalent.</para>
	/// </remarks>
	/// <typeparam name='T'>The type of the nodes on which this <see cref="T:IPathNode`1"/> operates.</typeparam>
	/// <remarks>
	/// <para>A <see cref="T:INullPathNode`1"/> returns <c>false</c> on every <see cref="M:IPathNode`1.Validate"/> call.</para>
	/// <para>An <see cref="T:INullPathNode`1"/> will not match any item in a given tree, the tree will simply not be evaluated.</para>
	/// </remarks>
	public class NullPathNode<T> : INullPathNode<T> where T : IComposition<T> {

		#region Constants
		/// <summary>
		/// Get the hash code used for <see cref="T:NullPathNode`1"/> instances.
		/// </summary>
		/// <remarks>
		/// <para>Since every <see cref="T:NullPathNode`1"/> instance is equivalent with every other
		/// <see cref="T:INUllPathNode`1"/> instance, as long as <typeparamref name="T"/> is equivalent,
		/// all instances must return the same <see cref="M:Object.GetHashCode"/> value.</para>
		/// </remarks>
		public const int HashCode = 936236269;
		/// <summary>
		/// The single instance of the <see cref="T:NullPathNode`1"/> for a type parameter <typeparamref name="T"/> created.
		/// </summary>
		public static readonly NullPathNode<T> Instance = new NullPathNode<T> ();
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:NullPathNode`1"/> class.
		/// </summary>
		/// <remarks>
		/// <para>In order to reduce the memory footprint, it is advisable to construct only one instance and
		/// copy the reference further. The constructor is thus made private.</para>
		/// </remarks>
		private NullPathNode () {
		}
		#endregion
		#region Equals method
		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:NullPathNode`1"/>.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:NullPathNode`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current
		/// <see cref="T:NullPathNode`1"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// <para>Every <see cref="T:NullPathNode`1"/> instance is equivalent with every other
		/// <see cref="T:INUllPathNode`1"/> instance, as long as <typeparamref name="T"/> is equivalent.</para>
		/// </remarks>
		public override bool Equals (object obj) {
			return (obj is INullPathNode<T>);
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="T:NullPathNode`1"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		/// <remarks>
		/// <para>Since every <see cref="T:NullPathNode`1"/> instance is equivalent with every other
		/// <see cref="T:INUllPathNode`1"/> instance, as long as <typeparamref name="T"/> is equivalent,
		/// all instances must return the same <see cref="M:Object.GetHashCode"/> value.</para>
		/// </remarks>
		public override int GetHashCode () {
			return HashCode;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:NullPathNode`1"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:NullPathNode`1"/>.</returns>
		public override string ToString () {
			return string.Format ("[Epsilon]");
		}
		#endregion
		#region IValidater implementation
		/// <summary>
		/// Validate the given instance.
		/// </summary>
		/// <param name="toValidate">The given instance to validate.</param>
		/// <returns><c>true</c> if the given instance is validate; otherwise <c>false</c>.</returns>
		/// <remarks>
		/// <para>A <see cref="T:INullPathNode`1"/> returns <c>false</c> on every <see cref="M:IPathNode`1.Validate"/> call.</para>
		/// </remarks>
		public bool Validate (T toValidate) {
			return false;
		}
		#endregion
		#region IPath implementation
		/// <summary>
		/// Evaluate the specified tree using this <see cref="T:IPath`1"/> instance and return all possible matches.
		/// </summary>
		/// <param name="tree">The given tree to evaluate.</param>
		/// <returns>A <see cref="T:IEnumerable`1"/> that contains all possible nodes in the tree that match
		/// the path specifications. This will in many cases be evaluated lazily.</returns>
		/// <remarks>
		/// <para>An <see cref="T:INullPathNode`1"/> will not match any item in a given tree, the tree will simply not be evaluated.</para>
		/// </remarks>
		public IEnumerable<T> Evaluate (T tree) {
			yield break;
		}

		/// <summary>
		/// Compiles this <see cref="T:IPath`1"/> instance into an NFA to evaluate trees.
		/// </summary>
		/// <remarks>
		/// <para>In case this method has not been called before the <see cref="M:Evaluate"/> method
		/// has been called, the method will be called automatically.</para>
		/// <para>After compilation, the path cannot be modified any further.</para>
		/// <para>Some instances don't compile first: for instance <see cref="T:IPathNode`1"/> instances. In that
		/// case the compile method does nothing.</para>
		/// </remarks>
		public void Compile () {
		}
		#endregion
	}
}

