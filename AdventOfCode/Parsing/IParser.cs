using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Parsing {

	// I used abstract class instead of interface because the interface method definitions were annoying
	public abstract class IParser<I, T> {

		abstract internal T Parse(I input);

	}

	public abstract class IFilter<I, T> : IParser<IEnumerable<I>, IEnumerable<T>> {
	}

	public abstract class IReader<T> : IParser<StreamReader, T> {
	}


	/// <summary>
	/// Used for parsing objects, since a new object needs to be created for every input needed.
	/// </summary>
	/// <typeparam name="I"></typeparam>
	public interface IObjectParser<I> {

		/// <summary>
		/// Parse the object using the given input. Assume it is a new object.
		/// </summary>
		/// <param name="input"></param>
		public void Parse(I input);

	}
	
}
