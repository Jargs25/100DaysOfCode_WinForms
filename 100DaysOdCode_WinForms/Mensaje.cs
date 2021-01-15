using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100DaysOdCode_WinForms
{
    public partial class Mensaje : Form
    {
        string rutaImagen = Directory.GetCurrentDirectory();

        public Mensaje()
        {
            InitializeComponent();
            rutaImagen = rutaImagen.Replace("bin\\Debug", "Iconos");
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
                    oMensaje.btnAceptar.Location = new Point(135, 112);
                    oMensaje.btnNoAceptar.Visible = true;
                    oMensaje.btnNoAceptar.Location = new Point(214, 112);
                    oMensaje.lblMensaje.Location = new Point(39, 23);
                    oMensaje.lblMensaje.Size = new Size(349, 80);
                    break;
                default:
                    oMensaje.btnAceptar.Location = new Point(176, 112);
                    oMensaje.lblMensaje.Location = new Point(39, 23);
                    oMensaje.lblMensaje.Size = new Size(349, 80);
                    break;
            }
            return oMensaje.ShowDialog();
        }
        public static DialogResult Show(string mensaje, int opcion, int imagen)
        {
            Mensaje oMensaje = new Mensaje();
            oMensaje.lblMensaje.Text = mensaje;
            switch (opcion)
            {
                case 1:
                    oMensaje.btnAceptar.Text = "SI";
                    oMensaje.btnAceptar.Location = new Point(135, 112);
                    oMensaje.btnNoAceptar.Visible = true;
                    oMensaje.btnNoAceptar.Location = new Point(214, 112);
                    oMensaje.lblMensaje.Location = new Point(122, 23);
                    oMensaje.lblMensaje.Size = new Size(266, 80);
                    break;
                default:
                    oMensaje.btnAceptar.Location = new Point(176, 112);
                    oMensaje.lblMensaje.Location = new Point(122, 23);
                    oMensaje.lblMensaje.Size = new Size(266, 80);
                    break;
            }
            switch (imagen)
            {
                case 1:
                    oMensaje.pbxImagen.Image = Image.FromFile(oMensaje.rutaImagen + "\\Info.png");
                    break;
                case 2:
                    oMensaje.pbxImagen.Image = Image.FromFile(oMensaje.rutaImagen + "\\Warning.png");
                    break;
                default:
                    oMensaje.pbxImagen.Image = Image.FromFile(oMensaje.rutaImagen + "\\Info.png");
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
