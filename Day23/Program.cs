using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Day23 {
	class Program : SingleParsedInputProgramStructure<Cups> {

		Program() : base(x => new Cups(x)) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Cups cups) {
			GetCrabby(ref cups, cups.Count, 100);

			//Find the cup labeled 1
			/*while (cups.Next() != 1) {
			}*/

			//Start with the cup AFTER the cup labeled 1 and return each cup in order as a string.
			IEnumerable<int> oneCup = cups[1].AsEnumerable().Select(node => node.Value);
			return new string(oneCup.Skip(1).Select(x => (char)('0' + x)).ToArray());
		}

		static void GetCrabby(ref Cups cups, int numCups, int numMoves) {
			int minCup = cups.Min();
			int maxCup = cups.Max();
			if (numCups > cups.Count) {
				int cupsToAdd = numCups - cups.Count;
				cups = new Cups(cups, Enumerable.Range(maxCup + 1, cupsToAdd));
				maxCup += cupsToAdd;
			}
			

			LinkedListNode<int> currentCup = cups.First;
			for (int i = 0; i < numMoves; i++) {
				//Pick up the next 3 cups for later use
				List<LinkedListNode<int>> pickedUp = currentCup.RemoveAfter(3).ToList(); //cups.Skip(1).Take(3).ToList();

				//Calcluate our destination cup
				int destination = currentCup.Value;
				do {
					destination--;
					if (destination < minCup) destination = maxCup;
				} while (pickedUp.Select(x => x.Value).Contains(destination));

				//Move to the next element after our removed cups and search for the desination cup
				//while also moving elements to fill the void of the removed cups
				/*cups.Next(4);
				while (cups.Current != destination) {
					cups[cups.Index - 3] = cups.Current;
					cups.Next();
				}*/
				LinkedListNode<int> destinationCup = cups[destination];

				//We found our destination cup, so we move it and add our removed cups to have the effect of inserting the cups after the destination
				//cups[cups.Index - 3] = cups.Current;
				//cups.Previous(2);
				//cups.Fill(pickedUp);
				destinationCup.AddAfter(pickedUp);

				//The crab should now be sitting on the first of the cups we picked up, so we have to move him back to our current cup.
				//cups.Index = currentCupIndex;

				//Move the crab to the next cup for the next move.
				currentCup = currentCup.CircularNext(); //cups.Next();
			}
		}

		protected override string CalculatePart2(Cups cups) {
			/*GetCrabby(cups, 1000000, 10000000);

			//Find the cup labeled 1
			while (cups.Next() != 1) {
			}

			//Start with the cup AFTER the cup labeled 1 and return each cup in order as a string.
			return (new BigInteger(cups.Peek(1)) * new BigInteger(cups.Peek(2))).ToString();*/
			GetCrabby(ref cups, 1000000, 10000000);

			//Find the cup labeled 1
			IEnumerable<int> oneCup = cups[1].AsEnumerable().Select(node => node.Value);

			//Start with the cup AFTER the cup labeled 1 and return each cup in order as a string.
			return (new BigInteger(oneCup.Skip(1).First()) * new BigInteger(oneCup.Skip(2).First())).ToString();
		}
	}
}
