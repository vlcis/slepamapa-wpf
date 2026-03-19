using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _10._03._2026_slepa_mapa
{
    public partial class MainWindow : Window
    {
        List<MapPoint> points = new List<MapPoint>()
        {
            new MapPoint(){ Name="Brno", XPercent=0.6766, YPercent=0.7153 },
            new MapPoint(){ Name="Praha", XPercent=0.3476, YPercent=0.3942 },
            new MapPoint(){ Name="Ostrava", XPercent=0.8917, YPercent=0.4599 },
            new MapPoint(){ Name="Plzeň", XPercent=0.20, YPercent=0.45 },
            new MapPoint(){ Name="Liberec", XPercent=0.55, YPercent=0.20 },
            new MapPoint(){ Name="Olomouc", XPercent=0.75, YPercent=0.55 },
            new MapPoint(){ Name="Hradec Králové", XPercent=0.60, YPercent=0.40 },
            new MapPoint(){ Name="Pardubice", XPercent=0.55, YPercent=0.45 },
            new MapPoint(){ Name="Zlín", XPercent=0.80, YPercent=0.70 },
            new MapPoint(){ Name="České Budějovice", XPercent=0.35, YPercent=0.75 }
        };

        List<MapPoint> gamePoints;
        int currentIndex = 0;
        int score = 0;
        MapPoint currentTarget;
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        void StartGame()
        {
            score = 0;
            currentIndex = 0;

            gamePoints = points.OrderBy(x => rnd.Next()).ToList();

            NextRound();
        }

        void NextRound()
        {
            DrawPoints();

            if (currentIndex >= gamePoints.Count)
            {
                MessageBox.Show($"Konec hry!\nSkóre: {score}/{gamePoints.Count}");
                StartGame();
                return;
            }

            currentTarget = gamePoints[currentIndex];

            TxtTarget.Text = $"Najdi město: {currentTarget.Name}";
            TxtScore.Text = $"Skóre: {score}/{gamePoints.Count}";
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
                    Content = "",
                    Width = 20,
                    Height = 20,
                    Background = Brushes.Red,
                    BorderBrush = Brushes.Black,
                    Tag = point
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x - 10);
                Canvas.SetTop(btn, y - 10);

                OverlayCanvas.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MapPoint point = btn.Tag as MapPoint;

            if (point == currentTarget)
            {
                score++;
                btn.Background = Brushes.Green;
                MessageBox.Show("Správně!");
            }
            else
            {
                btn.Background = Brushes.Red;
                MessageBox.Show($"Špatně! To bylo {point.Name}");
            }

            currentIndex++;
            NextRound();
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }
    }
}