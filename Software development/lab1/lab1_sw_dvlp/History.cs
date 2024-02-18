using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Diagnostics.Tracing;



namespace Converter
{ 
public struct Record
{
    public string p1;
    public string p2;
    public string number1;
    public string number2;
    public Record(string p_1, string p_2, string n1, string n2)
    {
        p1 = p_1;
        p2 = p_2;
        number1 = n1;
        number2 = n2;
    }


}
    public class History
    {
        private List<Record> records;
        //конструктор класса 
        public History()
        {
            records = new List<Record>();
        }

        public void AddRecord(Record record)
        {
            records.Add(record);
        }

        public void Clear()
        {
            records.Clear();
        }
        public int Count()
        {
            Console.WriteLine(records.Count());
            return records.Count();
        }
        public Record GetRecord(int num)
        {
            if (records.Count() != 0 && records.Count() > num)
            {
                return records[num];
            }
            Record record = new("unable to access", "unable to access", "unable to access", "unable to access");
            return record;

        }
    }
}
