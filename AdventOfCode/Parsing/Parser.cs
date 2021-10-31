using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Parsing {
	public abstract class ParserBase<TInput, TOutput> {

		internal ParserBase() {
		}

		// Actually compute the final parsed input
		abstract internal TOutput ParseInput(TInput input);

	}

	public class Parser<TInput, TOutput> : ParserBase<TInput, TOutput> {

		private Func<TInput, TOutput> func;

		internal Parser(Func<TInput, TOutput> filter) {
			this.func = filter;
		}

		internal override TOutput ParseInput(TInput input) {
			return func.Invoke(input);
		}

	}

	public sealed class ParserFilter<TInput, TOutput> : Parser<TInput, IEnumerable<TOutput>> {

		internal ParserFilter(Func<TInput, IEnumerable<TOutput>> filter) : base(filter) {
		}

	}

	public class Parser<T> : ParserBase<T, T> {

		public Parser() { }

		internal override T ParseInput(T input) {
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
