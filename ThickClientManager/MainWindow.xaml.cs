using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThickClientManager.DataTransferObject;

namespace ThickClientManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _apiLink;
        private HttpClient _client;
        private readonly string _jsonMediaType = "application/json";

        private readonly string _baseTeaAddress = "/api/baseTea/";

        public MainWindow()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private async void BtnSaveApiLink_Click(object sender, RoutedEventArgs e)
        {
            _apiLink = TxtApiLink.Text;

            //Initialize HTTP Client
            _client.BaseAddress = new Uri(_apiLink);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_jsonMediaType));

            //load base tea
            try
            {
                LoadDataBaseTea();
            }
            catch (Exception)
            {
                MessageBox.Show("An error occured");
            } 
        }

        #region Load Data

        private async void LoadDataBaseTea()
        {
            HttpResponseMessage response = await _client.GetAsync(_baseTeaAddress);
            if (response.IsSuccessStatusCode)
            {
                //get data as Json string 
                string data = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var baseTeas = JSserializer.Deserialize<List<BaseTea>>(data);
                //bind to datatable
                DataBaseTea.ItemsSource = baseTeas;
            }
        }

        #endregion

        #region Base Tea

        private async void BtnAddBaseTea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var baseTea = new BaseTea()
                {
                    Name = TxtBaseTeaName.Text
                };
                var response = await _client.PostAsJsonAsync(_baseTeaAddress, baseTea);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base tea not added");
            } 
        }

        private void DataBaseTea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataBaseTea.SelectedItem != null && DataBaseTea.SelectedItem.GetType() == typeof (BaseTea))
            {
                BaseTea selectedBaseTea = (BaseTea)DataBaseTea.SelectedItem;
                TxtBaseTeaName.Text = selectedBaseTea.Name;
                LblBaseTeaId.Content = selectedBaseTea.Id;
            }
        }

        private async void BtnUpdateBaseTea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var baseTea = new BaseTea()
                {
                    Id = (int)LblBaseTeaId.Content,
                    Name = TxtBaseTeaName.Text
                };

                var response = await _client.PutAsJsonAsync(_baseTeaAddress + baseTea.Id, baseTea);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base tea not editted");
            } 
        }

        private async void BtnDeleteBaseTea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(_baseTeaAddress + LblBaseTeaId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Base tea not deleted");
            }
        }

        #endregion

    }
}
