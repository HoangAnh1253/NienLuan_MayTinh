using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Scienticfic_Calculator.Compute;
namespace Scienticfic_Calculator
{
    public partial class Calculator : Form
    {
        Double kq = 0;
        int countNgoacDon = 0;
        public Calculator()
        {
            InitializeComponent();
        }

        private void Operator_click(object sender, EventArgs e)//one of (+,-,x,÷) buttons is clicked
        {
            Button opt = (Button)sender;
            if(!txtScreen.Text.EndsWith(" "))
            {
                if(opt.Text.Equals("x"))
                    txtScreen.Text = txtScreen.Text + " * ";
                else if(opt.Text.Equals("÷"))
                    txtScreen.Text = txtScreen.Text + " / ";
                else
                    txtScreen.Text = txtScreen.Text + " " + opt.Text + " ";
            }
        }

       

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

        private void NumberBtn_Clicked(object sender, EventArgs e)
        {
            if (txtScreen.Text == "0")
                txtScreen.Text = "";

            Button number = (Button)sender;
            if (number.Text == ".")
            {
                if (!txtScreen.Text.Contains("."))
                    txtScreen.Text = txtScreen.Text + number.Text;
            }
            else
                txtScreen.Text = txtScreen.Text + number.Text;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            kq = Compute.compute(txtScreen.Text);
            txtScreen.Text = kq.ToString();
        }

        private void btnAC_click(object sender, EventArgs e)
        {
            txtScreen.Text = "0";
        }

        private void btnDel_click(object sender, EventArgs e)
        {
            if (txtScreen.Text.Length > 0)
            {
                if (txtScreen.Text.EndsWith(" "))
                {
                    txtScreen.Text = txtScreen.Text.Remove(txtScreen.Text.Length - 3, 3);
                }
                else
                    txtScreen.Text = txtScreen.Text.Remove(txtScreen.Text.Length - 1, 1);
            }
            if (txtScreen.Text == "")
                txtScreen.Text = "0";
        }

        private void btnSqrt_Click(object sender, EventArgs e)
        {
            countNgoacDon++;
            if (txtScreen.Text.EndsWith(" ")||txtScreen.Text==""||txtScreen.Text=="0")
            {
                txtScreen.Text = txtScreen.Text + " √(";
            }
        }

        private void btnMoNgoacDon_Click(object sender, EventArgs e)
        {
            countNgoacDon++;
            if (txtScreen.Text.EndsWith(" ") || txtScreen.Text == "" || txtScreen.Text == "0")
            {
                txtScreen.Text = txtScreen.Text + "(";
            }
        }

        private void btnDongNgoacDon_Click(object sender, EventArgs e)
        {
            if(countNgoacDon>0)
            {
                txtScreen.Text = txtScreen.Text + ")";
                countNgoacDon--;
            }
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
            if ((txtScreen.Text.EndsWith(")") || !txtScreen.Text.EndsWith(" "))&&!txtScreen.Text.EndsWith("(")&& !txtScreen.Text.Equals("") && ! txtScreen.Text.EndsWith("^"))
            {
                txtScreen.Text = txtScreen.Text + "^";
            }
        }
    }
}
