using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace ProyectoDSI
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadAndPlaySound();
        }

        private async void LoadAndPlaySound()
        {
            //Si es la inicializacion inicial de la aplicacion se activa la musica
            if (Model.FirstLog)
            {
                App.GlobalMediaPlayer.Source = MediaSource.CreateFromStorageFile(await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/NoGood.mp3")));
                App.GlobalMediaPlayer.Volume = Model.MusicVolume;
                App.GlobalMediaPlayer.IsLoopingEnabled= true;
                App.GlobalMediaPlayer.Play();
                Model.FirstLog= false;
            }
            
        }

        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void PlayButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Mapa));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Opciones));
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }
    }
}
