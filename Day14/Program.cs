using Common;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Day14 {
	class Program : ParsedInputProgramStructure<Instruction> {

		Program() : base(x => Instruction.Parse(x)) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Instruction[] input) {
			long[] memory = new long[70000];
			long mask = 0;
			long maskSet = 0;
			foreach (Instruction op in input) {
				op.Update(memory, ref mask, ref maskSet);
			}
			return (memory.Sum()).ToString();
		}

		protected override string CalculatePart2(Instruction[] input) {
			Dictionary<long, long> memory = new Dictionary<long, long>();
			long mask = 0;
			long maskSet = 0;
			foreach (Instruction op in input) {
				op.UpdatePart2(memory, ref mask, ref maskSet);
			}
			BigInteger result = BigInteger.Zero;
			foreach (KeyValuePair<long, long> pair in memory) {
				result += new BigInteger(pair.Value);
			}
			return result.ToString();
		}
	}
}
