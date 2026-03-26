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
               new MapPoint(){ Name="Praha", XPercent=0.3565, YPercent=0.3991 },
    new MapPoint(){ Name="Brno", XPercent=0.6765, YPercent=0.7115 },
    new MapPoint(){ Name="Ostrava", XPercent=0.9076, YPercent=0.4729 },
    new MapPoint(){ Name="Plzeň", XPercent=0.1497, YPercent=0.5367  },
    new MapPoint(){ Name="Liberec",XPercent=0.4258, YPercent=0.1209  },
    new MapPoint(){ Name="Olomouc", XPercent=0.7331, YPercent=0.5246  },
    new MapPoint(){ Name="Hradec Králové", XPercent=0.5906, YPercent=0.3662  },
    new MapPoint(){ Name="Pardubice", XPercent=0.5675, YPercent=0.3963 },
    new MapPoint(){ Name="Zlín", XPercent=0.8238, YPercent=0.7007 },
    new MapPoint(){ Name="České Budějovice", XPercent=0.3552, YPercent=0.8178 }
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



        private void GetImageDisplayData(out double displayedWidth, out double displayedHeight, out double offsetX, out double offsetY)
        {
            double imageRatio = MapImage.Source.Width / MapImage.Source.Height;
            double controlRatio = MapImage.ActualWidth / MapImage.ActualHeight;

            offsetX = 0;
            offsetY = 0;

            if (controlRatio > imageRatio)
            {
                // pruhy vlevo/vpravo
                displayedHeight = MapImage.ActualHeight;
                displayedWidth = displayedHeight * imageRatio;
                offsetX = (MapImage.ActualWidth - displayedWidth) / 2;
            }
            else
            {
                // pruhy nahoře/dole
                displayedWidth = MapImage.ActualWidth;
                displayedHeight = displayedWidth / imageRatio;
                offsetY = (MapImage.ActualHeight - displayedHeight) / 2;
            }
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
            if (MapImage.Source == null) return;

            OverlayCanvas.Children.Clear();

            GetImageDisplayData(out double displayedWidth, out double displayedHeight, out double offsetX, out double offsetY);

            foreach (var point in points)
            {
                double x = offsetX + displayedWidth * point.XPercent;
                double y = offsetY + displayedHeight * point.YPercent;

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
        //smazat
        private void MapImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (MapImage.Source == null) return;

            //// Pozice kliknutí vůči prvku Image
            //Point pos = e.GetPosition(MapImage);

            //// Získání dat o tom, kde přesně je vykreslený vnitřek obrázku
            //GetImageDisplayData(out double displayedWidth, out double displayedHeight, out double offsetX, out double offsetY);

            //// Výpočet relativní pozice VŮČI OBRÁZKU (bez černých/bílých okrajů)
            //double clickX = pos.X - offsetX;
            //double clickY = pos.Y - offsetY;

            //// Kontrola, zda jsme klikli v rámci mapy a ne do "pruhů"
            //if (clickX >= 0 && clickX <= displayedWidth && clickY >= 0 && clickY <= displayedHeight)
            //{
            //    double xPercent = clickX / displayedWidth;
            //    double yPercent = clickY / displayedHeight;

            //    // Formátování pro snadné kopírování do tvého Listu
            //    string output = $"new MapPoint {{ Name=\"Nové Město\", XPercent={xPercent:F4}, YPercent={yPercent:F4} }},";
            //    Clipboard.SetText(output); // Rovnou se ti to zkopíruje do schránky
            //    MessageBox.Show($"Souřadnice uloženy do schránky:\n{output}");
            //}
        }
    }
    }