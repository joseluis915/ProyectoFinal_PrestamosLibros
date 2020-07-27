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
    public partial class rPrestamos : Window
    {
        private Prestamos prestamos = new Prestamos();
        public rPrestamos()
        {
            InitializeComponent();
            this.DataContext = prestamos;
            //—————————————————————————————————————[ VALORES DEL ComboBox ]—————————————————————————————————————
            EstudianteIdComboBox.ItemsSource = EstudiantesBLL.GetEstudiantes();
            EstudianteIdComboBox.SelectedValuePath = "EstudianteId";
            EstudianteIdComboBox.DisplayMemberPath = "Nombres";
            //—————————————————————————————————————[ VALORES DEL ComboBox ]—————————————————————————————————————
            LibroIdComboBox.ItemsSource = LibrosBLL.GetLibros();
            LibroIdComboBox.SelectedValuePath = "LibroId";
            LibroIdComboBox.DisplayMemberPath = "Titulo";
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = prestamos;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.prestamos = new Prestamos();
            this.DataContext = prestamos;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (PrestamoIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Prestamos encontrado = PrestamosBLL.Buscar(prestamos.PrestamoId);

            if (encontrado != null)
            {
                prestamos = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este Prestamo no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                //—————————————————————————————————————[ Limpiar y hacer focus en el Id]—————————————————————————————————————
                PrestamoIdTextbox.Clear();
                PrestamoIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Agregar Fila ]———————————————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new PrestamosDetalle
            {
                PrestamosId = this.prestamos.PrestamoId,
                LibroId = Convert.ToInt32(LibroIdComboBox.SelectedValue.ToString()),
                CantidadLibro = Convert.ToSingle(CantidadLibroTextBox.Text)
            };
            //——————————————————————————————[ Nombre en el ComboBox ]——————————————————————————————
            //filaDetalle.libros = (Libros)LibroIdComboBox.SelectedItem;
            //——————————————————————————————[Tiempo Total]——————————————————————————————
            prestamos.LibrosTotal += Convert.ToDouble(CantidadLibroTextBox.Text.ToString());
            //——————————————————————————————————————————————————————————————————————————
            this.prestamos.Detalle.Add(filaDetalle);
            Cargar();

            LibroIdComboBox.SelectedIndex = -1;
            CantidadLibroTextBox.Clear();
        }
        //——————————————————————————————————————————————————————————————[ Remover Fila ]———————————————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
            {
                prestamos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                Cargar();
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

                //—————————————————————————————————[ Prestamo Id ]—————————————————————————————————
                if (PrestamoIdTextbox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Prestamo Id) esta vacio.\n\nAsigne un Id al Prestamo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    PrestamoIdTextbox.Text = "0";
                    PrestamoIdTextbox.Focus();
                    PrestamoIdTextbox.SelectAll();
                    return;
                }

                //—————————————————————————————————[ Estudiante Id ]—————————————————————————————————
                if (EstudianteIdComboBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Estudiante Id) esta vacio.\n\nSelecione una Id de Estudiante.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EstudianteIdComboBox.Focus();
                    return;
                }

                var paso = PrestamosBLL.Guardar(this.prestamos);
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
                if (PrestamosBLL.Eliminar(Utilidades.ToInt(PrestamoIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ TextChanged ]———————————————————————————————————————————————————————————————
        //——————————————————————————————————————————[ Prestamo Id ]——————————————————————————————————————————
        private void PrestamoIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (PrestamoIdTextbox.Text.Trim() != string.Empty)
                {
                    int.Parse(PrestamoIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Prestamo Id) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                PrestamoIdTextbox.Text = "0";
                PrestamoIdTextbox.Focus();
                PrestamoIdTextbox.SelectAll();
            }
        }
        //——————————————————————————————————————————[ Cantidad Libro ]——————————————————————————————————————————
        private void CantidadLibroTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CantidadLibroTextBox.Text.Trim() != string.Empty)
                {
                    double.Parse(CantidadLibroTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Cantidad Libros) no es un numero.\n\nPorfavor, digite un numero.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                CantidadLibroTextBox.Text = "0";
                CantidadLibroTextBox.Focus();
                CantidadLibroTextBox.SelectAll();
            }
        }
    }
}