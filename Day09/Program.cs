using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day09 {
	class Program : ParsedInputProgramStructure<long> {

		Program() : base(long.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(long[] input) {
			List<long> preamble = new List<long>();
			for(int i = 0; i < 25; i++) {
				preamble.Add(input[i]);
			}
			foreach(long data in input.Skip(25)) {
				if(!ValidXMas(preamble, data)) {
					return data.ToString();
				} else {
					preamble.RemoveAt(0);
					preamble.Add(data);
				}
			}
			throw new Exception("Could not compute.");
		}

		static bool ValidXMas(List<long> preamble, long data) {
			for(int i = 0; i < preamble.Count - 1; i++) {
				for(int j = i + 1; j < preamble.Count; j++) {
					if(preamble[i] + preamble[j] == data) {
						return true;
					}
				}
			}
			return false;
		}

		protected override string CalculatePart2(long[] input) {
			return "null";
		}
	}
}
