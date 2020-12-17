using Common;
using System;

namespace Day17 {
	class Program : FullParsedInputProgramStructure<Map> {
		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Map input) {
			for(int i = 0; i < 6; i++) {
				input.Simulate(2, 3, 3);
			}
			return input.CountActive().ToString();
		}

		protected override string CalculatePart2(Map input) {
			return "null";
		}
	}
}
