using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Convert_p_10
    {
        //Преобразовать цифру в число
        private static double Char_to_num(char ch)
        {
            const string symbols = "0123456789ABCDEF";
            return symbols.IndexOf(ch);
        }

        //Преобразовать строку в число
        private static double Convert(string p_num, int p, double point_indx)
        {
            double result = 0;
            for (int i = 0; i < p_num.Length; ++i)
            {
                result += Char_to_num(p_num[i]) * Math.Pow(p, point_indx - 1 - i);//перевод числа из произвольной системы в десятичную
            }
            return result;
        }

        //Преобразовать из с.сч. с основанием р в с.сч. с основанием 10.
        public static double Do(string P_num, int p)
        {
            int point_indx = P_num.IndexOf('.');

            if (point_indx == -1)//если нет точки
            {              
                return Convert(P_num, p, P_num.Length);
            }
            else
            {
                string p_num_pntless = P_num.Remove(point_indx, 1);//убираем точку
                return Convert(p_num_pntless, p, point_indx);
            }
        }
    }
}
