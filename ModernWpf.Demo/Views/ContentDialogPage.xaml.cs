using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Demo.Views
{
    /// <summary>
    /// Interaction logic for ContentDialogPage.xaml
    /// </summary>
    public partial class ContentDialogPage : UserControl
    {
        public ContentDialogPage()
        {
            InitializeComponent();
        }
        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            ContentDialogExample dialog = new ContentDialogExample();
            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                DialogResult.Text = "User saved their work";
            }
            else if (result == ContentDialogResult.Secondary)
            {
                DialogResult.Text = "User did not save their work";
            }
            else
            {
                DialogResult.Text = "User cancelled the dialog";
            }
        }
    }
}
