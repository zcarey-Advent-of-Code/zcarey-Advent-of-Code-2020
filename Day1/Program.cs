using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1 {
	class Program {

		const string InputFileName = "Input.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			int[] input = loadInputData();

			Console.WriteLine("Calculating...");
			timer.Start();
			int answer = calculateAnswer(input);
			long answerPart2 = calculatePart2(input);
			timer.Stop();

			Console.WriteLine("The answer is {0}.", answer);
			Console.WriteLine("The answer to part 2 is {0}.", answerPart2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		static int[] loadInputData() {
			//Dirty, but quick and I think still get's the point across.
			return File.ReadAllLines(InputFileName).Select(x => int.Parse(x)).ToArray();
		}

		static int calculateAnswer(int[] data) {
			for (int i = 0; i < data.Length - 1; i++) {
				for (int j = i + 1; j < data.Length; j++) {
					if (data[i] + data[j] == 2020) {
						return data[i] * data[j];
					}
				}
			}
			throw new Exception("Unable to find the answer!!!");
		}

		static long calculatePart2(int[] data) {
			for (int i = 0; i < data.Length - 2; i++) {
				for (int j = i + 1; j < data.Length - 1; j++) {
					for (int k = j + 1; k < data.Length; k++) {
						if (data[i] + data[j] + data[k] == 2020) {
							return data[i] * data[j] * data[k];
						}
					}
				}
			}
			throw new Exception("Unable to find the answer!!!");
		}
	}
}
