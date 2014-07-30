//
//  IEnumerables.cs
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
using System.Collections.Generic;

namespace NUtils.Collections {
	#region Tuple Enumerables
	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`2"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2> : IEnumerable<Tuple<T1,T2>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`3"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3> : IEnumerable<Tuple<T1,T2,T3>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`4"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	/// <typeparam name='T4'>The type of the fourth element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3,T4> : IEnumerable<Tuple<T1,T2,T3,T4>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`5"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	/// <typeparam name='T4'>The type of the fourth element of the tuples.</typeparam>
	/// <typeparam name='T5'>The type of the fifth element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3,T4,T5> : IEnumerable<Tuple<T1,T2,T3,T4,T5>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`6"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	/// <typeparam name='T4'>The type of the fourth element of the tuples.</typeparam>
	/// <typeparam name='T5'>The type of the fifth element of the tuples.</typeparam>
	/// <typeparam name='T6'>The type of the sixth element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3,T4,T5,T6> : IEnumerable<Tuple<T1,T2,T3,T4,T5,T6>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`7"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	/// <typeparam name='T4'>The type of the fourth element of the tuples.</typeparam>
	/// <typeparam name='T5'>The type of the fifth element of the tuples.</typeparam>
	/// <typeparam name='T6'>The type of the sixth element of the tuples.</typeparam>
	/// <typeparam name='T7'>The type of the seventh element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3,T4,T5,T6,T7> : IEnumerable<Tuple<T1,T2,T3,T4,T5,T6,T7>> {
	}

	/// <summary>
	/// A "marker interface" used to mark a <see cref="T:IEnumerable`1"/> instances that
	/// enumerates <see cref="T:Tuple`8"/> instaces.
	/// </summary>
	/// <typeparam name='T1'>The type of the first element of the tuples.</typeparam>
	/// <typeparam name='T2'>The type of the second element of the tuples.</typeparam>
	/// <typeparam name='T3'>The type of the third element of the tuples.</typeparam>
	/// <typeparam name='T4'>The type of the fourth element of the tuples.</typeparam>
	/// <typeparam name='T5'>The type of the fifth element of the tuples.</typeparam>
	/// <typeparam name='T6'>The type of the sixth element of the tuples.</typeparam>
	/// <typeparam name='T7'>The type of the seventh element of the tuples.</typeparam>
	/// <typeparam name='T8'>The type of the eighth element of the tuples.</typeparam>
	public interface IEnumerable<T1,T2,T3,T4,T5,T6,T7,T8> : IEnumerable<Tuple<T1,T2,T3,T4,T5,T6,T7,T8>> {
	}
	#endregion
}

