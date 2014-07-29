//
//  FunctionUtils.cs
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

namespace NUtils.Functional {
	/// <summary>
	/// A set of utility functions used to generate and combine functions.
	/// </summary>
	public static class FunctionUtils {

		/// <summary>
		/// The identity function, used to returns the same element. Sometimes useful when a function is
		/// required, but not useful.
		/// </summary>
		/// <param name="x">The parameter, that must be returned.</param>
		/// <returns>The same element as the given one.</returns>
		/// <typeparam name="T">The type of elements handled by the identity function.</typeparam>
		public static T Identity<T> (this T x) {
			return x;
		}

		/// <summary>
		/// Modifies the original given <paramref name="function"/> by adding a parameter to the
		/// right that has no use. This is done to convert a function in such way that a given method
		/// can call the modified function.
		/// </summary>
		/// <returns>A modified function with additional parameters to the right that are useless.</returns>
		/// <param name="originalFunction">The original function to modify.</param>
		/// <typeparam name="TX1">The type of the introduced parameter.</typeparam>
		/// <typeparam name="TF">The resulting type of applying the given original and resulting function.</typeparam>
		public static Func<TX1,TF> ShiftRightParameter<TX1,TF> (this Func<TF> originalFunction) {
			return x1 => originalFunction ();
		}

		/// <summary>
		/// Modifies the original given <paramref name="function"/> by adding a parameter to the
		/// right that has no use. This is done to convert a function in such way that a given method
		/// can call the modified function.
		/// </summary>
		/// <returns>A modified function with additional parameters to the right that are useless.</returns>
		/// <param name="originalFunction">The original function to modify.</param>
		/// <typeparam name="TX1">The type of the first parameter of the original function.</typeparam>
		/// <typeparam name="TX2">The type of the introduced parameter.</typeparam>
		/// <typeparam name="TF">The resulting type of applying the given original and resulting function.</typeparam>
		public static Func<TX1,TX2,TF> ShiftRightParameter<TX1,TX2,TF> (this Func<TX1,TF> originalFunction) {
			return (x1,x2) => originalFunction (x1);
		}

		/// <summary>
		/// Modifies the original given <paramref name="function"/> by adding a parameter to the
		/// right that has no use. This is done to convert a function in such way that a given method
		/// can call the modified function.
		/// </summary>
		/// <returns>A modified function with additional parameters to the right that are useless.</returns>
		/// <param name="originalFunction">The original function to modify.</param>
		/// <typeparam name="TX1">The type of the first parameter of the original function.</typeparam>
		/// <typeparam name="TX2">The type of the second parameter of the original function.</typeparam>
		/// <typeparam name="TX3">The type of the introduced parameter.</typeparam>
		/// <typeparam name="TF">The resulting type of applying the given original and resulting function.</typeparam>
		public static Func<TX1,TX2,TX3,TF> ShiftRightParameter<TX1,TX2,TX3,TF> (this Func<TX1,TX2,TF> originalFunction) {
			return (x1,x2,x3) => originalFunction (x1, x2);
		}
	}
}

