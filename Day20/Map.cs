using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	class Map {

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
			}

			set {
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

	}
}
