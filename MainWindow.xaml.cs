using System.Windows;

namespace SamokatAppClean
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        private void btnClients_Click(object sender, RoutedEventArgs e) { new ClientsWindow().Show(); Close(); }
        private void btnCouriers_Click(object sender, RoutedEventArgs e) { new CouriersWindow().Show(); Close(); }
        private void btnTransport_Click(object sender, RoutedEventArgs e) { new TransportWindow().Show(); Close(); }
        private void btnOrders_Click(object sender, RoutedEventArgs e) { new OrdersWindow().Show(); Close(); }
    }
}