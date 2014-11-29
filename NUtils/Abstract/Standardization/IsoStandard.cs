//
//  IsoStandard.cs
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

namespace NUtils.Abstract.Standardization {

	/// <summary>
	/// An implementation of the <see cref="T:IIsoStandard"/> interface containing information
	/// about an ISO standard.
	/// </summary>
	public class IsoStandard : NameShadow, IIsoStandard {
		#region Fields
		/// <summary>
		/// The name of the ISO standard.
		/// </summary>
		private readonly string name;
		#endregion
		#region IName implementation
		/// <summary>
		/// The name of the ISO standard.
		/// </summary>
		/// <value>A <see cref="string"/> containing the name of the ISO standard.</value>
		/// <remarks>
		/// <para>The name is guaranteed to be effective.</para>
		/// </remarks>
		public override string Name {
			get {
				return name;
			}
		}
		#endregion
		#region INote implementation
		/// <summary>
		/// A note describing the ISO standard.
		/// </summary>
		/// <value>A textual representation of the ISO standard.</value>
		/// <remarks>
		/// <para>The note is guaranteed to be effective.</para>
		/// </remarks>
		public string Note {
			get;
			private set;
		}
		#endregion
		#region IId implementation
		/// <summary>
		/// The identification number of the ISO standard.
		/// </summary>
		/// <value>An unsigned integer representing the identification number of the ISO standard.</value>
		public uint Id {
			get;
			private set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="IsoStandard"/> class with a given identification number, name and note
		/// describing the standard.
		/// </summary>
		/// <param name="id">The identification number of the standard.</param>
		/// <param name="name">The name of the standard, must be effective.</param>
		/// <param name="note">A description of the ISO standard, optional, by default the empty string, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given <paramref name="name"/> is not effective.</exception>
		/// <exception cref="ArgumentNullException">If the given <paramref name="note"/> is not effective.</exception>
		public IsoStandard (uint id, string name, string note = "") {
			if (name == null) {
				throw new ArgumentNullException ("name", "The name must be effective.");
			}
			if (note == null) {
				throw new ArgumentNullException ("note", "The note must be effective.");
			}
			Contract.EndContractBlock ();
			this.Id = id;
			this.name = name;
			this.Note = note;
		}
		#endregion
		#region Equals method
		/// <summary>
		/// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="IsoStandard"/>.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object"/> to compare with the current <see cref="IsoStandard"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Object"/> is equal to the current
		/// <see cref="IsoStandard"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>
		/// <para>An <see cref="T:IsoStandard"/> is equal to another <see cref="T:IIsoStandard"/> if the identification numbers
		/// match, the name is not taken into account.</para>
		/// </remarks>
		public override bool Equals (object obj) {
			IIsoStandard isos = obj as IIsoStandard;
			return (isos != null && this.Id == isos.Id);
		}
		#endregion
		#region GetHashCode method
		/// <summary>
		/// Serves as a hash function for a <see cref="IsoStandard"/> object.
		/// </summary>
		/// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
		public override int GetHashCode () {
			return this.Id.GetHashCode ();
		}
		#endregion
		#region ToString method
		/// <summary>
		/// Returns a <see cref="String"/> that represents the current <see cref="IsoStandard"/>.
		/// </summary>
		/// <returns>A <see cref="String"/> that represents the current <see cref="IsoStandard"/>.</returns>
		/// <remarks>
		/// <para>The textual representation is formatted as <c>ISO [id]: [name]</c>.</para>
		/// </remarks>
		public override string ToString () {
			return string.Format ("ISO {0}: {1}", Id, Name);
		}
		#endregion
	}
}

