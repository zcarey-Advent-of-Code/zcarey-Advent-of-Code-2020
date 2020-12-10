using Common;
using System;

namespace Day10 {
	class Program : ParsedInputProgramStructure<int> {

		Program() : base(int.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(int[] input) {
			throw new NotImplementedException();
		}

		protected override string CalculatePart2(int[] input) {
			return "null";
		}
	}
}
