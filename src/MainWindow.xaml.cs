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
                //
                // Optimize for your use case via https://scottsauber.com/2022/05/10/improving-azure-key-vault-performance-in-asp-net-core-by-up-to-10x/
                //
                var client = new SecretClient(new Uri(this.kvUri.Text), new DefaultAzureCredential(new DefaultAzureCredentialOptions
                {
                    ExcludeAzurePowerShellCredential = false,
                    ExcludeAzureCliCredential = true,
                    ExcludeEnvironmentCredential = true,
                    ExcludeInteractiveBrowserCredential = true,
                    ExcludeSharedTokenCacheCredential = true,
                    ExcludeVisualStudioCodeCredential = true,
                    ExcludeVisualStudioCredential = true,
                    ExcludeManagedIdentityCredential = true,
                }));
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
