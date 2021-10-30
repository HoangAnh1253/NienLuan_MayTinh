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
            String str = "123456.78910";
            MessageBox.Show(Compute.convert_ThisNumType_to_AnotherNumtype("123AB.CD", 16, 10));
            MessageBox.Show(Compute.convert_ThisNumType_to_AnotherNumtype("123AB.CD", 16,2));
            MessageBox.Show(Compute.convert_ThisNumType_to_AnotherNumtype("123AB.CD", 16, 8));
            Application.Run(new Calculator());
        }
        
    }
}
