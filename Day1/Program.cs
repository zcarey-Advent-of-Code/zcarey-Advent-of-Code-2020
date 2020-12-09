﻿using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day1 {
	class Program : ParsedInputProgramStructure<int> {

		Program() : base(int.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("Input.txt");
		}

		protected override string CalculatePart1(int[] input) {
			for (int i = 0; i < input.Length - 1; i++) {
				for (int j = i + 1; j < input.Length; j++) {
					if (input[i] + input[j] == 2020) {
						return (input[i] * input[j]).ToString();
					}
				}
			}
			throw new Exception("Unable to find the answer!!!");
		}

		protected override string CalculatePart2(int[] input) {
			for (int i = 0; i < input.Length - 2; i++) {
				for (int j = i + 1; j < input.Length - 1; j++) {
					for (int k = j + 1; k < input.Length; k++) {
						if (input[i] + input[j] + input[k] == 2020) {
							return (input[i] * input[j] * input[k]).ToString();
						}
					}
				}
			}
			throw new Exception("Unable to find the answer!!!");
		}
	}
}
