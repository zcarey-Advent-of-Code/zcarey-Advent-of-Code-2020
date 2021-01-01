using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day22 {
	class Deck : FullInputParser {

		public int Player { get; private set; }
		public IEnumerable<int> Cards { get => cards; }

		private List<int> cards;

		public void Parse(string[] input) {
			this.Player = int.Parse(input[0].Substring("Player ".Length, 1));
			this.cards = input.Skip(1).Select(int.Parse).ToList();
		}

		public override string ToString() {
			return string.Join(", ", this.cards);
		}
	}
}
