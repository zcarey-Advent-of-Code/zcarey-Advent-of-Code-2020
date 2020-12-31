using Common;
using System;

namespace Day21 {
	class Program : ParsedInputProgramStructure<Food> {

		Program() : base(Food.Parse) {
		}

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Food[] input) {
			foreach(Food food in input) {
				Console.WriteLine(food);
			}
			return "null";
		}

		protected override string CalculatePart2(Food[] input) {
			return "null";
		}
	}
}
