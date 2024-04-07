using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Сalculator
{
    ///структура записи истории
    public struct Record
    {
        string p1;
        string p2;
        string number1;
        string number2;
        string operation;
        string res;
        public Record(string p1, string p2,string number1,string number2,string res,string operation)//конструктор
        {
            this.p1 = p1;
            this.p2 = p2;
            this.number1 = number1;
            this.number2 = number2;
            this.res = res;
            switch (operation)
            {
                case "Add":
                    this.operation = "+";
                    break;
                case "Sub":
                    this.operation = "-";
                    break;
                case "Mul":
                    this.operation = "*";
                    break;
                case "Div":
                    this.operation = "/";
                    break;
                default:
                    this.operation = operation;
                    break;
            }
        }
        public override string ToString()//формат. в строку
        {
            if(this.p2!="")
                return String.Format("{0}({1}) {2} {3}({4}) = {5}({6}){7}",number1,p1,operation,number2,p2,res,p1, Environment.NewLine);
            else 
                return String.Format("{0}({1}) {2} = {3}{4}",number1,p1,operation,res,Environment.NewLine);
        }

    }
    
    internal class History
    {
       List <Record> records;

        public History()
        {
            records = new List <Record>();
        }
        public void AddRecord(string p1,string p2,string number1,string number2,string res,string operation)
        {
            Record record=new Record(p1, p2, number1, number2, res,operation);
            records.Add(record);
        }

        public void AddRecordOne(string p1,string number1,string res,string operation)
        {
            Record record = new Record(p1, "", number1, "", res, operation);
            records.Add(record);
        }
        public void Clear()
        {
            records.Clear();
        }

        public int Count()
        {
            return records.Count;
        }

        public string GetRecord()
        {
            string result = "";
            foreach (Record record in records)
            {
                result+= record.ToString()+"\n";
            }
            return result;
        }

    }
}
