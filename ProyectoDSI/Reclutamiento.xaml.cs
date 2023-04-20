﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
            currentMoney = Model.getMoney();
            MoneyText.Text = currentMoney.ToString();
            if (ListaAgentes != null)
            {
                foreach (Agente ag in Model.GetAllAgentes()) ListaAgentes.Add(ag);
                Model.shuffleReclutas();
                foreach (Agente ag in Model.GetAllReclutas()) ListaReclutas.Add(ag);
            }
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
            Agente Sel = e.ClickedItem as Agente;
            currentSel= Sel;
            Clase.Text = Sel.Clase;
            MainGun.Text = Sel.ArmaPrincipal;
            Description.Text = Sel.Descripcion;
            Level.Text = Sel.Nivel.ToString();
            LevelBar.Value = Sel.Nivel;
            LifeStat.Value = Sel.Vida;
            DistanceStat.Value = Sel.AtaqueDistancia;
            MeleeStat.Value = Sel.AtaqueMelee;
            MovementStat.Value = Sel.CasillasMovimiento;
            Precio.Text = Sel.Precio.ToString();
            string stringPath = Sel.Imagen;
            Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            AgentImage.Source = imageBitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentMoney - int.Parse(Precio.Text) > 0)
            {
                currentMoney -= int.Parse(Precio.Text);
                MoneyText.Text = currentMoney.ToString();
                Model.ListaReclutas.Remove(currentSel);
                Model.ListaAgentes.Add(currentSel);
                gridViewReclutas.ItemsSource = null;
                gridViewReclutas.ItemsSource = Model.ListaReclutas;
                
            }
        }
    }
}
