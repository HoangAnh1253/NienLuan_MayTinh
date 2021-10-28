using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using static Scienticfic_Calculator.Calculator;

namespace Scienticfic_Calculator
{
    class Compute
    {
        public static double compute(string bieuThuc)
        {
            double kq = 0;
            DataTable dt = new DataTable();
            Object v;

            //Xu ly ()

            //Xu ly Ans
            while(bieuThuc.Contains("Ans"))
            {
                bieuThuc = bieuThuc.Replace("Ans", Calculator.kq.ToString());
            }
            //xu ly PI
            while(bieuThuc.Contains("π"))
            {
                bieuThuc = bieuThuc.Replace("π", Math.PI.ToString());
            }

            //Xu ly dau ^
            while (bieuThuc.Contains("^"))
            {
                string veTrc = getBieuThucTruocViTri(bieuThuc, bieuThuc.IndexOf("^")),
                       veSau = getBieuThucSauViTri(bieuThuc, bieuThuc.IndexOf("^"));
                string tempStr = veTrc + "^" + veSau;
                double numOfVeTrc = Convert.ToDouble(compute(veTrc)),
                       numOfVeSau = Convert.ToDouble(compute(veSau));
                bieuThuc = bieuThuc.Replace(tempStr, Math.Pow(numOfVeTrc, numOfVeSau).ToString());
                Console.WriteLine(bieuThuc);
            }

            //Xu ly dau !
            while (bieuThuc.Contains("!"))
            {
                string veTrc = getBieuThucTruocViTri(bieuThuc, bieuThuc.IndexOf("!"));
                string tempStr = veTrc + "!";
                double numOfVeTrc = Convert.ToDouble(compute(veTrc));
                if (isInt(numOfVeTrc))
                {
                    bieuThuc = bieuThuc.Replace(tempStr, factorial(Convert.ToInt32(numOfVeTrc)).ToString());
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Biểu thức trước dấu ! phải có giá trị là số nguyên");
                    return 0;
                }

            }
           
         
            
            //Xu ly cac bieu thuc √, sin, cos, tan, cotan, logarit...
            String firstSymbol;
            do
            {
                firstSymbol = getFirstSymbolOf(bieuThuc);
                if (!firstSymbol.Equals(""))
                {   
                    string tempStr = getBieuThucOf(bieuThuc, firstSymbol);
                    if(firstSymbol.Equals("√"))
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Sqrt(compute(tempStr)).ToString());
                    else if(firstSymbol.Equals("log"))
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Log10(compute(tempStr)).ToString());
                    else if(firstSymbol.Equals("ln"))
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Log(compute(tempStr)).ToString());
                    else if(firstSymbol.Equals("sin"))
                    {
                        double radians = toRadians(compute(tempStr));
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Sin(radians).ToString());
                    }
                    else if (firstSymbol.Equals("cos"))
                    {
                        double radians = toRadians(compute(tempStr));
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Cos(radians).ToString());
                    }
                    else if (firstSymbol.Equals("tan"))
                    {
                        double radians = toRadians(compute(tempStr));
                        bieuThuc = bieuThuc.Replace(firstSymbol + tempStr, Math.Tan(radians).ToString());
                    }

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
                sinIndex = bieuThuc.IndexOf("sin"),
                cosIndex = bieuThuc.IndexOf("cos"),
                tanIndex = bieuThuc.IndexOf("tan"),
                logIndex = bieuThuc.IndexOf("log"),
                lnIndex = bieuThuc.IndexOf("ln");
            List<int> indexList = new List<int>();
            indexList.Add(sqrtIndex);
            indexList.Add(sinIndex);
            indexList.Add(cosIndex);
            indexList.Add(tanIndex);
            indexList.Add(logIndex);
            indexList.Add(lnIndex);
            
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
                symbol = "sin";
            else if (indexList[0] == cosIndex)
                symbol = "cos";
            else if (indexList[0] == tanIndex)
                symbol = "tan";
            else if (indexList[0] == logIndex)
                symbol = "log";
            else if (indexList[0] == lnIndex)
                symbol = "ln";
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
                } while (index>=0 && (Regex.IsMatch(bieuThuc[index].ToString(), @"[\d]") ||  bieuThuc[index] == '.'));
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
                } while (index < bieuThuc.Length && (Regex.IsMatch(bieuThuc[index].ToString(), @"[\d]") || bieuThuc[index]=='.'));
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

        public static double factorial(int number)
        {
            if (number == 1 || number == 0)
                return 1;
            else
                return number * factorial(number - 1);
        }

        public static bool isInt (double x)
        {
            bool isInteger = false;
            if (x==Convert.ToInt32(x))
            {
                isInteger = true;
            }
            return isInteger;
        }

        public static double toRadians(double number)
        {
            return (number * Math.PI) / 180;
        }

        public static string convert_Integer_to_AnotherNumberType(int number,int baseNum)
        {
            if (number == 0)
                return "";
            else
            {
                int remain = number % baseNum;
                string remainStr = remain.ToString();
                if(remain>=10)
                {
                    switch (remain)
                    {
                        case 10:
                            remainStr = "A";
                            break;
                        case 11:
                            remainStr = "B";
                            break;
                        case 12:
                            remainStr = "C";
                            break;
                        case 13:
                            remainStr = "D";
                            break;
                        case 14:
                            remainStr = "E";
                            break;
                        case 15:
                            remainStr = "F";
                            break;
                    }
                }
                return convert_Integer_to_AnotherNumberType(number / baseNum, baseNum) + remainStr;
            }
        }


        public static string convert_Decimal_to_AnotherNumberType(double number, int baseNum)
        {
            string kq=".";
            int maxlength = 15;
            double x = number;
            do
            {
                x = x * baseNum;
                int y = (int) Math.Round(x - 0.5, MidpointRounding.AwayFromZero);
                kq = kq + y.ToString();
                x = x - y;
                Console.WriteLine(x);
            } while (x!=0 && kq.Length<maxlength);
            if (kq.Length == maxlength)
                kq = kq + "...";
            return kq;
        }
    }
}
