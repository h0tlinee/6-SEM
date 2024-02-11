using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter //определяем пространство имен
{
    class Convert_10_p //объявление класса
    {
        const string dictionary = "0123456789ABCDEF";

        //целое значение в символ
        public static char Int_to_char(int n)
        {
            return dictionary[n];
        }
        //преобразование целой части в нужную систему счисления
        public static string Int_to_p(int n,int p)
        {
            string result = "";
            while (n >= p){//если n<p, то достигли последней операции деления
                int ostatok = n % p;//ищем остаток
                result = result.Insert(0, Int_to_char(ostatok).ToString());//вставвялем остатки в обр порядке, предварительно преобразовав
                n = n / p;//целочисленное деление
            }
            result = result.Insert(0, Int_to_char(n).ToString());//последний остаток будет n если стало меньше p
            return result;


        }
        //преобразование дробной части в нужную систему счисления
        public static string Flt_to_p(double n,int p,int c)//c-точность
        {
            string result = "";
            for(int i = 0; i < c; i++)//пока не достигли треб.точности
            {
                n *= p;//умножаем на основание выбранной системы
                result=result.Insert(result.Length,Int_to_char((int)n).ToString());//в прямом порядке вставляем целые части произведений
                n = n - (int)n;//выделяем дробную часть
                if (n % 1 == 0)//если дробная часть 0
                {
                    break;
                }
            }
            return result;
        }
        //преобразовать десятичное действительное число в нужную систему счисления
        public static string Do(double n,int p,int c)
        {
            bool minus = false;//знак минус перед числом
            if (n < 0)
            {
                minus = true;
                n = n * (-1);
            }
            int n_int = (int)n;//выделяем целую часть
            double n_flt = n - n_int;//выделяем дробную часть
            if (n_flt == 0)//целое число
            {
                if(minus) //отрицательное целое число
                {
                    return "-" + Int_to_p(n_int, p);
                }
                return Int_to_p(n_int, p);//положительное целое число
            }
            else
            {
                if (minus)//отрицательное дробное число
                {
                    return "-" + Int_to_p(n_int, p) + "." + Flt_to_p(n_flt, p, c);
                }
                return Int_to_p(n_int, p) + "." + Flt_to_p(n_flt, p, c);//положительное дробное число
            }
        }

    }
}