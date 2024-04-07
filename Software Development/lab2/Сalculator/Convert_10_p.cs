using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сalculator
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
        public static string Int_to_p(long n, int p)
        {
            string result = "";

            while (n >= p)//пока не достигли последней операции деления
            {
                int remainder = (int)(n % p);//отсаток от числа
                result = result.Insert(0, Int_to_char(remainder).ToString());//вставляем проебразованные остатки с конца
                n = n / p;//переходим к след. разряду
            }

            result = result.Insert(0, Int_to_char((int)n).ToString());//добавляем последний остаток
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
            bool isNegative = false;

            if (n < 0)
            {
                isNegative = true;
                n *= -1;
            }

            long integerPart = (long)n;
            double fractionPart = n - integerPart;

            if (fractionPart == 0)
            {
                if (isNegative)
                {
                    return "-" + Int_to_p(integerPart, p);
                }

                return Int_to_p(integerPart, p);
            }
            else
            {
                if (isNegative)
                {
                    return "-" + Int_to_p(integerPart, p) + "," + Flt_to_p(fractionPart, p, c);
                }

                return Int_to_p(integerPart, p) + "," + Flt_to_p(fractionPart, p, c);
            }
        }
    }
}
