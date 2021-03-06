//
//  ILabelDotAttribute.cs
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
	/// An <see cref="T:IDotAttribute"/> that describes the label that is attached to a node or edge.
	/// </summary>
	public interface ILabelDotAttribute : INodeDotAttribute, IEdgeDotAttribute {

		/// <summary>
		/// Get the label associated with this dot attribute.
		/// </summary>
		/// <value>The label associated with this dot attribute that will be printed on the corresponding node or edge.</value>
		string Label {
			get;
		}
	}
}

