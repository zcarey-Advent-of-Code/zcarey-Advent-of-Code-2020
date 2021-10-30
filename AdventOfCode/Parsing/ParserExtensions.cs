using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions {

		public static ParserFilter<T> Combine<T>(this Parser<IEnumerable<IEnumerable<T>>> source) {
			return source.Filter(ParserExtensions.Combine);
		}

		private static IEnumerable<T> Combine<T>(IEnumerable<IEnumerable<T>> input) {
			foreach(IEnumerable<T> list in input) {
				foreach(T item in list) {
					yield return item;
				}
			}
		}

	}
}
