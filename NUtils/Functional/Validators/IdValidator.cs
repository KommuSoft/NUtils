//
//  IdValidator.cs
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
using NUtils.Abstract;

namespace NUtils.Functional.Validators {

	/// <summary>
	/// An implementation of the <see cref="T:IIdValidator"/> interface that validates <see cref="T:IId"/> instances
	/// by checking equivalence with a given <see cref="T:Id"/>.
	/// </summary>
	public class IdValidator : IIdValidator {

		#region IId implementation
		/// <summary>
		/// Gets the identifier of this instance.
		/// </summary>
		/// <value>
		/// The identifier of this instance.
		/// </value>
		public uint Id {
			get;
			protected set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:IdValidator"/> class with the given <paramref name="id"/> to verify.
		/// </summary>
		/// <remarks>
		/// <para>The id to verify, only instances with an <see cref="P:IId.Id"/> that is the same as the given
		/// one, are accepted.</para>
		/// </remarks>
		public IdValidator (uint id) {
			this.Id = id;
		}
		#endregion
		#region IValidater implementation
		/// <summary>
		/// Validate the given instance.
		/// </summary>
		/// <param name="toValidate">The given instance to validate.</param>
		/// <returns><c>true</c> if the given instance has an id that is the same as this <see cref="P:Id"/>; otherwise <c>false</c>.</returns>
		public bool Validate (IId toValidate) {
			return (toValidate != null && toValidate.Id == this.Id);
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:IdValidator"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:IdValidator"/>.</returns>
		public override string ToString () {
			return string.Format ("#{0}", this.Id);
		}
		#endregion
		#region Equals method
		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:IdValidator"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="T:IdValidator"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="System.Object"/> is equal to the current
		/// <see cref="T:IdValidator"/>; otherwise, <c>false</c>.</returns>
		public override bool Equals (object obj) {
			IIdValidator iiv = obj as IIdValidator;
			if (iiv != null) {
				return this.Id == iiv.Id;
			}
			return false;
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="T:IdValidator"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			return this.Id.GetHashCode ();
		}
		#endregion
	}
}

