using System;
using System.Windows.Forms;

namespace TLBSMS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void adminButton_Click(object sender, EventArgs e) { Hide(); new Form2().Show(); }

        private void managerButton_Click(object sender, EventArgs e) { Hide(); new Form4().Show(); }
    }
}
