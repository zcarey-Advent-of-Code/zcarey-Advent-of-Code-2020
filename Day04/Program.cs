using AdventOfCode;
using AdventOfCode.Parsing;
using AdventOfCode.Parsing.Filters;
using Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day4 {
	class Program : ProgramStructure<Passport[]> {

		Program() : base(new Parser()
			.Filter(new LineReader())
			.Filter(new TextBlockFilter())
			.FilterCreate<Passport>()
			.ToArray()
		) { }

		static void Main(string[] args) {
			new Program().Run(args);
			//new Program().Run("Valids.txt");
			//new Program().Run("Examples.txt");
		}

		protected override object SolvePart1(Passport[] input) {
			int valid = 0;
			foreach (Passport passport in input) {
				passport.Validate();
				if (passport.IsValid) {
					valid++;
				}
			}
			return valid.ToString();
		}

		protected override object SolvePart2(Passport[] input) {
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
