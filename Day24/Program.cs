using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day24 {
	class Program : ParsedInputProgramStructure<Directions> {

		Program() : base(Directions.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Directions[] input) {
			Dictionary<Point, bool> grid = new Dictionary<Point, bool>();
			foreach(Directions directions in input) {
				Point result = directions.GetAbsoluteOffset();
				if (!grid.ContainsKey(result)) grid[result] = false; //Because tiles are by-default white, so now that it's flipped it is black
				else grid[result] ^= true;
			}

			return grid.Select(pair => pair.Value).Count(x => x == false).ToString();
		}

		protected override string CalculatePart2(Directions[] input) {
			return "null";
		}
	}
}
