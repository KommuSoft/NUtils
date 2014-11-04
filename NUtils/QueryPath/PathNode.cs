//
//  PathNode.cs
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
using System.Collections.Generic;
using System.Linq;
using NUtils.Functional;
using NUtils.Designpatterns;

namespace NUtils.QueryPath {

	/// <summary>
	/// A basic implementation of the <see cref="T:IPathNode`1"/> interface. A <see cref="T:PathNode`1"/>
	/// is a conjunction of different types of constraints. All constraints must be fulfilled in order to
	/// validate a node.
	/// </summary>
	public class PathNode<T> : PathNodeBase<T>, IPath<T> where T : IComposition<T> {

		#region Fields
		private readonly ICollection<IPathNode<T>> subConstraints;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class, the resulting <see cref="T:IPathNode`1"/>
		/// will match any value.
		/// </summary>
		public PathNode () : this(new IPathNode<T>[0x00]) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class.
		/// </summary>
		/// <param name="subConstraints">A list of subconstraints that must all be fulfilled in order to validate a node.</param>
		public PathNode (IEnumerable<IPathNode<T>> subConstraints) {
			this.subConstraints = subConstraints.ToLinkedList ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class.
		/// </summary>
		/// <param name="subConstraints">A list of subconstraints that must all be fulfilled in order to validate a node.</param>
		public PathNode (params IPathNode<T>[] subConstraints) : this((IEnumerable<IPathNode<T>>) subConstraints) {
		}
		#endregion
		#region IValidater implementation
		/// <summary>
		/// Validate the given instance.
		/// </summary>
		/// <param name="toValidate">The given instance to validate.</param>
		/// <returns><c>true</c> if the given instance is validate; otherwise <c>false</c>.</returns>
		public override bool Validate (T toValidate) {
			return this.subConstraints.All (x => x.Validate (toValidate));
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:PathNode`1"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:PathNode`1"/>.</returns>
		public override string ToString () {
			int len = this.subConstraints.Count;
			if (len <= 0x00) {
				return ".";
			} else if (len <= 0x01) {
				return this.subConstraints.First ().ToString ();
			} else {
				return string.Format ("[{0}]", string.Join (",", this.subConstraints));
			}
		}
		#endregion
	}
}

