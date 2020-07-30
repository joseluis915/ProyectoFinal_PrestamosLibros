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
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.usuarios = new Usuarios();
            this.DataContext = usuarios;
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (UsuarioIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show($"Este Usuario no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                UsuarioIdTextBox.Focus();
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
                //—————————————————————————————————[ Usuario Id ]—————————————————————————————————
                if (UsuarioIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Usuario Id) esta vacio.\n\nAsigne un Id al Usuario.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    UsuarioIdTextBox.Text = "0";
                    UsuarioIdTextBox.Focus();
                    UsuarioIdTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Nombres ]—————————————————————————————————
                if (NombresTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombres) esta vacio.\n\nEscriba sus Nombres.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    NombresTextBox.Clear();
                    NombresTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Apellidos ]—————————————————————————————————
                if (ApellidosTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Apellidos) esta vacio.\n\nEscriba sus Apellidos.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ApellidosTextBox.Clear();
                    ApellidosTextBox.Focus();
                    return;
                }
                //—————————————————————————————————[ Fecha Creación ]—————————————————————————————————
                if (FechaCreacionDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha Creación) esta vacio.\n\nSeleccione una fecha.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaCreacionDatePicker.Focus();
                    return;
                }
                //—————————————————————————————————[ Nombre Usuario ]—————————————————————————————————
                if (NombreUsuarioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Usuario) esta vacio.\n\nAsigne un Nombre al Usuario.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    NombreUsuarioTextBox.Focus();
                    NombreUsuarioTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Nombre Usuario ]—————————————————————————————————
                if (NombreUsuarioTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombre Usuario) esta vacio.\n\nAsigne un Nombre al Usuario.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    NombreUsuarioTextBox.Focus();
                    NombreUsuarioTextBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Contraseña ]—————————————————————————————————
                if (ContrasenaPasswordBox.Password == string.Empty)
                {
                    MessageBox.Show("El Campo (Contraseña) esta vacio.\n\nAsigne una Contraseña al Usuario.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ContrasenaPasswordBox.Focus();
                    ContrasenaPasswordBox.SelectAll();
                    return;
                }
                //—————————————————————————————————[ Confirmar Contraseña ]—————————————————————————————————
                if (ConfirmarContrasenaPasswordBox.Password == string.Empty)
                {
                    MessageBox.Show("El Campo (Confirmar Contraseña) esta vacio.\n\nConfirme la Contraseña del Usuario.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    ConfirmarContrasenaPasswordBox.Focus();
                    ConfirmarContrasenaPasswordBox.SelectAll();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
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
                    MessageBox.Show("Transaccion Exitosa", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //——————————————————————————————————————————————————————————————[ Eliminar ]———————————————————————————————————————————————————————————————
        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            //—————————————————————————————————[ Evitar que se borre el Usuario Admin Id #1 ]—————————————————————————————————
            if (UsuarioIdTextBox.Text == "1")
            {
                MessageBox.Show("No se pudo eliminar este Usuario\n\nNo puede eliminar el Usuario Prinicpal", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                UsuarioIdTextBox.Text = "0";
                UsuarioIdTextBox.Focus();
                UsuarioIdTextBox.SelectAll();
                return;
            }

            if (UsuariosBLL.Eliminar(Utilidades.ToInt(UsuarioIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}