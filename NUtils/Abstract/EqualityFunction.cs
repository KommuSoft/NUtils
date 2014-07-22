//
//  EqualityFunction.cs
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
namespace NUtils.Abstract {
	/// <summary>
	/// A function who has the proper signature for an equality test function.
	/// </summary>
	/// <remarks>
	/// <typeparam name='TX'>The type on which the equality function is defined.</typeparam>
	/// All equality functions (not all functions with the same signature are equality functions of course) must be
	/// reflexive, symmetric and transitive.
	/// </remarks>
	public delegate bool EqualityFunction<TX> (TX x1, TX x2);
}

