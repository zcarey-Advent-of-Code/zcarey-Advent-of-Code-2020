using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Parsing {
	public abstract class ParserBase<T> {

		internal ParserBase() {
		}

		// Invoke the underlying conversion
		abstract internal T Invoke(StreamReader input);

		// Actually compute the final parsed input
		internal T ParseInput(StreamReader input) {
			return Invoke(input);
		}


		public Parser<O> Parse<O>(Func<T, O> filter) {
			return new Parser<O>(
				(StreamReader input) => filter.Invoke(Invoke(input))
			);
		}

		public Parser<O> Parse<O>(IParser<T, O> filter) {
			return new Parser<O>(
				(StreamReader input) => filter.Parse(Invoke(input))
				);
		}

		public ParserFilter<P> Filter<P>(Func<T, IEnumerable<P>> filter) {
			return new ParserFilter<P>(
				(StreamReader input) => filter.Invoke(Invoke(input))
				);
		}

		public ParserFilter<P> Filter<P>(IParser<T, IEnumerable<P>> filter) {
			return new ParserFilter<P>(
				(StreamReader input) => filter.Parse(Invoke(input))
			);
		}

		public Parser<O> Create<O>() where O : IObjectParser<T>, new() {
			return this.Parse(
				(T input) => {
					O obj = new O();
					obj.Parse(input);
					return obj;
				}
			);
		}

	}

	public class Parser<T> : ParserBase<T> {

		private Func<StreamReader, T> func;

		internal Parser(Func<StreamReader, T> filter) {
			this.func = filter;
		}

		internal override T Invoke(StreamReader input) {
			return func.Invoke(input);
		}

	}

	public sealed class ParserFilter<T> : Parser<IEnumerable<T>> {

		internal ParserFilter(Func<StreamReader, IEnumerable<T>> filter) : base(filter) {
		}

		public ParserFilter<O> Filter<O>(Func<T, O> filter) {
			return base.Filter(
				(IEnumerable<T> input) => input.Select(filter)
			);
		}

		public Parser<T[]> ToArray() {
			return base.Parse(
				(IEnumerable<T> input) => input.ToArray()
			);
		}

		public ParserFilter<O> ForEach<O>(IParser<T, O> filter) {
			return base.Filter(
				(IEnumerable<T> input) => ForEach(input, filter)
			);
		}

		private static IEnumerable<O> ForEach<O>(IEnumerable<T> inputs, IParser<T, O> filter) {
			foreach(T input in inputs) {
				yield return filter.Parse(input);
			}
		}

		public ParserFilter<O> ForEach<O>(Func<T, O> filter) {
			return base.Filter(
				(IEnumerable<T> ParseInput) =>
			);
		}

		public ParserFilter<O> FilterCreate<O>() where O : IObjectParser<T>, new() {
			return base.Filter(FilterCreate<O>);
		}

		private static IEnumerable<O> FilterCreate<O>(IEnumerable<T> inputs) where O : IObjectParser<T>, new() {
			foreach (T input in inputs) {
				O obj = new O();
				obj.Parse(input);
				yield return obj;
			}
		}

	}

	public sealed class Parser : ParserBase<StreamReader> {

		public Parser() : base() {

		}

		internal override StreamReader Invoke(StreamReader input) {
			return input;
		}

		public Parser<O> Parse<I, O>() where I : IReader<O>, new() {
			return base.Parse(new I());
		}

		public ParserFilter<P> Filter<I, P>() where I : IReader<IEnumerable<P>>, new() {
			return base.Filter(new I());
		}

	}
}
