using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_Utils {

		public static Parser<TInput, TOutput[]> ToArray<TInput, TOutput>(this ParserBase<TInput, IEnumerable<TOutput>> source) {
			return source.Parse(
				(IEnumerable<TOutput> input) => input.ToArray()
			);
		}

	}
}
