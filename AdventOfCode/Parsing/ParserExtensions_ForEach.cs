using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Parsing {
	public static class ParserExtensions_ForEach {

		/// <summary>
		/// Applys a function to each element in the input array.
		/// </summary>
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserFilter<TInput, TOutput> source, Func<TOutput, T> filter) {
			return source.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter)
			);
		}

		/// <summary>
		/// Applys an <see cref="IParser{I, T}"/> to each element in the input array.
		/// </summary>
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserFilter<TInput, TOutput> source, IParser<TOutput, T> filter) {
			return source.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter.Parse)
			);
		}

		/// <summary>
		/// Applys a <see cref="ParserBase{TInput, TOutput}"/> to each element in the input array.
		/// </summary>
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserFilter<TInput, TOutput> source, ParserBase<TOutput, T> filter) {
			return source.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter.ParseInput)
			);
		}

		/// <summary>
		/// Applys a function to each element in the input array.
		/// </summary>
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserBase<TInput, TOutput[]> source, Func<TOutput, T> filter) {
			return source.Filter(
				(TOutput[] input) => ForEach(input, filter)
			);
		}

		/// <summary>
		/// Applys an <see cref="IParser{I, T}"/> to each element in the input array.
		/// </summary>
		public static ParserFilter<TInput, T> ForEach<TInput, TOutput, T>(this ParserBase<TInput, TOutput[]> source, IParser<TOutput, T> filter) {
			return source.Filter(
				(TOutput[] input) => ForEach(input, filter.Parse)
			);
		}

		/// <summary>
		/// Applys a <see cref="ParserBase{TInput, TOutput}"/> to each element in the input array.
		/// </summary>
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

	}
}
