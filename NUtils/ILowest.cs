using System;

namespace NUtils {
	/// <summary>
	/// An interface specifying that this instance contains elements with an inherent order relation and that the
	/// lowest element can be derived (optionally given an additional lower bound).
	/// </summary>
	public interface ILowest<T> where T : IComparable<T> {

		/// <summary>
		/// Returns the lowest element contained in this instance.
		/// </summary>
		/// <returns>The lowest element contained in this instance.</returns>
		/// <remarks>
		/// <para>The elements are not necessarily stored explicitly: for instance an interval contains all points between two given numbers.</para>
		/// </remarks>
		T GetLowest ();

		/// <summary>
		/// Returns the lowest element contained in this instance that is greater than or equal to the given <paramref name="lower"/> bound.
		/// </summary>
		/// <returns>An element contained in this instance that is greater than or equal to the given <paramref name="lower"/> bound and the
		/// lowest element that satisfies this constraint.</returns>
		/// <param name="lower">The given lower bound.</param>
		/// <remarks>
		/// <para>The elements are not necessarily stored explicitly: for instance an interval contains all points between two given numbers.</para>
		/// </remarks>
		T GetLowest (T lower);
	}
}

