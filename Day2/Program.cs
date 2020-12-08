
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2 {
	class Program {

		const string InputFileName = "Input.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			string[] input = loadInputData();

			Console.WriteLine("Calculating...");
			timer.Start();
			int answer = calculateAnswer(input);
			int answer2 = calculateAnswerPart2(input);
			timer.Stop();

			Console.WriteLine("The answer is {0}.", answer);
			Console.WriteLine("The answer for part 2 is {0}.", answer2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		static string[] loadInputData() {
			return File.ReadAllLines(InputFileName);
		}

		static int calculateAnswer(string[] data) {
			// A bit dirty but I think it still gets the point across
			return data.Select(x => processInputLine(x)).Where(x => x == true).Count();
		}

		static bool processInputLine(string input) {
			int separator = input.IndexOf(':');
			return new Policy(input).isValidPassword(input.Substring(separator + 2));
		}

		static int calculateAnswerPart2(string[] data) {
			// A bit dirty but I think it still gets the point across
			return data.Select(x => processInputLinePart2(x)).Where(x => x == true).Count();
		}

		static bool processInputLinePart2(string input) {
			int separator = input.IndexOf(':');
			return new Policy(input).isValidPart2Password(input.Substring(separator + 2));
		}

	}
}
