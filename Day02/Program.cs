using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2 {
	class Program : ParsedInputProgramStructure {

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(string[] input) {
			// A bit dirty but I think it still gets the point across
			return input.Select(x => processInputLine(x)).Where(x => x == true).Count().ToString();
		}

		protected override string CalculatePart2(string[] input) {
			// A bit dirty but I think it still gets the point across
			return input.Select(x => processInputLinePart2(x)).Where(x => x == true).Count().ToString();
		}

		static bool processInputLine(string input) {
			int separator = input.IndexOf(':');
			return new Policy(input).isValidPassword(input.Substring(separator + 2));
		}

		static bool processInputLinePart2(string input) {
			int separator = input.IndexOf(':');
			return new Policy(input).isValidPart2Password(input.Substring(separator + 2));
		}
	}
}
