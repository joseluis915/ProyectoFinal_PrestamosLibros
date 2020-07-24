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
    public partial class rEntradasLibros : Window
    {
        private EntradasLibros entradasLibros = new EntradasLibros();
        public rEntradasLibros()
        {
            InitializeComponent();
            this.DataContext = entradasLibros;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = entradasLibros;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.entradasLibros = new EntradasLibros();
            this.DataContext = entradasLibros;
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
            EntradasLibros encontrado = EntradasLibrosBLL.Buscar(entradasLibros.EntradaLibroId);

            if (encontrado != null)
            {
                entradasLibros = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Esta Entrada de libro no fue encontrada.\n\nAsegurese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                //—————————————————————————————————————[ Limpiar y hacer focus en el Id]—————————————————————————————————————
                LibroIdTextBox.Text = "";
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
                if (EntradaLibroIdTextbox.Text.Trim() == "")
                {
                    MessageBox.Show("El Campo (Libro Id) esta vacio.\n\nEscriba una Id de Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EntradaLibroIdTextbox.Clear();
                    EntradaLibroIdTextbox.Focus();
                    return;
                }

                if (LibroIdTextBox.Text.Trim() == "")
                {
                    MessageBox.Show("El Campo (Libro Id) esta vacio.\n\nEscriba una Id de Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    LibroIdTextBox.Clear();
                    LibroIdTextBox.Focus();
                    return;
                }
                if (FechaDatePicker.Text.Trim() == "")
                {
                    MessageBox.Show($"El Campo (Fecha) esta vacio.\n\nEscriba una cantidad.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }

                if (CantidadTextBox.Text.Trim() == "")
                {
                    MessageBox.Show($"El Campo (Cantidad) esta vacio.\n\nEscriba una cantidad.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Clear();
                    CantidadTextBox.Focus();
                    return;
                }

                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = EntradasLibrosBLL.Guardar(this.entradasLibros);
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
                if (EntradasLibrosBLL.Eliminar(Utilidades.ToInt(LibroIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————[ EntradaLibroId - TextChanged ]——————————————————————————————————————————
        private void EntradaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EntradaLibroIdTextbox.Text.Trim() != "")
                {
                    int.Parse(EntradaLibroIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                EntradaLibroIdTextbox.Clear();
                EntradaLibroIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————[ LibroId - TextChanged ]——————————————————————————————————————————
        private void LibroIdTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (LibroIdTextBox.Text.Trim() != "")
                {
                    int.Parse(LibroIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Libro Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                LibroIdTextBox.Clear();
                LibroIdTextBox.Focus();
            }
        }

        //——————————————————————————————————————————[ Cantidad - TextChanged ]——————————————————————————————————————————
        private void CantidadTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadTextBox.Text.Trim() != "")
                {
                    int.Parse(CantidadTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Clear();
                CantidadTextBox.Focus();
            }
        }
    }
}