using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class TransportWindow : Window
    {
        private SamokatEntities context = new SamokatEntities();
        public TransportWindow() { InitializeComponent(); LoadData(); }
        private void LoadData() => dgTransport.ItemsSource = context.transport.ToList();
        private void Search()
        {
            string term = txtSearch.Text.Trim();
            dgTransport.ItemsSource = string.IsNullOrEmpty(term)
                ? context.transport.ToList()
                : context.transport.Where(t => t.transport_name.Contains(term)).ToList();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e) => Search();
        private void dgTransport_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void btnAdd_Click(object sender, RoutedEventArgs e) { if (new TransportEditDialog(context).ShowDialog() == true) LoadData(); }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgTransport.SelectedItem as transport;
            if (selected == null) { MessageBox.Show("Выберите транспорт"); return; }
            if (new TransportEditDialog(context, selected).ShowDialog() == true) LoadData();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgTransport.SelectedItem as transport;
            if (selected == null) { MessageBox.Show("Выберите транспорт"); return; }
            if (context.couriers.Any(c => c.transport_id == selected.id))
                if (MessageBox.Show("Транспорт используется курьерами, удалить?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    return;
            context.transport.Remove(selected);
            context.SaveChanges();
            LoadData();
            txtStatus.Text = "Удалено";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) { new MainWindow().Show(); Close(); }
    }
}