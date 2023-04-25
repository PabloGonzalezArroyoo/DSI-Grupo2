using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel.DataTransfer;
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
    public sealed partial class Agentes : Page
    {
        Agente currentCuartelSel;
        Agente currentEscuadronSel;

        public ObservableCollection<Agente> ListaAgentes { get; } = new ObservableCollection<Agente>();
        public ObservableCollection<Agente> ListaSquad { get; } = new ObservableCollection<Agente>();

        public Agentes()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ListaAgentes != null)
            {
                foreach (Agente ag in Model.GetAllAgentes()) ListaAgentes.Add(ag);
                foreach (Agente ag in Model.GetAllSquad()) ListaSquad.Add(ag);
            }
        }

        private void RecruitmentButton_OnClick(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Reclutamiento));
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

        private void CuartelGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Agente Sel = e.ClickedItem as Agente;
            currentCuartelSel= Sel;
            nuevaAsignacion(Sel);
        }
        private void EscuadronGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Agente Sel = e.ClickedItem as Agente;
            currentEscuadronSel= Sel;
            nuevaAsignacion(Sel);
        }

        private void nuevaAsignacion(Agente agente)
        {
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
            string stringPath = agente.Imagen;
            Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            AgentImage.Source = imageBitmap;
            HealthPoints.Text = agente.Vida.ToString();
            MeleePoints.Text = agente.AtaqueMelee.ToString();
            DistPoints.Text = agente.AtaqueDistancia.ToString();
            MovPoints.Text = agente.CasillasMovimiento.ToString();

            if ((currentCuartelSel != null && Model.ListaSquad.Count < Constants.SQUAD_LIMIT) || currentEscuadronSel != null)
            BotonAsignar.IsEnabled = true;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (currentCuartelSel == null)
            {
                Model.ListaAgentes.Add(currentEscuadronSel);
                Model.ListaSquad.Remove(currentEscuadronSel);
                currentEscuadronSel = null;
            }
            else if (currentEscuadronSel == null && Model.ListaSquad.Count < Constants.SQUAD_LIMIT)
            {
                Model.ListaSquad.Add(currentCuartelSel);
                Model.ListaAgentes.Remove(currentCuartelSel);
                currentCuartelSel = null;
            }
            else if(currentEscuadronSel!=null&&currentCuartelSel!=null)
            {
                Model.ListaAgentes.Add(currentEscuadronSel);
                Model.ListaSquad.Add(currentCuartelSel);
                Model.ListaAgentes.Remove(currentCuartelSel);
                Model.ListaSquad.Remove(currentEscuadronSel);
                currentEscuadronSel = null;
                currentCuartelSel = null;
            }
            BotonAsignar.IsEnabled = false;
            updateListas();
        }

        private void EscuadronGrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Agente agente = e.Items[0] as Agente;
            e.Data.SetText(Model.ListaSquad.IndexOf(agente).ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void EscuadronGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void EscuadronGrid_Drop(object sender, DragEventArgs e)
        {

            var item = await e.DataView.GetTextAsync();
            int index = int.Parse(item);
            if (Model.ListaSquad.Count < Constants.SQUAD_LIMIT)
            {
                Model.ListaSquad.Add(Model.ListaAgentes[index]);
                Model.ListaAgentes.RemoveAt(index);
                updateListas();
            }
        }

        private void CuartelGrid_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            Agente agente = e.Items[0] as Agente;
            e.Data.SetText(Model.ListaAgentes.IndexOf(agente).ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }

        private void CuartelGrid_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        private async void CuartelGrid_Drop(object sender, DragEventArgs e)
        {
            
           var item = await e.DataView.GetTextAsync();
           int index =int.Parse(item);
           Model.ListaAgentes.Add(Model.ListaAgentes[index]);
           Model.ListaSquad.RemoveAt(index);
            updateListas();
        }

        private void updateListas()
        {
            CountText.Text = Model.ListaSquad.Count.ToString()+"/4";
            CuartelGrid.ItemsSource = null;
            EscuadronGrid.ItemsSource = null;
            CuartelGrid.ItemsSource = Model.ListaAgentes;
            EscuadronGrid.ItemsSource = Model.ListaSquad;
        }
    }
}
