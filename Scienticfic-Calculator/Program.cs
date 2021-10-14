using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using static Scienticfic_Calculator.Compute;
namespace Scienticfic_Calculator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DataTable dt = new DataTable();
            object v = dt.Compute("2*2", "");
            String str = " 22222^33333 ";
            MessageBox.Show(Compute.compute("2+√(1+√(9))*2").ToString()+Compute.getBieuThucTruocViTri(str,str.IndexOf("^")) + "^" + Compute.getBieuThucSauViTri(str, str.IndexOf("^")));

            MessageBox.Show(v.ToString());
            Application.Run(new Calculator());
        }
        
    }
}
