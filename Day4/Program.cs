using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day4 {
	class Program {

		const string InputFileName = "Input.txt";
		//const string InputFileName = "Valids.txt";
		//const string InputFileName = "Examples.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			Passport[] input = loadInputData();
			Passport2[] input2 = loadInputData2();

			Console.WriteLine("Calculating...");
			timer.Start();
			int answer = calculateAnswer(input);
			int part2 = calculatePart2(input2);
			timer.Stop();

			Console.WriteLine("The answer is {0}.", answer);
			Console.WriteLine("The answer to part 2 is {0}.", part2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		static Passport[] loadInputData() {
			//Dirty, but quick and I think still get's the point across.
			return File.ReadAllLines(InputFileName).GetGroups().Select(x => new Passport(x.GetElements())).ToArray();
		}

		static Passport2[] loadInputData2() {
			//Dirty, but quick and I think still get's the point across.
			return File.ReadAllLines(InputFileName).GetGroups().Select(x => new Passport2(x.GetElements())).ToArray();
		}

		static int calculateAnswer(Passport[] data) {
			int valid = 0;
			foreach(Passport passport in data) {
				passport.Validate();
				if (passport.IsValid) {
					valid++;
				}
			}
			return valid;
		}

		static int calculatePart2(Passport2[] data) {
			int valid = 0;
			foreach (Passport passport in data) {
				passport.Validate();
				if (passport.IsValid) {
					valid++;
				}
			}
			return valid;
		}

	}
}
