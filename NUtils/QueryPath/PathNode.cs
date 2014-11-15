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
using System.Linq;
using NUtils.Designpatterns;
using System.Collections.Generic;
using NUtils.Abstract;

namespace NUtils.QueryPath {

	/// <summary>
	/// A special path node that filters on the type of the node <typeparamref name="TType"/>.
	/// </summary>
	/// <typeparam name='T'>The type of the tree nodes that will be queried.</typeparam>
	/// <typeparam name='TType'>The type of the nodes that will be accepted.</typeparam>
	public class PathNode<T,TType> : PathNodeBase<T> where T : IComposition<T> {

		#region Fields
		/// <summary>
		/// A <see cref="T:List`1"/> of <see cref="T:IValidator`1"/> instance that must all hold such that the node is accepted.
		/// </summary>
		private readonly List<IValidater<T>> validators = null;
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`2"/> class with no additional validators.
		/// </summary>
		/// <remarks>
		/// <para>Note that there is an implicit constraint to the <typeparamref name="TType"/> type parameter.</para>
		/// </remarks>
		public PathNode () {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`2"/> class with an optional list of validators.
		/// </summary>
		/// <param name='validators'>A list of validators that must be fulfilled, optional, if not effective, there are no additional validators.</param>
		/// <remarks>
		/// <para>Note that there is an implicit constraint to the <typeparamref name="TType"/> type parameter.</para>
		/// <para>Non effective <paramref name="validators"/> are ignored.</para>
		/// </remarks>
		public PathNode (IEnumerable<IValidater<T>> validators) {
			if (validators != null) {
				List<IValidater<T>> lst = validators.Effectives ().ToList ();
				if (lst.Count > 0x00) {
					this.validators = lst;
				}
			} else {
				this.validators = null;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`2"/> class with an optional list of validators.
		/// </summary>
		/// <param name='validators'>An array of validators that must be fulfilled.</param>
		/// <remarks>
		/// <para>Note that there is an implicit constraint to the <typeparamref name="TType"/> type parameter.</para>
		/// <para>Non effective <paramref name="validators"/> are ignored.</para>
		/// </remarks>
		public PathNode (params IValidater<T>[] validators) : this((IEnumerable<IValidater<T>>) validators) {
		}
		#endregion
		#region IValidater implementation
		/// <summary>
		/// Validate the given instance.
		/// </summary>
		/// <param name="toValidate">The given instance to validate.</param>
		/// <returns><c>true</c> if the given instance is validate; otherwise <c>false</c>.</returns>
		public override bool Validate (T toValidate) {
			return toValidate is TType;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:PathNode`2"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:PathNode`2"/>.</returns>
		public override string ToString () {
			Type tt = typeof(T);
			Type tttype = typeof(TType);
			if (tttype.IsAssignableFrom (tt)) {
				return ".";
			} else {
				return string.Format (tttype.Name);
			}
		}
		#endregion
	}

	/// <summary>
	/// A generic implementation of a <see cref="T:IPathNode`1"/> with no constraints.
	/// </summary>
	/// <typeparam name='T'>The type of the tree nodes that will be queried.</typeparam>
	public class PathNode<T> : PathNode<T,T> where T : IComposition<T> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class with no additional validators.
		/// </summary>
		public PathNode () : base() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class with an optional list of validators.
		/// </summary>
		/// <param name='validators'>A list of validators that must be fulfilled, optional, if not effective, there are no additional validators.</param>
		/// <remarks>
		/// <para>Non effective <paramref name="validators"/> are ignored.</para>
		/// </remarks>
		public PathNode (IEnumerable<IValidater<T>> validators) : base(validators) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:PathNode`1"/> class with an optional list of validators.
		/// </summary>
		/// <param name='validators'>An array of validators that must be fulfilled.</param>
		/// <remarks>
		/// <para>Non effective <paramref name="validators"/> are ignored.</para>
		/// </remarks>
		public PathNode (params IValidater<T>[] validators) : base(validators) {
		}
		#endregion
	}
}

