using System;
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
using System.Collections.Generic;
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.UI.Consultas
{
    public partial class cEstudiantes : Window
    {
        public cEstudiantes()
        {
            InitializeComponent();
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Estudiantes>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0: 
                        listado = EstudiantesBLL.GetList(e => e.EstudianteId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                    case 1:
                        try
                        {
                            listado = EstudiantesBLL.GetList(e => e.UsuarioId == Utilidades.ToInt(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }

                        break;
                    case 2:
                        try
                        {
                        listado = EstudiantesBLL.GetList(e => e.Nombres.Contains(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        
                        break;
                    case 3:
                        try
                        {
                            listado = EstudiantesBLL.GetList(e => e.Apellidos.Contains(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Por favor, ingrese un Critero valido");
                        }
                        break;
                }
            }
            else
            {
                //MessageBox.Show("Has dejado el Campo (Criterio) vacio.\n\nPor lo tanto, se mostrarán todos los Estudiantes", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                listado = EstudiantesBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = EstudiantesBLL.GetList(c => c.FechaNacimiento.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = EstudiantesBLL.GetList(c => c.FechaNacimiento.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}