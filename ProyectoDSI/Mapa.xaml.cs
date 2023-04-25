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
        Popup popup;
        Button boton;

        public Mapa()
        {
            this.InitializeComponent();
        }

        private void Navigate(Type type)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(type);
        }

        private void DeactivatePopUp()
        {
            if (popup != null)
            {
                popup.IsOpen = false;
                popup = null;
            }
        }

        private void AgentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(Agentes));
            DeactivatePopUp();
        }

        private void RecruitmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(Reclutamiento));
            DeactivatePopUp();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(MainPage));
            DeactivatePopUp();
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(Opciones));
            DeactivatePopUp();
        }

        private void LevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Navigate(typeof(Sigilo));
            DeactivatePopUp();
        }

        private void NewLevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Crea una nueva instancia del popup
            if (popup == null) {
                popup = new Popup();

                // Configura el contenido del popup
                ContentControl cc = new ContentControl();
                cc.Style = (Style)this.Resources["PopupContentStyle"];
                cc.ApplyTemplate();

                // Carga el contenido del DataTemplate
                popup.Child = cc;
                popup.Width = 500;
                popup.Height = 500;

                // Posición del popup en relación al botón
                boton = sender as Button;
                Point relativePoint = boton.TransformToVisual(this).TransformPoint(new Point(0, 0));
                double x = relativePoint.X;
                double y = relativePoint.Y;
                popup.HorizontalOffset = x + 100;
                popup.VerticalOffset = y - 100;

                string[] aux = boton.Name.Split('l');
                Casos caso = Model.GetCasoById(int.Parse(aux[1]));

                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(popup); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(cc, i);

                    if (i == 1)
                    {
                        TextBlock txt = child as TextBlock;
                        txt.Text = caso.Nombre;
                    }
                    // Haz algo con el elemento hijo
                }

                // Abre el popup en relación al botón
                popup.IsOpen = true;

                Image image = boton.Content as Image;
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/checkLocation2.png", UriKind.RelativeOrAbsolute));
            }
            else
            {
                DeactivatePopUp();
                Image image = boton.Content as Image;
                image.Source = new BitmapImage(new Uri("ms-appx:///Assets/Mapa/location.png", UriKind.RelativeOrAbsolute));
            }
        }
    }
}
