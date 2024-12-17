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

namespace Projekt___paint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Point currentPoint = new Point(); //obiekt klasy Point, który przechowuje współrzędne punktu
        //zamienic drawStyle na enumerate
        enum DrawStyle
        {
            Freestyle,
            Points,
            StraightLine,
            Polyline, //linia łamana
            EditLine,
            Ellipse,
            Rectangle,
            Polygon,
            Arrow,
            Tree,
            Triangle,
            Star
        }

        DrawStyle drawStyle = DrawStyle.Freestyle;

        Color selectedColor = Color.FromRgb(255, 0, 0);
        public MainWindow()
        {
            InitializeComponent();
        }


        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = DrawStyle.Freestyle;
        }

        private void buttonPoints_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = DrawStyle.Points;
        }

        private void buttonLines_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = DrawStyle.StraightLine;
        }

        private void paintSurface_MouseMove(object sender, MouseEventArgs e)
        {

            //rysowanie linii
            //najpierw sprawdzamy czy myszka jest wciśnięta I SPRAWDZENIE RODZAJU RYSOWANIA
            if (e.LeftButton == MouseButtonState.Pressed && drawStyle == DrawStyle.Freestyle)
            {
                //wykorzystamy tworzenie linii pomiędzy pobranymi punktami
                Brush brushColor = new SolidColorBrush(selectedColor);
                Line line = new Line();
                line.Stroke = brushColor;
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(this).X; //pobranie nowej pozycji od momentu kliknięcia
                line.Y2 = e.GetPosition(this).Y;

                //aktualizacja aktualnej pozycji 
                currentPoint = e.GetPosition(this);

                //dodanie obiektu do canvasa (paintSurfdace)
                paintSurface.Children.Add(line);
            }
        }

        private void paintSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {//mozna zaimplementować w switchu
            //rysowanie punktów
            //currentPoint = e.GetPosition(this);
            if (drawStyle == DrawStyle.Points)
            {
                //musimy utworzyć punkty, tworząc obiekt klasy Elipse
                Ellipse elipse = new Ellipse();
                //ustawienie rozmiarów 
                elipse.Width = 6;
                elipse.Height = 6;

                //Ustawienie wspolrzednych punktu, tak, zeby wspolrzedne byly na sroku, stad to dzielenie na 2
                Canvas.SetTop(elipse, e.GetPosition(this).Y - elipse.Height / 2);
                Canvas.SetLeft(elipse, e.GetPosition(this).X - elipse.Width / 2);

                Brush brushColor = new SolidColorBrush(selectedColor);
                //pokolorowanie
                elipse.Fill = brushColor;
                

                //dodawanie punktu
                paintSurface.Children.Add(elipse);
            }

            if (drawStyle == DrawStyle.Rectangle)
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Width = 60;
                rectangle.Height = 40;

                Canvas.SetLeft(rectangle, e.GetPosition(this).X - rectangle.Width/2);
                Canvas.SetTop(rectangle, e.GetPosition(this).Y - rectangle.Height/2);

                Brush brushColor = new SolidColorBrush(selectedColor);
                rectangle.Stroke = brushColor;

                paintSurface.Children.Add(rectangle);

            }

            if (drawStyle == DrawStyle.Points)
            {
                Polygon polygon = new();

                double mouseX = e.GetPosition(this).X;
                double mouseY = e.GetPosition(this).Y;

                double polygonSize = 20;

                Point point1 = new(mouseX - polygonSize, mouseY + (2 * polygonSize));
                Point point2 = new(mouseX + polygonSize, mouseY + (2 * polygonSize));
                Point point3 = new(mouseX + (2 * polygonSize), mouseY + 0);
                Point point4 = new(mouseX + polygonSize, mouseY - (2 * polygonSize));
                Point point5 = new(mouseX - polygonSize, mouseY - (2 * polygonSize));
                Point point6 = new(mouseX - (2 * polygonSize), mouseY + 0);

                PointCollection polygonPoints =
                [
                    point1,
                    point2,
                    point3,
                    point4,
                    point5,
                    point6,
                ];

                polygon.Points = polygonPoints;

                Brush brushColor = new SolidColorBrush(selectedColor);
                polygon.Stroke = brushColor;


                paintSurface.Children.Add(polygon);
            }
        }

        private void paintSurface_MouseDown(object sender, MouseButtonEventArgs e)//ZDARZENIE POZWALAJĄCE OKREŚŁIĆ JAKA JEST POZYCJA I STAN MYSZKI
        {
            if (e.ButtonState == MouseButtonState.Pressed)
            {
                currentPoint = e.GetPosition(this);//SPRAWDZENIE CZY JEST WCIŚNIETY
                e.GetPosition(this); //Pobranie lokalizacji kursora
            }
        }

        private void drawSegment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editSegmment_Click(object sender, RoutedEventArgs e)
        {

        }

        private void drawPolygon_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = DrawStyle.Polygon;
        }

        private void drawRectangle_Click(object sender, RoutedEventArgs e)
        {
            drawStyle = DrawStyle.Rectangle;
        }

        private void colorPicker_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorPickerWindows colorPickerWindows = new ColorPickerWindows();
            colorPickerWindows.Show();
            //TO DO: Dodać walidację danych oraz ich pobieranie, zamiana RGB na VSH, wzór na platformie
        }

        //TO DO: prosta linia miedzy dwoma kliknietymi punktami - stworzyc dwa punkty i polaczyc to w linie
        //TO DO: edycja utworzonej linii - zmiana punktu początkowego i końcowego

        //TO DO: rysowanie elipsy/koła,linii łamanej,prostokąta,wielokąta,kształtów predefiniowanych np coina,strzalka itp min 4 ksztalty
        //moga byc zaimplementowane z gotowym rozmiarem

        //pamiętać o Soblu w open.CVprzetwarzanie obrazów
        //trzy macierze, dla kazdego koloru osobno (RGB)



        //klasa Brush() służy do malowania i wypełniania wnętrza kształtów
        /*
         solidcolorbrush
        lineargradientbrush
        radialgradientbrush
        imagebrush
        drawingbrush
        visualbrush
         


        klasa Color


        klasa Canvas()
        setTop/Left

        klasaShape()
        fill,height,opacity,width,stroke,strokethickness
        po tej klasie ksztalty dziedzicza i maja te wlasnosci


        klasaLine()


        klasa Ellipse()

        klas MenuItem()
         
         Klasa Path()

        KlasaRectangle

        k PointCollection

        k Polygon
         
         */
    }
}