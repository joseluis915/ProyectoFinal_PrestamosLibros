﻿using System;
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
            if (EntradaLibroIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            EntradasLibros encontrado = EntradasLibrosBLL.Buscar(Utilidades.ToInt(EntradaLibroIdTextBox.Text));

            if (encontrado != null)
            {
                this.entradasLibros = encontrado;
                Cargar();
            }
            else
            {
                this.entradasLibros = new EntradasLibros();
                this.DataContext = this.entradasLibros;
                MessageBox.Show($"Esta Entrada de Libro no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                EntradaLibroIdTextBox.SelectAll();
                EntradaLibroIdTextBox.Focus();
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
                //—————————————————————————————————[ EntradaLibro Id ]—————————————————————————————————
                if (EntradaLibroIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (EntradaLibro Id) está vacío.\n\nDebe asignar un Id a la Entrada del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EntradaLibroIdTextBox.Text = "0";
                    EntradaLibroIdTextBox.Focus();
                    EntradaLibroIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Usuario Id ]—————————————————————————————————
                if (UsuarioIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Usuario Id) está vacío.\n\nPorfavor, Seleccione su Nombre de Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    UsuarioIdComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Libro Id ]—————————————————————————————————
                if (LibroIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Libro Id) está vacío.\n\nAsigne un Id al Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    LibroIdComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Fecha ]—————————————————————————————————
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Salida del Libro.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Cantidad ]—————————————————————————————————
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) está vacío.\n\nEscriba la cantidad de Libros.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
               
                LibrosBLL.SumarSalidaLibros(Convert.ToInt32(LibroIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------

                var paso = EntradasLibrosBLL.Guardar(entradasLibros);
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
                if (EntradasLibrosBLL.Eliminar(Utilidades.ToInt(EntradaLibroIdTextBox.Text)))
                {
                    LibrosBLL.RestarSalidaLibros(Convert.ToInt32(LibroIdComboBox.SelectedValue), Convert.ToDouble(CantidadTextBox.Text)); //-----------------
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //——————————————————————————————————————————[ EntradaLibro Id]——————————————————————————————————————————
        private void EntradaLibroIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EntradaLibroIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EntradaLibroIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (EntradaLibro Id) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                EntradaLibroIdTextBox.Text = "0";
                EntradaLibroIdTextBox.Focus();
                EntradaLibroIdTextBox.SelectAll();
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
                MessageBox.Show($"El valor digitado en el campo (Cantidad) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadTextBox.Text = "0";
                CantidadTextBox.Focus();
                CantidadTextBox.SelectAll();
            }
        }
    }
}