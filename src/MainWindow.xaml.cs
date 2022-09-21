using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Windows;

namespace KeyVaultSearch
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            this.kvSecretValue.Text = "loading...";

            try
            {
                var client = new SecretClient(new Uri(this.kvUri.Text), new DefaultAzureCredential());
                KeyVaultSecret theSecret = await client.GetSecretAsync(this.kvSecret.Text);
                this.kvSecretValue.Text = theSecret.Value;
            }
            catch (Exception ex)
            {
                this.kvSecretValue.Text = ex.Message;
            }
        }
    }
}
