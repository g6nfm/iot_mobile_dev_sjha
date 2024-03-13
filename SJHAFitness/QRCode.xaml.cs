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

            int userId = App.CurrentUser.UserID;
            string userIdString = userId.ToString();

            this.BindingContext = userIdString;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(userIdString, QRCodeGenerator.ECCLevel.L);

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
