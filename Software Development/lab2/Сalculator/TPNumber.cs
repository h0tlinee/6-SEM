using System;

namespace Сalculator
{
    class TPNumber
    {
        private double number;//поле число
        private int p;//основание сс
        private int accuracy;//точность задания числа

        //конструктор для числа в десятичной сс
        public TPNumber(double n, int p, int accuracy)
        {
            number = n;
            this.p = p;
            this.accuracy = accuracy;
        }

        //конструктор для числа заданного в виде строки
        public TPNumber(string value, int p, int accuracy)
        {
            number = Convert_p_10.Do(value, p);
            this.p = p;
            this.accuracy = accuracy;
        }

        //создание копии
        public TPNumber Copy()
        {
            return new TPNumber(number, p, accuracy);
        }

        //св-во для получения числа в строковом виде
        public string StrNumber
        {
            get { return Convert_10_p.Do(number, p, accuracy); }
            set { number =  Convert_p_10.Do(value, p); }
        }

        //св-во для получения числа в десятичном виде
        public double DecimalNumber
        {
            get { return number; }
            set { number = value; }
        }

        //св-во для получения/задания основания в строковом виде
        public string StrP
        {
            get{ return p.ToString(); }
            set{ p = Convert.ToInt32(value); }
        }

        //св-во для получения/задания основания в числовом виде
        public int IntP
        {
            get{ return p; }
            set{ p = value; }
        }

        //св-во для получения/задания точности в числовом виде
        public int IntAcc
        {
            get{ return accuracy; }
            set{ accuracy = value; }
        }

        //св-во для получения/задания точности в строковом виде
        public string StrAcc
        {
            get{ return accuracy.ToString(); }
            set{ accuracy = Convert.ToInt32(value); }
        }

        //оператор сложения
        public static TPNumber operator +(TPNumber n1, TPNumber n2)
        {
            return new TPNumber(n1.DecimalNumber + n2.DecimalNumber, n1.IntP, Math.Max(n1.IntAcc, n2.IntAcc));
        }

        //оператор вычитания
        public static TPNumber operator -(TPNumber n1, TPNumber n2)
        {
            return new TPNumber(n1.DecimalNumber - n2.DecimalNumber, n1.IntP, Math.Max(n1.IntAcc, n2.IntAcc));
        }

        //оператор умножения
        public static TPNumber operator *(TPNumber n1, TPNumber n2)
        {
            return new TPNumber(n1.DecimalNumber * n2.DecimalNumber, n1.IntP, Math.Max(n1.IntAcc, n2.IntAcc));
        }

        //оператор деления
        public static TPNumber operator /(TPNumber n1, TPNumber n2)
        {
            return new TPNumber(n1.DecimalNumber / n2.DecimalNumber, n1.IntP, Math.Max(n1.IntAcc, n2.IntAcc));
        }

        public static bool operator ==(TPNumber left, TPNumber right)
        {
            if (left.number == right.number && left.accuracy == right.accuracy)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(TPNumber left, TPNumber right)
        {
            if (left.number == right.number && left.accuracy == right.accuracy)
            {
                return false;
            }
            return true;
        }

        //возведение в квадрат
        public TPNumber Sqr()
        {
            return new TPNumber(DecimalNumber * DecimalNumber, IntP, accuracy);
        }

        //обращение числа
        public TPNumber Rev()
        {
            return new TPNumber(1 / DecimalNumber, IntP, accuracy);
        }
    }
}