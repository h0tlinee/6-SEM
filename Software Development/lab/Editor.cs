using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Editor
    {
        //Поле для хранения редактируемого числа
        private string number = "";

        //Разделитель целой и дробной частей
        private const char delim = '.';

        //Ноль
        private const string zero = "0";

        private bool deliminator = false;

        //Свойство для чтения редактируемого числа
        public string Number
        { 
            get { return number; } 
        }

        //точность
        public int Acc
        {
            get
            {
                int i = 0;
                if (number.Length > 0)
                {
                    while (number[i] != delim && i < number.Length - 1)
                    {
                        i++;
                    }
                    return number.Length - i - 1;
                }
                else
                {
                    return 0;
                }
                
            }
        }

        //целое значение в символ
        private static char Int_to_char(int n)
        {
            const string dictionary = "0123456789ABCDEF";
            return dictionary[n];
        }

        //Добавить символ
        private void AddDigit(char symbol)
        {
            if (number != "0" && number != "-0")
            {
                if (symbol == '0')
                {
                    AddZero();
                }
                else
                {
                    number += Char.ToUpper(symbol);
                }
            }
        }

        //Добавить ноль
        private void AddZero()
        {
            number += zero;
        }

        //Добавить разделитель
        private void AddDelim()
        {
            if (!deliminator && number != "" && number != "-")
            {
                deliminator = true;
                number += delim;
            }
        }

        //Удалить символ справа
        private void Bs()
        {
            if (number.Length > 1)
            {
                number = number.Substring(0, number.Length - 1);
            }
            else if (number.Length > 0)
            {
                number = "";
            }
        }

        //Очистить редактируемое число
        private void Clear()
        {
            deliminator = false;
            number = "";
        }

        public string DoEdit(int j)
        {
            if (j == 0) AddZero();
            if (j >= 1 && j <= 15) AddDigit(Int_to_char(j));
            if (j == 16) AddDelim();
            if (j == 17) Bs();
            if (j == 18) Clear();
            return number;
        }
    }
}
