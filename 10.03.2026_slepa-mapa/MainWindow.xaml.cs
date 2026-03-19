using _10._03._2026_slepa_mapa;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _10._03._2026_slepa_mapa
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MapPoint> points = new List<MapPoint>()
        {
            new MapPoint(){ Name="Brno", XPercent=0.6766, YPercent=0.7153 },
            new MapPoint(){ Name="Praha", XPercent=0.3476, YPercent=0.3942 },
            new MapPoint(){ Name="Ostrava", XPercent=0.8917, YPercent=0.4599 }
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPoints();
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var point in points)
            {
                double x = MapImage.ActualWidth * point.XPercent;
                double y = MapImage.ActualHeight * point.YPercent;

                Button btn = new Button()
                {
                    Content = point.Name,
                    Tag = point
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x);
                Canvas.SetTop(btn, y);

                OverlayCanvas.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MapPoint point = btn.Tag as MapPoint;

            MessageBox.Show(point.Name);
        }
        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X / MapImage.ActualWidth;
            double yPercent = pos.Y / MapImage.ActualHeight;

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}