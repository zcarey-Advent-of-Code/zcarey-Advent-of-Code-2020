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
				if (depth < 0) return true; //Base case
				bool result = true;
				foreach(int ruleId in requiredRules) {
					if(ruleId == baseId) {
						if(!matchLooping(rules, message, ref index, baseId, depth - 1)){
							result = false;
						}
					} else {
						if(!rules[ruleId].match2(rules, message, ref index)) {
							result = false;
						}
					}
				}
				return result;
			}

			protected override bool match2(Dictionary<int, Rule> rules, string message, ref int index) {
				//To match this rule in Part2, each looping rule is matched as many times as possible before returning
				if(ID == 0) {
					//Assume rule 0 only has 2 rules
					MultiRule rule1 = (MultiRule)rules[requiredRules[0]];
					MultiRule rule2 = (MultiRule)rules[requiredRules[1]];
					/*for(int firstRuleMatches = 1; ; firstRuleMatches++) {
						index = 0;
						if(!rule1.matchLooping(rules, message, ref index, firstRuleMatches)){
							//Once the first rule doesn't match (after a certain number of loops) we know the entire message doesn't match
							return false;
						}
						int baseIndex = index;
						for(int secondRuleMatches = 1; ; secondRuleMatches++) {
							index = baseIndex; //Reset the index to the end of rule1 matches
							if (rule2.matchLooping(rules, message, ref index, secondRuleMatches)) {
								if(index == message.Length) {
									return true;
								}
							} else {
								break;
							}
						}
					}*/
					for (int firstRuleMatches = 0; ; firstRuleMatches++) {
						index = 0;
						if (!rule1.matchLooping(rules, message, ref index, firstRuleMatches)) {
							if (index >= message.Length) {
								return false;
							} else {
								continue; //Try more!!!
							}
						}
						int baseIndex = index;
						for (int secondRuleMatches = 0; ; secondRuleMatches++) {
							//if (firstRuleMatches < 0 && secondRuleMatches < 0) continue;
							index = baseIndex; //Reset the index to the end of rule1 matches
							if (rule2.matchLooping(rules, message, ref index, secondRuleMatches)) {
								if (index == message.Length) {
									return true;
								}
							} else {
								if(index >= message.Length) {
									break;
								} else {
									continue;
								}
							}
						}
					}
					//if (!rules[requiredRules[0]].match2(rules, message, ref index)) return false; //Need to
					//return false;
					//return matchRule1(rules, message, ref index);
				} else {
					bool result = true;
					foreach (int ruleId in requiredRules) {
						if (!rules[ruleId].match2(rules, message, ref index)) {
							result = false;
						}
					}
					return result;
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
			private StandardRule[] ruleGroups;

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
					int baseIndex = index;
					foreach (Rule rule in ruleGroups) {
						index = baseIndex;
						if (rule.match2(rules, message, ref index)) {
							return true;
						}
					}
					return false;
				}
			}

			public bool matchLooping(Dictionary<int, Rule> rules, string message, ref int index, int depth) {
				if (!Looping) throw new Exception("Not a looping rule."); //return false;
				return ruleGroups[1].matchLooping(rules, message, ref index, this.ID, depth);
			}
		}

	}
}
