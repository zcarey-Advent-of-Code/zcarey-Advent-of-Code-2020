using Common;
using System;
using System.Drawing;

namespace Day12 {
	class Program : ParsedInputProgramStructure<Instruction> {

		Program() : base(x => new Instruction(x)) {
		}

		protected override bool HasVisualizer => throw new NotImplementedException();

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(Instruction[] input) {
			int angle = 0;
			Point location = new Point();
			foreach(Instruction instruction in input) {
				location += instruction.Update(ref angle);
			}

			return (Math.Abs(location.X) + Math.Abs(location.Y)).ToString();
		}

		protected override string CalculatePart2(Instruction[] input) {
			return "null";
		}
	}
}
