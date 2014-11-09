//
//  DotAttribute.cs
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

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// A basic implementation of the <see cref="T:IDotAttribute"/> interface.
	/// </summary>
	public abstract class DotAttribute : NameBase, IDotAttribute {

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DotAttribute"/> class with the given name of the attribute.
		/// </summary>
		/// <param name="name">The name of the attribute to be created.</param>
		protected DotAttribute (string name) : base(name) {
		}
		#endregion
		#region Introduced protected methods
		/// <summary>
		/// Format the value of the given <see cref="T:IDotAttribute"/>.
		/// </summary>
		/// <returns>A string representing the value of the given dot attribute if there is a value, <c>null</c> otherwise.</returns>
		protected abstract string ValueFormat ();
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="string"/> that represents the current <see cref="T:DotAttribute"/>.
		/// </summary>
		/// <returns>A <see cref="string"/> that represents the current <see cref="T:DotAttribute"/>.</returns>
		public override string ToString () {
			string valfor = this.ValueFormat ();
			if (valfor != null) {
				return string.Format ("{0}{1}{2}", this.Name, DotVisualUtils.AttributeAssignment, valfor);
			} else {
				return this.Name;
			}
		}
		#endregion
	}
}

