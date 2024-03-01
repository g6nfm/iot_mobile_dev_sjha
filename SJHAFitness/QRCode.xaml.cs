using CommunityToolkit.Maui.Views;
using QRCoder;
using SJHAFitness.Views;

namespace SJHAFitness
{
    public partial class QRCode : ContentPage
    {
        public QRCode()
        {
            InitializeComponent();
        }

        private void OnGenerateClicked(object sender, EventArgs e)
        {
            Random rand = new Random();
            int randomNumber = rand.Next(10000000, 100000000);
            string randomNumberString = randomNumber.ToString();

            // Set the BindingContext of the page to the random number string
            this.BindingContext = randomNumberString;


            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(randomNumberString, QRCodeGenerator.ECCLevel.L);
            PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeBytes = qRCode.GetGraphic(20);
            QrCodeImage.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
        }
        private void MenuPopup(object sender, EventArgs e)
        {
            var popup = new MenuBarItems();

            this.ShowPopup(popup);
        }

    }
}
