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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProyectoDSI
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Mapa : Page
    {
        public Mapa()
        {
            this.InitializeComponent();
        }

        private void AgentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Agentes));
        }

        private void RecruitmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Reclutamiento));
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(MainPage));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Opciones));
        }

        private void LevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Sigilo));
        }

        private void NewLevelButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Crea una nueva instancia del popup
            Popup myPopup = new Popup();

            // Configura el contenido del popup
            myPopup.Child = new TextBlock { Text = "Hola, mundo!" };
            myPopup.Width = 200;
            myPopup.Height = 200;

            // Establece la posición del popup en relación al botón
            Button boton = sender as Button;
            myPopup.HorizontalOffset = 20;
            myPopup.VerticalOffset = 30;

            // Abre el popup en relación al botón
            myPopup.IsOpen = true;
        }
    }
}
