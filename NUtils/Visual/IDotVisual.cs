//
//  IDotVisual.cs
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
using System.IO;

namespace NUtils.Visual {

	/// <summary>
	/// An interface specifying that the object can be visualized using a <c>.dot</c> file (GraphViz DOT Graph).
	/// </summary>
	public interface IDotVisual {

		/// <summary>
		/// Write a GraphViz DOT Graph stream to the given <paramref name="textWriter"/> visualizing this instance.
		/// </summary>
		/// <param name="textWriter">The <see cref="T:TextWriter"/> to write this instance to.</param>
		void WriteDotText (TextWriter textWriter);
	}
}

