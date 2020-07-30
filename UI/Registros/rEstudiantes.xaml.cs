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
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
//Using agregados
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.UI.Registros
{
    public partial class rEstudiantes : Window
    {
        private Estudiantes estudiantes = new Estudiantes();
        public rEstudiantes()
        {
            InitializeComponent();
            this.DataContext = estudiantes;
        }
        //——————————————————————————————————————————————————————————————[ Cargar ]———————————————————————————————————————————————————————————————
        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = estudiantes;
        }
        //——————————————————————————————————————————————————————————————[ Limpiar ]——————————————————————————————————————————————————————————————
        private void Limpiar()
        {
            this.estudiantes = new Estudiantes();
            this.DataContext = estudiantes;
            EstudianteIdTextBox.Focus();
            EstudianteIdTextBox.SelectAll();
        }
        //——————————————————————————————————————————————————————————————[ Validar ]——————————————————————————————————————————————————————————————
        private bool Validar()
        {
            bool Validado = true;
            if (EstudianteIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }
        //——————————————————————————————————————————————————————————————[ Buscar ]———————————————————————————————————————————————————————————————
        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Estudiantes encontrado = EstudiantesBLL.Buscar(Utilidades.ToInt(EstudianteIdTextBox.Text));

            if (encontrado != null)
            {
                this.estudiantes = encontrado;
                Cargar();
            }
            else
            {
                this.estudiantes = new Estudiantes();
                this.DataContext = this.estudiantes;
                MessageBox.Show($"Este Estudiante no fue encontrado.\n\nAsegurese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                EstudianteIdTextBox.Focus();
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

                //———————————————————————————————[ Estudiante Id ]———————————————————————————————
                if (EstudianteIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Estudiante Id) esta vacio.\n\nAsigne un Id al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    EstudianteIdTextBox.Text = "0";
                    EstudianteIdTextBox.Focus();
                    EstudianteIdTextBox.SelectAll();
                    return;
                }
                //———————————————————————————————[ Matricula ]———————————————————————————————
                if (MatriculaTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Matricula) esta vacio.\n\nAsigne una Matricula al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MatriculaTextBox.Text = "0";
                    MatriculaTextBox.Focus();
                    MatriculaTextBox.SelectAll();
                    return;
                }
                if (MatriculaTextBox.Text.Length != 8)
                {
                    MessageBox.Show($"La Matricula ({MatriculaTextBox.Text}) no es valida.\n\nLa Matricula debe tener 8 digitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MatriculaTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Nombres ]———————————————————————————————
                if (NombresTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Nombres) esta vacio.\n\nAsigne un Nombre al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    NombresTextBox.Clear();
                    NombresTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Apellidos ]———————————————————————————————
                if (ApellidosTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Apellidos) esta vacio.\n\nAsigne un Apellido al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    ApellidosTextBox.Clear();
                    ApellidosTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Cedula ]———————————————————————————————
                if (CedulaTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cedula) esta vacio.\n\nAsigne una Cedula al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CedulaTextBox.Text = "0";
                    CedulaTextBox.Focus();
                    CedulaTextBox.SelectAll();
                    return;
                }
                if (CedulaTextBox.Text.Length != 11)
                {
                    MessageBox.Show($"La Cédula ({CedulaTextBox.Text}) no es valida.\n\nLa cedula debe tener 11 digitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CedulaTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Genero ]———————————————————————————————
                if (FemeninoRadioButton.IsChecked.Value == false && MasculinoRadioButton.IsChecked.Value == false)
                {
                    MessageBox.Show("El Campo (Genero) esta vacio.\n\nAsigne un Genero al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FemeninoRadioButton.Focus();
                    return;
                }
                //———————————————————————————————[ Telefono ]———————————————————————————————
                if (TelefonoTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Telefono) esta vacio.\n\nAsigne un Telefono al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Text = "0";
                    TelefonoTextBox.Focus();
                    TelefonoTextBox.SelectAll();
                    return;
                }
                if (TelefonoTextBox.Text.Length != 10)
                {
                    MessageBox.Show($"El Teféfono ({TelefonoTextBox.Text}) no es valido.\n\nEl Teféfono debe tener 10 digitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TelefonoTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Fecha Nacimiento ]———————————————————————————————
                if (FechaNacimientoDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Fecha Nacimiento) esta vacio.\n\nAsigne una Fecha de Nacimiento al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    FechaNacimientoDatePicker.Focus();
                    return;
                }
                //———————————————————————————————[ Direccion ]———————————————————————————————
                if (DireccionTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Direccion) esta vacio.\n\nAsigne una Direccion al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    DireccionTextBox.Clear();
                    DireccionTextBox.Focus();
                    return;
                }
                //———————————————————————————————[ Correo ]———————————————————————————————
                if (CorreoTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Correo) esta vacio.\n\nAsigne una Correo al Estudiante.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CorreoTextBox.Clear();
                    CorreoTextBox.Focus();
                    return;
                }
                //———————————————————————————————————————————————————————[ VALIDAR SI ESTA VACIO - FIN ]———————————————————————————————————————————————————————
                var paso = EstudiantesBLL.Guardar(estudiantes);
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
                if (EstudiantesBLL.Eliminar(Utilidades.ToInt(EstudianteIdTextBox.Text)))
                {
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //—————————————————————————————————————————————————————————————[ TEXT CHANGED ]—————————————————————————————————————————————————————————————

        //—————————————————————————————————[ Estudiante Id ]—————————————————————————————————
        private void EstudianteIdTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (EstudianteIdTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(EstudianteIdTextBox.Text);
                }
            }
            catch
            {
                MessageBox.Show($"El valor digitado en el campo (Estudiante Id) no es un numero.\n\nPorfavor, digite un numero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                EstudianteIdTextBox.Text = "0";
                EstudianteIdTextBox.Focus();
                EstudianteIdTextBox.SelectAll();
            }
        }
        //—————————————————————————————————[ Matricula ]—————————————————————————————————
        private void MatriculaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (MatriculaTextBox.Text.Trim() != string.Empty)
                {
                    int.Parse(MatriculaTextBox.Text);
                }

                if (MatriculaTextBox.Text.Length != 8)
                {
                    MatriculaTextBox.Foreground = Brushes.Red;
                }
                else
                {
                    MatriculaTextBox.Foreground = Brushes.Black;
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Matricula) no es un numero.\n\nPorfavor, digite un numero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                MatriculaTextBox.Text = "0";
                MatriculaTextBox.Focus();
                MatriculaTextBox.SelectAll();
            }
        }
        //—————————————————————————————————[ Cedula ]—————————————————————————————————
        private void CedulaTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (CedulaTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(CedulaTextBox.Text);
                }

                if (CedulaTextBox.Text.Length != 11)
                {
                    CedulaTextBox.Foreground = Brushes.Red;
                }
                else
                {
                    CedulaTextBox.Foreground = Brushes.Black;
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Cedula) no es un numero.\n\nPorfavor, digite numeros (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CedulaTextBox.Text = "0";
                CedulaTextBox.Focus();
                CedulaTextBox.SelectAll();
            }
        }
        //—————————————————————————————————[ Telefono ]—————————————————————————————————
        private void TelefonoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (TelefonoTextBox.Text.Trim() != string.Empty)
                {
                    long.Parse(TelefonoTextBox.Text);
                }

                if (TelefonoTextBox.Text.Length != 10)
                {
                    TelefonoTextBox.Foreground = Brushes.Red;
                }
                else
                {
                    TelefonoTextBox.Foreground = Brushes.Black;
                }
            }
            catch
            {
                MessageBox.Show("El valor digitado en el campo (Telefono) no es un numero.\n\nPorfavor, digite un numero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Text = "0";
                TelefonoTextBox.Focus();
                TelefonoTextBox.SelectAll();
            }
        }
    }
}