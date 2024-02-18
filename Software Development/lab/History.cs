using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public struct Record
    {
        int p1;
        int p2;
        string number1;
        string number2;
        public Record(int p1, int p2, string n1, string n2)
        {
            this.p1 = p1;
            this.p2 = p2;
            number1 = n1;
            number2 = n2;
        }
        public override string ToString()
        {
            return String.Format("{0} (Основание: {1}) = {2} (Основание: {3}){4}", number1, p1, number2, p2, Environment.NewLine);
        }
    }

    class History
    {
        List<Record> L;

        public History()
        {
            L = new List<Record>();
        }

        public void AddRecord(int p1, int p2, string n1, string n2)
        {
            Record record = new Record(p1, p2, n1, n2);
            L.Add(record);
        }

        public void Clear()
        {
            L.Clear();
        }

        public int Count()
        {
            return L.Count();
        }

        public string GetRecord(int num)
        {
            string result = "";
            foreach (Record record in L)
            {
                result += record + "\n";
            }
            return result;
        }
    }
}
