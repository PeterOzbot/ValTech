using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KDTreeLibrary;


namespace KDTreeConsole {
    class Program {
        public static KDTree<KDRectangle> tree = new KDTree<KDRectangle>(4);
        static void Main(string[] args) {

            // this is example form the image ExampleRectangles.png
            KDRectangle blue = new KDRectangle(3, 3, 4, 5, "blue");
            KDRectangle Yellow = new KDRectangle(4, 5, 6, 6, "Yellow");
            KDRectangle red = new KDRectangle(2, 5, 3, 6, "red");
            KDRectangle orange = new KDRectangle(1, 2, 5, 7, "orange");

            Console.WriteLine("Add-------------------------");
            //  Add(blue);
            Add(Yellow);
            Console.WriteLine("Add-----------------------------");
            Add(red);
            Console.WriteLine("Add-----------------------------");
            Add(blue);
            //  Add(orange);




            //List<KDRectangle> Rectangles = new List<KDRectangle>();
            //Random random = new Random();
            //// random generation of rectangles
            //for (int i = 0 ; i < 100000 ; i++) {

            //    double x1 = random.NextDouble() * (5000 - 0) + 0;
            //    double y1 = random.NextDouble() * (5000 - 0) + 0;

            //    double height = 28;
            //    double length = random.NextDouble() * (500 - 0) + 0;

            //    double x2 = x1 + length;
            //    double y2 = y1 + height;

            //    //Console.WriteLine(String.Format("x1={0} x2={1} y1={2} y2={3}", x1, x2, y1, y2));
            //    //  Console.WriteLine("i= " +i + " --------------------------------------------");
            //    //Thread.Sleep(100);
            //    Rectangles.Add(new KDRectangle(x1, y1, x2, y2, String.Format("x1={0} x2={1} y1={2} y2={3}", x1, x2, y1, y2)));
            //}

            //Stopwatch st = new Stopwatch();
            //st.Start();
            //foreach (KDRectangle Rectangle in Rectangles) {
            //    Add(Rectangle);
            //}
            ////st.Stop();


            //Console.WriteLine("ElapsedMilliseconds:    " + st.ElapsedMilliseconds);
            //Console.WriteLine("ElapsedTicks:    " + st.ElapsedTicks);



            Console.WriteLine("Done");
            Console.ReadLine();
        }


        public static bool Add(KDRectangle Rectangle) {
            if (tree.Size == 0) {
                tree.AddPoint(new double[] { Rectangle.LeftTop.X, Rectangle.RightBottom.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.Y }, Rectangle);
                return true;
            }
            else {
                NearestNeighbour<KDRectangle> pIter = tree.NearestNeighbors(new double[] { Rectangle.LeftTop.X, Rectangle.RightBottom.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.Y }, 10);
                while (pIter.MoveNext()) {
                    KDRectangle overlaper = pIter.Current;
                    if (doOverlap(Rectangle.LeftTop, Rectangle.RightBottom, overlaper.LeftTop, overlaper.RightBottom)) {
                        Console.WriteLine(Rectangle.Name);
                        Rectangle.Filled = true;
                        return false;
                    }
                }

                tree.AddPoint(new double[] { Rectangle.LeftTop.X, Rectangle.RightBottom.X, Rectangle.LeftTop.Y, Rectangle.RightBottom.Y }, Rectangle);
                return true;
            }
        }

        // Returns true if two rectangles (l1, r1) and (l2, r2) overlap
        public static bool doOverlap(Point l1, Point r1, Point l2, Point r2) {
            // Console.WriteLine("checking overlap");
            // If one rectangle is on left side of other
            if (l1.X > r2.X || l2.X > r1.X)
                return false;

            // If one rectangle is above other
            if (l1.Y < r2.Y || l2.Y < r1.Y)
                return false;

            return true;
        }


    }
}
