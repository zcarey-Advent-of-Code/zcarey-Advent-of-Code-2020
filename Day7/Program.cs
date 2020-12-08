using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day7 {
	class Program {

		const string InputFileName = "Input.txt";
		//const string InputFileName = "Example.txt";
		//const string InputFileName = "Example2.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			Console.WriteLine();
			Console.WriteLine();
			Rule[] data = File.ReadAllLines(InputFileName).Select(x => new Rule(x)).ToArray();

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

		static int calculatePart1(Rule[] input) {
			Dictionary<string, bool> validColors = new Dictionary<string, bool>();
			foreach(Rule rule in input) {
				validColors[rule.BagColor] = false;
			}
			//validColors["shiny gold"] = true;
			bool tryAgain = true;
			while (tryAgain) {
				tryAgain = false;
				foreach (Rule rule in input) {
					if (validColors[rule.BagColor] == false) {
						bool anyColorValid = false;
						foreach (KeyValuePair<string, int> pair in rule.Rules) {
							if (pair.Key == "shiny gold" || validColors[pair.Key] == true) {
								anyColorValid = true;
								break;
							}
						}
						validColors[rule.BagColor] = anyColorValid;
						if (anyColorValid) {
							tryAgain = true;
						}
					}
				}
			}

			int count = validColors.Where(x => x.Value == true).Count();
			return count;
		}

		static int calculatePart2(Rule[] input) {
			Dictionary<string, int> bagCount = new Dictionary<string, int>();
			return calculateBags(input, bagCount, "shiny gold");
		}

		static int calculateBags(Rule[] input, Dictionary<string, int> bagCount, string color) {
			if (bagCount.ContainsKey(color)) return bagCount[color];

			Rule bagRule = input.Where(x => x.BagColor == color).First();
			int bags = 0;
			foreach(KeyValuePair<string, int> rule in bagRule.Rules) {
				if (bagCount.ContainsKey(rule.Key)) {
					bags += rule.Value + bagCount[rule.Key] * rule.Value;
				} else {
					bags += rule.Value + calculateBags(input, bagCount, rule.Key) * rule.Value;
				}
			}

			bagCount[color] = bags;
			return bags;
		}
	}
}
