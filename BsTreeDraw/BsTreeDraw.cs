using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TreeCollections;
using System.Windows.Forms;

namespace BsTreeDraw
{
    class BsTreeDraw: BsTree
    {
        public void Draw(PictureBox pb)
        {
            int dy = pb.Height / (Height() + 1);
            Graphics g = pb.CreateGraphics();
            DrawNode(root, g, 0, pb.Width, dy, 0, pb.Width / 2, 0);
        }
        private void DrawNode(Node p, Graphics g, int left, int right, int dy, int level, int xp, int yp)
        {
            if (p == null)
                return;

            int x = (left + right) / 2;
            int y = ++level * dy;

            g.DrawLine(new Pen(Color.Black), x, y - 10, xp, yp);
            g.DrawEllipse(new Pen(Color.Green), x - 10, y - 10, 20, 20);
            g.DrawString("" + p.val, new Font("Arial", 10), Brushes.Black, x - 7, y - 7);

            DrawNode(p.left, g, left, x, dy, level, x, y + 10);
            DrawNode(p.right, g, x, right, dy, level, x, y + 10);
        }
    }
}
