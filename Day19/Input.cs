using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Day19 {

	class Input : FullInputParser {

		public Dictionary<int, Rule> Rules { get; } = new Dictionary<int, Rule>();
		private List<string> messages = new List<string>();

		public IEnumerable<string> Messages { get => messages; }

		public Rule Rule0 { get => Rules[0]; }

		public void Parse(string[] input) {
			foreach(string line in input) {
				int index = line.IndexOf(':');
				if(index >= 0) {
					Rule rule = Rule.Parse(line, index);
					Rules[rule.ID] = rule;
				} else {
					messages.Add(line);
				}
			}
		}

	}
}
