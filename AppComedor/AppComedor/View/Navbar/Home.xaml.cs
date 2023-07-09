using AppComedor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using WebSocketSharp;
using WebSocketSharp.Net;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;

namespace AppComedor.View.Navbar
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public string name { get; set; }
        public string email { get; set; }
        public ImageSource image { get; set; }

        private WebSocket webSocket;
        public Home()
        {
            InitializeComponent();

            LoadCategories();

            ConnectWebSocket();
        }

        public Home(string NameValue, string EmailValue, ImageSource ImageValue)
        {
            InitializeComponent();
            name = NameValue;
            email = EmailValue;
            image = ImageValue;

            lblName.Text = name;
            lblEmail.Text = email;
            imgProfile.Source = image;

            // Resto del código...
            ConnectWebSocket();
            LoadCategories();
        }

        public async void LoadCategories()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://apiapp-production.up.railway.app/api/categoria");
            var categorias = JsonConvert.DeserializeObject<List<CategoriasAPI>>(response);

            stackLayout.Children.Clear();

            foreach (var categoria in categorias)
            {
                Button btnCategoria = new Button
                {
                    Text = categoria.nombre,
                    CornerRadius = 5,
                    WidthRequest = 120,
                    TextColor = Color.White,
                    TextTransform = TextTransform.None,
                    BackgroundColor = Color.FromHex("#1F86FF")

                };

                btnCategoria.Clicked += (sender, e) =>
                {
                    // Acciones a realizar al hacer clic en el botón de la categoría
                };

                // Agregar el botón a un contenedor, por ejemplo, un StackLayout
                stackLayout.Children.Add(btnCategoria);
            }
        }



        public void UpdateData(string NameValue, string EmailValue, ImageSource ImageValue)
        {
            lblName.Text = NameValue;
            lblEmail.Text = EmailValue;
            imgProfile.Source = ImageValue;
        }

        public class CategoriasAPI
        {
            public int id_categoria { get; set; }
            public string nombre { get; set; }

        }

        private void ConnectWebSocket()
        {
            webSocket = new WebSocket("wss://apiapp-production.up.railway.app/api/categoria");

            webSocket.OnOpen += (sender, e) =>
            {
                // WebSocket connection opened
            };

            webSocket.OnMessage += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // Handle received message
                }
            };

            webSocket.OnError += (sender, e) =>
            {
                // Handle WebSocket errors
            };

            webSocket.OnClose += (sender, e) =>
            {
                // WebSocket connection closed
            };

            webSocket.Connect();
        }

       
    }
}