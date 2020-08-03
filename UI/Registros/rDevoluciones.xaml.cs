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
                MessageBox.Show($"Esta Devolución no fue encontrado.\n\nAsegúrese que existe o cree una nueva.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                DevolucionIdTextbox.SelectAll();
                DevolucionIdTextbox.Focus();
            }
        }
        //——————————————————————————————————————————————————————————————[ Agregar Fila ]———————————————————————————————————————————————————————————————
        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            //—————————————————————————————————[ Libro Id ]—————————————————————————————————
            if (LibroIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Libro Id) está vacío.\n\nPorfavor, Seleccione el Libro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                LibroIdComboBox.IsDropDownOpen = true;
                return;
            }
            if (DiasTextBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Dias Transcurridos) está vacio.\n\nEscriba cuantos dias transcurrieron.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DiasTextBox.Focus();
                return;
            }

            var filaDetalle = new DevolucionesDetalle
            {
                DevolucionId = this.devoluciones.DevolucionId,
                LibroId = Convert.ToInt32(LibroIdComboBox.SelectedValue.ToString()),
                //——————————————————————————————[ Nombre en el ComboBox ]——————————————————————————————
                libros = (Libros)LibroIdComboBox.SelectedItem,
                //—————————————————————————————————————————————————————————————————————————————————————
                LibrosDevueltos = Convert.ToSingle(LibrosDevueltosTextBox.Text),
                Dias = Convert.ToInt32(DiasTextBox.Text.ToString())
            };
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
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    MessageBox.Show("El Campo (Devolución Id) está vacío.\n\nAsigne un Id al Préstamo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DevolucionIdTextbox.Text = "0";
                    DevolucionIdTextbox.Focus();
                    DevolucionIdTextbox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Usuario Id ]—————————————————————————————————
                if (UsuarioIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Usuario Id) está vacío.\n\nPorfavor, Seleccione su Nombre de Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    UsuarioIdComboBox.IsDropDownOpen = true;
                    return;
                }
                //—————————————————————————————————[ Estudiante Id ]—————————————————————————————————
                if (EstudianteIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Estudiante Id) está vacío.\n\nSelecione una Id de Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EstudianteIdComboBox.IsDropDownOpen = true;
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
                MessageBox.Show($"El valor digitado en el campo (Préstamo Id) no es un número.\n\nPor favor, digite un número.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DevolucionIdTextbox.Text = "0";
                DevolucionIdTextbox.Focus();
                DevolucionIdTextbox.SelectAll();
            }
        }

        private void DiasTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (DiasTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(DiasTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Dias Transcurridos) no es un número.\n\nPor favor, digite un número de dias valido.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DiasTextBox.Text = "0";
                DiasTextBox.Focus();
                DiasTextBox.SelectAll();
            }
        }
    } 
}