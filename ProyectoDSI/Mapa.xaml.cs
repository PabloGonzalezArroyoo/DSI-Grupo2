using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
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
        string caso;

        public Mapa()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Recorrerla lista de casos actualizada
            for (int i = 0; i < Model.ListaCasos.Count; i++)
            {
                // Si estoy completado, cambio mi imagen de idle
                if (Model.ListaCasos[i].Completed)
                {
                    string botonName = "Level" + (i + 1);
                    Button b = (Button) this.FindName(botonName);
                    Image image = b.Content as Image;
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation1.png", UriKind.RelativeOrAbsolute));
                }
            }

            // Reseteo el botón
            boton = null;
        }

        private void Navigate(Type type)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(type);
        }

        private void DeactivatePopUp(Button b, string casoAux)
        {
            if (PopupHint != null)
            {
                PopupHint.IsOpen = false;
            }

            // Asignar imagen de completado o no
            // NOTA:
            // - location.png   -> no seleccionado
            // - checkLocation1 -> completado
            // - checkLocation2 -> seleccionado
            if (boton != null && b != null)
            {
                Image imagePrevButton, imageNewButton;
                imagePrevButton = boton.Content as Image;
                imageNewButton = b.Content as Image;

                // Si el caso anterior es distinto a este, compruebo el caso anterior para devolver el botón
                // al estado correcto
                if (caso != casoAux)
                {
                    Casos casoChange = Model.GetCasoById(int.Parse(caso));
                    if (!casoChange.Completed)
                        imagePrevButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/location.png", UriKind.RelativeOrAbsolute));
                    else
                        imagePrevButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation1.png", UriKind.RelativeOrAbsolute));
                }
                // Si soy el mismo, simplemente me miro en mi propio caso de nuevo y me reseteo
                else
                {
                    Casos casoChange = Model.GetCasoById(int.Parse(casoAux));
                    if (!casoChange.Completed)
                        imageNewButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/location.png", UriKind.RelativeOrAbsolute));
                    else
                        imageNewButton.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation1.png", UriKind.RelativeOrAbsolute));
                }
            }
        }

        private void AgentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp(boton, caso);
            Navigate(typeof(Agentes));
        }

        private void RecruitmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp(boton, caso);
            Navigate(typeof(Reclutamiento));
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp(boton, caso);
            Navigate(typeof(MainPage));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp(boton, caso);
            Navigate(typeof(Opciones));
        }

        private void LevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DeactivatePopUp(boton, caso);

            // Marcar nivel como completado
            string[] aux = boton.Name.Split('l');
            Model.SetCasoComplete(int.Parse(aux[1]));

            Navigate(typeof(Sigilo));
        }

        private void NewLevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Guardar botón y el id de su caso
            Button b = sender as Button;
            string[] aux = b.Name.Split('l');
            string casoAux = aux[1];

            if (!PopupHint.IsOpen) {
                // Posición del popup en relación al botón
                Point relativePoint = b.TransformToVisual(this).TransformPoint(new Point(0, 0));
                double x = relativePoint.X;
                double y = relativePoint.Y;
                switch (aux[1])
                {
                    case "1": case "3": case "4": case "6":
                        PopupHint.HorizontalOffset = x + 100;
                        PopupHint.VerticalOffset = y - 100;
                        break;
                    case "2":
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
                Casos casoChange = Model.GetCasoById(int.Parse(aux[1]));
                TextoCaso.Text = casoChange.Nombre;
                TextoUno.Text = casoChange.Descripcion1;
                TextoDos.Text = casoChange.Descripcion2;
                string dificulty = "";
                for (int i = 0; i < casoChange.Dificultad; i++) dificulty += "★ ";
                TextoDificultad.Text = dificulty;

                // Abre el popup en relación al botón
                PopupHint.IsOpen = true;

                // Cambiar imagen del botón por la de selección
                Image image = b.Content as Image;
                if(casoChange.Completed)
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation2.png", UriKind.RelativeOrAbsolute));
                else
                    image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/locationSelected.png", UriKind.RelativeOrAbsolute));
                boton = b;
                caso = casoAux;

                // Mantener foco en el nuevo botón
                JugarButton.Focus(FocusState.Programmatic);
            }
            else if(PopupHint.IsOpen)
            {
                // Desactivar popup
                DeactivatePopUp(b, casoAux);
            }
        }

        private void OnPreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            // Salir del popup por teclado y mando
            if (e.Key == VirtualKey.Escape)
            {
                e.Handled = false;

                // Foco al anterior botón y desactivar popup
                boton.Focus(FocusState.Programmatic);
                DeactivatePopUp(boton, caso);
            }
            else e.Handled = true;
        }
    }
}
