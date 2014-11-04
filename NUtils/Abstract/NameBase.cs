//
//  NameBase.cs
//
//  Author:
//       Willem Van Onsem <vanonsem.willem@gmail.com>
//
//  Copyright (c) 2013 Willem Van Onsem
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

namespace ZincOxide.Utils.Abstract {

	/// <summary>
	/// An implementation of the <see cref="IName"/> interface.
	/// </summary>
	public abstract class NameBase : NameShadow, IName {

		#region Fields
		private string name;
		#endregion
		#region IName implementation
		/// <summary>
		///  Gets the name of this instance. 
		/// </summary>
		/// <value>
		///  The name of this instance. 
		/// </value>
		public override string Name {
			get {
				return this.name;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ZincOxide.Utils.Abstract.NameBase"/> class with a given
		/// initial name.
		/// </summary>
		/// <param name='name'>
		/// The initial name of this instance.
		/// </param>
		/// <remarks>
		/// <para>If no name is given; <c>null</c> is used.</para>
		/// </remarks>
		protected NameBase (string name = null) {
			this.SetName (name);
		}
		#endregion
		#region Protected name setter
		/// <summary>
		/// Sets the stored name in this <see cref="NameBase"/> instance.
		/// </summary>
		/// <param name="name">The new name to set.</param>
		protected virtual void SetName (string name) {
			this.name = name;
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="ZincOxide.Utils.Abstract.NameBase"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents the current <see cref="ZincOxide.Utils.Abstract.NameBase"/>.
		/// </returns>
		/// <remarks>
		/// <para>The default textual representation of an <see cref="IName"/> instance is the <see cref="Name"/>
		/// itself.</para>
		/// </remarks>
		public override string ToString () {
			return this.name;
		}
		#endregion
	}
}