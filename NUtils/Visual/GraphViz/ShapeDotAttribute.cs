//
//  ShapeDotAttribute.cs
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

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// An attribute that can be assigned to nodes to alter the shape of the node.
	/// </summary>
	public class ShapeDotAttribute : DotAttribute, IShapeDotAttribute {

		#region Constants
		/// <summary>
		/// The name of this attribute.
		/// </summary>
		public const string AttributeName = "shape";
		#endregion
		#region IShapeDotAttribute implementation
		/// <summary>
		/// Get the shape of the node to which this attribute is assigned.
		/// </summary>
		/// <value>A <see cref="T:IDotShape"/> that describes the shape of the assigned node.</value>
		public IDotShape Shape {
			get;
			protected set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeDotAttribute"/> class with the default shape.
		/// </summary>
		/// <remarks>
		/// <para>The default shape is the <see cref="T:CircleDotShape"/> instance.</para>
		/// </remarks>
		public ShapeDotAttribute () : this(CircleDotShape.Instance) {

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ShapeDotAttribute"/> class with the given <see cref="T:IDotShape"/> that describes
		/// the shape of the node.
		/// </summary>
		/// <param name="shape">The shape that is assigned to the node with this attribute, must be effective.</param>
		/// <exception cref="ArgumentNullException">If the given shape is not effective.</exception>
		/// <remarks>
		/// <para>The exists a default constructor <see cref="C:ShapeDotAttribute()"/> where the shape is by default
		/// a circle.</para>
		/// </remarks>
		public ShapeDotAttribute (IDotShape shape) : base(AttributeName) {
			if (shape == null) {
				throw new ArgumentNullException ("shape", "The shape must be effective.");
			}
			Contract.EndContractBlock ();
			this.Shape = shape;
		}
		#endregion
		#region implemented abstract members of DotAttribute
		/// <summary>
		/// The value formatting for the <see cref="T:ShapeDotAttribute"/>. This is simply the name of the shape.
		/// </summary>
		/// <returns>The formatted name of the shape, in other words the name of the shape.</returns>
		protected override string ValueFormat () {
			return this.Shape.Name;
		}
		#endregion
	}
}

