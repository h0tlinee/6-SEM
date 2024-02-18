using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    class Control_
    {
        const int pin = 10;
        const int pout = 2;
        public History history = new History();
        public Editor editor = new Editor();
        public enum State {Редактирование, Преобразовано}
        public State St { get; set; }

        public Control_()
        {
            St = State.Редактирование;
            Pin = pin;
            Pout = pout;
        }

        public int Pin { get; set; }
        public int Pout { get; set; }
        
        public string DoCmnd(int j)
        {
            if (j == 19)
            {
                double r = Convert_p_10.Do(editor.Number, (Int16)Pin);
                string res = Convert_10_p.Do(r, (Int32)Pout, acc());
                St = State.Преобразовано;
                history.AddRecord(Pin, Pout, editor.Number, res);
                return res;
            }
            else
            {
                St = State.Редактирование;
                return editor.DoEdit(j);
            }
        }

        private int acc()
        {
            return (int)Math.Round(editor.Acc * Math.Log(Pin) /
            Math.Log(Pout) + 0.5);
        }
    }
}
