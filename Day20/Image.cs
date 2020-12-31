using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Day20 {
/*	class Image : IEnumerable<Point> {

		public int Size { get; }
		public Operation Operation { get; set; }

		private bool[,] image;

		public Image(Tile[,] map, int size) {
			int tileSize = map[0, 0].Size - 2;
			this.Size = size * tileSize;//size * tileSize - size * 2;
			this.image = new bool[Size, Size];
*/
			/*for (int y = 0, tileY = 0; y < Size; y++, tileY = y / Size) {
				for (int x = 0, tileX = 0; x < Size; x++, tileX = x / Size) {
					image[x, y] = map[tileX, tileY][x % (tileSize - 2) + 1, y % (tileSize - 2) + 1];
				}
			}*/
/*			for(int tiley = 0; tiley < size; tiley++) {
				for(int tilex = 0; tilex < size; tilex++) {
					for(int y = 0; y < tileSize; y++) {
						for(int x = 0; x < tileSize; x++) {
							image[tileSize * tilex + x, tileSize * tiley + y] = map[tilex, tiley][x + 1, y + 1];
						}
					}
				}
			}
		}

		public bool this[Point p] {
			get {
				if ((p.X < 0) || (p.Y < 0) || (p.X >= Size) || (p.Y >= Size)) return false;
				if (Operation.Transpose) p = new Point(p.Y, p.X);
				if (Operation.FlipVertical) p = new Point(p.X, Size - p.Y - 1);
				if (Operation.FlipHorizontal) p = new Point(Size - p.X - 1, p.Y);
				return image[p.X, p.Y];
			}
		}
		public bool this[int x, int y] {
			get {
				//if ((x < 0) || (y < 0) || (x >= Size) || (y >= Size)) return false;
				//else return image[x, y];
				return this[new Point(x, y)];
			}
		}

		public IEnumerable<bool> GetRegion(int x, int y, int w, int h) { return GetRegion(new Rectangle(x, y, w, h)); }
		public IEnumerable<bool> GetRegion(Point location, Size area) { return GetRegion(new Rectangle(location, area)); }
		public IEnumerable<bool> GetRegion(Rectangle region) {
			for (int y = region.Y, i = 0; i < region.Height; y++, i++) {
				for(int x = region.X, j = 0; j < region.Width; x++, j++) {
					bool debug = this[x, y];
					yield return debug;
				}
			}
		}

		public IEnumerator<Point> GetEnumerator() {
			return image.Indices().GetEnumerator(); //getAllPoints().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return this.GetEnumerator();
		}
*/
		/*private IEnumerable<Point> getAllPoints() {
			for(int y = 0; y < Size; y++) {
				for(int x = 0; x < Size; x++) {
					yield return new Point(x, y);
				}
			}
		}*/
/*
		public override string ToString() {
			StringBuilder sb = new StringBuilder();
			for(int y = 0; y < Size; y++) {
				for(int x = 0; x < Size; x++) {
					sb.Append(this[x, y] ? '#' : '.');
				}
				if (y < (Size - 1)) sb.AppendLine();
			}
			return sb.ToString();
		}
	}
*/
}
