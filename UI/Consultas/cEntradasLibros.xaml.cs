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
    public partial class cEntradasLibros : Window
    {
        public cEntradasLibros()
        {
            InitializeComponent();
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<EntradasLibros>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = EntradasLibrosBLL.GetList(l => l.LibroId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                MessageBox.Show("Has dejado el Campo (Criterio) vacio.\n\nPor lo tanto, aparecerán todas las Entradas de Libros", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                listado = EntradasLibrosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = EntradasLibrosBLL.GetList(c => c.Fecha.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = EntradasLibrosBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
