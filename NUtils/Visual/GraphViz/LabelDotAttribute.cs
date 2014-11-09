//
//  LabelDotAttribute.cs
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

namespace NUtils.Visual.GraphViz {

	/// <summary>
	/// An implementation of the <see cref="T:ILabelDotAttribute"/> interface that assigns a label to a node or edge.
	/// </summary>
	public class LabelDotAttribute : DotAttribute, ILabelDotAttribute {

		#region Constants
		/// <summary>
		/// The name of this attribute.
		/// </summary>
		public const string AttributeName = "label";
		#endregion
		#region ILabelDotAttribute implementation
		/// <summary>
		/// Get the label associated with this dot attribute.
		/// </summary>
		/// <value>The label associated with this dot attribute that will be printed on the corresponding node or edge.</value>
		public string Label {
			get;
			protected set;
		}
		#endregion
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LabelDotAttribute"/> class with the label to be assigned to the node or edge.
		/// </summary>
		/// <param name="label">The label that must be assigned to the given label or edge.</param>
		public LabelDotAttribute (string label) : base(AttributeName) {
			this.Label = label;
		}
		#endregion
		#region implemented abstract members of DotAttribute
		/// <summary>
		/// Format the given label by adding additional quotes to the the assigned label.
		/// </summary>
		/// <returns>The formatted version of this <see cref="Label"/>.</returns>
		protected override string ValueFormat () {
			return string.Format ("{0}{1}{2}", DotVisualUtils.StringOpen, this.Label, DotVisualUtils.StringClose);
		}
		#endregion
	}
}

