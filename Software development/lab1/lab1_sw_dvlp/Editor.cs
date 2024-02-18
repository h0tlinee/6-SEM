using System;
using System.Globalization;

namespace Converter
{
    class Editor{

        const string dictionary = "0123456789ABCDEF";
        private string number="";
        const string delim = ".";
        const string zero = "0";
        private bool minus=false;
        private bool delimm = false;
        public string Number
        { get { return number; } }

        

        public string AddDigit(char symbol) {
            if (number != "0" && number != "-0")
            {
                if (symbol == '0')
                {
                    AddZero();
                    return number;
                }
                else
                {
                    number += Char.ToUpper(symbol);
                    return number;
                }
            }
            return number;
        }
        
          public int Acc(){ 
           
                int i = 0;
                while (number[i] != '.' && i < number.Length - 1) 
                {
                    i++;
                }
                return number.Length - i - 1;
           

        } 
       
        public string AddZero(){
            number+=zero;
            return number;
         }
         
        public string AddDelim(){
            if (!delimm && number != "" && number != "-")
            {
                delimm= true;
                number += delim;
                return number;
            }
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
        
        public string ChangeSign(){
            if (minus)
            {
                minus = false;
                number = number.Remove(0, 1);
                return number;
            }
            else
            {
                minus = true;
                number = "-" + number;
                return number;
            }

        }
       
        
  
       
    }
}
