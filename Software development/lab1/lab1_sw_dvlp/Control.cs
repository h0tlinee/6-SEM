using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Control
    {

        //Основание системы сч. исходного числа.
        int pin = 10;
        //Основание системы сч. результата.
        int pout = 16;
        //Число разрядов в дробной части результата.
        const int accuracy = 10;
        public History his = new History();
        public enum State { Editing, Converted }
        //Свойство для чтения и записи состояние Конвертера.
        public State St { get; set; }
        double DecNumber = 0;
        
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
        public int Pin
        {
            get
            {
                return pin;
            }
            set
            {
                pin = value;
            }
        }

        //Свойство для чтения и записи основание системы сч. р2.
        public int Pout
        {
            get
            {
                return pout;
            }
            set
            {
                pout = value;
            }
        }


        public string Convert()
        {
            if (ed.Number == "")
            {
                throw new Exception("Поле ввода пустое");
            }
            DecNumber=Convert_p_10.Do(ed.Number,pin);
            string result = Convert_10_p.Do(DecNumber, pout, acc());
            his.AddRecord(new Record(pin.ToString(), pout.ToString(), ed.Number, result));
            return result;
        }
        //Выполнить команду конвертера.
        
        //TODO:ПОНЯТЬ ЗАЧЕМ ЗДЕСЬ ЭТО НАДО И НАДО ЛИ ВООБЩЕ
        private int acc()
        {
         return (int)Math.Round(ed.Acc() * Math.Log(Pin) /
        Math.Log(Pout) + 0.5);
        }
        // }
    }
}
