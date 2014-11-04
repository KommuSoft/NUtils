//
//  NameContract.cs
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
using System.Diagnostics.Contracts;

namespace ZincOxide.Utils.Abstract {

	/// <summary>
	/// A contract class of a <see cref="IName"/> instance.
	/// </summary>
	[ContractClassFor(typeof(IName))]
	public abstract class NameContract : IName {
		#region IName implementation
		/// <summary>
		/// Gets the name of this instance.
		/// </summary>
		/// <value>
		/// The name of this instance.
		/// </value>
		public virtual string Name {
			get {
				return default(string);
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NameContract"/> class.
		/// </summary>
		protected NameContract () {
		}
		#endregion
		#region IName implementation
		/// <summary>
		/// Check if the given <see cref="IName"/> instance has the same <see cref="Name"/> as this instance.
		/// </summary>
		/// <returns>
		/// <c>true</c> if the given <see cref="IName"/> instance has the same <see cref="Name"/> as this instance;
		/// <c>false</c> otherwise.
		/// </returns>
		/// <param name='other'>
		/// The <see cref="IName"/> instance to compare with.
		/// </param>
		public bool EqualName (IName other) {
			return default(bool);
		}
		#endregion
	}
}

