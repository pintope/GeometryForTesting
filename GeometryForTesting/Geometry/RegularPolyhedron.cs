namespace GeometryForTesting.Geometry
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Media3D;


    public abstract class RegularPolyhedron
    {
        protected virtual void Initialize(double edge)
        {
            Edge = edge;
            Initialize(new DiffuseMaterial(new SolidColorBrush(Colors.DeepSkyBlue)));
        }

        public virtual void Initialize(Material material)
        {
            Shape = new GeometryModel3D();
            MeshGeometry3D shapeMesh = CreateShape();
            Shape.Geometry = shapeMesh;
            Shape.Material = material;
        }

        /// <summary>
        /// Creates a scene and start animation
        /// </summary>
        public void Animate(MainWindow window)
        {
            DirectionalLight light = new DirectionalLight
            {
                Color = Colors.White,
                Direction = new Vector3D(-1, -1, -1)
            };

            PerspectiveCamera camera = new PerspectiveCamera
            {
                FarPlaneDistance = 20,
                NearPlaneDistance = 1,
                FieldOfView = 45,
                Position = new Point3D(2, 2, 3),
                LookDirection = new Vector3D(-2, -2, -3),
                UpDirection = new Vector3D(0, 1, 0)
            };

            // Collect cube and light in one model.
            Model3DGroup modelGroup = new Model3DGroup();
            modelGroup.Children.Add(Shape);
            modelGroup.Children.Add(light);
            ModelVisual3D modelsVisual = new ModelVisual3D
            {
                Content = modelGroup
            };

            // Create scene.
            Viewport3D viewport = new Viewport3D
            {
                Camera = camera
            };

            viewport.Children.Add(modelsVisual);
            window.Canvas.Children.Add(viewport);
            viewport.Height = 600;
            viewport.Width = 600;
            Canvas.SetTop(viewport, 0);
            Canvas.SetLeft(viewport, 0);
            window.Width = viewport.Width;
            window.Height = viewport.Height;

            // Create and start animation
            AxisAngleRotation3D axis = new AxisAngleRotation3D(new Vector3D(1, 1, 0), 0);
            RotateTransform3D rotate = new RotateTransform3D(axis);
            Shape.Transform = rotate;
            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 360,
                Duration = new Duration(TimeSpan.FromSeconds(20.0)),
                RepeatBehavior = RepeatBehavior.Forever
            };

            NameScope.SetNameScope(window.Canvas, new NameScope());
            window.Canvas.RegisterName("cubeaxis", axis);
            Storyboard.SetTargetName(animation, "cubeaxis");
            Storyboard.SetTargetProperty(animation, new PropertyPath(AxisAngleRotation3D.AngleProperty));
            Storyboard RotCube = new Storyboard();
            RotCube.Children.Add(animation);
            RotCube.Begin(window.Canvas);
        }


        protected abstract MeshGeometry3D CreateShape();
        public abstract double GetVolume();

        public GeometryModel3D Shape { get; private set; }
        public double Edge { get; private set; }
    }
}
