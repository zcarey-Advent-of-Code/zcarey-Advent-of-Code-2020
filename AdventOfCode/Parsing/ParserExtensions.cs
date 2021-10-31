using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions {

		#region Combine
		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<IEnumerable<TOutput>>> source) {
			return source.Filter(ParserExtensions.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<TOutput[]>> source) {
			return source.Filter(ParserExtensions.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, IEnumerable<TOutput>[]> source) {
			return source.Filter(ParserExtensions.Combine);
		}

		public static ParserFilter<TInput, TOutput> Combine<TInput, TOutput>(this ParserBase<TInput, TOutput[][]> source) {
			return source.Filter(ParserExtensions.Combine);
		}

		private static IEnumerable<T> Combine<T>(IEnumerable<IEnumerable<T>> input) {
			foreach(IEnumerable<T> list in input) {
				foreach(T item in list) {
					yield return item;
				}
			}
		}
		#endregion

		#region ForEach
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserBase<TInput, TOutput[]> source, Func<TOutput, T> filter) {
			return source.Filter(
				(TOutput[] input) => ForEach(input, filter)
			);
		}

		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserBase<TInput, TOutput[]> source, IParser<TOutput, T> filter) {
			return source.Filter(
				(TOutput[] input) => ForEach(input, filter.Parse)
			);
		}

		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserBase<TInput, TOutput[]> source, Parser<TOutput, T> filter) {
			return source.Filter(
				(TOutput[] input) => ForEach(input, filter.ParseInput)
			);
		}

		private static IEnumerable<T> ForEach<TOutput, T>(IEnumerable<TOutput> inputs, Func<TOutput, T> filter) {
			foreach (TOutput input in inputs) {
				yield return filter.Invoke(input);
			}
		}
		#endregion

	}
}
