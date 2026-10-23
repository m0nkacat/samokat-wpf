using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SamokatAppClean
{
    public partial class OrderEditDialog : Window
    {
        private SamokatEntities context;
        private orders editingOrder;
        private bool isEdit;
        public OrderEditDialog(SamokatEntities ctx, orders order = null)
        {
            InitializeComponent();
            context = ctx;
            cbClient.ItemsSource = context.clients.ToList();
            cbCourier.ItemsSource = context.couriers.ToList();
            if (order != null)
            {
                editingOrder = order;
                isEdit = true;
                dpOrderDate.SelectedDate = editingOrder.order_date;
                cbClient.SelectedValue = editingOrder.client_id;
                cbCourier.SelectedValue = editingOrder.courier_id;
                txtDeliveryAddress.Text = editingOrder.delivery_address;
                foreach (ComboBoxItem item in cbStatus.Items)
                    if (item.Content.ToString() == editingOrder.status) cbStatus.SelectedItem = item;
                txtWeight.Text = editingOrder.total_weight_kg.ToString();
            }
            else dpOrderDate.SelectedDate = DateTime.Today;
        }
        private bool Validate()
        {
            if (dpOrderDate.SelectedDate == null) { txtError.Text = "Выберите дату"; return false; }
            if (cbClient.SelectedValue == null) { txtError.Text = "Выберите клиента"; return false; }
            if (cbCourier.SelectedValue == null) { txtError.Text = "Выберите курьера"; return false; }
            if (string.IsNullOrWhiteSpace(txtDeliveryAddress.Text)) { txtError.Text = "Введите адрес"; return false; }
            if (cbStatus.SelectedItem == null) { txtError.Text = "Выберите статус"; return false; }
            if (!decimal.TryParse(txtWeight.Text, out decimal w) || w <= 0) { txtError.Text = "Вес должен быть > 0"; return false; }
            txtError.Text = "";
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;
            try
            {
                if (!isEdit)
                {
                    orders newOrder = new orders
                    {
                        order_date = dpOrderDate.SelectedDate.Value,
                        client_id = (int)cbClient.SelectedValue,
                        courier_id = (int)cbCourier.SelectedValue,
                        delivery_address = txtDeliveryAddress.Text.Trim(),
                        status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString(),
                        total_weight_kg = decimal.Parse(txtWeight.Text.Trim())
                    };
                    context.orders.Add(newOrder);
                }
                else
                {
                    editingOrder.order_date = dpOrderDate.SelectedDate.Value;
                    editingOrder.client_id = (int)cbClient.SelectedValue;
                    editingOrder.courier_id = (int)cbCourier.SelectedValue;
                    editingOrder.delivery_address = txtDeliveryAddress.Text.Trim();
                    editingOrder.status = ((ComboBoxItem)cbStatus.SelectedItem).Content.ToString();
                    editingOrder.total_weight_kg = decimal.Parse(txtWeight.Text.Trim());
                }
                context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) { DialogResult = false; Close(); }
    }
}