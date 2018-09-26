namespace GeometryForTesting.Geometry
{
    using System;
    using System.Windows.Media;
    using System.Windows.Media.Media3D;


    class Cube : RegularPolyhedron
    {
        public Cube()
            : this(1)
        {
        }

        public Cube(double edge)
        {
            base.Initialize(edge);
        }

        public override double GetVolume()
        {
            return Edge * Edge * Edge;
        }

        /// <summary>
        /// Create and returns a cubic mesh.
        /// </summary>
        protected override MeshGeometry3D CreateShape()
        {
            var cube = new MeshGeometry3D();
            var corners = new Point3DCollection
            {
                new Point3D(0.5, 0.5, 0.5),
                new Point3D(-0.5, 0.5, 0.5),
                new Point3D(-0.5, -0.5, 0.5),
                new Point3D(0.5, -0.5, 0.5),
                new Point3D(0.5, 0.5, -0.5),
                new Point3D(-0.5, 0.5, -0.5),
                new Point3D(-0.5, -0.5, -0.5),
                new Point3D(0.5, -0.5, -0.5)
            };
            cube.Positions = corners;

            int[] indices ={
                //Front
                0,1,2,
                0,2,3,
                //Back
                4,7,6,
                4,6,5,
                //Right
                4,0,3,
                4,3,7,
                //Left
                1,5,6,
                1,6,2,
                //Top
                1,0,4,
                1,4,5,
                //Bottom
                2,6,7,
                2,7,3 };

            Int32Collection Triangles = new Int32Collection();
            foreach (Int32 index in indices)
            {
                Triangles.Add(index);
            }

            cube.TriangleIndices = Triangles;
            return cube;
        }
    }
}
