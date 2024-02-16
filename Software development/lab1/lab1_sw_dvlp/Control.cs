using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;

class Control

 {
       
        //Основание системы сч. исходного числа.
         int pin = 10;
        //Основание системы сч. результата.
         int pout = 16;
        //Число разрядов в дробной части результата.
        const int accuracy = 10;
        public History his = new History();
        public enum State {Editing, Converted}
        //Свойство для чтения и записи состояние Конвертера.
        public State St { get; set; }
        //Конструктор.
         public Control()
        {
            St = State.Editing;
            Pin = pin;
            Pout = pout;
        }
        //объект редактор
        public Editor ed = new Editor();
        //Свойство для чтения и записи основание системы сч. р1.
        public int Pin { 
        get{
            return pin;
        }
        set{
            pin=value;
        } 
        }

        //Свойство для чтения и записи основание системы сч. р2.
        public int Pout { 
        get{
             return pout;
        }
        set{
             pout=value;
        } 
        }
        //Выполнить команду конвертера.
        public string DoCmnd(int j)
        {
            //TODO:ПОТЕСТИТЬ
            if (j == 19)
            {
                string str_pin=pin.ToString();
                string str_pout=pout.ToString();
                double r = Convert_p_10.Do(ed.Number,
                (Int16)pin);
                string res = Convert_10_p.Do(r,
                (Int32)Pout,ed.Acc());
                St = State.Editing;
             
                his.AddRecord(str_pin, str_pout, ed.Number, res);
                return res;
            }
            else
            {
                St = State.Editing;
                return ed.DoEdit(j);
            }

        }
        //TODO:ПОНЯТЬ ЗАЧЕМ ЗДЕСЬ ЭТО НАДО И НАДО ЛИ ВООБЩЕ
        // private int acc()
        // {
        // return (int)Math.Round(ed.Acc() * Math.Log(Pin) /
        // Math.Log(Pout) + 0.5);
        // }
        // }
}
