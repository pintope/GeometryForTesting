namespace GeometryForTesting
{
    using System.Windows;
    using System.Windows.Controls;
    using Geometry;


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Icosahedron icosahedron = new Icosahedron(1, 1);
            Icosahedron icosahedron = new Icosahedron(1);
            icosahedron.Animate(this);

            //Cube cube = new Cube();
            //cube.Animate(this);
        }


        public Canvas Canvas
        {
            get
            {
                return canvas;
            }
        }
    }
}
