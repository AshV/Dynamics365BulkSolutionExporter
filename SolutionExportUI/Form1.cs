using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace SolutionExport_UI
{
    public partial class Form1 : Form
    {
        ws w = new ws();
        public Form1()
        {
            InitializeComponent();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            lbl_solution.Text="Solution : "+tb_solution.Text;
            lbl_version.Text = "Version : "+tb_version.Text;
            lbl_auth.Text = "Author : " + tb_auth.Text;
            w.ff_CopyPanel(pnl,pnl_fl);
        }
        int i = 0;
        bool bol_color = false;
        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            Panel pnl =new Panel();
            pnl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)|System.Windows.Forms.AnchorStyles.Right)));

            pnl.Controls.Add(chkbox);
            pnl.Controls.Add(lbl_auth);
            pnl.Controls.Add(lbl_version);
            pnl.Controls.Add(lbl_solution);
            pnl.Location = new System.Drawing.Point(3, 3);
            pnl.Name = "pnl"+i;
            pnl.Size = new System.Drawing.Size(300, 92);
            pnl_fl.Controls.Add(pnl);
            if (bol_color)
            {   pnl.BackColor = System.Drawing.Color.LemonChiffon;
                bol_color = false;
            }
            else
            {   pnl.BackColor = System.Drawing.Color.Beige;
                bol_color = true; 
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            lbox.Items.Clear();
            pnl_fl2.Controls.Clear();
            int i = 0;
            foreach (Panel c in pnl_fl.Controls)
            {
                i++;
                foreach (Control cbox in c.Controls)
                {
                    if(cbox is CheckBox)
                    {
                        if (((CheckBox)cbox).Checked)
                        {
                            Label lb = c.Controls.Find("lbl_solution"+i, true).FirstOrDefault() as Label;
                           /// lbox.Items.Add(c.Name + "-->" + lb.Text);
                            lbl_solution_view.Text = lb.Text;
                            w.ff_CopyPanel(pnl_view,pnl_fl2);
                        }
                        break;
                    }
                }
            }
        }
    }
}