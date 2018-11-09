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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//Librerías para multiprocesamiento
using System.Threading;
using System.Diagnostics;

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;

        enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;

        public MainWindow()
        {
            
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();
            tiempoAnterior = stopwatch.Elapsed;

            //1.- Establecer instrucciones
            ThreadStart threadStart =
                new ThreadStart(actualizar);
            //2.- Inicializar el Thread
            Thread threadMoverEnemigos =
                new Thread(threadStart);
            //3.- Ejecutar el Thread
            threadMoverEnemigos.Start();

            /*imgRana.RenderTransform =
                new RotateTransform(90);*/
            
        }

        void actualizar()
        {
            while (true)
            {
                Dispatcher.Invoke(
                () =>
                {
                    var tiempoActual = stopwatch.Elapsed;
                    var deltaTime =
                        tiempoActual - tiempoAnterior;

                    if (estadoActual == EstadoJuego.Gameplay)
                    {
                        double leftCarroActual =
                        Canvas.GetLeft(imgCarro);
                        Canvas.SetLeft(
                            imgCarro, leftCarroActual - (20 * deltaTime.TotalSeconds));
                        if (Canvas.GetLeft(imgCarro) <= -100)
                        {
                            Canvas.SetLeft(imgCarro, 800);
                        }


                        //Intersección en X
                        double xCarro =
                            Canvas.GetLeft(imgCarro);
                        double xRana =
                            Canvas.GetLeft(imgRana);
                        if (xRana + imgRana.Width >= xCarro &&
                            xRana <= xCarro + imgCarro.Width)
                        {
                            lblInterseccionX.Text =
                            "SI HAY INTERSECCION EN X!!!";
                        }
                        else
                        {
                            lblInterseccionX.Text =
                            "No hay interseccion en X";
                        }
                        double yCarro =
                            Canvas.GetTop(imgCarro);
                        double yRana =
                            Canvas.GetTop(imgRana);
                        if (yRana + imgRana.Height >= yCarro &&
                            yRana <= yCarro + imgCarro.Height)
                        {
                            lblInterseccionY.Text =
                                "SI HAY INTERSECCION EN Y!!!";
                        }
                        else
                        {
                            lblInterseccionY.Text =
                                "No hay interseccion en Y";
                        }

                        if (xRana + imgRana.Width >= xCarro &&
                            xRana <= xCarro + imgCarro.Width &&
                            yRana + imgRana.Height >= yCarro &&
                            yRana <= yCarro + imgCarro.Height)
                        {
                            lblColision.Text =
                                "HAY COLISION!!!";
                            estadoActual = EstadoJuego.Gameover;
                            miCanvas.Visibility = Visibility.Collapsed;
                            canvasGameOver.Visibility =
                                Visibility.Visible;
                        }
                        else
                        {
                            lblColision.Text =
                                "No hay colision";
                        }
                    } else if (estadoActual == EstadoJuego.Gameover)
                    {

                    }

                    

                    


                    tiempoAnterior = tiempoActual;
                   

                }
                );
            }
            
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if (estadoActual == EstadoJuego.Gameplay)
            {
                if (e.Key == Key.Up)
                {
                    double topRanaActual =
                        Canvas.GetTop(imgRana);
                    Canvas.SetTop(imgRana, topRanaActual - 15);
                }
                if (e.Key == Key.Down)
                {
                    double topRanaActual =
                        Canvas.GetTop(imgRana);
                    Canvas.SetTop(imgRana, topRanaActual + 15);
                }
                if (e.Key == Key.Left)
                {
                    double leftRanaActual =
                        Canvas.GetLeft(imgRana);
                    Canvas.SetLeft(imgRana, leftRanaActual - 15);
                }
                if (e.Key == Key.Right)
                {
                    double leftRanaActual =
                        Canvas.GetLeft(imgRana);
                    Canvas.SetLeft(imgRana, leftRanaActual + 15);
                }
            }
        }

       
    }
}
