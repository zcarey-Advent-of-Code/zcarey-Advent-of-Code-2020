using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day8 {
	class Program : ParsedInputProgramStructure<Instruction> {

		Program() : base(x => new Instruction(x)) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(Instruction[] program) {
			bool[] visited = new bool[program.Length];

			int programCounter = 0;
			int accumulator = 0;
			while (programCounter < program.Length) {
				if (visited[programCounter] == true) {
					return accumulator.ToString();
				}
				visited[programCounter] = true;
				program[programCounter].Execute(ref programCounter, ref accumulator);
			}

			return accumulator.ToString();
		}

		protected override string CalculatePart2(Instruction[] program) {
			for (int i = 0; i < program.Length; i++) {
				Instruction copy = program[i];
				if (copy.Op == Operation.Jmp || copy.Op == Operation.Nop) {
					program[i].Op = swapOp(copy.Op);
					int? result = runProgram(program);
					if (result != null) {
						return result.ToString();
					} else {
						program[i] = copy;
					}
				}
			}
			throw new Exception("Could not solve.");
		}

		static Operation swapOp(Operation op) {
			if (op == Operation.Jmp) {
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
