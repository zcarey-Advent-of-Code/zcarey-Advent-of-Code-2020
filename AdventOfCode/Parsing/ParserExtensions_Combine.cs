using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_Combine {

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<IEnumerable<TOutput>>> source) {
			return source.Filter(ParserExtensions_Combine.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<TOutput[]>> source) {
			return source.Filter(ParserExtensions_Combine.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<TOutput>[]> source) {
			return source.Filter(ParserExtensions_Combine.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, TOutput[][]> source) {
			return source.Filter(ParserExtensions_Combine.Combine);
		}

		private static IEnumerable<T> Combine<T>(IEnumerable<IEnumerable<T>> input) {
			foreach (IEnumerable<T> list in input) {
				foreach (T item in list) {
					yield return item;
				}
			}
		}

	}
}
