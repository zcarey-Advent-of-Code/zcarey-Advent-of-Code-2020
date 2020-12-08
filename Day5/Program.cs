using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Day5 {
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
			/*
			printPart2Debug(data);
			Console.WriteLine();
			Console.WriteLine();
			*/
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
			return input.Select(x => new Seat(x)).Max(x => x.ID);
		}

		static int calculatePart2(string[] input) {
			Seat[,] map = fillMap(input);
			for(int y = 0; y <= 127; y++) {
				for(int x = 1; x <= 6; x++) {
					if(!map[x, y].Filled && map[x - 1, y].Filled && map[x + 1, y].Filled) {
						return map[x, y].ID;
					}
				}
			}
			throw new Exception("Unable to solve!");
		}

		private static Seat[,] fillMap(string[] input) {
			Seat[,] map = new Seat[8, 128];
			IEnumerable<Seat> seats = input.Select(x => new Seat(x));
			foreach (Seat seat in seats) {
				map[seat.Column, seat.Row] = seat;
			}
			for(int y = 0; y <= 127; y++) {
				for(int x = 0; x <= 7; x++) {
					if(!map[x, y].Filled) {
						map[x, y].Column = x;
						map[x, y].Row = y;
					}
				}
			}
			return map;
		}

		static void printPart2Debug(string[] input) {
			Seat[,] map = fillMap(input);
			for(int y = 0; y < 128; y++) {
				StringBuilder sb = new StringBuilder();
				for(int x = 0; x < 8; x++) {
					if(map[x, y].Filled) {
						sb.Append('#');
					} else {
						sb.Append('.');
					}
				}
				Console.WriteLine(sb);
			}
		}
	}
}
