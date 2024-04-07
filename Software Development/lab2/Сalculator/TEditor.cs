namespace Сalculator
{
    class TEditor
    {
        private string number = "0";//поле числа
        private const string delim = ",";//разделитель
        private const string zero = "0";//ноль
        private bool negative = false;//отрицательность числа
        private bool real = false;//вещественное число

        //сво-во получения/задания числа 
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        //добавление символа
        private void AddDigit(int n)
        {
            string symbols = "0123456789ABCDEF";

            if (number == zero)
            {
                number = symbols[n].ToString();
            }
            else
            {
                number += symbols[n];
            }
        }

        //добваление нуля
        private void AddZero()
        {
            if (number != zero)
            {
                number += zero;
            }
        }

        //добавление раззделителя
        private void AddDelim()
        {
            if (!real)
            {
                real = true;
                number += delim;
            }
        }

        //изменение знака
        private void ChangeSign()
        {
            if (number == zero)
            {
                return;
            }
            else if (negative)
            {
                negative = false;
                number = number.Remove(0, 1);
            }
            else
            {
                negative = true;
                number = "-" + number;
            }
        }

        //удаление правого символа
        private void BackSpace()
        {
            if (number == zero)
            {
                return;
            }
            else
            {
                switch (number[number.Length - 1])
                {
                    case '-':
                        negative = false;
                        break;
                    case ',':
                        real = false;
                        break;
                    default:
                        break;
                }

                if (number.Length > 1)
                {
                    number = number.Remove(number.Length - 1, 1);
                }
                else
                {
                    number = zero;
                }
            }
        }

        //Очистка поля числа и обнуление парматеров
        private void Clear()
        {
            negative = false;
            real = false;
            number = zero;
        }

        //выполнение команды с кодом operation
        public string DoEdit(int operation)
        {
            if (operation == 0)
            {
                AddZero();
            }
            else if (operation >= 1 && operation <= 15)
            {
                AddDigit(operation);
            }
            else if (operation == 16)
            {
                ChangeSign();
            }
            else if (operation == 17)
            {
                AddDelim();
            }
            else if (operation == 18)
            {
                BackSpace();
            }
            else if (operation == 19 || operation == 20)
            {
                Clear();
            }

            return number;
        }
    }
}
