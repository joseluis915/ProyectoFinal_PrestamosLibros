using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//Using agregados
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.UI.Registros
{
    public partial class rEditoriales : Window
    {
        private Editoriales editoriales = new Editoriales();
        public rEditoriales()
        {
            InitializeComponent();
            this.DataContext = editoriales;
            //—————————————————————————————————————[ ComboBox UsuarioId ]—————————————————————————————————————
            UsuarioIdComboBox.ItemsSource = UsuariosBLL.GetUsuarios();
            UsuarioIdComboBox.SelectedValuePath = "UsuarioId";
            UsuarioIdComboBox.DisplayMemberPath = "NombreUsuario";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = editoriales;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.editoriales = new Editoriales();
            this.DataContext = editoriales;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (EditorialIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Editoriales encontrado = EditorialesBLL.Buscar(Utilidades.ToInt(EditorialIdTextBox.Text));

            if (encontrado != null)
            {
                this.editoriales = encontrado;
                Cargar();
            }
            else
            {
                this.editoriales = new Editoriales();
                this.DataContext = this.editoriales;
                MessageBox.Show($"Esta Editorial no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                EditorialIdTextBox.SelectAll();
                EditorialIdTextBox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO ]———————————————————————————————————————————————————————

                //—————————————————————————————————[ Editorial Id]—————————————————————————————————
                if (EditorialIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Editorial Id) está vacío.\n\nAsigne un Id al Editorial.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EditorialIdTextBox.Text = "0";
                    EditorialIdTextBox.Focus();
                    EditorialIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Usuario Id ]—————————————————————————————————
                if (UsuarioIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Usuario Id) está vacío.\n\nPorfavor, Seleccione su Nombre de Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    UsuarioIdComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Descripcion ]—————————————————————————————————
                if (DescripcionTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Descripción) está vacío.\n\nDescriba la Editorial.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DescripcionTextBox.Clear();
                    DescripcionTextBox.Focus();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = EditorialesBLL.Guardar(editoriales);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (EditorialesBLL.Eliminar(Utilidades.ToInt(EditorialIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ Editorial Id ]——————————————————————————————————————————
        private void EditorialIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EditorialIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EditorialIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Editorial Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                EditorialIdTextBox.Text = "0";
                EditorialIdTextBox.Focus();
                EditorialIdTextBox.SelectAll();
            }
        }
    }
}