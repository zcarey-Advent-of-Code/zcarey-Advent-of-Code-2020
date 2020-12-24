using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	class Program : BlockParsedInputProgramStructure<Tile> {

		//static Operation[] ValidOperations = Operation.GetAllOperations().ToArray();
		static readonly Operation[] ValidOperations = {
			Operation.Original,
			Operation.Rotate90,
			Operation.Rotate180,
			Operation.Rotate270,
			Operation.HorizontalFlip,
			Operation.VerticalFlip
		};

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Tile[] input) {
			Map map = new Map();
			
		}

		private bool placeTime()

		protected override string CalculatePart2(Tile[] input) {
			throw new NotImplementedException();
		}
	}
}
