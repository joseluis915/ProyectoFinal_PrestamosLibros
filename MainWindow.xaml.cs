using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Using agregados
using ProyectoFinal_PrestamosLibros.UI.Registros;
using ProyectoFinal_PrestamosLibros.UI.Consultas;

namespace ProyectoFinal_PrestamosLibros
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //——————————————————————————————————————————————[ EVENTOS - REGISTROS ]——————————————————————————————————————————————
        //----------------------------[ Estudiantes ]----------------------------
        private void rEstudiantesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEstudiantes rEstudiantes = new rEstudiantes();
            rEstudiantes.Show();
        }

        //----------------------------[ Editoriales ]----------------------------
        private void rEditorialesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEditoriales rEditoriales = new rEditoriales();
            rEditoriales.Show();
        }

        //----------------------------[ Libros ]----------------------------------
        private void rLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rLibros rLibros = new rLibros();
            rLibros.Show();
        }

        //----------------------------[ EntradasLibros ]----------------------------
        private void rEntradasLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rEntradasLibros rEntradasLibros = new rEntradasLibros();
            rEntradasLibros.Show();
        }

        //----------------------------[ SalidasLibros ]----------------------------
        private void rSalidasLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rSalidasLibros rSalidasLibros = new rSalidasLibros();
            rSalidasLibros.Show();
        }

        //----------------------------[ Prestamos ]----------------------------
        private void rPrestamosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPrestamos rPrestamos = new rPrestamos();
            rPrestamos.Show();
        }

        //----------------------------[ Devoluciones ]----------------------------
       private void rDevolucionesMenuItem_Click(object sender, RoutedEventArgs e)
       {
            rDevoluciones rDevoluciones = new rDevoluciones();
            rDevoluciones.Show();
        }

        //——————————————————————————————————————————————[ EVENTOS - CONSULTAS ]——————————————————————————————————————————————
        //----------------------------[ Usuarios ]----------------------------
        private void cUsuariosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cUsuarios cUsuarios = new cUsuarios();
            cUsuarios.Show();
        }
        //----------------------------[ Estudiantes ]----------------------------
        private void cEstudiantesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEstudiantes cEstudiantes = new cEstudiantes();
            cEstudiantes.Show();
        }
        //----------------------------[ Libros ]----------------------------
        private void cLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cLibros cLibros = new cLibros();
            cLibros.Show();
        }
        //----------------------------[ Editoriales ]----------------------------
        private void cEditorialesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEditoriales cEditoriales = new cEditoriales();
            cEditoriales.Show();
        }
        //----------------------------[ EntradasLibros ]----------------------------
        private void cEntradasLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEntradasLibros cEntradasLibros = new cEntradasLibros();
            cEntradasLibros.Show();
        }
        //----------------------------[ SalidasLibros ]----------------------------
        private void cSalidasLibrosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cSalidasLibros cSalidasLibros = new cSalidasLibros();
            cSalidasLibros.Show();
        }
        //----------------------------[ Prestamos ]----------------------------

        //----------------------------[ Devoluciones ]----------------------------

        //——————————————————————————————————————————————[ INFORMACION ]——————————————————————————————————————————————
        private void InformacionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"Propietario:\t\t{PropietarioLabel.Content}\n\nVersion:\t\t\t{VersionLabel.Content}\n\nCreacion:\t\t{CreacionLabel.Content}\n\nUltima Modificacion:\t{ModificacionLabel.Content}\n\nPara mas informacion:\tejemplo@ucne.edu.do",
                "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}