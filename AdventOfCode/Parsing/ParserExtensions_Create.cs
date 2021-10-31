using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_Create {

		public static Parser<TInput, T> Create<TInput, TOutput, T>(this ParserBase<TInput, TOutput> source) where T : IObjectParser<TOutput>, new() {
			return source.Parse(
				(TOutput input) => {
					T obj = new T();
					obj.Parse(input);
					return obj;
				}
			);
		}

		public static ParserFilter<TInput, T> FilterCreate<TInput, TOutput, T>(this ParserBase<TInput, IEnumerable<TOutput>> source) where T : IObjectParser<TOutput>, new() {
			return source.Filter(FilterCreate<TOutput, T>);
		}

		private static IEnumerable<T> FilterCreate<TOutput, T>(IEnumerable<TOutput> inputs) where T : IObjectParser<TOutput>, new() {
			foreach (TOutput input in inputs) {
				T obj = new T();
				obj.Parse(input);
				yield return obj;
			}
		}

	}
}
