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
    public partial class cDevoluciones : Window
    {
        public cDevoluciones()
        {
            InitializeComponent();
        }
        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Devoluciones>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = DevolucionesBLL.GetList(d => d.DevolucionId == Utilidades.ToInt(CriterioTextBox.Text));
                        break;
                }
            }
            else
            {
                //MessageBox.Show("Has dejado el Campo (Criterio) vacio.\n\nPor lo tanto, se mostrarán todas las Devoluciones", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
                listado = DevolucionesBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = DevolucionesBLL.GetList(c => c.Fecha.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = DevolucionesBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
