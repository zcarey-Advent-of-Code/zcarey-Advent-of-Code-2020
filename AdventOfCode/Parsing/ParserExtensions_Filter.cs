using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_Filter {

		public static ParserFilter<TInput, T> Filter<TInput, TOutput, T>(this ParserBase<TInput, TOutput> source, Func<TOutput, IEnumerable<T>> filter) {
			return new ParserFilter<TInput, T>(
				(TInput input) => filter.Invoke(source.ParseInput(input))
				);
		}

		public static ParserFilter<TInput, T> Filter<TInput, TOutput, T>(this ParserBase<TInput, TOutput> source, IParser<TOutput, IEnumerable<T>> filter) {
			return new ParserFilter<TInput, T>(
				(TInput input) => filter.Parse(source.ParseInput(input))
			);
		}

		public static ParserFilter<TInput, T> Filter<TInput, TOutput, T>(this ParserFilter<TInput, TOutput> source, Func<TOutput, T> filter) {
			return source.Filter(
				(IEnumerable<TOutput> input) => input.Select(filter)
			);
		}

	}
}
