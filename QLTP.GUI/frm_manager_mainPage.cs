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
    public partial class frm_manager_mainPage : Form
    {
        public frm_manager_mainPage()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            frm_mainPage frm = new frm_mainPage(); // Mở form chính
            frm.ShowDialog();
            this.Close(); // Đóng form đăng ký
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_managerCustomer frm = new frm_managerCustomer();
            frm.ShowDialog();
            this.Close();
        }

        private void quảnLýSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_managerProduct frm = new frm_managerProduct();
            frm.ShowDialog();
            this.Close();   
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void quảnLýNhânSựToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
