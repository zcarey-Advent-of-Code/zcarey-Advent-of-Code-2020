using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	class Map {

		//private Dictionary<Point, Tile> map = new Dictionary<Point, Tile>();
		private Tile[,] map;
		public int Size { get; private set; }

		public Map(int size) {
			this.map = new Tile[size, size];
			this.Size = size;
		}

		private bool isValidKey(Point key) {
			return (key.X >= 0) && (key.Y >= 0) && (key.X < Size) && (key.Y < Size);
		}

		public Tile this[Point key]{
			get {
				if (isValidKey(key)) {
					return map[key.X, key.Y];
				} else {
					return null;
				}
				/*Tile result;
				if(map.TryGetValue(key, out result)){
					return result;
				} else {
					return null;
				}*/
			}

			set {
				//map[key] = value;
				map[key.X, key.Y] = value;
			}
		}

		public Tile this[int x, int y] {
			get => this[new Point(x, y)];
			set => this[new Point(x, y)] = value;
		}

		public bool ValidTileLocation(Point loc, Tile tile) {
			if (!isValidKey(loc) || (this[loc] != null)) return false;

			//Check Up
			Tile up = this[loc.X, loc.Y - 1];
			if (up != null && !tile.Top.SequenceEqual(up.Bottom)) return false;

			//Check Down
			Tile down = this[loc.X, loc.Y + 1];
			if (down != null && !tile.Bottom.SequenceEqual(down.Top)) return false;

			//Check Left
			Tile left = this[loc.X - 1, loc.Y];
			if (left != null && !tile.Left.SequenceEqual(left.Right)) return false;

			//Check Right
			Tile right = this[loc.X + 1, loc.Y];
			if (right != null && !tile.Right.SequenceEqual(right.Left)) return false;

			return true;
		}

		public IEnumerable<Tile> Corners {
			get {
				yield return map[0, 0];
				yield return map[Size - 1, 0];
				yield return map[Size - 1, Size - 1];
				yield return map[0, Size - 1];
			}
		}

		/*internal Rectangle GetBounds() {
			IEnumerable<Point> tiles = map.Where(x => x.Value != null).Select(x => x.Key);
			Point first = tiles.First();
			int left = first.X;
			int right = first.X;
			int top = first.Y;
			int bottom = first.Y;
			foreach(Point tile in tiles.Skip(1)) {
				left = Math.Min(left, tile.X);
				right = Math.Max(right, tile.X);
				top = Math.Min(top, tile.Y);
				bottom = Math.Max(bottom, tile.Y);
			}
			return new Rectangle(left, top, right - left, bottom - top);
		}

		internal bool TilesInASquare() {
			IEnumerable<Point> tiles = map.Where(x => x.Value != null).Select(x => x.Key);

			//Find the left and right of the first row
			Point first = tiles.First();
			int left = first.X;
			int right = first.X;
			while(this[left - 1, first.Y] != null) {
				left = left - 1;
			}
			while (this[left + 1, first.Y] != null) {
				right = right + 1;
			}

			//Check rows above
			for(int y = first.Y - 1; ; y--) {
				bool allTilesPresent = true;
				bool atLeastOneTileFound = false;
				if ((this[left - 1, y] != null) || (this[right + 1, y] != null)) return false;
				for(int x = left; x <= right; x++) {
					if(this[x, y] != null) {
						atLeastOneTileFound = true;
					} else {
						allTilesPresent = false;
					}
				}
				if (!allTilesPresent) {
					if (atLeastOneTileFound) {
						return false;
					} else {
						break;
					}
				}
			}

			//Check rows below
			for (int y = first.Y + 1; ; y++) {
				bool allTilesPresent = true;
				bool atLeastOneTileFound = false;
				if ((this[left - 1, y] != null) || (this[right + 1, y] != null)) return false;
				for (int x = left; x <= right; x++) {
					if (this[x, y] != null) {
						atLeastOneTileFound = true;
					} else {
						allTilesPresent = false;
					}
				}
				if (!allTilesPresent) {
					if (atLeastOneTileFound) {
						return false;
					} else {
						break;
					}
				}
			}

			return true;
		}*/
	}
}
