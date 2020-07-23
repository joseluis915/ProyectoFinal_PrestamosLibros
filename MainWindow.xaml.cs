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

        //----------------------------[ Devoluciones ]----------------------------



        //——————————————————————————————————————————————[ EVENTOS - CONSULTAS ]——————————————————————————————————————————————
        //----------------------------[ Estudiantes ]----------------------------
        private void cEstudiantesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cEstudiantes cEstudiantes = new cEstudiantes();
            cEstudiantes.Show();
        }

        //----------------------------[ Editoriales ]----------------------------
        private void cUsuariosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cUsuarios cUsuarios = new cUsuarios();
            cUsuarios.Show();
        }

        //----------------------------[ EntradasLibros ]----------------------------

        //----------------------------[ SalidasLibros ]----------------------------

        //----------------------------[ Prestamos ]----------------------------

        //----------------------------[ Devoluciones ]----------------------------

    }
}