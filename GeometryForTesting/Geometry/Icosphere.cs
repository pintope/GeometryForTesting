namespace GeometryForTesting.Geometry
{
    using System.Windows.Media.Media3D;
    using System.Collections.Generic;


    public class Icosphere : RegularPolyhedron
    {
        private MathTools tool;
        private MeshGeometry3D geometry;
        private int index;
        private readonly int subdivisionLevel;
        private Dictionary<long, int> middlePointIndexCache;


        public Icosphere(double edge = 1, int level = 0)
        {
            // Dependency.
            tool = new MathTools();

            subdivisionLevel = level;
            Initialize(edge);
        }

        public override double GetVolume()
        {
            // El volumen es 4Π(1*Sen(2Π/5)³)/3.
            return tool.Mul(tool.Div(4.0, 3.0), tool.Mul(tool.PI, tool.Pow(tool.Mul(Edge, tool.Sin(tool.Mul(2.0, tool.Div(tool.PI, 5.0)))), 3.0)));
        }

        /// <summary>
        /// Create and returns an icosahedral mesh mesh.
        /// </summary>
        protected override MeshGeometry3D CreateShape()
        {
            geometry = new MeshGeometry3D();
            middlePointIndexCache = new Dictionary<long, int>();
            index = 0;

            // Create 12 vertices of an icosahedron.
            var t = (1.0 + System.Math.Sqrt(5.0)) / 2.0;

            AddVertex(new Point3D(-1, t, 0));
            AddVertex(new Point3D(1, t, 0));
            AddVertex(new Point3D(-1, -t, 0));
            AddVertex(new Point3D(1, -t, 0));
            AddVertex(new Point3D(0, -1, t));
            AddVertex(new Point3D(0, 1, t));
            AddVertex(new Point3D(0, -1, -t));
            AddVertex(new Point3D(0, 1, -t));
            AddVertex(new Point3D(t, 0, -1));
            AddVertex(new Point3D(t, 0, 1));
            AddVertex(new Point3D(-t, 0, -1));
            AddVertex(new Point3D(-t, 0, 1));

            // Create 20 triangles of the icosahedron.
            var faces = new List<TriangleIndices>
            {
                // 5 faces around point 0.
                new TriangleIndices(0, 11, 5),
                new TriangleIndices(0, 5, 1),
                new TriangleIndices(0, 1, 7),
                new TriangleIndices(0, 7, 10),
                new TriangleIndices(0, 10, 11),

                // 5 adjacent faces.
                new TriangleIndices(1, 5, 9),
                new TriangleIndices(5, 11, 4),
                new TriangleIndices(11, 10, 2),
                new TriangleIndices(10, 7, 6),
                new TriangleIndices(7, 1, 8),

                // 5 faces around point 3.
                new TriangleIndices(3, 9, 4),
                new TriangleIndices(3, 4, 2),
                new TriangleIndices(3, 2, 6),
                new TriangleIndices(3, 6, 8),
                new TriangleIndices(3, 8, 9),

                // 5 adjacent faces.
                new TriangleIndices(4, 9, 5),
                new TriangleIndices(2, 4, 11),
                new TriangleIndices(6, 2, 10),
                new TriangleIndices(8, 6, 7),
                new TriangleIndices(9, 8, 1)
            };

            // Iterate over all subdivision levels for an icosphere.
            for (int i = 0; i < subdivisionLevel; i++)
            {
                var faces2 = new List<TriangleIndices>();
                foreach (var tri in faces)
                {
                    // replace triangle by 4 triangles
                    int a = GetMiddlePoint(tri.v1, tri.v2);
                    int b = GetMiddlePoint(tri.v2, tri.v3);
                    int c = GetMiddlePoint(tri.v3, tri.v1);

                    faces2.Add(new TriangleIndices(tri.v1, a, c));
                    faces2.Add(new TriangleIndices(tri.v2, b, a));
                    faces2.Add(new TriangleIndices(tri.v3, c, b));
                    faces2.Add(new TriangleIndices(a, b, c));
                }

                faces = faces2;
            }

            // Add triangles to mesh.
            foreach (var tri in faces)
            {
                geometry.TriangleIndices.Add(tri.v1);
                geometry.TriangleIndices.Add(tri.v2);
                geometry.TriangleIndices.Add(tri.v3);
            }

            return geometry;
        }

        // Adds vertex to mesh, fixes position to be on unit sphere, returns index.
        private int AddVertex(Point3D p)
        {
            double length = System.Math.Sqrt(p.X * p.X + p.Y * p.Y + p.Z * p.Z);
            geometry.Positions.Add(new Point3D(p.X / length, p.Y / length, p.Z / length));
            return index++;
        }

        // Returns index of point in the middle of p1 and p2.
        private int GetMiddlePoint(int p1, int p2)
        {
            // First checks if we have it already.
            bool firstIsSmaller = p1 < p2;
            long smallerIndex = firstIsSmaller ? p1 : p2;
            long greaterIndex = firstIsSmaller ? p2 : p1;
            long key = (smallerIndex << 32) + greaterIndex;

            if (middlePointIndexCache.TryGetValue(key, out int ret))
            {
                return ret;
            }

            // Not in cache, calculates it.
            Point3D point1 = geometry.Positions[p1];
            Point3D point2 = geometry.Positions[p2];
            Point3D middle = new Point3D(
                (point1.X + point2.X) / 2.0,
                (point1.Y + point2.Y) / 2.0,
                (point1.Z + point2.Z) / 2.0);

            // Adds vertex makes sure point is on unit sphere.
            int i = AddVertex(middle);

            // Stores it, returns index.
            middlePointIndexCache.Add(key, i);
            return i;
        }


        private struct TriangleIndices
        {
            public int v1;
            public int v2;
            public int v3;

            public TriangleIndices(int v1, int v2, int v3)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.v3 = v3;
            }
        }
    }
}