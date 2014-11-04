//
//  NameIdBase.cs
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

namespace ZincOxide.Utils.Abstract {

	/// <summary>
	/// An implementation of the <see cref="INameId"/> interface.
	/// </summary>
	public abstract class NameIdBase : NameBase, INameId {

		private static uint idDispatcher = 0x00;

		private readonly uint id;

        #region IId implementation
		/// <summary>
		///  Gets the identifier of this instance. 
		/// </summary>
		/// <value>
		///  The identifier of this instance. 
		/// </value>
		public uint Id {
			get {
				return this.id;
			}
		}
        #endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="NameIdBase"/> class with a given initial name.
		/// </summary>
		/// <param name='name'>
		/// The given initial name.
		/// </param>
		/// <remarks>
		/// <para>If no name is given; <c>null</c> is used.</para>
		/// <para>An identifier is assigned to the instance automatically in a round robin fashion. Therefore
		/// the chance that two instances have the same identifier is almost zero.</para>
		/// </remarks>
		protected NameIdBase (string name = null) : base(name) {
			this.id = idDispatcher++;
		}
		#endregion

	}
}

