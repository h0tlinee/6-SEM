using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Converter;

Control cnt=new Control();
cnt.Pin = 10;
cnt.Pout = 8;

System.Console.WriteLine(cnt.ed.Number);
System.Console.WriteLine(cnt.ed.AddDigit('1'));
System.Console.WriteLine(cnt.ed.AddDigit('5'));
System.Console.WriteLine(cnt.ed.ChangeSign());
System.Console.WriteLine(cnt.ed.AddDelim());
System.Console.WriteLine(cnt.ed.AddDigit('5'));
System.Console.WriteLine(cnt.ed.AddDigit('6'));
System.Console.WriteLine(cnt.ed.AddDigit('6'));




System.Console.WriteLine(cnt.Convert());
