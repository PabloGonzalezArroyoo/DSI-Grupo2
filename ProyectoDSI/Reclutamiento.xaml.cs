using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Storage;
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
    public sealed partial class Reclutamiento : Page
    {
        int currentMoney;
        Agente currentSel;
        public ObservableCollection<Agente> ListaAgentes { get; } = new ObservableCollection<Agente>();
        public ObservableCollection<Agente> ListaReclutas { get; } = new ObservableCollection<Agente>();

        public Reclutamiento()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            LoadFX();
            currentMoney = Model.getMoney();
            MoneyText.Text = currentMoney.ToString();
            if (ListaAgentes != null)
            {
                foreach (Agente ag in Model.GetAllAgentes()) ListaAgentes.Add(ag);
                Model.shuffleReclutas();
                foreach (Agente ag in Model.GetAllReclutas()) ListaReclutas.Add(ag);
            }
        }
        private async void LoadFX()
        {
            App.FXMediaPlayer.Source = MediaSource.CreateFromStorageFile(await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/Agentes/CatSound.mp3")));
        }

        private void AgentsButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Agentes));
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Mapa));
        }

        private void OptionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Opciones));
        }

        private void Actualizar_OnClick(object sender, RoutedEventArgs e)
        {
            gridViewReclutas.ItemsSource = null;
            gridViewReclutas.ItemsSource = Model.shuffleReclutas();
        }

        private void gridViewReclutas_ItemClick(object sender, ItemClickEventArgs e)
        {
            BotonComprar.IsEnabled = true;
            Agente agente = e.ClickedItem as Agente;
            currentSel= agente;
            Clase.Text = agente.Clase;
            MainGun.Text = agente.ArmaPrincipal;
            SecondaryGun.Text = agente.ArmaSecundaria;
            Description.Text = agente.Descripcion;
            Level.Text = agente.Nivel.ToString();
            LevelBar.Value = agente.Experiencia;
            LifeStat.Value = agente.Vida;
            DistanceStat.Value = (agente.AtaqueDistancia * 100) / Constants.MAX_DIST_ATTACK;
            MeleeStat.Value = (agente.AtaqueMelee * 100) / Constants.MAX_MELEE_ATTACK;
            MovementStat.Value = (agente.CasillasMovimiento * 100) / Constants.MAX_MOVEMENT;
            Precio.Text = agente.Precio.ToString();
            string stringPath = agente.Imagen;
            Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            AgentImage.Source = imageBitmap;

            // Puntos sobre barras de progreso
            HealthPoints.Text = agente.Vida.ToString();
            MeleePoints.Text = agente.AtaqueMelee.ToString();
            DistPoints.Text = agente.AtaqueDistancia.ToString();
            MovPoints.Text = agente.CasillasMovimiento.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney - int.Parse(Precio.Text) > 0)
            {
                App.FXMediaPlayer.Play();
                currentMoney -= int.Parse(Precio.Text);
                MoneyText.Text = currentMoney.ToString();
                Model.ListaReclutas.Remove(currentSel);
                Model.ListaAgentes.Add(currentSel);
                gridViewReclutas.ItemsSource = null;
                gridViewReclutas.ItemsSource = Model.ListaReclutas;
                BotonComprar.IsEnabled= false;
            }
        }
    }
}
