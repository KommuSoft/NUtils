//
//  Maybe.cs
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
	/// A class which is the equivalent of the <c>Maybe</c> type in Haskell. It is used if we are not sure if an object of type parameter X can be returned.
	/// </summary>
	/// <typeparam name="X">The type of object that is expected.</typeparam>
	public class Maybe<X> {

		private readonly bool nothing = true;
		private readonly X val = default(X);

		/// <summary>
		/// A boolean who is True if the result can not be represented by the <typeparamref name="X"/>.
		/// </summary>
		public bool Nothing {
			get {
				return this.nothing;
			}
		}

		/// <summary>
		/// The answer it holds if the answer of a method can be represented by an <typeparamref name="X"/> object, otherwise the default value of this type.
		/// </summary>
		public X Value {
			get {
				return this.val;
			}
		}

		/// <summary>
		/// A constructor who creates a <see cref="T:Maybe"/> object if the answer cannot be represented. This is equivalent to Haskells <c>Nothing</c>.
		/// </summary>
		public Maybe () {
		}

		/// <summary>
		/// A constructor who creates a <see cref="T:Maybe"/> object if the answer can be represented. This is equivalent to Haskells <c>Just</c>.
		/// </summary>
		/// <param name="val">
		/// The answer to store in the <see cref="T:Maybe"/> object.
		/// </param>
		public Maybe (X val) {
			this.nothing = false;
			this.val = val;
		}

		/// <summary>
		/// Converts an answer implicit to a <see cref="T:Maybe"/> object.
		/// </summary>
		/// <param name="val">
		/// The value who must be converted to a <see cref="T:Maybe"/> object.
		/// </param>
		/// <returns>
		/// A maybe object who encapsulates the given value <paramref name="val"/>. This object is equivalent to Haskells <c>Just val</c>.
		/// </returns>
		public static implicit operator Maybe<X> (X val) {
			return new Maybe<X> (val);
		}
	}
}

