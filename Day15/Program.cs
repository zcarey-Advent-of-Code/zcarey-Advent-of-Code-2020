﻿using Common;
using System;
using System.Collections.Generic;

namespace Day15 {
	class Program : ParsedSeparatedInputProgramStructure<int> {

		Program() : base(",", int.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(int[] input) {
			Dictionary<int, int> lastSpoken = new Dictionary<int, int>();
			int t = 1;
			int nextNumber = 0;
			foreach(int baseNumber in input) {
				if (lastSpoken.ContainsKey(baseNumber)) {
					nextNumber = t - lastSpoken[baseNumber];
				} else {
					nextNumber = 0;
				}
				//Console.WriteLine("{0}: {1}", t, baseNumber);
				lastSpoken[baseNumber] = t++;
			}

			int spokenNumber;
			for(; t < 2020; t++) {
				spokenNumber = nextNumber;
				//Console.WriteLine("{0}: {1}", t, spokenNumber);
				if (lastSpoken.ContainsKey(spokenNumber)) {
					nextNumber = t - lastSpoken[spokenNumber];
				} else {
					nextNumber = 0;
				}
				lastSpoken[spokenNumber] = t;
			}

			return nextNumber.ToString();
		}

		protected override string CalculatePart2(int[] input) {
			Dictionary<int, int> lastSpoken = new Dictionary<int, int>();
			int t = 1;
			int nextNumber = 0;
			foreach (int baseNumber in input) {
				if (lastSpoken.ContainsKey(baseNumber)) {
					nextNumber = t - lastSpoken[baseNumber];
				} else {
					nextNumber = 0;
				}
				//Console.WriteLine("{0}: {1}", t, baseNumber);
				lastSpoken[baseNumber] = t++;
			}

			int spokenNumber;
			for (; t < 30000000; t++) {
				spokenNumber = nextNumber;
				//Console.WriteLine("{0}: {1}", t, spokenNumber);
				if (lastSpoken.ContainsKey(spokenNumber)) {
					nextNumber = t - lastSpoken[spokenNumber];
				} else {
					nextNumber = 0;
				}
				lastSpoken[spokenNumber] = t;
			}

			return nextNumber.ToString();
		}
	}
}
