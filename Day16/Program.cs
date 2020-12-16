using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16 {
	class Program : FullParsedInputProgramStructure<InputData> {
		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("example.txt");
		}

		protected override string CalculatePart1(InputData input) {
			int ticketScanningErrorRate = 0;
			foreach(int[] ticket in input.OtherTickets) {
				foreach(int number in ticket) {
					if(!FindValidRules(input, number).Any()) {
						ticketScanningErrorRate += number;
					}
				}
			}
			return ticketScanningErrorRate.ToString();
		}

		private IEnumerable<Rule> FindValidRules(InputData input, int num) {
			foreach(Rule rule in input.Rules) {
				if (rule.FitsInRange(num)) {
					yield return rule;
				}
			}
		}

		protected override string CalculatePart2(InputData input) {
			return "null";
		}
	}
}
