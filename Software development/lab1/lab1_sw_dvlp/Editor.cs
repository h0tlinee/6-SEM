using System;
using System.Globalization;

namespace Converter
{
    class Editor{

        public Editor(){
            number=zero;
        }
        const string dictionary = "0123456789ABCDEF";
        //Поле для хранения редактируемого числа.
        public string number;
        //Разделитель целой и дробной частей.
        const string delim = ".";
        //Ноль.
        const string zero = "0";
        //Свойствое для чтения редактируемого числа.
        
        // public string Number
        // { get { } }

        //целое значение в символ
        public static char Int_to_char(int n)
        {
            return dictionary[n];
        }
        //Добавить цифру.
        public  string AddDigit(char n) {
            number+=n;
            return number;
        }
        //Точность представления результата.
          public int Acc(int n){ 
            return n;
        } 
        //Добавить ноль.
        public string AddZero(){
            number+=zero;
            return number;
         }
         //Добавить разделитель.
        public string AddDelim(){ 
            number+=delim;
            return number;
        }
        //Удалить символ справа.
        public string Bs() { 
            number = number[..^1];
            return number;
        }
        //Очистить редактируемое число.
        public string Clear() { 
            number = "";
            return number;
        }
        
  
       
    }
}
