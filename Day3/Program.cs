using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day3 {
	class Program {

		const string InputFileName = "Input.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			string[] input = loadInputData();
			Map map = new Map(input); //Parse the data into a map

			Console.WriteLine("Calculating...");
			timer.Start();
			int answer = calculateAnswer(map);
			BigInteger part2 = calculatePart2(map);
			timer.Stop();

			Console.WriteLine("The answer is {0}.", answer);
			Console.WriteLine("The answer to part 2 is {0}.", part2);
			Console.WriteLine("This only took {0:g}!", timer.Elapsed);
			Console.Write("Press any key to exit.");
			Console.ReadKey();
		}

		static string[] loadInputData() {
			return File.ReadAllLines(InputFileName);
		}

		//There is actually a faster way to do this, but eh, input isn't large enough to care tbh.
		static int calculateAnswer(Map map) {
			int trees = map.HitTree ? 1 : 0;
			for (; map.OnSlope; map.Slide()) {
				if (map.HitTree) {
					trees++;
				}
			}
			return trees;
		}

		static BigInteger calculatePart2(Map map) {
			return new BigInteger(calculateSlope(map, 1, 1))
				* new BigInteger(calculateSlope(map, 3, 1))
				* new BigInteger(calculateSlope(map, 5, 1))
				* new BigInteger(calculateSlope(map, 7, 1))
				* new BigInteger(calculateSlope(map, 1, 2));
		}

		static int calculateSlope(Map map, int right, int down) {
			Size slope = new Size(right, down);
			map.Reset();
			int trees = map.HitTree ? 1 : 0;
			for (; map.OnSlope; map.Slide(slope)) {
				if (map.HitTree) {
					trees++;
				}
			}
			return trees;
		}
	}
}
