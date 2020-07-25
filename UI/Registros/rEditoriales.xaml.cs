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
    public partial class rEditoriales : Window
    {
        private Editoriales editoriales = new Editoriales();
        public rEditoriales()
        {
            InitializeComponent();
            this.DataContext = editoriales;
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
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Esta Editorial no fue encontrada.\n\nAsegurese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
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
                    MessageBox.Show("El Campo (Editorial Id) esta vacio.\n\nAsigne un Id al Editorial.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EditorialIdTextBox.Text = "0";
                    EditorialIdTextBox.Focus();
                    EditorialIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Descripcion ]—————————————————————————————————
                if (DescripcionTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Descripcion) esta vacio.\n\nDescriba la Editorial.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    DescripcionTextBox.Clear();
                    DescripcionTextBox.Focus();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = EditorialesBLL.Guardar(editoriales);
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
                if (EditorialesBLL.Eliminar(Utilidades.ToInt(EditorialIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show($"El valor digitado en el campo (Editorial Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                EditorialIdTextBox.Text = "0";
                EditorialIdTextBox.Focus();
                EditorialIdTextBox.SelectAll();
            }
        }
    }
}