//
//  IFFormatFormatProviderToString.cs
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

namespace NUtils.Textual {
	/// <summary>
	/// An interface specifying that an instance supports being converted to a string, given a <see cref="String"/> that
	/// provides the requested format and an <see cref="IFormatProvider"/> instance that provides how certain elements
	/// like numbers should be represented.
	/// </summary>
	public interface IFormatFormatProviderToString : IFormatToString, IFormatProviderToString {

		/// <summary>
		/// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
		/// </summary>
		/// <returns>
		/// The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.
		/// </returns>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific formatting information.</param>
		string ToString (string format, IFormatProvider provider);
	}
}