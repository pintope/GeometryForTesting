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
            Icosphere icosphere = new Icosphere(1, 1);
            icosphere.Animate(this);

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
