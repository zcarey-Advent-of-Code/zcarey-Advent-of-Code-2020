using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	class Image : IEnumerable<Point> {

		public int Size { get; }

		private bool[,] image;

		public Image(Tile[,] map, int size) {
			int tileSize = map[0, 0].Size;
			this.Size = size * tileSize - size * 2;
			this.image = new bool[Size, Size];

			for (int y = 0, tileY = 0; y < Size; y++, tileY = y / Size) {
				for (int x = 0, tileX = 0; x < Size; x++, tileX = x / Size) {
					image[x, y] = map[tileX, tileY][x % (tileSize - 2) + 1, y % (tileSize - 2) + 1];
				}
			}
		}

		public bool this[Point p] {
			get => this[p.X, p.Y];
		}
		public bool this[int x, int y] {
			get {
				if ((x < 0) || (y < 0) || (x >= Size) || (y >= Size)) return false;
				else return image[x, y];
			}
		}

		public IEnumerable<bool> GetRegion(int x, int y, int w, int h) { return GetRegion(new Rectangle(x, y, w, h)); }
		public IEnumerable<bool> GetRegion(Point location, Size area) { return GetRegion(new Rectangle(location, area)); }
		public IEnumerable<bool> GetRegion(Rectangle region) {
			for (int y = region.Y, i = 0; i < region.Height; y++, i++) {
				for(int x = region.X, j = 0; j < region.Width; x++, j++) {
					yield return this[x, y];
				}
			}
		}

		public IEnumerator<Point> GetEnumerator() {
			return image.Indices().GetEnumerator(); //getAllPoints().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}

		/*private IEnumerable<Point> getAllPoints() {
			for(int y = 0; y < Size; y++) {
				for(int x = 0; x < Size; x++) {
					yield return new Point(x, y);
				}
			}
		}*/
	}
}
