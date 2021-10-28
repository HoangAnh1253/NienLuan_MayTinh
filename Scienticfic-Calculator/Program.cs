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
            String str = "S2^2";
            MessageBox.Show(Compute.convert_Decimal_to_AnotherNumberType(0.123,2));
            Application.Run(new Calculator());
        }
        
    }
}
