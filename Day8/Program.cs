using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day8 {
	class Program {

		const string InputFileName = "Input.txt";
		static Stopwatch timer = new Stopwatch();

		static void Main(string[] args) {
			Console.WriteLine("Loading data...");
			Console.WriteLine();
			Console.WriteLine();
			Instruction[] data = File.ReadAllLines(InputFileName).Select(x => new Instruction(x)).ToArray();

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

		static int calculatePart1(Instruction[] program) {
			bool[] visited = new bool[program.Length];

			int programCounter = 0;
			int accumulator = 0;
			while(programCounter < program.Length) {
				if(visited[programCounter] == true) {
					return accumulator;
				}
				visited[programCounter] = true;
				program[programCounter].Execute(ref programCounter, ref accumulator);
			}

			return accumulator;
		}

		static int calculatePart2(Instruction[] program) {
			for(int i = 0; i <  program.Length; i++) {
				Instruction copy = program[i];
				if (copy.Op == Operation.Jmp || copy.Op == Operation.Nop) {
					program[i].Op = swapOp(copy.Op);
					int? result = runProgram(program);
					if (result != null) {
						return (int)result;
					} else {
						program[i] = copy;
					}
				}
			}
			throw new Exception("Could not solve.");
		}

		static Operation swapOp(Operation op) {
			if(op == Operation.Jmp) {
				return Operation.Nop;
			} else {
				return Operation.Jmp;
			}
		}

		static int? runProgram(Instruction[] program) {
			bool[] visited = new bool[program.Length];

			int programCounter = 0;
			int accumulator = 0;
			while (programCounter < program.Length) {
				if (visited[programCounter] == true) {
					return null;
				}
				visited[programCounter] = true;
				program[programCounter].Execute(ref programCounter, ref accumulator);
			}

			return accumulator;
		}
	}
}
