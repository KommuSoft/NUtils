//
//  IdBase.cs
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
	/// An implementation of the <see cref="IId"/> interface that automatically assigns a read-only identifer
	/// to each object instantiated from this class.
	/// </summary>
	public abstract class IdBase : IId {

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
		/// Initializes a new instance of the <see cref="ZincOxide.Utils.Abstract.IdBase"/> class.
		/// </summary>
		/// <remarks>
		/// <para>An identifier is assigned to the instance automatically in a round robin fashion. Therefore
		/// the chance that two instances have the same identifier is almost zero.</para>
		/// </remarks>
		protected IdBase () {
			this.id = idDispatcher++;
		}
		#endregion

	}

}