//
//  ITransitionSensitive.cs
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

namespace NUtils.Maths {
	/// <summary>
	/// An interface specifying that the instance is sensitive to a <see cref="T:ITransition"/> instance.
	/// </summary>
	/// <typeparam name='TTransition'>
	/// The type of the transition function (sometimes used to be more specific, by default <see cref="ITranstion"/>).
	/// </typeparam>
	public interface ITransitionSensitive<out TTransition> where TTransition : ITransition {

		/// <summary>
		/// Gets the transition this <see cref="T:ITransitionSensitive`1"/> instance is sensitive to.
		/// </summary>
		/// <value>The transition this <see cref="T:ITransitionSensitive`1"/> instance is sensitive to.</value>
		TTransition Transition {
			get;
		}
	}
}