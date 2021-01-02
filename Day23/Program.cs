using Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Day23 {
	class Program : SingleParsedInputProgramStructure<Cups> {

		Program() : base(x => new Cups(x)) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Cups cups) {
			int minCup = cups.Min();
			int maxCup = cups.Max();
			for (int i = 0; i < 100; i++) {
				//Pick up the next 3 cups for later use
				List<int> pickedUp = cups.Skip(1).Take(3).ToList();

				//Calcluate our destination cup
				int destination = cups.Current;
				do {
					destination--;
					if (destination < minCup) destination = maxCup;
				} while (pickedUp.Contains(destination));
				int currentCupIndex = cups.Index;

				//Move to the next element after our removed cups and search for the desination cup
				//while also moving elements to fill the void of the removed cups
				cups.Next(4);
				while(cups.Current != destination) {
					cups[cups.Index - 3] = cups.Current;
					cups.Next();
				}
				//We found our destination cup, so we move it and add our removed cups to have the effect of inserting the cups after the destination
				cups[cups.Index - 3] = cups.Current;
				cups.Previous(2);
				cups.Fill(pickedUp);

				//The crab should now be sitting on the first of the cups we picked up, so we have to move him back to our current cup.
				cups.Index = currentCupIndex;

				//Move the crab to the next cup for the next move.
				cups.Next();
			}

			//Find the cup labeled 1
			while(cups.Next() != 1) {
			}

			//Start with the cup AFTER the cup labeled 1 and return each cup in order as a string.
			return new string(cups.Skip(1).Select(x => (char)('0' + x)).ToArray());
		}

		protected override string CalculatePart2(Cups input) {
			return "null";
		}
	}
}
