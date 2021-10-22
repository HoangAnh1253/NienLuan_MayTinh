using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace Scienticfic_Calculator
{
    class Compute
    {
        public static double compute(string bieuThuc)
        {
            double kq = 0;
            DataTable dt = new DataTable();
            Object v;
            //if (bieuThuc.Contains("√"))
            //{
            //    string tempStr = getBieuThucOf(bieuThuc, "√").ToString();
            //    bieuThuc = bieuThuc.Replace("√" + tempStr, Math.Sqrt(compute(tempStr)).ToString());
            //}
            //Xu ly dau ^
            while (bieuThuc.Contains("^"))
            {
                string veTrc = getBieuThucTruocViTri(bieuThuc, bieuThuc.IndexOf("^")),
                       veSau = getBieuThucSauViTri(bieuThuc, bieuThuc.IndexOf("^"));
                Console.WriteLine(veTrc + "^" + veSau);
                string tempStr = veTrc + "^" + veSau;
                double numOfVeTrc = Convert.ToDouble(compute(veTrc)),
                       numOfVeSau = Convert.ToDouble(compute(veSau));
                bieuThuc = bieuThuc.Replace(tempStr, Math.Pow(numOfVeTrc, numOfVeSau).ToString());
                Console.WriteLine(bieuThuc);
            }
         
            //Xu ly cac bieu thuc √,sin,...
            String firstSymbol;
            do
            {
                firstSymbol = getFirstSymbolOf(bieuThuc);
                if (!firstSymbol.Equals(""))
                {
                    string tempStr = getBieuThucOf(bieuThuc, firstSymbol);
                    bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Sqrt(compute(tempStr)).ToString());
                }
            } while (!firstSymbol.Equals(""));

            
            v = dt.Compute(bieuThuc, "");
            kq = Convert.ToDouble(v);
            return kq;
        }

        public static string getBieuThucOf(string bieuThuc, string symbol)
        {
            string kq = "";
            int indexOfSymbol = bieuThuc.IndexOf(symbol);
            if(indexOfSymbol!=-1)
            {
                bool end = false;
                int countOpenMark = 0;
                int i = indexOfSymbol + symbol.Length;
                do
                {
                    if (bieuThuc[i] == ')')
                        countOpenMark--;
                    else if (bieuThuc[i] == '(')
                        countOpenMark++;
                    kq = kq + bieuThuc[i++];
                    if(countOpenMark==0)
                    {
                        end = true;
                    }
                } while (!end);
            }
            return kq;
        }

        public static string getFirstSymbolOf(string bieuThuc)
        {
            string symbol="";
            int sqrtIndex = bieuThuc.IndexOf("√"),
                sinIndex = bieuThuc.IndexOf("Sin");
            List<int> indexList = new List<int>();
            indexList.Add(sqrtIndex);
            indexList.Add(sinIndex);
            for (int i = 0; i < indexList.Count; i++)
            {
                if (indexList[i] < 0)
                {
                    indexList.Remove(indexList[i]);
                    i--;
                }  
            }
            if (indexList.Count == 0)
                return symbol;
            indexList.Sort();
            if (indexList[0] == sqrtIndex)
                symbol = "√";
            else if (indexList[0] == sinIndex)
                symbol = "Sin";
            return symbol;
        }

        public static string getBieuThucTruocViTri(string bieuThuc, int index)
        {
            string kq="";
            if (Regex.IsMatch(bieuThuc[index-1].ToString(), @"[\d]"))//Neu bieu thuc phia truoc la so
            {
                Stack<char> num = new Stack<char>();
                index--;
                do
                {
                    
                    num.Push(bieuThuc[index]);
                    index--;
                } while (index>=0 && Regex.IsMatch(bieuThuc[index].ToString(), @"[\d]"));
                while(num.Count!=0)
                {
                    kq = kq + num.Pop();
                }
            }
            else//neu bieu thuc phia truoc khong phai la so
            {
                int countNgoacDon = 0;
                Stack<char> num = new Stack<char>();
                index--;
                do
                {
                    if (bieuThuc[index] == ')')
                        countNgoacDon++;
                    else if (bieuThuc[index] == '(')
                        countNgoacDon--;
                    num.Push(bieuThuc[index]);
                    index--;
                } while (index >= 0 && (bieuThuc[index] != ' ' || countNgoacDon>0));
                while (num.Count != 0)
                {
                    kq = kq + num.Pop();
                }
            }
            return kq;
        }

        public static string getBieuThucSauViTri(string bieuThuc, int index)
        {
            string kq="";
            Queue<char> num = new Queue<char>();
            if (Regex.IsMatch(bieuThuc[index + 1].ToString(), @"[\d]"))//Bieu thuc dung sau la so
            {
                index++;
                do
                {
                    num.Enqueue(bieuThuc[index]);
                    index++;
                } while (index < bieuThuc.Length && Regex.IsMatch(bieuThuc[index].ToString(), @"[\d]"));
                while (num.Count != 0)
                {
                    kq = kq + num.Dequeue();
                }
            }else if(bieuThuc[index+1]=='(')//Bieu thuc dung sau bat dau bang dau "("
            {
                int countNgoacDon = 0;
                index++;
                do
                {
                    if (bieuThuc[index] == '(')
                        countNgoacDon++;
                    else if (bieuThuc[index] == ')')
                        countNgoacDon--;
                    num.Enqueue(bieuThuc[index]);
                    index++;
                } while(index<bieuThuc.Length && countNgoacDon > 0);
                while (num.Count != 0)
                {
                    kq = kq + num.Dequeue();
                }
            }
            else//Bieu thuc bat dau bang mot symbol (VD: Sin, √)
            {
                int countNgoacDon = 0;
                index++;

                do//Lay Symbol
                {
                    kq = kq + bieuThuc[index];
                    index++;
                } while (bieuThuc[index]!='(');

                do //Lay bieu thuc trong ngoac ()
                {
                    if (bieuThuc[index] == '(')
                    {
                        countNgoacDon++;
                    }
                    else if (bieuThuc[index] == ')')
                        countNgoacDon--;
                    num.Enqueue(bieuThuc[index]);
                    index++;
                } while (index < bieuThuc.Length && countNgoacDon > 0);
                while (num.Count != 0)
                {
                    kq = kq + num.Dequeue();
                }
            }
            return kq;
        }
    }
}
