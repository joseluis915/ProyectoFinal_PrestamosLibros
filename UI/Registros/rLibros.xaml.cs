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
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Esta Entrada de libro no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("El Campo (Libro Id) está vacío.\n\nAsigne un Id al Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    LibroIdTextBox.Text = "0";
                    LibroIdTextBox.Focus();
                    LibroIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Titulo ]—————————————————————————————————
                if (TituloTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Titulo) está vacío.\n\nEscriba un de Titulo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TituloTextBox.Clear();
                    TituloTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ ISBN ]—————————————————————————————————
                if (ISBNTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (ISBN) está vacío.\n\nEscriba un de ISBN.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ISBNTextBox.Clear();
                    ISBNTextBox.Focus();
                    return;
                }
                if (ISBNTextBox.Text.Length != 10 && ISBNTextBox.Text.Length != 13)
                {
                    MessageBox.Show($"El ISBN ({ISBNTextBox.Text}) no es válido.\n\nEl ISBN debe tener 10 o 13 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ISBNTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Existencia ]—————————————————————————————————
                if (ExistenciaTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Existencia) está vacío.\n\nEscriba la existencia actual del Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show($"El valor digitado en el campo (Libro Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                LibroIdTextBox.Text = "0";
                LibroIdTextBox.Focus();
                LibroIdTextBox.SelectAll();
            }
        }
        //——————————————————————————————————————————[ ISBN ]——————————————————————————————————————————
        private void ISBNTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ISBNTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(ISBNTextBox.Text);
                }

                if (ISBNTextBox.Text.Length == 10 || ISBNTextBox.Text.Length == 13)
                {
                    ISBNTextBox.Foreground = Brushes.Black;
                }
                else
                {
                    ISBNTextBox.Foreground = Brushes.Red;
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (ISBN) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                ISBNTextBox.Text = "0";
                ISBNTextBox.Focus();
                ISBNTextBox.SelectAll();
            }   
        }
        //——————————————————————————————————————————[ Existencia ]——————————————————————————————————————————
        private void ExistenciaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            double Existencia;

            if (double.TryParse(ExistenciaTextBox.Text, out Existencia))
            {
                if (Existencia < 0)
                {
                    ExistenciaTextBox.Foreground = Brushes.Red;
                }
                if (Existencia == 0)
                {
                    ExistenciaTextBox.Foreground = Brushes.Black;
                }
                else
                {
                    ExistenciaTextBox.Foreground = Brushes.Green;
                }
            }  
        }
    }
}