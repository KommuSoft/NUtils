//
//  BoxDotShape.cs
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
	/// A shape supported by the GraphViz DOT graph format that shapes a node as a box.
	/// </summary>
	public class BoxDotShape : NameShadow, IDotShape {

		#region Constants
		/// <summary>
		/// The GraphViz DOT graph identifier for this shape.
		/// </summary>
		public const string Identifier = "box";
		/// <summary>
		/// The single instance created of this shape.
		/// </summary>
		public static readonly BoxDotShape Instance = new BoxDotShape ();
		#endregion
		#region IName implementation
		/// <summary>
		/// Gets the name of this shape, the GraphViz DOT graph identifier.
		/// </summary>
		/// <value>The name of this shape.</value>
		public override string Name {
			get {
				return Identifier;
			}
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:BoxDotShape"/> class.
		/// </summary>
		/// <remarks>
		/// <para>Private by singleton pattern, use <see cref="Instance"/> to obtain the instance.</para>
		/// </remarks>
		private BoxDotShape () {
		}
		#endregion
	}
}