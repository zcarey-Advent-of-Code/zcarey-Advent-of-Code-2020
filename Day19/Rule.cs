using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19 {
	abstract class Rule {

		public int ID { get; private set; }

		public bool Match(Dictionary<int, Rule> rules, string message) {
			int index = 0;
			return this.match(rules, message, ref index) && (index == message.Length);
		}

		protected abstract bool match(Dictionary<int, Rule> rules, string message, ref int index);

		public static Rule Parse(string input, int index) {
			int id = int.Parse(input.Substring(0, index));
			input = input.Substring(index + 1).Trim();
			Rule rule = null;
			if (input.StartsWith('\"')) {
				rule = new BaseRule(input);
			} else {
				index = input.IndexOf('|');
				if(index >= 0) {
					rule = new MultiRule(input);
				} else {
					rule = new StandardRule(input);
				}
			}
			rule.ID = id;
			return rule;
		}

		private class BaseRule : Rule {
			private char rule;
			public BaseRule(string input) {
				if (input.Length != 3 || input[0] != '\"' || input[2] != '\"') throw new Exception("Bad input");
				this.rule = input[1];
			}

			protected override bool match(Dictionary<int, Rule> rules, string message, ref int index) {
				if (index >= message.Length) return false;
				else return message[index++] == rule;
			}
		}

		private class StandardRule : Rule {
			private int[] requiredRules;

			public StandardRule(string input) {
				requiredRules = input.Split().Select(int.Parse).ToArray();
			}

			protected override bool match(Dictionary<int, Rule> rules, string message, ref int index) {
				foreach(int ruleId in requiredRules) {
					if(!rules[ruleId].match(rules, message, ref index)) {
						return false;
					}
				}
				return true;
			}
		}

		private class MultiRule : Rule {
			private Rule[] ruleGroups;

			public MultiRule(string input) {
				ruleGroups = input.Split('|').Select(x => new StandardRule(x.Trim())).ToArray();
			}

			protected override bool match(Dictionary<int, Rule> rules, string message, ref int index) {
				foreach(Rule rule in ruleGroups) {
					int ruleIndex = index;
					if(rule.match(rules, message, ref ruleIndex)) {
						index = ruleIndex;
						return true;
					}
				}
				return false;
			}
		}

	}
}
