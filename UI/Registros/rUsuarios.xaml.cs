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
    public partial class rUsuarios : Window
    {
        private Usuarios usuarios = new Usuarios();
        public rUsuarios()
        {
            InitializeComponent();
            this.DataContext = usuarios;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = usuarios;
            ContrasenaPasswordBox.Password = string.Empty;
            ConfirmarContrasenaPasswordBox.Password = string.Empty;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.usuarios = new Usuarios();
            this.DataContext = usuarios;
            ContrasenaPasswordBox.Password = string.Empty;
            ConfirmarContrasenaPasswordBox.Password = string.Empty;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (UsuarioIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Usuarios encontrado = UsuariosBLL.Buscar(Utilidades.ToInt(UsuarioIdTextBox.Text));

            if (encontrado != null)
            {
                this.usuarios = encontrado;
                Cargar();
            }
            else
            {
                this.usuarios = new Usuarios();
                this.DataContext = this.usuarios;
                MessageBox.Show($"Este Usuario no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                UsuarioIdTextBox.SelectAll();
                UsuarioIdTextBox.Focus();
            }
            if (UsuarioIdTextBox.Text == "1")
            {
                EliminarButton.IsEnabled = false;
            }
            else
            {
                EliminarButton.IsEnabled = true;
            }
        }
        //——————————————————————————————————————————————————————————————[ Nuevo ]———————————————————————————————————————————————————————————————
        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            EliminarButton.IsEnabled = true;
        }
        //——————————————————————————————————————————————————————————————[ Guardar ]———————————————————————————————————————————————————————————————
        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            {
                if (!Validar())
                    return;

                //———————————————————————————————————————————————————————[ VALIDAR TEXTBOX ]———————————————————————————————————————————————————————
                //—————————————————————————————————[ Usuario Id ]—————————————————————————————————
                if (UsuarioIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Usuario Id) está vacío.\n\nAsigne un Id al Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    UsuarioIdTextBox.Text = "0";
                    UsuarioIdTextBox.Focus();
                    UsuarioIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Nombres ]—————————————————————————————————
                if (NombresTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombres) está vacío.\n\nEscriba sus Nombres.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombresTextBox.Clear();
                    NombresTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Apellidos ]—————————————————————————————————
                if (ApellidosTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Apellidos) está vacío.\n\nEscriba sus Apellidos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ApellidosTextBox.Clear();
                    ApellidosTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha Creación ]—————————————————————————————————
                if (FechaCreacionDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha Creación) está vacío.\n\nSeleccione una fecha.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaCreacionDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Nombre Usuario ]—————————————————————————————————
                if (NombreUsuarioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Usuario) está vacío.\n\nAsigne un Nombre al Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombreUsuarioTextBox.Focus();
                    NombreUsuarioTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Nombre Usuario ]—————————————————————————————————
                if (NombreUsuarioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Usuario) está vacío.\n\nAsigne un Nombre al Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombreUsuarioTextBox.Focus();
                    NombreUsuarioTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Contraseña ]—————————————————————————————————
                if (ContrasenaPasswordBox.Password == string.Empty)
                {
                    MessageBox.Show("El Campo (Contraseña) está vacío.\n\nAsigne una Contraseña al Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ContrasenaPasswordBox.Focus();
                    ContrasenaPasswordBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Confirmar Contraseña ]—————————————————————————————————
                if (ConfirmarContrasenaPasswordBox.Password == string.Empty)
                {
                    MessageBox.Show("El Campo (Confirmar Contraseña) está vacío.\n\nConfirme la Contraseña del Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ConfirmarContrasenaPasswordBox.Focus();
                    ConfirmarContrasenaPasswordBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Validar Contraseñas ]—————————————————————————————————
                if (ConfirmarContrasenaPasswordBox.Password != ContrasenaPasswordBox.Password)
                {
                    MessageBox.Show("Las Contraseñas escritas no coinciden", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ContrasenaPasswordBox.Clear();
                    ConfirmarContrasenaPasswordBox.Clear();
                    ContrasenaPasswordBox.Focus();
                    return;
                }

                var paso = UsuariosBLL.Guardar(usuarios);
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
            //—————————————————————————————————[ Evitar que se borre el Usuario Admin Id #1 ]—————————————————————————————————
            if (UsuarioIdTextBox.Text == "1")
            {
                MessageBox.Show("No se pudo eliminar este Usuario.\n\nNo puede eliminar este Usuario.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                UsuarioIdTextBox.Focus();
                UsuarioIdTextBox.SelectAll();
                return;
            }

            if (UsuariosBLL.Eliminar(Utilidades.ToInt(UsuarioIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}