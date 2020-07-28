using System;
using System.Collections.Generic;
using System.Windows;
//Using agregados
using ProyectoFinal_PrestamosLibros.BLL;
using ProyectoFinal_PrestamosLibros.Entidades;

namespace ProyectoFinal_PrestamosLibros.UI.Consultas
{
    public partial class cUsuarios : Window
    {
        public cUsuarios()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Usuarios>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = UsuariosBLL.GetList(u => u.UsuarioId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;

                    case 1:                     
                        listado = UsuariosBLL.GetList(e => e.Nombres.Contains(CriterioTextBox.Text, StringComparison.OrdinalIgnoreCase));
                        break;
                }
            }
            else
            {
                //MessageBox.Show("Has dejado el Campo (Criterio) vacio.\n\nPor lo tanto, se mostrarán todos los Usuarios", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                listado = UsuariosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = UsuariosBLL.GetList(c => c.FechaCreacion.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = UsuariosBLL.GetList(c => c.FechaCreacion.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }


    }
}
