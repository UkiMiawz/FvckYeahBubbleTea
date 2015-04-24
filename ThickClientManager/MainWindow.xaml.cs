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
using ThickClientManager.Global;

namespace ThickClientManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _apiLink;
        private readonly HttpClient _client;

        public MainWindow()
        {
            InitializeComponent();
            _client = new HttpClient();
            TabDataAll.IsEnabled = false;
        }

        private async void BtnSaveApiLink_Click(object sender, RoutedEventArgs e)
        {
            var tempApiLink = TxtApiLink.Text;
            Uri uriResult;

            if (!(Uri.TryCreate(tempApiLink, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp))
            {
                MessageBox.Show(Constants.UriError);
                return;
            }

            _apiLink = String.Format("{0}://{1}",uriResult.Scheme, uriResult.Authority);

            //Initialize HTTP Client
            _client.BaseAddress = new Uri(_apiLink);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.JsonMediaType));

            try
            {
                LoadDataBaseTea();
                LoadDataCustomer();
                LoadDataFlavor();
                LoadDataTeaSize();
                LoadDataTopping();

                TabDataAll.IsEnabled = true;
                TxtApiLink.IsEnabled = false;
                BtnSaveApiLink.IsEnabled = false;
            }
            catch (Exception)
            {
                MessageBox.Show(Constants.GeneralError);
            } 
        }

        #region Load Data

        private async void LoadDataBaseTea()
        {
            HttpResponseMessage response = await _client.GetAsync(Constants.BaseTeaAddress);
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

        private async void LoadDataCustomer()
        {
            HttpResponseMessage response = await _client.GetAsync(Constants.CustomerAddress);
            if (response.IsSuccessStatusCode)
            {
                //get data as Json string 
                string data = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var customers = JSserializer.Deserialize<List<Customer>>(data);
                //bind to datatable
                DataCustomer.ItemsSource = customers;
            }
        }

        private async void LoadDataFlavor()
        {
            HttpResponseMessage response = await _client.GetAsync(Constants.FlavorAddress);
            if (response.IsSuccessStatusCode)
            {
                //get data as Json string 
                string data = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var flavors = JSserializer.Deserialize<List<Flavor>>(data);
                //bind to datatable
                DataFlavor.ItemsSource = flavors;
            }
        }

        private async void LoadDataTeaSize()
        {
            HttpResponseMessage response = await _client.GetAsync(Constants.TeaSizeAddress);
            if (response.IsSuccessStatusCode)
            {
                //get data as Json string 
                string data = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var sizes = JSserializer.Deserialize<List<TeaSize>>(data);
                //bind to datatable
                DataTeaSize.ItemsSource = sizes;
            }
        }

        private async void LoadDataTopping()
        {
            HttpResponseMessage response = await _client.GetAsync(Constants.ToppingAddress);
            if (response.IsSuccessStatusCode)
            {
                //get data as Json string 
                string data = await response.Content.ReadAsStringAsync();
                JavaScriptSerializer JSserializer = new JavaScriptSerializer();
                var toppings = JSserializer.Deserialize<List<Topping>>(data);
                //bind to datatable
                DataTopping.ItemsSource = toppings;
            }
        }

        #endregion

        #region Base Tea

        private bool BaseTeaIsEmpty()
        {
            if (String.IsNullOrWhiteSpace(TxtBaseTeaName.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }
            return false;
        }

        private async void BtnAddBaseTea_Click(object sender, RoutedEventArgs e)
        {
            if (BaseTeaIsEmpty()) return;

            try
            {
                var baseTea = new BaseTea()
                {
                    Name = TxtBaseTeaName.Text
                };
                var response = await _client.PostAsJsonAsync(Constants.BaseTeaAddress, baseTea);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
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
            if(BaseTeaIsEmpty()) return;

            try
            {
                var baseTea = new BaseTea()
                {
                    Id = (int)LblBaseTeaId.Content,
                    Name = TxtBaseTeaName.Text
                };

                var response = await _client.PutAsJsonAsync(Constants.BaseTeaAddress + baseTea.Id, baseTea);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            } 
        }

        private async void BtnDeleteBaseTea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(Constants.BaseTeaAddress + LblBaseTeaId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataBaseTea();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        #endregion

        #region Flavor

        private bool FlavorIsEmpty()
        {
            if (String.IsNullOrWhiteSpace(TxtFlavorName.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }

            return false;
        }

        private async void BtnAddFlavor_Click(object sender, RoutedEventArgs e)
        {
            if (FlavorIsEmpty()) return;

            try
            {
                var flavor = new Flavor()
                {
                    Name = TxtFlavorName.Text
                };
                var response = await _client.PostAsJsonAsync(Constants.FlavorAddress, flavor);
                response.EnsureSuccessStatusCode();
                LoadDataFlavor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private void DataFlavor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataFlavor.SelectedItem != null && DataFlavor.SelectedItem.GetType() == typeof(Flavor))
            {
                Flavor selectedFlavor = (Flavor)DataFlavor.SelectedItem;
                TxtFlavorName.Text = selectedFlavor.Name;
                LblFlavorId.Content = selectedFlavor.Id;
            }
        }

        private async void BtnUpdateFlavor_Click(object sender, RoutedEventArgs e)
        {
            if (FlavorIsEmpty()) return;

            try
            {
                var flavor = new Flavor()
                {
                    Id = (int)LblFlavorId.Content,
                    Name = TxtFlavorName.Text
                };

                var response = await _client.PutAsJsonAsync(Constants.FlavorAddress + flavor.Id, flavor);
                response.EnsureSuccessStatusCode();
                LoadDataFlavor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private async void BtnDeleteFlavor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(Constants.FlavorAddress + LblFlavorId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataFlavor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        #endregion

        #region Tea Size

        private bool TeaSizeIsEmpty()
        {
            int output = 0;

            if (String.IsNullOrWhiteSpace(TxtTeaSizeName.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }
            else if (String.IsNullOrWhiteSpace(TxtTeaSizePrice.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }
            else if (!int.TryParse(TxtTeaSizePrice.Text, out output))
            {
                MessageBox.Show(Constants.PriceError);
                return true;
            }
            return false;
        }

        private async void BtnAddTeaSize_Click(object sender, RoutedEventArgs e)
        {
            if (TeaSizeIsEmpty()) return;

            try
            {
                var teaSize = new TeaSize()
                {
                    Name = TxtTeaSizeName.Text,
                    Price = int.Parse(TxtTeaSizePrice.Text)
                };
                var response = await _client.PostAsJsonAsync(Constants.TeaSizeAddress, teaSize);
                response.EnsureSuccessStatusCode();
                LoadDataTeaSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private void DataTeaSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataTeaSize.SelectedItem != null && DataTeaSize.SelectedItem.GetType() == typeof(TeaSize))
            {
                TeaSize selectedTeaSize = (TeaSize)DataTeaSize.SelectedItem;
                TxtTeaSizeName.Text = selectedTeaSize.Name;
                TxtTeaSizePrice.Text = selectedTeaSize.Price.ToString();
                LblTeaSizeId.Content = selectedTeaSize.Id;
            }
        }

        private async void BtnUpdateTeaSize_Click(object sender, RoutedEventArgs e)
        {
            if (TeaSizeIsEmpty()) return;

            try
            {
                var teaSize = new TeaSize()
                {
                    Id = (int)LblTeaSizeId.Content,
                    Price = int.Parse(TxtTeaSizePrice.Text),
                    Name = TxtTeaSizeName.Text
                };

                var response = await _client.PutAsJsonAsync(Constants.TeaSizeAddress + teaSize.Id, teaSize);
                response.EnsureSuccessStatusCode();
                LoadDataTeaSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private async void BtnDeleteTeaSize_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(Constants.TeaSizeAddress + LblTeaSizeId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataTeaSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        #endregion

        #region Topping

        private bool ToppingIsEmpty()
        {
            int output = 0;

            if (String.IsNullOrWhiteSpace(TxtToppingName.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }
            else if (String.IsNullOrWhiteSpace(TxtToppingPrice.Text))
            {
                MessageBox.Show(Constants.NameError);
                return true;
            }
            else if (!int.TryParse(TxtToppingPrice.Text, out output))
            {
                MessageBox.Show(Constants.PriceError);
                return true;
            }
            return false;
        }

        private async void BtnAddTopping_Click(object sender, RoutedEventArgs e)
        {
            if (ToppingIsEmpty()) return;

            try
            {
                var topping = new Topping()
                {
                    Name = TxtToppingName.Text,
                    Price = int.Parse(TxtToppingPrice.Text)
                };
                var response = await _client.PostAsJsonAsync(Constants.ToppingAddress, topping);
                response.EnsureSuccessStatusCode();
                LoadDataTopping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private void DataTopping_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataTopping.SelectedItem != null && DataTopping.SelectedItem.GetType() == typeof(Topping))
            {
                Topping selectedTopping = (Topping)DataTopping.SelectedItem;
                TxtToppingName.Text = selectedTopping.Name;
                TxtToppingPrice.Text = selectedTopping.Price.ToString();
                LblToppingId.Content = selectedTopping.Id;
            }
        }

        private async void BtnUpdateTopping_Click(object sender, RoutedEventArgs e)
        {
            if (ToppingIsEmpty()) return;

            try
            {
                var topping = new Topping()
                {
                    Id = (int)LblToppingId.Content,
                    Price = int.Parse(TxtToppingPrice.Text),
                    Name = TxtToppingName.Text
                };

                var response = await _client.PutAsJsonAsync(Constants.ToppingAddress + topping.Id, topping);
                response.EnsureSuccessStatusCode();
                LoadDataTopping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private async void BtnDeleteTopping_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(Constants.ToppingAddress + LblToppingId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataTopping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        #endregion

        #region Customer

        private async void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = new Customer()
                {
                    FirstName = TxtCustomerFirstName.Text,
                    LastName = TxtCustomerLastName.Text,
                    DateOfBirth = TxtCustomerDate.SelectedDate ?? DateTime.MinValue
                };
                var response = await _client.PostAsJsonAsync(Constants.CustomerAddress, customer);
                response.EnsureSuccessStatusCode();
                LoadDataCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private void DataCustomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataTopping.SelectedItem != null && DataTopping.SelectedItem.GetType() == typeof(Topping))
            {
                Customer selectedCustomer = (Customer)DataCustomer.SelectedItem;
                TxtCustomerFirstName.Text = selectedCustomer.FirstName;
                TxtCustomerLastName.Text = selectedCustomer.LastName;
                TxtCustomerDate.DisplayDate = selectedCustomer.DateOfBirth;
            }
        }

        private async void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var customer = new Customer()
                {
                    Id = (int)LblCustomerId.Content,
                    FirstName = TxtCustomerFirstName.Text,
                    LastName = TxtCustomerLastName.Text,
                    DateOfBirth = TxtCustomerDate.SelectedDate ?? DateTime.MinValue
                };

                var response = await _client.PutAsJsonAsync(Constants.ToppingAddress + customer.Id, customer);
                response.EnsureSuccessStatusCode();
                LoadDataCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        private async void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = await _client.DeleteAsync(Constants.CustomerAddress + LblToppingId.Content);
                response.EnsureSuccessStatusCode();
                LoadDataCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Constants.GeneralError);
            }
        }

        #endregion

    }
}
