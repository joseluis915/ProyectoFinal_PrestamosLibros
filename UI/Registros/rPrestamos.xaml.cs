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
            //—————————————————————————————————————[ ComboBox EstudianteId ]—————————————————————————————————————
            EstudianteIdComboBox.ItemsSource = EstudiantesBLL.GetEstudiantes();
            EstudianteIdComboBox.SelectedValuePath = "EstudianteId";
            EstudianteIdComboBox.DisplayMemberPath = "Nombres";
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
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Este Préstamo no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                PrestamoIdTextbox.SelectAll();
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
                //——————————————————————————————[ Nombre en el ComboBox ]——————————————————————————————
                libros = (Libros)LibroIdComboBox.SelectedItem,
                //—————————————————————————————————————————————————————————————————————————————————————
                CantidadLibro = Convert.ToSingle(CantidadLibroTextBox.Text)
            };
            //——————————————————————————————[Prestamos Total]——————————————————————————————
            prestamos.LibrosTotal += Convert.ToDouble(CantidadLibroTextBox.Text.ToString());
            //——————————————————————————————————————————————————————————————————————————
            this.prestamos.Detalle.Add(filaDetalle);
            Cargar();

            LibroIdComboBox.SelectedIndex = -1;
            CantidadLibroTextBox.Text = "1";
        }
        //——————————————————————————————————————————————————————————————[ Remover Fila ]———————————————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(CantidadLibroTextBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    prestamos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    prestamos.LibrosTotal -= total;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            CantidadLibroTextBox.Text = "1";
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
                    MessageBox.Show("El Campo (Préstamo Id) está vacío.\n\nAsigne un Id al Préstamo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    PrestamoIdTextbox.Text = "0";
                    PrestamoIdTextbox.Focus();
                    PrestamoIdTextbox.SelectAll();
                    return;
                }

                //—————————————————————————————————[ Estudiante Id ]—————————————————————————————————
                if (EstudianteIdComboBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Estudiante Id) está vacío.\n\nSelecione una Id de Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EstudianteIdComboBox.Focus();
                    return;
                }

                var paso = PrestamosBLL.Guardar(this.prestamos);
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
                if (PrestamosBLL.Eliminar(Utilidades.ToInt(PrestamoIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
                MessageBox.Show($"El valor digitado en el campo (Préstamo Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrestamoIdTextbox.Text = "0";
                PrestamoIdTextbox.Focus();
                PrestamoIdTextbox.SelectAll();
            }
        }
    }
}