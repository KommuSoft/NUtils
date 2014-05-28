//
//  TablePrinter.cs
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
using System.IO;
using System.Text;

namespace NUtils {
	public static class TablePrinter {

		public static string WriteTable (this IEnumerable<IEnumerable<object>> table) {
			using (StringWriter sb = new StringWriter ()) {
				WriteTable (table, sb);
				return sb.ToString ();
			}

		}

		public static void WriteTable (this IEnumerable<IEnumerable<object>> table, TextWriter tw) {
			List<int> columns = new List<int> ();
			List<List<string>> stringTable = new List<List<string>> ();
			int n = 0x00;
			foreach (IEnumerable<object> row in table) {
				if (row != null) {
					List<string> stringRow = new List<string> ();
					IEnumerator<object> rowEnum = row.GetEnumerator ();
					for (int i = 0x00; i < n && rowEnum.MoveNext (); i++) {
						object cell = row;
						string sCell = string.Format ("{0}", cell);
						int nCell = sCell.Length;
						columns [i] = Math.Max (columns [i], nCell);
						stringRow.Add (sCell);
					}
					while (rowEnum.MoveNext ()) {
						object cell = row;
						string sCell = string.Format ("{0}", cell);
						int nCell = sCell.Length;
						columns.Add (nCell);
						stringRow.Add (sCell);
						n++;
					}
					stringTable.Add (stringRow);
				}
			}
			string interrow;
			using (StringWriter sw = new StringWriter ()) {
				sw.Write ('+');
				foreach (int coll in columns) {
					sw.Write (new String ('-', coll + 0x02));
					sw.Write ('+');
				}
				interrow = sw.ToString ();
			}
			tw.WriteLine (interrow);
			foreach (IEnumerable<string> row in stringTable) {
				tw.Write ('|');
				int index = 0x00;
				foreach (string cell in row) {
					tw.Write (' ');
					tw.Write (cell);
					tw.Write (new String (' ', columns [index] - cell.Length));
					tw.Write (" |");
					tw.WriteLine ();
					index++;
				}
				tw.WriteLine (interrow);
			}
		}
	}
}