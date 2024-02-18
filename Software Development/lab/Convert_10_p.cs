using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Convert_10_p
    {
        //Преобразовать целое в символ
        public static char Int_to_char(int n)
        {
            const string dictionary = "0123456789ABCDEF";
            return dictionary[n];
        }

        //Преобразовать десятичное целое в с.сч. с основанием р
        public static string Int_to_p(int n, int p)
        {
            string result = "";

            while (n >= p)//пока не достигли последней операции деления
            {
                int remainder = n % p;//отсаток от числа
                result = result.Insert(0, Int_to_char(remainder).ToString());//вставляем проебразованные остатки с конца
                n = n / p;//переходим к след. разряду
            }

            result = result.Insert(0, Int_to_char(n).ToString());//добавляем последний остаток
            return result;
        }

        //Преобразовать десятичную дробь в с.сч. с основанием р
        public static string Flt_to_p(double n, int p, int c)
        {
            string result = "";
            for (int i = 0; i < c; ++i)//пока не достигли треб.точности
            {
                n = n * p;//умножаем на основание выбранной системы
                result = result.Insert(result.Length, Int_to_char((int)n).ToString());//в прямом порядке вставляем целые части произведений
                n = n - (int)n;//выделяем дробную часть

                if (n % 1 == 0)//если перевели полностью
                {
                    break;//выходим
                }
            }
            return result;
        }

        //Преобразовать десятичное действительное число в с.сч. с осн. р.
        public static string Do(double n, int p, int c)
        {
            int n_int = (int)n;//целая часть числа
            double n_flt;
            if (n > 1)
            {
                n_flt = n - n_int;//десятичная часть числа
            }
            else
            {
                n_flt = n;
            }


            if (n_flt == 0)//если число целое
            {
                return Int_to_p(n_int, p);//иначе вычисляем целое число в p-ой системе сч.
            }
            else//иначе вычислем дробь
            { 
                return Int_to_p(n_int, p) + "." + Flt_to_p(n_flt, p, c);//иначе вычисляем число в p-ой системе сч.
            }
        }
    }
}
