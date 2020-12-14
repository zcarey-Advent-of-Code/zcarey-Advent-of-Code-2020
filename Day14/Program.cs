using Common;
using System.Linq;

namespace Day14 {
	class Program : ParsedInputProgramStructure<Instruction> {

		Program() : base(x => Instruction.Parse(x)) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
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
			return "null";
		}
	}
}
