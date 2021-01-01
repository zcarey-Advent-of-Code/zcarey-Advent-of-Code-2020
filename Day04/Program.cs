using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day4 {
	class Program : BlockParsedInputProgramStructure<Passport> {

		static void Main(string[] args) {
			new Program().Run("Input.txt");
			//new Program().Run("Valids.txt");
			//new Program().Run("Examples.txt");
		}

		protected override string CalculatePart1(Passport[] input) {
			int valid = 0;
			foreach (Passport passport in input) {
				passport.Validate();
				if (passport.IsValid) {
					valid++;
				}
			}
			return valid.ToString();
		}

		protected override string CalculatePart2(Passport[] input) {
			int valid = 0;
			foreach (Passport passport in input) {
				passport.Validate2();
				if (passport.IsValid) {
					valid++;
				}
			}
			return valid.ToString();
		}
	}
}
