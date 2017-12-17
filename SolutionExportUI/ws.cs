using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SolutionExport_UI
{
    class ws
    {
        int i = 0;
        bool bol_color = false;
        Color clr_alter = System.Drawing.Color.NavajoWhite;
        public void ff_CopyPanel(Panel p1, FlowLayoutPanel fpnl)
        {
            i++;
            Panel p2 = new Panel();
            p2.Name = "pnl" + i;
            p2.Size = new Size(p1.Width, p1.Height);
            if (bol_color)
            {
                bol_color = false;
                p2.BackColor = clr_alter;
            }
            else
            {
                p2.BackColor = p1.BackColor;
                bol_color = true;
            }
            foreach (Control c in p1.Controls)
            {
                if (c is Label)
                {
                    Label lbl = new Label();
                    lbl.Name = c.Name + i;
                    lbl.Text = c.Text;
                    int lx = c.Location.X;
                    int ly = c.Location.Y;
                    lbl.Location = new Point(lx, ly);
                    lbl.Size = new Size(c.Width, c.Height);
                    float fz = c.Font.Size;
                    double fFontSize = c.Font.Size;
                    double fFontSize_2 = (fFontSize) * 10 / 10;
                    FontStyle fs = c.Font.Style;
                    lbl.Font = new Font(c.Font.FontFamily, (float)(fFontSize_2), c.Font.Style);
                    lbl.ForeColor = c.ForeColor;
                    p2.Controls.Add(lbl);
                }
                else if (c is CheckBox)
                {
                    CheckBox cbox2 = new CheckBox();
                    cbox2.Name = c.Name + i;
                    cbox2.Text = c.Text;
                    cbox2.Location = new Point(c.Location.X, c.Location.Y);
                    p2.Controls.Add(cbox2);
                }
                else if (c is TextBox)
                {
                    TextBox tb = new TextBox();
                    tb.Name = c.Name + i;
                    tb.Text = c.Text;
                    tb.Location = new Point(c.Location.X, c.Location.Y);
                    tb.Size = new Size(c.Width, c.Height);
                    p2.Controls.Add(tb);
                }
                else if (c is Button)
                {
                    Button tb = new Button();
                    tb.Name = c.Name + i;
                    tb.Text = c.Text;
                    tb.Location = new Point(c.Location.X, c.Location.Y);
                    tb.Size = new Size(c.Width, c.Height);
                    tb.BackColor = c.BackColor;
                    p2.Controls.Add(tb);
                }
            }
            fpnl.Controls.Add(p2);
        }
    }
}
