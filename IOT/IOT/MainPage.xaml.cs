using IOT.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IOT
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            var allPersons = await firebaseHelper.GetAllSirena();
            lstSirena.ItemsSource = allPersons;
        }

        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.AddSirena(Convert.ToInt32(txtIdSirena.Text), Convert.ToInt32(txtEstadoSirena.Text), Convert.ToInt32(txtTipoSonido.Text));
            txtEstadoSirena.Text = string.Empty;
            txtIdSirena.Text = string.Empty;
            txtTipoSonido.Text = string.Empty;
            await DisplayAlert("Success", "Person Added Successfully", "OK");
            var allSirena = await firebaseHelper.GetAllSirena();
            lstSirena.ItemsSource = allSirena;
        }

        private async void BtnRetrive_Clicked(object sender, EventArgs e)
        {
            var sirena = await firebaseHelper.GetSirena(Convert.ToInt32(txtIdSirena.Text));
            if (sirena != null)
            {
                txtEstadoSirena.Text = sirena.estado_Sirena.ToString();
                txtIdSirena.Text = sirena.id_Sirena.ToString();
                txtTipoSonido.Text = sirena.tipo_Sonido.ToString();
                await DisplayAlert("Success", "Person Retrive Successfully", "OK");

            }
            else
            {
                await DisplayAlert("Success", "No Person Available", "OK");
            }

        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.UpdateSirena(Convert.ToInt32(txtIdSirena.Text), Convert.ToInt32(txtEstadoSirena.Text), Convert.ToInt32(txtTipoSonido.Text));

            txtEstadoSirena.Text = string.Empty;
            txtIdSirena.Text = string.Empty;
            txtTipoSonido.Text = string.Empty;

            await DisplayAlert("Success", "Person Updated Successfully", "OK");
            var allSirena = await firebaseHelper.GetAllSirena();
            lstSirena.ItemsSource = allSirena;
        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            await firebaseHelper.DeleteSirena(Convert.ToInt32(txtIdSirena.Text));
            await DisplayAlert("Success", "Person Deleted Successfully", "OK");
            var allSirena = await firebaseHelper.GetAllSirena();
            lstSirena.ItemsSource = allSirena;
        }
    }
}
