using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Common {

	public abstract class ProgramStructure<T> {

		private Stopwatch timer = new Stopwatch();

		protected abstract string CalculatePart1(T input);
		protected abstract string CalculatePart2(T input);

		public void Run(string FileName, bool skipPart1 = false) {
			Console.WriteLine("Loading data...");
			Console.WriteLine();
			Console.WriteLine();
			T input1 = LoadInputData(FileName);
			T input2 = LoadInputData(FileName);

			Console.WriteLine("Part 1");
			if (!skipPart1) {
				timer.Start();
				string answer1 = CalculatePart1(input1);
				timer.Stop();
				Console.WriteLine("The answer is {0}.", answer1);
				Console.WriteLine("This only took {0:g}!", timer.Elapsed);
			} else {
				Console.WriteLine("Part 1 was skipped!");
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Part 2");
			timer.Restart();
			string answer2 = CalculatePart2(input2);
			timer.Stop();
			Console.WriteLine("The answer is {0}.", answer2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		protected abstract T LoadInputData(string FileName);

	}

	public abstract class ProgramStructure : ProgramStructure<string> {
		protected sealed override string LoadInputData(string FileName) {
			return File.ReadAllText(FileName);
		}
	}

	public abstract class ParsedInputProgramStructure<T> : ProgramStructure<T[]> {
		private Func<string, T> Parser;

		protected ParsedInputProgramStructure(Func<string, T> ParseFunc) {
			this.Parser = ParseFunc;
		}

		protected sealed override T[] LoadInputData(string FileName) {
			return File.ReadAllLines(FileName).Select(x => Parser(x)).ToArray();
		}
	}

	public abstract class ParsedInputProgramStructure : ProgramStructure<string[]> {
		protected sealed override string[] LoadInputData(string FileName) {
			return File.ReadAllLines(FileName);
		}
	}

	public abstract class SingleParsedInputProgramStructure<T> : ProgramStructure<T> {
		private Func<string, T> Parser;

		protected SingleParsedInputProgramStructure(Func<string, T> ParseFunc) {
			this.Parser = ParseFunc;
		}

		protected sealed override T LoadInputData(string FileName) {
			return Parser(File.ReadAllText(FileName));
		}
	}

	public interface FullInputParser {
		void Parse(string[] input); //Throw an exception is parsing fails.
	}

	public abstract class FullParsedInputProgramStructure<T> : ProgramStructure<T> where T : FullInputParser, new() {
		protected sealed override T LoadInputData(string FileName) {
			T result = new T();
			result.Parse(File.ReadAllLines(FileName));
			return result;
		}
	}

	public abstract class BlockInputProgramStructure : ProgramStructure<IEnumerable<string[]>> {
		protected sealed override IEnumerable<string[]> LoadInputData(string FileName) {
			return File.ReadAllLines(FileName).GetGroups().Select(x => x.ToArray());
		}
	}

	public abstract class BlockParsedInputProgramStructure<T> : ProgramStructure<T[]> where T : FullInputParser, new() {
		protected sealed override T[] LoadInputData(string FileName) {
			return File.ReadAllLines(FileName).GetGroups().Select(x => {
				T result = new T();
				result.Parse(x.ToArray());
				return result;
			}).ToArray();
		}
	}

	public abstract class SeparatedInputProgramStructure : ProgramStructure<string[]> {
		private string separator;
		protected SeparatedInputProgramStructure(string Separator) {
			this.separator = Separator;
		}
		protected sealed override string[] LoadInputData(string FileName) {
			return File.ReadAllText(FileName).Split(separator);
		}
	}

	public abstract class ParsedSeparatedInputProgramStructure<T> : ProgramStructure<T[]> {
		private string separator;
		private Func<string, T> Parser;

		protected ParsedSeparatedInputProgramStructure(string Separator, Func<string, T> ParseFunc) {
			this.separator = Separator;
			this.Parser = ParseFunc;
		}
		protected sealed override T[] LoadInputData(string FileName) {
			return File.ReadAllText(FileName).Split(separator).Select(Parser).ToArray();
		}
	}
}
