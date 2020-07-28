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

namespace ProyectoFinal_PrestamosLibros.UI.Login
{
    public partial class Login : Window
    {
        Usuarios usuarios = new Usuarios();
        MainWindow MenuPrincipal = new MainWindow();

        public Login()
        {
            InitializeComponent();
        }
        //———————————————————————————————————————————————————[ CERRAR - Al ingresar usuario o contraseña incorrecta]———————————————————————————————————————————————————
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }
        //———————————————————————————————————————————————————[ CANCELAR ]———————————————————————————————————————————————————
        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        //———————————————————————————————————————————————————[ INGRESAR ]———————————————————————————————————————————————————
        private void IngresarButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = UsuariosBLL.Autenticar(NombreUsuarioTextBox.Text, ContrasenaPasswordBox.Password);

            //—————————————————————————————————[ NombreUsuario Vacio]—————————————————————————————————
            if (NombreUsuarioTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Nombre Usuario) esta vacio.\n\nPorfavor, escriba su nombre de usuario.", "Precaución", MessageBoxButton.OK, MessageBoxImage.Warning);
                NombreUsuarioTextBox.Clear();
                NombreUsuarioTextBox.Focus();
                return;
            }

            if (paso)
            {
                this.Hide();
                MenuPrincipal.Show();
                //this.WindowState = WindowState.Minimized; //Minimiza el LogIn
            }
            else
            {
                MessageBox.Show("Nombre de Usuario o Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                ContrasenaPasswordBox.Clear();
                NombreUsuarioTextBox.Focus();
            }
        }
        //———————————————————————————————————————————————————[ NOMBRE USUARIO - ENTER ]———————————————————————————————————————————————————
        private void NombreUsuarioTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ContrasenaPasswordBox.Focus();
            }
        }
        //———————————————————————————————————————————————————[CONTRASEÑA - ENTER ]———————————————————————————————————————————————————
        private void ContrasenaPasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                bool paso = UsuariosBLL.Autenticar(NombreUsuarioTextBox.Text, ContrasenaPasswordBox.Password);

                if (paso)
                {
                    this.Hide();
                    MenuPrincipal.Show();
                }
                else
                {
                    MessageBox.Show("Nombre de Usuario o Contraseña incorrecta", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ContrasenaPasswordBox.Clear();
                    NombreUsuarioTextBox.Focus();
                }
            }
        }
    }
}
