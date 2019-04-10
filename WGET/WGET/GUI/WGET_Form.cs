using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WGET.Classes;

namespace WGET.GUI
{
    // Main From
    public partial class WGET_Form : Form
    {
        string FolderPath = "";
        public WGET_Form()
        {
            InitializeComponent();
            Status.Text = "status";
        }

        //Browse Botton
        private void Browse_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources._4;
            Output_Path_Textbox.ForeColor = Color.FromArgb(78, 184, 206);
            Browse.ForeColor = Color.FromArgb(78, 184, 206); ;
            panel2.BackColor = Color.FromArgb(78, 184, 206);
            Output_Path.ShowDialog();
            Output_Path_Textbox.Text = Output_Path.SelectedPath.ToString();
            FolderPath= Output_Path.SelectedPath.ToString();
        }


        //Download Button
        private void Download_Click(object sender, EventArgs e)
        {
            Status.Text = "status";
            if (!Check_All_Inputs())
            {
                return;
            }
            Downloading_Process();
        }

        // Check All Inputs are Valid and Not Empty
        private bool Check_All_Inputs()
        {
            if (URL_Textbox.Text == "" || URL_Textbox.Text == "Enter URL :")
            {
                MessageBox.Show("Please, Enter File URL!", "Empty Field!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (Output_Path_Textbox.Text == "" || Output_Path_Textbox.Text == "Select Folder Path :")
            {
                MessageBox.Show("Please, Choose Output Folder!", "Empty Field!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }

        }

        // Downloading Process
        private void Downloading_Process()
        {
            string URL = URL_Textbox.Text;
            Status.Text = "Downloading";

           HTTP_Client Req = new HTTP_Client(URL);


            if (Req.GetStatus() == "200 OK")
            {
                Status.Text = Req.GetStatus(); ;
                Req.Writing_Data(FolderPath);

                Status.Text = "Successfully Downloaded";

            }

            else
            {

                Status.Text = Req.GetStatus();
            }


        }

        #region GUI Functions
        private void URL_Textbox_Click(object sender, EventArgs e)
        {
            URL_Textbox.Clear();
            pictureBox1.Image = Properties.Resources._2;
            URL_Textbox.ForeColor = Color.FromArgb(78, 184, 206);
            panel1.BackColor = Color.FromArgb(78, 184, 206);
        }

        private void URL_Textbox_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._2;
            URL_Textbox.ForeColor = Color.FromArgb(78, 184, 206);
            panel1.BackColor = Color.FromArgb(78, 184, 206);
        }

        private void URL_Textbox_Leave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1;
            URL_Textbox.ForeColor = Color.WhiteSmoke;
            panel1.BackColor = Color.WhiteSmoke;
        }

        private void Output_Path_Textbox_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources._4;
            Output_Path_Textbox.ForeColor = Color.FromArgb(78, 184, 206);
            Browse.ForeColor= Color.FromArgb(78, 184, 206); ;
            panel2.BackColor = Color.FromArgb(78, 184, 206);

        }

        private void Output_Path_Textbox_Leave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources._3;
            Output_Path_Textbox.ForeColor = Color.WhiteSmoke;
            Browse.ForeColor = Color.WhiteSmoke ;
            panel2.BackColor = Color.WhiteSmoke;
        }

        private void Browse_Leave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources._3;
            Output_Path_Textbox.ForeColor = Color.WhiteSmoke;
            Browse.ForeColor = Color.WhiteSmoke;
            panel2.BackColor = Color.WhiteSmoke;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources._7;
        }


        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources._6;

        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources._8;
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Properties.Resources._9;
        }

        #endregion GUI Functions

    }

}
