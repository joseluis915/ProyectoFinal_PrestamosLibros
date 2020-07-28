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
    public partial class rLibros : Window
    {
        private Libros libros = new Libros();
        public rLibros()
        {
            InitializeComponent();
            this.DataContext = libros;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = libros;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.libros = new Libros();
            this.DataContext = libros;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (LibroIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Libros encontrado = LibrosBLL.Buscar(Utilidades.ToInt(LibroIdTextBox.Text));

            if (encontrado != null)
            {
                this.libros = encontrado;
                Cargar();
            }
            else
            {
                this.libros = new Libros();
                this.DataContext = this.libros;
                MessageBox.Show($"Esta Entrada de libro no fue encontrada.\n\nAsegurese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                LibroIdTextBox.Focus();
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
                //—————————————————————————————————[ Libro Id ]—————————————————————————————————
                if (LibroIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) esta vacio.\n\nAsigne un Id al Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    LibroIdTextBox.Text = "0";
                    LibroIdTextBox.Focus();
                    LibroIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Titulo ]—————————————————————————————————
                if (TituloTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Titulo) esta vacio.\n\nEscriba un de Titulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    TituloTextBox.Clear();
                    TituloTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ ISBN ]—————————————————————————————————
                if (ISBNTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (ISBN) esta vacio.\n\nEscriba un de ISBN.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ISBNTextBox.Clear();
                    ISBNTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) esta vacio.\n\nSeleccione una fecha.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Existencia ]—————————————————————————————————
                if (ExistenciaTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Existencia) esta vacio.\n\nEscriba la existencia altual del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ExistenciaTextBox.Text = "0";
                    ExistenciaTextBox.Focus();
                    ExistenciaTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = LibrosBLL.Guardar(libros);
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
                if (LibrosBLL.Eliminar(Utilidades.ToInt(LibroIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ LibroId ]——————————————————————————————————————————
        private void LibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (LibroIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(LibroIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Libro Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                LibroIdTextBox.Text = "0";
                LibroIdTextBox.Focus();
                LibroIdTextBox.SelectAll();
            }
        }

        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ExistenciaTextBox.Text != "0")
            {
                ExistenciaTextBox.Foreground = Brushes.Red;
            }
            else
            {
                ExistenciaTextBox.Foreground = Brushes.Green;
            }
        }
    }
}