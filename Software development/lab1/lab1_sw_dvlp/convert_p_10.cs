using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter {
    class Convert_p_10
    {
        private static double Char_to_num(char ch)//символ в целое число
        {
            const string symbols = "0123456789ABCDEF";
            return symbols.IndexOf(ch);
        }
        private static double Convert(string p_num,int p,double weight)//преобразование строки целой и дробной частей без разделителя в число
        {
            double result = 0;
            for(int i=0;i<p_num.Length; i++)
            {
                result += Char_to_num(p_num[i]) * Math.Pow(p, weight - 1 - i);//обычный алгоритм перевода в 10,нумеруем разряды и умножаем каждое число на основание в степени разряда
            }
            return result;
        }
        public static double Do(string p_num,int p)//сам перевод строки в любую систему счисления
        {
            bool minus = false;
            if (p_num[0] == '-')
            {
                p_num = p_num.Substring(1,p_num.Length-1);//убрали минус и запомнили в bool
                minus = true;
            }
            int weight = p_num.IndexOf('.');
            if (weight == -1) //если нет точки то результат 0xFFFFFFF что для int=-1
            { 
                if(minus==true)
                {
                    return Convert(p_num, p, p_num.Length) * -1;
                }
                return Convert(p_num,p, p_num.Length);
            }
            string p_num_pntless=p_num.Remove(weight,1);//убираем разделитель
            if(minus==true)
            {
                return Convert(p_num_pntless, p, weight)*-1;
            }
            return Convert(p_num_pntless, p, weight);
        }

    }


}
