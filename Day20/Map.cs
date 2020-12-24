using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Day20 {
	class Map {

		private Dictionary<Point, Tile> map = new Dictionary<Point, Tile>();

		public Map() {
		}

		public Tile this[Point key]{
			get {
				Tile result;
				if(map.TryGetValue(key, out result)){
					return result;
				} else {
					return null;
				}
			}

			set {
				map[key] = value;
			}
		}

		public Tile this[int x, int y] {
			get => this[new Point(x, y)];
			set => this[new Point(x, y)] = value;
		}

		public bool ValidTileLocation(Point loc, Tile tile) {

		}

	}
}
