using System;
using Common;

namespace Day11 {
	class Program : FullParsedInputProgramStructure<Map>{
		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(Map input) {
			input.Simulate();
			return input.CountOccupiedSeats().ToString();
		}

		protected override string CalculatePart2(Map input) {
			return "null";
		}
	}
}
