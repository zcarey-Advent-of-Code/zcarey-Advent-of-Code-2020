using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Day20 {
	class Program : BlockParsedInputProgramStructure<Tile> {

		static Operation[] ValidOperations = Operation.GetAllOperations().ToArray();
		/*static Operation[] ValidOperations = {
			Operation.Rotate90,
			Operation.Rotate180,
			Operation.Rotate270,
			Operation.HorizontalFlip,
			Operation.VerticalFlip
		};*/
		static Map solvedMap;

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Tile[] input) {
			int size = (int)Math.Sqrt(input.Length);
			if ((size * size) != input.Length) throw new Exception("Tiles can't form a square!");
			Map map = new Map(size);
			/*Tile test1 = input.Where(x => x.ID == 1951).First();
			test1.Operation = Operation.VerticalFlip;
			map[0, 0] = test1;
			Tile test2 = input.Where(x => x.ID == 2311).First();
			test2.Operation = Operation.VerticalFlip;
			map[1, 0] = test2;
			Tile test3 = input.Where(x => x.ID == 3079).First();
			test3.Operation = Operation.Original;
			map[2, 0] = test3;
			List<Tile> tiles = input.Where(x => (x.ID != 1951) && (x.ID != 2311) && (x.ID != 3079)).ToList();*/
			if (!placeTile(map, /*tiles*/new List<Tile>(input), new Point(0,0))) {
				throw new Exception("Could not solve!");
			}
			solvedMap = map;
			Console.WriteLine(map);

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
	Console.WriteLine(image);
			//Find all the sea monsters and mark them in our result
			/*foreach(Point p in image) {
				pattern.Origin = p;
				foreach(Operation op in ValidOperations) {
					pattern.Operation = op;
					if (pattern.Match(image)) {
						pattern.Or(result);
					}
				}
			}*/
			foreach(Operation op in ValidOperations) {
				image.Operation = op;
				foreach(Point p in image) {
					pattern.Origin = p;
					if (pattern.Match(image)) {
						pattern.Or(result, op);
					}
				}
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Operation opp = Operation.RotatedHorizontalFlip;
			image.Operation = opp;
			for(int y = 0; y < image.Size; y++) {
				for(int x = 0; x < image.Size; x++) {
					Point p = new Point(x, y);
					if (opp.Transpose) p = new Point(p.Y, p.X);
					if (opp.FlipVertical) p = new Point(p.X, image.Size - p.Y - 1);
					if (opp.FlipHorizontal) p = new Point(image.Size - p.X - 1, p.Y);
					Console.Write((result[p.X, p.Y] ? 'O' : (image[x, y] ? '#' : '.')));
				}
				Console.WriteLine();
			}

			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			for (int y = 0; y < image.Size; y++) {
				for (int x = 0; x < image.Size; x++) {
					Point p = new Point(x, y);
					if (opp.Transpose) p = new Point(p.Y, p.X);
					if (opp.FlipVertical) p = new Point(p.X, image.Size - p.Y - 1);
					if (opp.FlipHorizontal) p = new Point(image.Size - p.X - 1, p.Y);
					Console.Write(result[p.X, p.Y] ? 'O' : '.');
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine();

			//Now our result is any space marked as a wave in the original image and not a sea monster in our current results
			image.Operation = Operation.Original;
			foreach (Point p in image) {
				result[p.X, p.Y] = image[p] && !result[p.X, p.Y];
			}

			for(int y = 0; y < image.Size; y++) {
				for(int x = 0; x < image.Size; x++) {
					Point p = new Point(x, y);
					if (opp.Transpose) p = new Point(p.Y, p.X);
					if (opp.FlipVertical) p = new Point(p.X, image.Size - p.Y - 1);
					if (opp.FlipHorizontal) p = new Point(image.Size - p.X - 1, p.Y);
					Console.Write(result[p.X, p.Y] ? '#' : '.');
				}
				Console.WriteLine();
			}

			return result.Values().Where(x => x == true).Count().ToString();
		}
	}
}
