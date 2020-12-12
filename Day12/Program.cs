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
			//new Program().Run("Example.txt");
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
			Point ship = new Point();
			Size waypoint = new Size(10, -1);
			foreach(Instruction instruction in input) {
				switch (instruction.Operation) {
					case 'N':
						relativeMove(ref waypoint, 0, -instruction.Units);
						break;
					case 'S':
						relativeMove(ref waypoint, 0, instruction.Units);
						break;
					case 'E':
						relativeMove(ref waypoint, instruction.Units, 0);
						break;
					case 'W':
						relativeMove(ref waypoint, -instruction.Units, 0);
						break;
					case 'L':
						rotateLeft(ref waypoint, instruction.Units);
						break;
					case 'R':
						rotateRight(ref waypoint, instruction.Units);
						break;
					case 'F':
						ship += waypoint * instruction.Units;
						break;
					default:
						throw new Exception("Invalid Operation");
				}
			}

			return (Math.Abs(ship.X) + Math.Abs(ship.Y)).ToString();
		}

		private void relativeMove(ref Size size, int x, int y) {
			size.Width += x;
			size.Height += y;
		}

		private void rotateLeft(ref Size waypoint, int degrees) {
			degrees = degrees % 360;
			for (int i = 0; i < degrees / 90; i++) {
				waypoint = new Size(waypoint.Height, -waypoint.Width);
			}
		}

		private void rotateRight(ref Size waypoint, int degrees) {
			degrees = degrees % 360;
			for (int i = 0; i < degrees / 90; i++) {
				waypoint = new Size(-waypoint.Height, waypoint.Width);
			}
		}
	}
}
