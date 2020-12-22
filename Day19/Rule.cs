using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19 {
	abstract class Rule {

		public int ID { get; private set; }
		protected bool Looping { get; set; }

		protected Rule(int ID) {
			this.ID = ID;
		}

		public bool Match(Dictionary<int, Rule> rules, string message) {
			int index = 0;
			return this.match(rules, message, ref index) && (index == message.Length);
		}

		public bool MatchLooping(Dictionary<int, Rule> rules, string message) {
			int index = 0;
			bool debug = this.match2(rules, message, ref index);
			return debug && (index == message.Length);
		}

		protected abstract bool match(Dictionary<int, Rule> rules, string message, ref int index);
		protected abstract bool match2(Dictionary<int, Rule> rules, string message, ref int index);

		public static Rule Parse(string input, int index) {
			int id = int.Parse(input.Substring(0, index));
			input = input.Substring(index + 1).Trim();
			Rule rule = null;
			if (input.StartsWith('\"')) {
				rule = new BaseRule(id, input);
			} else {
				index = input.IndexOf('|');
				if(index >= 0) {
					rule = new MultiRule(id, input);
				} else {
					rule = new StandardRule(id, input, -1);
				}
			}
			return rule;
		}

		private class BaseRule : Rule {
			private char rule;
			public BaseRule(int ID, string input) : base(ID) {
				if (input.Length != 3 || input[0] != '\"' || input[2] != '\"') throw new Exception("Bad input");
				this.rule = input[1];
			}

			protected override bool match(Dictionary<int, Rule> rules, string message, ref int index) {
				if (index >= message.Length) return false;
				else return message[index++] == rule;
			}

			protected override bool match2(Dictionary<int, Rule> rules, string message, ref int index) {
				return match(rules, message, ref index);
			}
		}

		private class StandardRule : Rule {
			private int[] requiredRules;

			public StandardRule(int ID, string input, int baseID) : base(ID) {
				requiredRules = input.Split().Select(int.Parse).ToArray();
				if (requiredRules.Contains(baseID)) {
					Looping = true;
				}
			}

			protected override bool match(Dictionary<int, Rule> rules, string message, ref int index) {
				foreach(int ruleId in requiredRules) {
					if(!rules[ruleId].match(rules, message, ref index)) {
						return false;
					}
				}
				return true;
			}

			public bool matchLooping(Dictionary<int, Rule> rules, string message, ref int index, int baseId, int depth) {
				if (!Looping) return false;
				if (depth == 0) return true; //Base case
				foreach(int ruleId in requiredRules) {
					if(ruleId == baseId) {
						if(!matchLooping(rules, message, ref index, baseId, depth - 1)){
							return false;
						}
					} else {
						if(!rules[ruleId].match2(rules, message, ref index)) {
							return false;
						}
					}
				}
				return true;
			}

			protected override bool match2(Dictionary<int, Rule> rules, string message, ref int index) {
				//To match this rule in Part2, each looping rule is matched as many times as possible before returning
				if(ID == 0) {
					//return matchRule1(rules, message, ref index);
				} else {
					return this.match(rules, message, ref index);
				}
				/*int lastIndex = index;
				foreach (int ruleId in requiredRules) {
					Rule rule = rules[ruleId];
					if (rule.Looping) { 
						while(rule.match2(rules, message, ref index)) {
							lastIndex = index;
						}
						index = lastIndex;
					} else {
						if (!rule.match2(rules, message, ref index)) {
							return false;
						}
					}
				}
				return true;*/
			}

			/*private bool matchRule1(Dictionary<int, Rule> rules, string message, ref int index) {
				int baseIndex = index;
				if(rules[requiredRules[0]].match2(rules, message, ref index)) {
					//Base case
					if (index >= message.Length) return false; //The first rule can't complete the message
					if (matchRule1(rules, message, ref index)) return true;
				}

				if(matchRule2(rules, message, ref baseIndex)) {
					index = baseIndex;
					return true;
				} else {
					return false;
				}
			}

			private bool matchRule2(Dictionary<int, Rule> rules, string message, ref int index) {
				if(rules[requiredRules[1]].match2(rules, message, ref index)) {
					if (index == message.Length) return true;
					else if (index > message.Length) return false;
					else {
						if (matchRule2(rules, message, ref index)) return true;
					}
				} 
				
				return false;
			}*/
		}

		private class MultiRule : Rule {
			private Rule[] ruleGroups;

			public MultiRule(int ID, string input) : base(ID) {
				ruleGroups = input.Split('|').Select(x => new StandardRule(-1, x.Trim(), ID)).ToArray();
				this.Looping = ruleGroups.Any(x => x.Looping);
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

			protected override bool match2(Dictionary<int, Rule> rules, string message, ref int index) {
				//if (repeats < 1) return false;
				//Going with the assumption that is the rule is marked as "looping" that there are only 2 rules:
				//The first one is the "base" rule, and the second rule is the "base rule" followed by a loop back to this rule.
				//With that assumption, this function is programmed so that it ignores the second rule and just loops the first rule as needed.
				if (Looping) {
					/*bool matchedOnce = false;
					int lastIndex = index;
					while (true) {
						if(ruleGroups[0].match2(rules, message, ref index)) {
							lastIndex = index;
							matchedOnce = true;
							if(index == message.Length) {
								return true; //We did it!!
							}
						} else {
							something;
							break;
						}
					}
					return matchedOnce;*/
					return ruleGroups[0].match2(rules, message, ref index);
				} else {
					//if (repeats != 1) return false;
					/*foreach (Rule rule in ruleGroups) {
						int ruleIndex = index;
						if (rule.Looping) {
							if(rule.match2(rules, message, ref ruleIndex)) {
								index = ruleIndex
							}
						} else {
							if (rule.match2(rules, message, ref ruleIndex)) {
								index = ruleIndex;
								return true;
							}
						}
					}
					return false;*/
					return this.match(rules, message, ref index);
				}
			}
		}

	}
}
