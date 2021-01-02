using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Grid = System.Collections.Generic.Dictionary<System.Drawing.Point, bool>;

namespace Day24 {
	class Program : ParsedInputProgramStructure<Directions> {

		static Grid part1Result;

		Program() : base(Directions.Parse) {
		}

		static void Main(string[] args) {
			new Program().Run("input.txt");
			//new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Directions[] input) {
			//Using axial coordinates for grid system
			//https://www.redblobgames.com/grids/hexagons/#coordinates-axial
			Grid grid = new Grid();
			foreach(Directions directions in input) {
				Point result = directions.GetAbsoluteOffset();
				if (!grid.ContainsKey(result)) grid[result] = false; //Because tiles are by-default white, so now that it's flipped it is black
				else grid[result] ^= true;
			}

			part1Result = grid;
			return grid.Select(pair => pair.Value).Count(x => x == false).ToString();
		}

		protected override string CalculatePart2(Directions[] input) {
			if(part1Result == null) {
				CalculatePart1(input);
			}

			//Using axial coordinates for grid system
			//https://www.redblobgames.com/grids/hexagons/#coordinates-axial
			Grid currentState = part1Result;
			for(int i = 0; i < 100; i++) {
				currentState = ConwaysGameOfLife(currentState);
			}

			return currentState.Select(pair => pair.Value).Count(x => x == false).ToString();
		}

		private Grid ConwaysGameOfLife(Grid currentState) {
			Grid nextState = new Grid();

			foreach(KeyValuePair<Point, bool> cell in currentState) {
				int blackTileCount = cell.Key.HexagonNeighbors().Select(p => {
					bool result;
					if (currentState.TryGetValue(p, out result)) {
						return result;
					} else {
						return true; //Tiles default to white.
					}
				}).Count(x => x == false); //Count black tiles

				if (cell.Value) {
					//White
					if(blackTileCount == 2) {
						//Gets flipped to black
						nextState[cell.Key] = false;
					}
				} else {
					//Black
					if()
				}
			}

			return nextState;
		}

	}
}
