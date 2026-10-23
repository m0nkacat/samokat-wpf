using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class TransportEditDialog : Window
    {
        private SamokatEntities context;
        private transport editingTransport;
        private bool isEdit;
        public TransportEditDialog(SamokatEntities ctx, transport transportItem = null)
        {
            InitializeComponent();
            context = ctx;
            if (transportItem != null)
            {
                editingTransport = transportItem;
                isEdit = true;
                txtName.Text = editingTransport.transport_name;
                txtCapacity.Text = editingTransport.capacity_kg.ToString();
            }
        }
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text)) { txtError.Text = "Введите название"; return false; }
            if (!int.TryParse(txtCapacity.Text, out int cap) || cap <= 0) { txtError.Text = "Введите положительное целое число"; return false; }
            bool nameExists = context.transport.Any(t => t.transport_name == txtName.Text.Trim() && (!isEdit || t.id != editingTransport.id));
            if (nameExists) { txtError.Text = "Название уже существует"; return false; }
            txtError.Text = "";
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;
            try
            {
                if (!isEdit)
                    context.transport.Add(new transport { transport_name = txtName.Text.Trim(), capacity_kg = int.Parse(txtCapacity.Text.Trim()) });
                else
                {
                    editingTransport.transport_name = txtName.Text.Trim();
                    editingTransport.capacity_kg = int.Parse(txtCapacity.Text.Trim());
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