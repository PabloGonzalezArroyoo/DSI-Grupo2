using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoDSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Mapa : Page
    {
        Button boton;

        public Mapa()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Casos> list = Model.ListaCasos;
            for (int i = 0; i < Model.ListaCasos.Count; i++)
            {
                if (Model.ListaCasos[i].Completed)
                {
                    string botonName = "Level" + (i + 1);
                    Button b = (Button) this.FindName(botonName);
                    Image image = b.Content as Image;
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation1.png", UriKind.RelativeOrAbsolute));
                }
            }
            boton = null;
        }

        private void Navigate(Type type)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(type);
        }

        private void DeactivatePopUp()
        {
            if (PopupHint != null)
            {
                PopupHint.IsOpen = false;
            }
        }

        private void AgentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp();
            Navigate(typeof(Agentes));
        }

        private void RecruitmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp();
            Navigate(typeof(Reclutamiento));
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp();
            Navigate(typeof(MainPage));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp();
            Navigate(typeof(Opciones));
        }

        private void LevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp();

            // Marcar nivel como completado
            string[] aux = boton.Name.Split('l');
            Model.SetCasoComplete(int.Parse(aux[1]));

            Navigate(typeof(Sigilo));
        }

        private void NewLevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            string[] aux = b.Name.Split('l');

            if (!PopupHint.IsOpen) {
                // Posición del popup en relación al botón
                Point relativePoint = b.TransformToVisual(this).TransformPoint(new Point(0, 0));
                double x = relativePoint.X;
                double y = relativePoint.Y;
                switch (aux[1])
                {
                    case "1": case "2": case "4": case "6":
                        PopupHint.HorizontalOffset = x + 100;
                        PopupHint.VerticalOffset = y - 100;
                        break;
                    case "3":
                        PopupHint.HorizontalOffset = x + 100;
                        PopupHint.VerticalOffset = y - 400;
                        break;
                    case "5":
                        PopupHint.HorizontalOffset = x + 100;
                        PopupHint.VerticalOffset = y - 200;
                        break;
                    case "7":
                        PopupHint.HorizontalOffset = x - 290;
                        PopupHint.VerticalOffset = y - 400;
                        break;
                    case "8":
                        PopupHint.HorizontalOffset = x - 290;
                        PopupHint.VerticalOffset = y - 100;
                        break;
                }

                // Rellenar información
                Casos caso = Model.GetCasoById(int.Parse(aux[1]));
                TextoCaso.Text = caso.Nombre;
                TextoUno.Text = caso.Descripcion1;
                TextoDos.Text = caso.Descripcion2;
                string dificulty = "";
                for (int i = 0; i < caso.Dificultad; i++) dificulty += "★ ";
                TextoDificultad.Text = dificulty;

                // Abre el popup en relación al botón
                PopupHint.IsOpen = true;

                // Cambiar imagen del botón por la de selección
                Image image = b.Content as Image;
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation2.png", UriKind.RelativeOrAbsolute));
                boton = b;
            }
            else
            {
                // Desactivar popup
                DeactivatePopUp();

                // Asignar imagen de completado o no
                // NOTA:
                // - location.png   -> no seleccionado
                // - checkLocation1 -> completado
                // - checkLocation2 -> seleccionado
                Image image;
                if (boton != null) image = boton.Content as Image;
                else image = b.Content as Image;

                Casos caso = Model.GetCasoById(int.Parse(aux[1]));
                if (!caso.Completed)
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/location.png", UriKind.RelativeOrAbsolute));
                else
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation1.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
