using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BsTreeDraw
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnDrawTree_Click(object sender, EventArgs e)
        {
            int[] ini = { 3, 7, 4, 9, 1, 12, 2, -5, 5 };
            BsTreeDraw lst = new BsTreeDraw();
            lst.Init(ini);
            lst.Draw(pictBox);
        }

    }
}
