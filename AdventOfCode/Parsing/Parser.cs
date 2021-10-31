using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Parsing {
	public abstract class ParserBase<TInput, TOutput> {

		internal ParserBase() {
		}

		// Invoke the underlying conversion
		abstract internal TOutput Invoke(TInput input);

		// Actually compute the final parsed input
		internal TOutput ParseInput(TInput input) {
			return Invoke(input);
		}


		public Parser<TInput, T> Parse<T>(Func<TOutput, T> filter) {
			return new Parser<TInput, T>(
				(TInput input) => filter.Invoke(Invoke(input))
			);
		}

		public Parser<TInput, T> Parse<T>(IParser<TOutput, T> filter) {
			return new Parser<TInput, T>(
				(TInput input) => filter.Parse(Invoke(input))
				);
		}

		public ParserFilter<TInput, T> Filter<T>(Func<TOutput, IEnumerable<T>> filter) {
			return new ParserFilter<TInput, T>(
				(TInput input) => filter.Invoke(Invoke(input))
				);
		}

		public ParserFilter<TInput, T> Filter<T>(IParser<TOutput, IEnumerable<T>> filter) {
			return new ParserFilter<TInput, T>(
				(TInput input) => filter.Parse(Invoke(input))
			);
		}

		public Parser<TInput, T> Create<T>() where T : IObjectParser<TOutput>, new() {
			return this.Parse(
				(TOutput input) => {
					T obj = new T();
					obj.Parse(input);
					return obj;
				}
			);
		}

	}

	public class Parser<TInput, TOutput> : ParserBase<TInput, TOutput> {

		private Func<TInput, TOutput> func;

		internal Parser(Func<TInput, TOutput> filter) {
			this.func = filter;
		}

		internal override TOutput Invoke(TInput input) {
			return func.Invoke(input);
		}

	}

	public sealed class ParserFilter<TInput, TOutput> : Parser<TInput, IEnumerable<TOutput>> {

		internal ParserFilter(Func<TInput, IEnumerable<TOutput>> filter) : base(filter) {
		}

		public ParserFilter<TInput, T> Filter<T>(Func<TOutput, T> filter) {
			return base.Filter(
				(IEnumerable<TOutput> input) => input.Select(filter)
			);
		}

		public Parser<TInput, TOutput[]> ToArray() {
			return base.Parse(
				(IEnumerable<TOutput> input) => input.ToArray()
			);
		}

		#region ForEach
		public ParserFilter<TInput, T> ForEach<T>(Func<TOutput, T> filter) {
			return base.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter)
			);
		}

		public ParserFilter<TInput, T> ForEach<T>(IParser<TOutput, T> filter) {
			return base.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter.Parse)
			);
		}

		public ParserFilter<TInput, T> ForEach<T>(ParserBase<TOutput, T> filter) {
			return base.Filter(
				(IEnumerable<TOutput> input) => ForEach(input, filter.ParseInput)
			);
		}

		private static IEnumerable<T> ForEach<T>(IEnumerable<TOutput> inputs, Func<TOutput, T> filter) {
			foreach (TOutput input in inputs) {
				yield return filter.Invoke(input);
			}
		}
		#endregion

		#region FilterCreate
		public ParserFilter<TInput, T> FilterCreate<T>() where T : IObjectParser<TOutput>, new() {
			return base.Filter(FilterCreate<T>);
		}

		private static IEnumerable<T> FilterCreate<T>(IEnumerable<TOutput> inputs) where T : IObjectParser<TOutput>, new() {
			foreach (TOutput input in inputs) {
				T obj = new T();
				obj.Parse(input);
				yield return obj;
			}
		}
		#endregion

	}

	public class Parser<T> : ParserBase<T, T> {

		public Parser() { }

		internal override T Invoke(T input) {
			return input;
		}
	}

	public sealed class Parser : Parser<StreamReader> {

		public Parser() { }

		/*
				public Parser<StreamReader, T> Parse<I, T>() where I : IReader<T>, new() {
					return base.Parse(new I());
				}

				public ParserFilter<StreamReader, T> Filter<I, T>() where I : IReader<IEnumerable<T>>, new() {
					return base.Filter(new I());
				}
		*/
	}

}
