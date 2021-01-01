using Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day3 {
	class Program : FullParsedInputProgramStructure<Map> {

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(Map input) {
			int trees = input.HitTree ? 1 : 0;
			for (; input.OnSlope; input.Slide()) {
				if (input.HitTree) {
					trees++;
				}
			}
			return trees.ToString();
		}

		//There is actually a faster way to do this, but eh, input isn't large enough to care tbh.
		protected override string CalculatePart2(Map input) {
			return (new BigInteger(calculateSlope(input, 1, 1))
				* new BigInteger(calculateSlope(input, 3, 1))
				* new BigInteger(calculateSlope(input, 5, 1))
				* new BigInteger(calculateSlope(input, 7, 1))
				* new BigInteger(calculateSlope(input, 1, 2))).ToString();
		}

		static int calculateSlope(Map map, int right, int down) {
			Size slope = new Size(right, down);
			map.Reset();
			int trees = map.HitTree ? 1 : 0;
			for (; map.OnSlope; map.Slide(slope)) {
				if (map.HitTree) {
					trees++;
				}
			}
			return trees;
		}
	}
}
