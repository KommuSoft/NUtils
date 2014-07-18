//
//  IOUtils.cs
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
using System.IO;

namespace NUtils.IO {
	/// <summary>
	/// A set of utility methods for reading an writing files (mainly used by <see cref="IReadable"/> and <see cref="IWriteable"/> instances).
	/// </summary>
	public static class IOUtils {
		#region IReadable methods
		/// <summary>
		/// Reads the data stored in the given stream and modifies the given <see cref="IReadable"/>.
		/// </summary>
		/// <param name="readable">The <see cref="IReadable"/> that reads data from the given <see cref="Stream"/>.</param>
		/// <param name="stream">The given <see cref="Stream"/> that contains the data.</param>
		public static void ReadFromStream (this IReadable readable, Stream stream) {
			using (TextReader tr = new StreamReader(stream)) {
				readable.ReadFromStream (tr);
			}
		}
		#endregion
		#region IWriteable methods
		/// <summary>
		/// Write the data of the given <see cref="IWriteable"/> to the given <see cref="Stream"/>.
		/// </summary>
		/// <param name="writeable">The given <see cref="IWriteable"/> that contains the data to write.</param>
		/// <param name="stream">The given <see cref="Stream"/> to write the data to.</param>
		public static void WriteToStream (this IWriteable writeable, Stream stream) {
			using (TextWriter tw = new StreamWriter(stream)) {
				writeable.WriteToStream (tw);
			}
		}
		#endregion
	}
}

