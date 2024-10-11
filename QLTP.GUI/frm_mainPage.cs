using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLTP.GUI
{
    public partial class frm_mainPage : Form
    {
        public frm_mainPage()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_login frm = new frm_login();
            frm.ShowDialog();
            this.Close();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_register frm = new frm_register();
            frm.ShowDialog();
            this.Close();
        }

        private void btn_quit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
