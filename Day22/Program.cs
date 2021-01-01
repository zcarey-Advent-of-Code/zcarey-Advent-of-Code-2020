using Common;
using System;

namespace Day22 {
	class Program : BlockParsedInputProgramStructure<Deck> {

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Deck[] input) {
			Console.WriteLine("Player {1}:\n{0}", input[0], input[0].Player);
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("Player {1}:\n{0}", input[1], input[1].Player);
			return "null";
		}

		protected override string CalculatePart2(Deck[] input) {
			return "null";
		}
	}
}
