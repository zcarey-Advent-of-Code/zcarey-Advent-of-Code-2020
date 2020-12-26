using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Day20 {
	class Program : BlockParsedInputProgramStructure<Tile> {

		//static Operation[] ValidOperations = Operation.GetAllOperations().ToArray();
		static readonly Operation[] ValidOperations = Operation.GetAllOperations().ToArray(); /*{
			Operation.Original,
			Operation.Rotate90,
			Operation.Rotate180,
			Operation.Rotate270,
			Operation.HorizontalFlip,
			Operation.VerticalFlip
		};*/

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Tile[] input) {
			int size = (int)Math.Sqrt(input.Length);
			if ((size * size) != input.Length) throw new Exception("Tiles can't form a square!");
			Map map = new Map(size);
			if(!placeTile(map, new List<Tile>(input),/* 0,*/ new Point())) {
				throw new Exception("Could not solve!");
			}
			/*Rectangle bounds = map.GetBounds();
			BigInteger idTL = map[bounds.Left, bounds.Top].ID;
			BigInteger idTR = map[bounds.Right, bounds.Top].ID;
			BigInteger idBR = map[bounds.Right, bounds.Bottom].ID;
			BigInteger idBL = map[bounds.Left, bounds.Bottom].ID;
			return (idTL * idTR * idBR * idBL).ToString();*/
			return map.Corners.Select(x => new BigInteger(x.ID)).Aggregate((x, y) => x * y).ToString();
		}

		private bool placeTile(Map map, List<Tile> tiles,/* int index,*/ Point previousLocation) {
			if (tiles.Count == 0 /*index >= tiles.Length*/) {
				//Base case
				return true; //map.TilesInASquare();
			}

			foreach (Tile tile in tiles) {
				//Tile tile = tiles[0]; //tiles[index];
				//tiles.RemoveAt(0);
				List<Tile> remaining = tiles.Where(x => x != tile).ToList();

				//Try up
				if (TryPlaceTile(map, remaining,/*tiles, index,*/ tile, new Point(previousLocation.X, previousLocation.Y - 1))) return true;

				//Try down
				if (TryPlaceTile(map, remaining,/*tiles, index,*/ tile, new Point(previousLocation.X, previousLocation.Y + 1))) return true;

				//Try left
				if (TryPlaceTile(map, remaining,/*tiles, index,*/ tile, new Point(previousLocation.X - 1, previousLocation.Y))) return true;

				//Try right
				if (TryPlaceTile(map, remaining,/*tiles, index,*/ tile, new Point(previousLocation.X + 1, previousLocation.Y))) return true;
			}
			return false;
		}

		private bool TryPlaceTile(Map map, List<Tile> remaining, /*Tile[] tiles, int index,*/ Tile tile, Point loc) {
			foreach (Operation op in ValidOperations) {
				tile.Operation = op;
				if (map.ValidTileLocation(loc, tile)) {
					map[loc] = tile;
					if (placeTile(map, remaining, /*tiles, index + 1,*/ loc)) return true;
					map[loc] = null;
				}
			}
			return false;
		}

		protected override string CalculatePart2(Tile[] input) {
			throw new NotImplementedException();
		}
	}
}
