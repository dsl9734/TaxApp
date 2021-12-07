﻿using System;
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
using System.Windows.Shapes;
using TaxApp.taxiBDDTableAdapters;
using static TaxApp.taxiBDD;

namespace TaxApp.Interfaz
{
    /// <summary>
    /// Lógica de interacción para inicio.xaml
    /// </summary>
    public partial class inicio : Window
    {
        string usuario,contrasena;
        Usuario.Usuario us = new TaxApp.Usuario.Usuario(); 

        public inicio()
        {
            InitializeComponent();
        }

        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            contrasena = Password.Text;
        }

        private void TextBox_Usuario(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            usuario = Usuario.Text;
        }

        private void Iniciar_Click(object sender, RoutedEventArgs e)
        {
            if (usuario != null && contrasena != null)
            {
                try {
                    int id = us.inicioSesion(usuario, contrasena);
                    if(id > 0)
                    {
                        MessageBox.Show("Se ha iniciado sesión correctamente.");
                        if (id != 1)
                        {
                            aplicacion_usuario window1 = new aplicacion_usuario();
                            this.Visibility = Visibility.Hidden;
                            window1.Show();
                        }
                        else
                        {
                            aplicacion_admin window1 = new aplicacion_admin();
                            this.Visibility = Visibility.Hidden;
                            window1.Show();
                        }
                    }

                    else
                    {
                        MessageBox.Show("Error interno al iniciar sesión.");
                    }
                    
                    
                }
                catch (Exception ex){ MessageBox.Show(ex.Message); }
            }
            else
            {
                MessageBox.Show("Alguna de las credenciales está vacía");
            }
        }

        private void Registrar_Click(object sender, RoutedEventArgs e)
        {
            registro window1 = new registro();
            this.Visibility = Visibility.Hidden;
            window1.Show();
        }
    }
}
