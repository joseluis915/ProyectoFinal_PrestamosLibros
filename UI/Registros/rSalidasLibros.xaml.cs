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
    public partial class rSalidasLibros : Window
    {
        private SalidasLibros salidasLibros = new SalidasLibros();
        public rSalidasLibros()
        {
            InitializeComponent();
            this.DataContext = salidasLibros;
            //—————————————————————————————————————[ ComboBox LibroId ]—————————————————————————————————————
            LibroIdComboBox.ItemsSource = LibrosBLL.GetLibros();
            LibroIdComboBox.SelectedValuePath = "LibroId";
            LibroIdComboBox.DisplayMemberPath = "Titulo";
            //—————————————————————————————————————[ ComboBox UsuarioId ]—————————————————————————————————————
            UsuarioIdComboBox.ItemsSource = UsuariosBLL.GetUsuarios();
            UsuarioIdComboBox.SelectedValuePath = "UsuarioId";
            UsuarioIdComboBox.DisplayMemberPath = "NombreUsuario";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = salidasLibros;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.salidasLibros = new SalidasLibros();
            this.DataContext = salidasLibros;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (SalidaLibroIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            SalidasLibros encontrado = SalidasLibrosBLL.Buscar(Utilidades.ToInt(SalidaLibroIdTextBox.Text));

            if (encontrado != null)
            {
                this.salidasLibros = encontrado;
                Cargar();
            }
            else
            {
                this.salidasLibros = new SalidasLibros();
                this.DataContext = this.salidasLibros;
                MessageBox.Show($"Esta Salida de Libro no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                SalidaLibroIdTextBox.SelectAll();
                SalidaLibroIdTextBox.Focus();
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
                //—————————————————————————————————[ SalidaLibro Id ]—————————————————————————————————
                if (SalidaLibroIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (SalidaLibro Id) está vacío.\n\nDebe asignar un Id a la Salida del Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SalidaLibroIdTextBox.Text = "0";
                    SalidaLibroIdTextBox.Focus();
                    SalidaLibroIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Libro Id ]—————————————————————————————————
                if (LibroIdComboBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) está vacío.\n\nAsigne un Id al Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LibroIdComboBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Entrada del Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Cantidad ]—————————————————————————————————
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) está vacío.\n\nEscriba la cantidad de Libros.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                
                LibrosBLL.RestarSalidaLibros(Convert.ToInt32(LibroIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------

                var paso = SalidasLibrosBLL.Guardar(salidasLibros);
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
                if (SalidasLibrosBLL.Eliminar(Utilidades.ToInt(SalidaLibroIdTextBox.Text)))
                {
                    LibrosBLL.SumarSalidaLibros(Convert.ToInt32(LibroIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ SalidaLibro Id]——————————————————————————————————————————
        private void SalidaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SalidaLibroIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(SalidaLibroIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                SalidaLibroIdTextBox.Text = "0";
                SalidaLibroIdTextBox.Focus();
                SalidaLibroIdTextBox.SelectAll();
            }
        }

        //——————————————————————————————————————————[ Libro Id ]——————————————————————————————————————————

        //——————————————————————————————————————————[ Cantidad ]——————————————————————————————————————————
        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(CantidadTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}
