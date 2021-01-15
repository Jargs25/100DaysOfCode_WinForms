using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100DaysOdCode_WinForms
{
    public partial class Mensaje : Form
    {

        public Mensaje()
        {
            InitializeComponent();
        }

        public static DialogResult Show(string mensaje)
        {
            Mensaje oMensaje = new Mensaje();
            oMensaje.lblMensaje.Text = mensaje;
            return oMensaje.ShowDialog();
        }

        public static DialogResult Show(string mensaje, int opcion)
        {
            Mensaje oMensaje = new Mensaje();
            oMensaje.lblMensaje.Text = mensaje;
            switch (opcion)
            {
                case 1:
                    oMensaje.btnAceptar.Text = "SI";
                    oMensaje.btnAceptar.Location = new Point(131, 100);
                    oMensaje.btnNoAceptar.Visible = true;
                    oMensaje.btnNoAceptar.Location = new Point(212, 100);
                    break;
                default:
                    oMensaje.btnAceptar.Location = new Point(165, 100);
                    break;
            }
            return oMensaje.ShowDialog();
        }

        private void btnOpcion(object sender, EventArgs e)
        {
            Close();
        }
    }
}
