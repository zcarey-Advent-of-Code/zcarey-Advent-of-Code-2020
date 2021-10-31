using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_Parse {

		public static Parser<TInput, T> Parse<TInput, TOutput, T>(this ParserBase<TInput, TOutput> source, Func<TOutput, T> filter) {
			return new Parser<TInput, T>(
				(TInput input) => filter.Invoke(source.ParseInput(input))
			);
		}

		public static Parser<TInput, T> Parse<TInput, TOutput, T>(this ParserBase<TInput, TOutput> source, IParser<TOutput, T> filter) {
			return new Parser<TInput, T>(
				(TInput input) => filter.Parse(source.ParseInput(input))
			);
		}

	}
}
