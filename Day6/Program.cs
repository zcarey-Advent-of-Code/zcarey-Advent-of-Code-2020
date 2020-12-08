using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Day6 {
	class Program {

		const string InputFileName = "Input.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			Console.WriteLine();
			Console.WriteLine();
			string[] data = File.ReadAllLines(InputFileName);

			Console.WriteLine("Part 1");
			timer.Start();
			int answer1 = calculatePart1(data);
			timer.Stop();
			Console.WriteLine("The answer is {0}.", answer1);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Part 2");
			timer.Restart();
			int answer2 = calculatePart2(data);
			timer.Stop();
			Console.WriteLine("The answer is {0}.", answer2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);

			Console.WriteLine();
			Console.WriteLine();
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		static int calculatePart1(string[] input) {
			int count = 0;
			foreach(IEnumerable<string> group in input.GetGroups()) {
				count += calculateGroup(group);
			}

			return count;
		}

		static int calculateGroup(IEnumerable<string> input) {
			List<char> questions = new List<char>();
			foreach(string line in input) {
				foreach(char c in line) {
					if (!questions.Contains(c)) {
						questions.Add(c);
					}
				}
			}
			return questions.Count;
		}

		static int calculatePart2(string[] input) {
			int count = 0;
			foreach (IEnumerable<string> group in input.GetGroups()) {
				count += calculateGroup2(group);
			}

			return count;
		}

		static int calculateGroup2(IEnumerable<string> input) {
			Dictionary<char, int> questions = new Dictionary<char, int>();
			int people = 0;
			foreach(string line in input) {
				people++;
				foreach(char c in line) {
					if (!questions.ContainsKey(c)) {
						questions[c] = 1;
					} else {
						questions[c]++;
					}
				}
			}

			int count = 0;
			foreach(KeyValuePair<char, int> pair in questions) {
				if(pair.Value == people) {
					count++;
				}
			}
			return count;
		}
	}
}
