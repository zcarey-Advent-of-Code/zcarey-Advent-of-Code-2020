using System;
using System.Collections.Generic;
using System.Text;

namespace Day17 {
	struct Point {

		public int X;
		public int Y;
		public int Z;

		public Point(int x, int y, int z) {
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public void Offset(int dx, int dy, int dz) {
			X += dx;
			Y += dy;
			Z += dz;
		} 

		public void Offset(Point delta) {
			X += delta.X;
			Y += delta.Y;
			Z += delta.Z;
		}

		public static Point operator +(Point left, Point right) {
			return new Point(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		public static Point operator -(Point left, Point right) {
			return new Point(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		public override int GetHashCode() {
			return unchecked(X + (31 * Y) + (31 * 31 * Z));
		}

	}
}
