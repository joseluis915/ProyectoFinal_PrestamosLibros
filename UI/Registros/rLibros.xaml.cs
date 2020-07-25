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
            if (LibroIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Libros encontrado = LibrosBLL.Buscar(Utilidades.ToInt(LibroIdTextbox.Text));

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
                LibroIdTextbox.Focus();
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
                if (LibroIdTextbox.Text.Trim() == "")
                {
                    MessageBox.Show("El Campo (Libro Id) esta vacio.\n\nEscriba una Id de Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    LibroIdTextbox.Clear();
                    LibroIdTextbox.Focus();
                    return;
                }

                if (TituloTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("El Campo (Titulo) esta vacio.\n\nEscriba un de Titulo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    TituloTextBox.Clear();
                    TituloTextBox.Focus();
                    return;
                }

                if (ISBNTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("El Campo (ISBN) esta vacio.\n\nEscriba un de ISBN.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ISBNTextBox.Clear();
                    ISBNTextBox.Focus();
                    return;
                }

                if (FechaDatePicker.Text.Trim() == "")
                {
                    MessageBox.Show($"El Campo (Fecha) esta vacio.\n\nSeleccione una fecha.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }

                if (ExistenciaTextBox.Text.Trim() == "")
                {
                    MessageBox.Show($"El Campo (Existencia) esta vacio.\n\nEscriba la existencia actual.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ExistenciaTextBox.Clear();
                    ExistenciaTextBox.Focus();
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
                if (LibrosBLL.Eliminar(Utilidades.ToInt(LibroIdTextbox.Text)))
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
                if (LibroIdTextbox.Text.Trim() != "")
                {
                    int.Parse(LibroIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Libro Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                LibroIdTextbox.Clear();
                LibroIdTextbox.Focus();
            }
        }

        //——————————————————————————————————————————[ Cantidad ]——————————————————————————————————————————
        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ExistenciaTextBox.Text.Trim() != "")
                {
                    int.Parse(ExistenciaTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Existencia) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ExistenciaTextBox.Clear();
                ExistenciaTextBox.Focus();
            }
        }
    }
}