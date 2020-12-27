using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day20 {
	class Pattern {

		public static Pattern SeaMonster = new Pattern(new string[]{
			"                  # ",
			"#    ##    ##    ###",
			" #  #  #  #  #  #   "
		});

		public Point Origin { get; set; }
		public Operation Operation { get; set; }
		public Size Area { get => new Size(Width, Height); }
		public int Width { get; private set; }
		public int Height { get; private set; }

		private bool[,] pattern;

		public Pattern(string[] pattern) {
			this.Width = pattern[0].Length;
			this.Height = pattern.Length;
			this.pattern = new bool[Width, Height];
			for (int y = 0; y < Height; y++) {
				string line = pattern[y];
				if (line.Length != Width) throw new Exception("String lengths must all match!");
				for (int x = 0; x < Width; x++) {
					char c = line[x];
					if ((c == ' ') || (c == '#')) {
						this.pattern[x, y] = (c == '#');
					} else {
						throw new FormatException("Pattern must only contain \' \' or \'#\'.");
					}
				}
			}
		}

		public void Or(bool[,] data) {
			Size area;
			foreach(Point p in getPatternPoints(out area)) {
				data[p.X, p.Y] |= (Operation.Transpose ? this.pattern[p.Y, p.X] : this.pattern[p.X, p.Y]);
			}
		}

		public bool Match(Image image) {
			Size area;
			IEnumerable<Point> points = getPatternPoints(out area);
			IEnumerable<bool> data;
			if (Operation.Transpose) {
				data = points.Select(p => pattern[p.Y, p.X]);
			} else {
				data = points.Select(p => pattern[p.X, p.Y]);
			}

			return image.GetRegion(Origin, area).SequenceEqual(data);
		}

		private IEnumerable<Point> getPatternPoints(out Size area) {
			area = (Operation.Transpose ? new Size(Height, Width) : this.Area);
			IEnumerable<Point> points = pattern.Indices(); //GetBasePoints();
			points = ApplyTranspose(points);
			points = ApplyVFlip(points, area);
			points = ApplyHFlip(points, area);
			return points;
		}

		/*private IEnumerable<Point> GetBasePoints() {
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					yield return new Point(x, y);
				}
			}
		}*/

		private IEnumerable<Point> ApplyTranspose(IEnumerable<Point> data) {
			if (this.Operation.Transpose) {
				return data.Select(p => new Point(p.Y, p.X));
			} else {
				return data;
			}
		}

		private IEnumerable<Point> ApplyVFlip(IEnumerable<Point> data, Size area) {
			if (this.Operation.FlipVertical) {
				return data.Select(p => new Point(p.X, area.Height - p.Y - 1));
			} else {
				return data;
			}
		}

		private IEnumerable<Point> ApplyHFlip(IEnumerable<Point> data, Size area) {
			if (this.Operation.FlipHorizontal) {
				return data.Select(p => new Point(area.Width - p.X - 1, p.Y));
			} else {
				return data;
			}
		}

	}
}
