//
//  TypePathNode.cs
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
	/// A special path node that filters on the type of the node <typeparamref name="Q"/>. Evidently
	/// the type must be an instantiation of the type of tree to process itself.
	/// </summary>
	public class TypePathNode<T,Q> : PathNodeBase<T> where Q : T where T : IComposition<T> {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:TypePathNode`2"/> class.
		/// </summary>
		/// <remarks>
		/// <para>No parameters are required since the type is handled by the typeparameter.</para>
		/// </remarks>
		public TypePathNode () {
		}
		#endregion
		#region IValidater implementation
		/// <summary>
		/// Validate the given instance.
		/// </summary>
		/// <param name="toValidate">The given instance to validate.</param>
		/// <returns><c>true</c> if the given instance is validate; otherwise <c>false</c>.</returns>
		public override bool Validate (T toValidate) {
			return toValidate is Q;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:TypePathNode`2"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:TypePathNode`2"/>.</returns>
		public override string ToString () {
			return string.Format (typeof(Q).Name);
		}
		#endregion
	}
}

