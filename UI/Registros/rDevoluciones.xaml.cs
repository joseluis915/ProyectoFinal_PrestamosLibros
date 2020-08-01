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
    public partial class rDevoluciones : Window
    {
        private Devoluciones devoluciones = new Devoluciones();
        public rDevoluciones()
        {
            InitializeComponent();
            this.DataContext = devoluciones;
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
            this.DataContext = devoluciones;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.devoluciones = new Devoluciones();
            this.DataContext = devoluciones;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (DevolucionIdTextbox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Devoluciones encontrado = DevolucionesBLL.Buscar(devoluciones.DevolucionId);

            if (encontrado != null)
            {
                devoluciones = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Esta Devolución no fue encontrado.\n\nAsegúrese que existe o cree una nueva.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                //—————————————————————————————————————[ Limpiar y hacer focus en el Id]—————————————————————————————————————
                DevolucionIdTextbox.Clear();
                DevolucionIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Agregar Fila ]———————————————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            var filaDetalle = new DevolucionesDetalle
            {
                DevolucionId = this.devoluciones.DevolucionId,
                LibroId = Convert.ToInt32(LibroIdComboBox.SelectedValue.ToString()),
                //——————————————————————————————[ Nombre en el ComboBox ]——————————————————————————————
                libros = (Libros)LibroIdComboBox.SelectedItem,
                //—————————————————————————————————————————————————————————————————————————————————————
                LibrosDevueltos = Convert.ToSingle(LibrosDevueltosTextBox.Text),
                Dias = double.Parse(LibrosDevueltosTextBox.Text)
            };
            //filaDetalle.libros = (Libros)LibroIdComboBox.SelectedItem;
            //——————————————————————————————[Tiempo Total]——————————————————————————————
            devoluciones.Total += Convert.ToDouble(LibrosDevueltosTextBox.Text.ToString());
            //——————————————————————————————————————————————————————————————————————————
            this.devoluciones.Detalle.Add(filaDetalle);
            Cargar();

            LibroIdComboBox.SelectedIndex = -1;
            LibrosDevueltosTextBox.Text = "1";
            DiasTextBox.Clear();
        }
        //——————————————————————————————————————————————————————————————[ Remover Fila ]———————————————————————————————————————————————————————————————
        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(LibrosDevueltosTextBox.Text);
                if (DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count - 1)
                {
                    devoluciones.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    devoluciones.Total -= total;
                    Cargar();
                }
            }
            catch
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            LibrosDevueltosTextBox.Text = "1";
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //—————————————————————————————————[ Devolucion Id ]—————————————————————————————————
                if (DevolucionIdTextbox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Devolución Id) está vacío.\n\nAsigne un Id al Préstamo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    DevolucionIdTextbox.Text = "0";
                    DevolucionIdTextbox.Focus();
                    DevolucionIdTextbox.SelectAll();
                    return;
                }

                //—————————————————————————————————[ Estudiante Id ]—————————————————————————————————
                if (EstudianteIdComboBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Estudiante Id) está vacío.\n\nSelecione una Id de Estudiante.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EstudianteIdComboBox.Focus();
                    return;
                }

                var paso = DevolucionesBLL.Guardar(this.devoluciones);
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
                if (DevolucionesBLL.Eliminar(Utilidades.ToInt(DevolucionIdTextbox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ TextChanged ]———————————————————————————————————————————————————————————————
        //——————————————————————————————————————————[ Devolucion Id ]——————————————————————————————————————————
        private void PrestamoIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DevolucionIdTextbox.Text.Trim() != string.Empty)
                {
                    int.Parse(DevolucionIdTextbox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Préstamo Id) no es un número.\n\nPor favor, digite un número.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                DevolucionIdTextbox.Text = "0";
                DevolucionIdTextbox.Focus();
                DevolucionIdTextbox.SelectAll();
            }
        }
    }
}