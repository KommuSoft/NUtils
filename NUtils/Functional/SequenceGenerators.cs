//
//  SequenceGenerators.cs
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
using System.Linq;
using System.Collections.Generic;

namespace NUtils.Functional {
	/// <summary>
	/// A utility class to generate several kinds of sequences with specific constraints.
	/// </summary>
	public static class SequenceGenerators {

		/// <summary>
		/// Nexts the sequence.
		/// </summary>
		/// <returns>The sequence.</returns>
		/// <param name="initial">Initial.</param>
		/// <param name="elementNext">Element next.</param>
		/// <param name="overflow">Overflow.</param>
		/// <param name="partiallyCorrect">Partially correct.</param>
		/// <typeparam name="TElement">The 1st type parameter.</typeparam>
		/// <typeparam name="TList">The 1st type parameter.</typeparam>
		/// <c<remarks>
		/// <para>The given <paramref name="initial"/> list is modified.</para>
		/// </remarks>
		public static bool NextSequence<TElement,TList> (this TList list, Func<TElement,TElement> elementNext, TElement initial, Predicate<TElement> overflow, Func<TList,int,bool> partiallyCorrect) where TList : IList<TElement> {
			return NextSequence<TElement,TList> (list, elementNext, initial, overflow, partiallyCorrect, list.Count);
		}

		/// <summary>
		/// Nexts the sequence.
		/// </summary>
		/// <returns>The sequence.</returns>
		/// <param name="initial">Initial.</param>
		/// <param name="elementNext">Element next.</param>
		/// <param name="overflow">Overflow.</param>
		/// <param name="partiallyCorrect">Partially correct.</param>
		/// <typeparam name="TElement">The 1st type parameter.</typeparam>
		/// <typeparam name="TList">The 1st type parameter.</typeparam>
		/// <c<remarks>
		/// <para>The given <paramref name="initial"/> list is modified.</para>
		/// </remarks>
		public static bool NextSequence<TElement,TList> (this TList list, Func<TElement,TElement> elementNext, TElement initial, Predicate<TElement> overflow, Func<TList,int,bool> partiallyCorrect, int length) where TList : IList<TElement> {
			if (length > 0x00) {
				int l1 = length - 0x01;
				do {
					list [l1] = elementNext (list [l1]);
				} while (!partiallyCorrect(list,length) && !overflow(list[l1]));
				while (overflow(list[l1])) {
					list [l1] = initial;
					if (!NextSequence (list, elementNext, initial, overflow, partiallyCorrect, l1)) {
						return false;
					}
					while (!partiallyCorrect(list,length)) {
						list [l1] = elementNext (list [l1]);
					}
				}
				return true;
			} else {
				return false;
			}
		}
	}
}

