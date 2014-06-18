using System;
using System.Collections.Generic;

namespace NUtils {
	/// <summary>
	/// An interface that specifies that the items of this instance can be enumerated that are larger or equal
	/// to a certain given lower bound.
	/// </summary>
	/// <typeparam name='T'>
	/// The type of elements to enumerate.
	/// </typeparam>
	public interface ILowerEnumerable<T> where T : IComparable<T> {

		/// <summary>
		/// Get an <see cref="T:IEnumerator`1"/> that enumerates all items that are larger than or equal to the given
		/// <paramref name="lower"/> bound.
		/// </summary>
		/// <returns>An <see cref="T:IEnumerator`1"/> that enumerates all items larger than or equal to the given
		/// <paramref name="lower"/> bound.</returns>
		/// <param name="lower">The given lower bound of the items to be enumerated.</param>
		/// <remarks>
		/// <para>Unless stated explicitly, there is no predefined order on how the items are enumerated: the
		/// items are larger than or equal to the lower bound, but not necessarily order in ascending order.</para>
		/// </remarks>
		IEnumerator<T> GetEnumeratorLower (T lower);
	}
}

