using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Day20 {
	class Program : BlockParsedInputProgramStructure<Tile> {

		static Operation[] ValidOperations = Operation.GetAllOperations().ToArray();
		static Map solvedMap;

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Tile[] input) {
			int size = (int)Math.Sqrt(input.Length);
			if ((size * size) != input.Length) throw new Exception("Tiles can't form a square!");
			Map map = new Map(size);
			if (!placeTile(map, new List<Tile>(input), new Point())) {
				throw new Exception("Could not solve!");
			}
			solvedMap = map;

			return map.Corners.Select(x => new BigInteger(x.ID)).Aggregate((x, y) => x * y).ToString();
		}

		private bool placeTile(Map map, List<Tile> tiles, Point location) {
			if (location.Y >= map.Size) {
				//Base case
				return tiles.Count == 0;
			} else if (tiles.Count == 0) {
				//Base case
				return false; 
			}

			foreach (Tile tile in tiles) {
				List<Tile> remaining = tiles.Where(x => x != tile).ToList();

				foreach (Operation op in ValidOperations) {
					tile.Operation = op;
					if (map.ValidTileLocation(location, tile)) {
						map[location] = tile;
						Point nextLocation = new Point(location.X + 1, location.Y);
						if (nextLocation.X >= map.Size) nextLocation = new Point(0, location.Y + 1);
						if (placeTile(map, remaining, nextLocation)) return true;
						map[location] = null;
					}
				}
			}

			return false;
		}

		protected override string CalculatePart2(Tile[] input) {
			//"Cheating" on run time by using using the result from Part1
			if (solvedMap == null) CalculatePart1(input);
			Image image = solvedMap.Image;
			Pattern pattern = Pattern.SeaMonster;
			bool[,] result = new bool[image.Size, image.Size];

			//Find all the sea monsters and mark them in our result
			foreach(Point p in image) {
				pattern.Origin = p;
				foreach(Operation op in ValidOperations) {
					pattern.Operation = op;
					if (pattern.Match(image)) {
						pattern.Or(result);
					}
				}
			}

			//Now our result is any space marked as a wave in the original image and not a sea monster in our current results
			foreach(Point p in image) {
				result[p.X, p.Y] = image[p] && !result[p.X, p.Y];
			}

			return result.Values().Where(x => x == true).Count().ToString();
		}
	}
}
