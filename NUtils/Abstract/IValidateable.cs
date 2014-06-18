using System;

namespace NUtils.Abstract {
	/// <summary>
	/// An interface specifying that an instance can be invalid, but that an instance has the ability to check this.
	/// </summary>
	public interface IValidateable {

		/// <summary>
		/// Gets a value indicating whether this instance is valid.
		/// </summary>
		/// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
		bool IsValid {
			get;
		}
	}
}

