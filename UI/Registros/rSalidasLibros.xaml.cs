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
            //—————————————————————————————————————[ VALORES DEL ComboBox ]—————————————————————————————————————
            LibroIdComboBox.ItemsSource = LibrosBLL.GetLibros();
            LibroIdComboBox.SelectedValuePath = "LibroId";
            LibroIdComboBox.DisplayMemberPath = "Titulo";
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
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Esta Entrada de libro no fue encontrada.\n\nAsegurese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
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
                    MessageBox.Show("El Campo (SalidaLibro Id) esta vacio.\n\nDebe asignar un Id a la Salida del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    SalidaLibroIdTextBox.Text = "0";
                    SalidaLibroIdTextBox.Focus();
                    SalidaLibroIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Libro Id ]—————————————————————————————————
                if (LibroIdComboBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) esta vacio.\n\nAsigne un Id al Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    LibroIdComboBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) esta vacio.\n\nSeleccione una fecha para la Entrada del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Cantidad ]—————————————————————————————————
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) esta vacio.\n\nEscriba la cantidad de Libros.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = SalidasLibrosBLL.Guardar(salidasLibros);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (SalidasLibrosBLL.Eliminar(Utilidades.ToInt(SalidaLibroIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}
