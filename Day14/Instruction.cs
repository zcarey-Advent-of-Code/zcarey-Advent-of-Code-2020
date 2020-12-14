using System;
using System.Collections.Generic;
using System.Text;

namespace Day14 {
	abstract class Instruction {

		public abstract void Update(long[] memory, ref long mask, ref long maskSet);

		public static Instruction Parse(string input) {
			Instruction result = null;
			if((result = Mask.TryParse(input)) == null) {
				if((result = Memory.TryParse(input)) == null) {
					throw new Exception("Unable to parse input.");
				}
			}
			return result;
		}

	}

	class Mask : Instruction {

		long mask = 0;
		long maskSet = 0;

		private Mask() { }

		public override void Update(long[] memory, ref long mask, ref long maskSet) {
			mask = this.mask;
			maskSet = this.maskSet;
		}

		public static Instruction TryParse(string input) {
			if (input.StartsWith("mask")) {
				string value = input.Substring(7);
				Mask result = new Mask();
				foreach(char c in value) {
					result.mask <<= 1;
					result.maskSet <<= 1;
					if(c == 'X') {
						result.mask |= 1;
					}else if(c == '1') {
						result.maskSet |= 1;
					}else if(c != '0') {
						throw new Exception("Bad input.");
					}
				}
				return result;
			} else {
				return null;
			}
		}
	}

	class Memory : Instruction {

		int index = 0;
		long value = 0;

		private Memory() { }

		public override void Update(long[] memory, ref long mask, ref long maskSet) {
			memory[index] = (this.value & mask) | maskSet;
		}

		public static Instruction TryParse(string input) {
			if (input.StartsWith("mem[")) {
				Memory result = new Memory();
				result.index = int.Parse(input.Substring(4, input.IndexOf(']') - 4));
				result.value = long.Parse(input.Substring(input.IndexOf('=') + 1));
				return result;
			} else {
				return null;
			}
		}
	}
}
