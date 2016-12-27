using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AzureDataServices
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            var result = dlg.ShowDialog();

            if (result == true)
            {
                textPath.Text = dlg.FileName;
            }
        }

        private async void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            var creds = new StorageCredentials(ConfigurationManager.AppSettings["StorageName"], ConfigurationManager.AppSettings["StorageKey"]);
            var acct = new CloudStorageAccount(creds, true);
            CloudBlobClient client = acct.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("testcontainer");

            ICloudBlob blob = container.GetBlockBlobReference(Path.GetFileName(textPath.Text));
            using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(textPath.Text)))
            {
                 await blob.UploadFromStreamAsync(stream);
            }
            MessageBox.Show("File Uploaded");
        }

        private void refreshFileList()
        {

        }
    }
}