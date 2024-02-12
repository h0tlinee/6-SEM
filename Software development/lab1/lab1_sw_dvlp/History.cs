using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Diagnostics.Tracing;

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
    List<Record> L = new List<Record>();
    //конструктор класса 
    public History()
    {
        String line;
        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("D:\\Programming\\C#\\Converter\\History.txt");
            //Read the first line of text
            line = sr.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                string[] subs = line.Split(' ');
                Record record = new(subs[0], subs[1], subs[2], subs[3]);
                L.Add(record);
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    // Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                    if (line == null) break;
                    subs = line.Split(' ');
                    record = new(subs[0], subs[1], subs[2], subs[3]);
                    L.Add(record);
                }
            }
            //close the file
            sr.Close();


        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }


    public void AddRecord(string p1, string p2, string n1, string n2)
    {
        Record record = new(p1, p2, n1, n2);
        L.Add(record);
        string record_string = p1 + ' ' + ' ' + p2 + ' ' + n1 + ' ' + n2;
        try
        {

            //Pass the filepath and filename to the StreamWriter Constructor
            StreamWriter sw = new StreamWriter("D:\\Programming\\C#\\Converter\\History.txt", true);
            //Write a line of text
            sw.WriteLine(record_string);
            //Close the file
            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }

    public void Clear()
    {
        L.Clear();
        File.WriteAllText("D:\\Programming\\C#\\Converter\\History.txt", String.Empty);
    }
    public int Count()
    {
        Console.WriteLine(L.Count());
        return L.Count();
    }
   public Record GetRecord(int num){
        if (L.Count()!=0 && L.Count()>num){
                return L[num];
        }
        Record record= new("unable to access","unable to access","unable to access","unable to access");
        return record;
        
   } 

}
