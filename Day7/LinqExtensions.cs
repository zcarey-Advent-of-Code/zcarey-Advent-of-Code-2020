using System;
using System.Collections.Generic;
using System.Text;

namespace Day7 {
	public static class LinqExtensions {

		/// <summary>
		/// Returns the lines of strings that are separated by a blank line (text blocks)
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<IEnumerable<string>> GetGroups(this IEnumerable<string> source) {
			List<string> group = new List<string>();
			foreach (string line in source) {
				if (!string.IsNullOrWhiteSpace(line)) {
					group.Add(line);
				} else {
					yield return group;
					group.Clear();
				}
			}

			if (group.Count > 0) {
				yield return group;
			}
		}

		/// <summary>
		/// Gets all string elements separated by either space or newlines
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		public static IEnumerable<string> GetElements(this IEnumerable<string> source) {
			foreach (string line in source) {
				string[] elements = line.Split();
				foreach (string element in elements) {
					yield return element;
				}
			}
		}

	}
}
