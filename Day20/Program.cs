using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace Day20 {
	class Program : BlockParsedInputProgramStructure<Tile> {

		//static Operation[] ValidOperations = Operation.GetAllOperations().ToArray();
		static readonly Operation[] ValidOperations = {
			Operation.Original,
			Operation.Rotate90,
			Operation.Rotate180,
			Operation.Rotate270,
			Operation.HorizontalFlip,
			Operation.VerticalFlip
		};

		static void Main(string[] args) {
			//new Program().Run("input.txt");
			new Program().Run("Example.txt");
		}

		protected override string CalculatePart1(Tile[] input) {
			Map map = new Map();
			if(!placeTile(map, input, 0, new Point())) {
				throw new Exception("Could not solve!");
			}
			Rectangle bounds = map.GetBounds();
			BigInteger idTL = map[bounds.Left, bounds.Top].ID;
			BigInteger idTR = map[bounds.Right, bounds.Top].ID;
			BigInteger idBR = map[bounds.Right, bounds.Bottom].ID;
			BigInteger idBL = map[bounds.Left, bounds.Bottom].ID;
			return (idTL * idTR * idBR * idBL).ToString();
		}

		private bool placeTile(Map map, Tile[] tiles, int index, Point previousLocation) {
			if (index >= tiles.Length) {
				//Base case
				return map.TilesInASquare();
			}

			Tile tile = tiles[index];

			//Try up
			if (TryPlaceTile(map, tiles, index, tile, new Point(previousLocation.X, previousLocation.Y - 1))) return true;

			//Try down
			if (TryPlaceTile(map, tiles, index, tile, new Point(previousLocation.X, previousLocation.Y + 1))) return true;

			//Try left
			if (TryPlaceTile(map, tiles, index, tile, new Point(previousLocation.X - 1, previousLocation.Y))) return true;

			//Try right
			if (TryPlaceTile(map, tiles, index, tile, new Point(previousLocation.X + 1, previousLocation.Y))) return true;

			return false;
		}

		private bool TryPlaceTile(Map map, Tile[] tiles, int index, Tile tile, Point loc) {
			foreach (Operation op in ValidOperations) {
				tile.Operation = op;
				if (map.ValidTileLocation(loc, tile)) {
					map[loc] = tile;
					if (placeTile(map, tiles, index + 1, loc)) return true;
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
