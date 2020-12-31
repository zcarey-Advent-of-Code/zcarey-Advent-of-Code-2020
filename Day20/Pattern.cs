using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
		//public Operation Operation { get; set; }
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

		public void Or(bool[,] data, Operation dataOp) {
			foreach(Point p in getPatternPoints()) {
				Point o = new Point(this.Origin.X + p.X, this.Origin.Y + p.Y);
				if (dataOp.Transpose) o = new Point(o.Y, o.X);
				if (dataOp.FlipVertical) o = new Point(o.X, data.GetLength(1) - o.Y - 1);
				if (dataOp.FlipHorizontal) o = new Point(data.GetLength(0) - o.X - 1, o.Y);
				data[o.X, o.Y] |= this.pattern[p.X, p.Y];
				//data[p.X, p.Y] |= (Operation.Transpose ? this.pattern[p.Y, p.X] : this.pattern[p.X, p.Y]);
			}
		}

		public void Or(SquareImage<bool> data) {
			foreach (Point p in getPatternPoints()) {
				data[Origin.X + p.X, Origin.Y + p.Y] |= this.pattern[p.X, p.Y];
			}
		}

		public bool Match(SquareImage<bool> image) {
			/*Size area;
			IEnumerable<Point> points = getPatternPoints(out area);
			IEnumerable<bool> data;
			if (Operation.Transpose) {
				data = points.Select(p => pattern[p.Y, p.X]);
			} else {
				data = points.Select(p => pattern[p.X, p.Y]);
			}*/

			/*return image.GetRegion(Origin, this.Area).SequenceEqual(getPatternPoints().Select(p => {
				bool debug = pattern[p.X, p.Y];
				return debug;
			}));*/
			/*return image.GetRegion(Origin, this.Area).SequenceEqual(getPatternPoints().Select(p => {
				bool debug = pattern[p.X, p.Y];
				return debug;
			}), new PatternComparer());*/
			Rectangle region = new Rectangle(Origin, this.Area);
			if (!image.RegionWithinBounds(region)) return false;
			return image[region].SequenceEqual(getPatternPoints().Select(p => {
				bool debug = pattern[p.X, p.Y];
				return debug;
			}), new PatternComparer());
		}

		private IEnumerable<Point> getPatternPoints(/*out Size area*/) {
			//area = (Operation.Transpose ? new Size(Height, Width) : this.Area);
			IEnumerable<Point> points = pattern.Indices(); //GetBasePoints();
			/*points = ApplyTranspose(points);
			points = ApplyVFlip(points, area);
			points = ApplyHFlip(points, area);*/
			return points;
		}

		//NOTE: The first sequence should be the image and the second sequence should be the pattern
		private class PatternComparer : IEqualityComparer<bool> {
			public bool Equals([AllowNull] bool image, [AllowNull] bool pattern) {
				if (image == null || pattern == null) return false;
				else return !pattern || (pattern && image);
			}

			public int GetHashCode([DisallowNull] bool obj) {
				return obj.GetHashCode();
			}
		}

		/*private IEnumerable<Point> GetBasePoints() {
			for (int y = 0; y < Height; y++) {
				for (int x = 0; x < Width; x++) {
					yield return new Point(x, y);
				}
			}
		}*/
		/*
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
		*/
	}
}
