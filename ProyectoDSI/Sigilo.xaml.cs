using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class Sigilo : Page
    {
        public ObservableCollection<Agente> ListaSquad { get; } = new ObservableCollection<Agente>();

        public Sigilo()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ListaSquad != null)
            {
                foreach (Agente ag in Model.GetAllSquad()) ListaSquad.Add(ag);
            }
        }

        // ESTOS MÉTODOS SE DEBEN BORRAR O RENOMBRAR, SON SOLO PARA PROBAR EL CAMBIO DE ESCENA
        private void ChangeScene(object sender, RoutedEventArgs e)
        {
            // Use Frame.Navigate to go to the next page.
            Frame.Navigate(typeof(Enfrentamiento));
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void AgentsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Agente Sel = e.ClickedItem as Agente;
            SelectedName.Text = Sel.Nombre;
            LifeSelected.Value = Sel.Vida;
            SelectedLifeValue.Text = Sel.Vida.ToString();
            string stringPath = Sel.Imagen;
            Uri imageUri = new Uri(stringPath, UriKind.RelativeOrAbsolute);
            BitmapImage imageBitmap = new BitmapImage(imageUri);
            Image myImage = new Image();
            SelectedPortrait.Source = imageBitmap;
        }
    }
}
