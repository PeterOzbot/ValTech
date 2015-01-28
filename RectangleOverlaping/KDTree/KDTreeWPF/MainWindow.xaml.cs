using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KDTreeLibrary;

namespace KDTreeWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static KDTree<KDRectangle> tree = new KDTree<KDRectangle>(4);
        public static List<KDRectangle> Rectangles = new List<KDRectangle>();
        public MainWindow() {
            InitializeComponent();





            Random random = new Random();
            // random generation of rectangles
            for (int i = 0 ; i < 100000 ; i++) {

                double x1 = random.NextDouble() * (1000);
                double y1 = random.NextDouble() * (1000);

                double height = 20;
                double length = random.NextDouble() * (200);

                double x2 = x1 + length;
                double y2 = y1 + height;

                //Console.WriteLine(String.Format("x1={0} x2={1} y1={2} y2={3}", x1, x2, y1, y2));
                //  Console.WriteLine("i= " +i + " --------------------------------------------");
                //Thread.Sleep(100);
                //                Rectangles.Add(new KDRectangle(x1, y1, x2, y2, String.Format("x1={0} x2={1} y1={2} y2={3}", x1, x2, y1, y2)));
                Rectangles.Add(new KDRectangle(x1, y1, x2, y2, i.ToString()));
            }





            //Rectangles.Add(new KDRectangle(613, 107, 747, 127, "Point"));
            //Rectangles.Add(new KDRectangle(29, 50, 107, 70, "Min"));
            //Rectangles.Add(new KDRectangle(558, 999, 585, 1019, "Max"));

            foreach (KDRectangle Rectangle in Rectangles) {
                Add(Rectangle);
            }




            Task.Run(() => {
                int notDrawn = 0;

                foreach (KDRectangle Rectangle1 in Rectangles) {

                    Task.Delay(100).Wait();

                    Dispatcher.Invoke(new Action(() => {

                        Grid gridContainer = new Grid();
                        gridContainer.Width = Rectangle1.RightBottom.X - Rectangle1.LeftTop.X;
                        gridContainer.Height = Rectangle1.RightBottom.Y - Rectangle1.LeftTop.Y;


                        Rectangle myRgbRectangle = new Rectangle();
                        myRgbRectangle.Width = Rectangle1.RightBottom.X - Rectangle1.LeftTop.X;
                        myRgbRectangle.Height = Rectangle1.RightBottom.Y - Rectangle1.LeftTop.Y;

                        gridContainer.Children.Add(myRgbRectangle);
                        gridContainer.Children.Add(new Label() { Content = Rectangle1.Name, VerticalContentAlignment = System.Windows.VerticalAlignment.Center, FontSize = 8 });

                        SolidColorBrush mySolidColorBrush = new SolidColorBrush();
                        mySolidColorBrush.Color = Colors.Red;// Color.FromArgb(10, 255, 0, 0);

                        SolidColorBrush mySolidColorBrush2 = new SolidColorBrush();
                        mySolidColorBrush2.Color = Colors.Green;

                        if (Rectangle1.Filled) {

                            notDrawn++;
                   
                                textBox.Text = "Not Drawn : " + notDrawn;
                       
                            //myRgbRectangle.Fill = mySolidColorBrush;

                            //MyCanvas.Children.Add(gridContainer);
                            //Canvas.SetTop(gridContainer, Rectangle1.LeftTop.Y);
                            //Canvas.SetLeft(gridContainer, Rectangle1.LeftTop.X);

                            //DoubleAnimation da = new DoubleAnimation();
                            //da.From = 1;
                            //da.To = 0;
                            //da.Duration = new Duration(TimeSpan.FromSeconds(1));
                            //da.AutoReverse = false;


                            //gridContainer.BeginAnimation(Rectangle.OpacityProperty, da);

                        }
                        else {
                            myRgbRectangle.Fill = mySolidColorBrush2;


                            MyCanvas.Children.Add(gridContainer);
                            Canvas.SetTop(gridContainer, Rectangle1.LeftTop.Y);
                            Canvas.SetLeft(gridContainer, Rectangle1.LeftTop.X);
                        }

                    }));
                }


            });

        }


        private static int count = 0;
        public static bool Add(KDRectangle Rectangle) {
            count++;
            if (tree.Size == 0) {
                tree.AddPoint(new double[] { Rectangle.LeftTop.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.X, Rectangle.RightBottom.Y }, Rectangle);
                return true;
            }
            else {
                NearestNeighbour<KDRectangle> pIter = tree.NearestNeighbors(new double[] { Rectangle.LeftTop.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.X, Rectangle.RightBottom.Y }, 1);
                while (pIter.MoveNext()) {
                    KDRectangle overlaper = pIter.Current;
                    if (doOverlap(Rectangle.LeftTop, Rectangle.RightBottom, overlaper.LeftTop, overlaper.RightBottom)) {
                        //  Console.WriteLine(Rectangle.Name);
                        Rectangle.Filled = true;
                        return false;
                    }
                }

                tree.AddPoint(new double[] { Rectangle.LeftTop.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.X, Rectangle.RightBottom.Y }, Rectangle);
                return true;
            }
        }

        // Returns true if two rectangles (l1, r1) and (l2, r2) overlap
        public static bool doOverlap(KDTreeLibrary.Point a1, KDTreeLibrary.Point a2, KDTreeLibrary.Point b1, KDTreeLibrary.Point b2) {
            if (a1.X < b2.X &&
              a2.X > b1.X &&
              a1.Y < b2.Y &&
              a2.Y > b1.Y) {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
