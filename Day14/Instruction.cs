using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Day14 {
	abstract class Instruction {

		public abstract void Update(long[] memory, ref long mask, ref long maskSet);
		public abstract void UpdatePart2(Dictionary<long, long> memory, ref long mask, ref long maskSet, StreamWriter debug);

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

		public override void UpdatePart2(Dictionary<long, long> memory, ref long mask, ref long maskSet, StreamWriter debug) {
			this.Update(null, ref mask, ref maskSet);
			//Console.WriteLine("Mask = {0}\t\tSet = {1}", Convert.ToString(this.mask, 2).PadLeft(36, '0'), Convert.ToString(this.maskSet, 2).PadLeft(36, '0'));
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

		long index = 0;
		long value = 0;

		private Memory() { }

		public override void Update(long[] memory, ref long mask, ref long maskSet) {
			memory[index] = (this.value & mask) | maskSet;
		}

		public override void UpdatePart2(Dictionary<long, long> memory, ref long mask, ref long maskSet, StreamWriter debug) {
			SetValues(memory, this.index | maskSet, this.value, 0, mask, debug);
		}

		private void SetValues(Dictionary<long, long> memory, long address, long value, int bitIndex, long mask, StreamWriter debug) {
			if (bitIndex >= 36) {
				debug.WriteLine("mem[" + Convert.ToString(address, 2) + "] = " + value);
				memory[address] = value;
				return;
			}
			long bitMask = (long)1 << bitIndex;
			if((mask & bitMask) != 0) {
				//Try both a 0 and a 1 in this position
				SetValues(memory, address & (~bitMask), value, bitIndex + 1, mask, debug); //Try 0
				SetValues(memory, address | bitMask, value, bitIndex + 1, mask, debug); //Try 1
			} else {
				SetValues(memory, address, value, bitIndex + 1, mask, debug);
			}
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
