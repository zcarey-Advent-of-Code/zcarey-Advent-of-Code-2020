using System;
using Common;

namespace Day11 {
	class Program : FullParsedInputProgramStructure<Map>{
		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(Map input) {
			bool updated = true;
			while (updated) {
				input.UpdateOccupiedCount();
				updated = input.UpdateSeatState(4);
			}
			return input.CountOccupiedSeats().ToString();
		}

		protected override string CalculatePart2(Map input) {
			bool updated = true;
			while (updated) {
				input.UpdateOccupiedCount2();
				updated = input.UpdateSeatState(5);
			}
			return input.CountOccupiedSeats().ToString();
		}
	}
}
