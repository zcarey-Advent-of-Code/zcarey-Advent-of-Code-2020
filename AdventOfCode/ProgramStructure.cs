﻿using AdventOfCode.Parsing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AdventOfCode {
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">The type of input data to be parsed.</typeparam>
	public abstract class ProgramStructure<T> {

		// Command line args
		private bool skipPart1 = false;

		// Run time timer
		private Stopwatch timer = new Stopwatch();

		// Input parser
		private ParserBase<StreamReader, T> parser;

		protected ProgramStructure(ParserBase<StreamReader, T> parser) {
			this.parser = parser;
		}

		abstract protected object SolvePart1(T input);

		abstract protected object SolvePart2(T input);

		public void Run(string[] args, string filename = "Input.txt") {
			parseArgs(args);

			Console.WriteLine("Parsing input...");
			Console.WriteLine();
			Console.WriteLine();
			T input = default(T);

			// Load the file into memory to prevent "closed file" exceptions later
			MemoryStream file = new MemoryStream();
			using (FileStream stream = File.OpenRead(filename)) {
				stream.CopyTo(file);
				stream.Flush();
			}

			using (file) {

				if (skipPart1) {
					Console.WriteLine("Skipped part 1.");
				} else {
					Console.WriteLine("Parsing input...");
					Console.WriteLine();
					Console.WriteLine();
					input = LoadInputData(file);

					Console.WriteLine("Running part 1:");
					timer.Restart();
					object result1 = SolvePart1(input);
					timer.Stop();
					Console.WriteLine("Finished in {0:g}.", timer.Elapsed);
					input = default(T);

					if (result1 == null) {
						Console.WriteLine("Part 1 failed to return a result.");
					} else if (result1 is Exception e) {
						Console.WriteLine("Part 1 returned with error: \"" + e.Message + "\"");
					} else {
						Console.WriteLine("Part 1 finished.");
						Console.WriteLine("Solution: " + result1.ToString());
						File.WriteAllText("Solution - Part 1.txt", result1.ToString());
					}
				}

				Console.WriteLine();
				Console.WriteLine();
				Console.WriteLine("Parsing input...");
				Console.WriteLine();
				Console.WriteLine();
				input = LoadInputData(file);

				Console.WriteLine("Running part 2:");
				timer.Restart();
				object result2 = SolvePart2(input);
				timer.Stop();
				Console.WriteLine("Finished in {0:g}.", timer.Elapsed);
				input = default(T);

				if (result2 == null) {
					Console.WriteLine("Part 2 failed to return a result.");
				} else if (result2 is Exception e) {
					Console.WriteLine("Part 2 returned with error: \"" + e.Message + "\"");
				} else {
					Console.WriteLine("Part 2 finished.");
					Console.WriteLine("Solution: " + result2.ToString());
					File.WriteAllText("Solution - Part 2.txt", result2.ToString());
				}

				Console.WriteLine();
				Console.WriteLine();
				//Console.Write("Press any key to exit.");
				//Console.ReadKey();
			}
		}

		private void parseArgs(string[] args) {
			foreach(string arg in args) {
				if(arg == "--part2") {
					skipPart1 = true;
				} else {
					throw new ArgumentException("Unknown argument.", arg);
				}
			}
		}

		private T LoadInputData(MemoryStream stream) {
			stream.Position = 0;
			return parser.ParseInput(new StreamReader(stream));
		}
	}

	/*public abstract class ProgramStructure : ProgramStructure<string> {
		
		protected ProgramStructure() : base(new StringParser()) {

		}

	}*/
}
