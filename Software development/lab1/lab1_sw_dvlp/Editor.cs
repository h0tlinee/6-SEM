using System;
using System.Globalization;

namespace Converter
{
    class Editor{

        // public Editor(){
        //     number=zero;
        // }
        const string dictionary = "0123456789ABCDEF";
        //Поле для хранения редактируемого числа.
        private string number;
        //Разделитель целой и дробной частей.
        const string delim = ".";
        //Ноль.
        const string zero = "0";
        //Свойствое для чтения редактируемого числа.
        
        public string Number
        { get { return number; } }

        //целое значение в символ
        public static char Int_to_char(int n)
        {
            return dictionary[n];
        }
        //Добавить цифру.
        private  string AddDigit(char n) {
            number+=n;
            return number;
        }
        //Точность представления результата.
          public int Acc(){ 
           
                int i = 0;
                while (number[i] != '.' && i < number.Length - 1) 
                {
                    i++;
                }
                return number.Length - i - 1;
           

        } 
        //Добавить ноль.
        private string AddZero(){
            number+=zero;
            return number;
         }
         //Добавить разделитель.
        private string AddDelim(){ 
            number+=delim;
            return number;
        }
        //Удалить символ справа.
        private string Bs() { 
            number = number[..^1];
            return number;
        }
        //Очистить редактируемое число.
        private string Clear() { 
            number = "";
            return number;
        }
        
        private string ChangeSign(){
           
                if (number[0]=='-'){
                    number = number.Substring(1, number.Length - 1);
                    
                }else if(number[0]=='0'){
                    return number;
                }
                else{
                    number=number.Insert(0,"-");
                }
                return number;
            

        }
        public dynamic DoEdit(int j,char s='0'){
            //добавить цифру
            if (j==0){
                return AddDigit(s);
            }
            if (j==1){
                return Acc();
            }
            if(j==2){
            return AddZero();
            }
            if(j==3){
            return AddDelim();
            }
            if(j==4){
            return Bs();
            }
            if(j==5){
            return Clear();
            }
            if(j==6){
            return ChangeSign();
            }
            return "Nan";
        }
  
       
    }
}
