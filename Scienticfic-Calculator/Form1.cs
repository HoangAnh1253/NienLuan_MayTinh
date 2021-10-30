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
        public static Double kq = 0;
        int baseNum;
        int countNgoacDon = 0;
        bool mouseDown;
        private Point offset;
        public Calculator()
        {
            InitializeComponent();
        }

        private void moustDown_Event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void mouseMove_Event(object sender, MouseEventArgs e)
        {
            if(mouseDown == true)
            {
                Point currentPosition = PointToScreen(e.Location);
                Location = new Point(currentPosition.X - offset.X, currentPosition.Y - offset.Y);
            }
        }

        private void mouseUp_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Operator_click(object sender, EventArgs e)//one of (+,-,x,÷) buttons is clicked
        {
            Button opt = (Button)sender;
            if(canEnterOperator())
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
            if(canEnterNumber())
            {
                if(number.Text.Equals("."))
                {
                    if (txtScreen.Text == "" || !Compute.getBieuThucTruocViTri(txtScreen.Text, txtScreen.Text.Length).Contains("."))
                    {
                        txtScreen.Text = txtScreen.Text + number.Text;
                    }  
                    return;
                }
                txtScreen.Text = txtScreen.Text + number.Text;
            }
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            kq = Compute.compute(txtScreen.Text);
            baseNum = 10;
            txtKQ.Text = kq.ToString();
        }

        private void btnAC_click(object sender, EventArgs e)
        {
            txtScreen.Text = "0";
            txtKQ.Text = "";
        }

        private void btnDel_click(object sender, EventArgs e)
        {
            if (txtScreen.Text.Length > 0)
            {
                if (txtScreen.Text.EndsWith(" "))
                {
                    if(txtScreen.Text.Length == 1)
                        txtScreen.Text = txtScreen.Text.Remove(txtScreen.Text.Length - 1, 1);
                    else
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
            if(txtScreen.Text.Equals("0"))
            {
                txtScreen.Text = " √(";
                countNgoacDon++;
            }
            else if (canEnterFunction())
            {
                txtScreen.Text = txtScreen.Text + " √(";
                countNgoacDon++;
            }
        }

        private void btnMoNgoacDon_Click(object sender, EventArgs e)
        {
            if (txtScreen.Text == "0")
                txtScreen.Text = "";
            
            if (canEnterNumber())
            {
                txtScreen.Text = txtScreen.Text + "(";
                countNgoacDon++;
            }
        }

        private void btnDongNgoacDon_Click(object sender, EventArgs e)
        {
            if (countNgoacDon>0)
            {
                txtScreen.Text = txtScreen.Text + ")";
                countNgoacDon--;
            }
        }

        private void btnPow_Click(object sender, EventArgs e)
        {
         
            if (canEnterOperator())
            {
                txtScreen.Text = txtScreen.Text + "^";
            }
        }

        private void btnGiaiThua_Click(object sender, EventArgs e)
        {
            //(txtScreen.Text.EndsWith(")") || !txtScreen.Text.EndsWith(" ")) && !txtScreen.Text.EndsWith("(") && !txtScreen.Text.Equals("") && !txtScreen.Text.EndsWith("^")
            if (canEnterOperator())
            {
                txtScreen.Text = txtScreen.Text + "!";
            }
        }

       

        private void math_function_clicked(object sender, EventArgs e)
        {
            Button opt = (Button)sender;
            
            //txtScreen.Text.EndsWith(" ") || txtScreen.Text == "" || txtScreen.Text.EndsWith("^") || txtScreen.Text.EndsWith("(")
            if (canEnterFunction())
            {
                txtScreen.Text = txtScreen.Text + " " + opt.Text + "(";
                countNgoacDon++;
            }
            else if (txtScreen.Text.Equals("0"))
            {
                txtScreen.Text = " " + opt.Text + "(";
                countNgoacDon++;
            }
        }

        private void btnPi_Click(object sender, EventArgs e)
        {
            if (canEnterFunction())
            {
                txtScreen.Text = txtScreen.Text + "π";
            }
            else if (txtScreen.Text.Equals("0"))
            {
                txtScreen.Text = "π";
            }

        }

        private void btnAnswer_Click(object sender, EventArgs e)
        {
            if (canEnterFunction())
            {
                txtScreen.Text = txtScreen.Text + "Ans";
            }
            else if (txtScreen.Text.Equals("0"))
            {
                txtScreen.Text = "Ans";
            }
        }

        public  bool canEnterNumber()
        {
            bool allow = false;
            if ((!txtScreen.Text.EndsWith(")") && !txtScreen.Text.EndsWith("!")) || txtScreen.Text.EndsWith("."))
            {
                allow = true;
            }
            return allow;
        }

        public bool canEnterOperator()
        {
            bool allow = true;
            if (txtScreen.Text.EndsWith(" ") || txtScreen.Text.EndsWith("(") || txtScreen.Text.EndsWith(".") || txtScreen.Text.EndsWith("^"))
            {
                allow = false;
            }
            else if (txtScreen.Text.EndsWith(")"))
                allow = true;
            return allow;
        }

        public bool canEnterFunction()
        {
            bool allow = false;
            if(!canEnterOperator())
            {
                allow = true;
            }
            return allow;
        }

        private void number_convert_click(object sender, EventArgs e)
        {
            Button btn = (Button) sender;
            int newBaseNum = 0;
            if(btn.Text.Equals("Dec"))
            {
                newBaseNum = 10;
            }
            else if(btn.Text.Equals("Bin"))
            {
                newBaseNum = 2;
            }
            else if(btn.Text.Equals("Hex"))
            {
                newBaseNum = 16;
            }
            else if(btn.Text.Equals("Oct"))
            {
                newBaseNum = 8;
            }

            if(baseNum == newBaseNum)
            {
                return;
            }
            else
            {
                txtKQ.Text = Compute.convert_ThisNumType_to_AnotherNumtype(txtKQ.Text, baseNum, newBaseNum);
                baseNum = newBaseNum;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
