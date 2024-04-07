namespace Сalculator
{
    class TProc
    {
        private TPNumber lopRes;/// Левый операнд и результат

        /// Свойство для получения и установки значения левого операнда и результата
        public TPNumber LopRes
        {
            get{ return lopRes.Copy(); }
            set{ lopRes = value.Copy(); }
        }
        
        private TPNumber rop;/// Правый операнд

        /// Свойство для получения и установки значения правого операнда
        public TPNumber Rop
        {
            get{ return rop.Copy(); }
            set{ rop = value.Copy(); }
        }

        public enum TOper { None = 21, Add = 25, Sub = 26, Mul = 27, Dvd = 28 };/// Набор двухоперадовых операций

        /// Свойство для получения и установки текущей операции
        public TOper Oper { get; set; }

        public enum TFunc { Rev = 30, Sqr = 29 };/// Набор однооперандовых функций

        /// Свойство для получения текущей функции
        public TFunc Func { get; set; }

        /// Конструктор
        public TProc()
        {
            lopRes = new TPNumber(0, 10, 8);
            rop = new TPNumber(0, 10, 8);
            Oper = TOper.None;
        }

        /// Состояние процессора по умолчанию
        public void ResetTProc()
        {
            lopRes = new TPNumber(0, 10, 8);
            rop = new TPNumber(0, 10, 8);
            Oper = TOper.None;
        }

        /// Сброс операции
        public void ResetTOper()
        {
            Oper = TOper.None;
        }

        /// Вычислить двухоперандовую операцию. Результат сохранить в левый операнд
        public void CalcOper()
        {
            switch (Oper)
            {
                case TOper.None:
                    break;
                case TOper.Add:
                    lopRes += rop;
                    break;
                case TOper.Sub:
                    lopRes -= rop;
                    break;
                case TOper.Mul:
                    lopRes *= rop;
                    break;
                case TOper.Dvd:
                    lopRes /= rop;
                    break;
            }
        }

        /// Вычислить значение функции для правого операнда. Результат сохранить в правый операнд
        public void CalcFunc()
        {
            switch (Func)
            {
                case TFunc.Sqr:
                    rop = rop.Sqr();
                    break;
                case TFunc.Rev:
                    rop = rop.Rev();
                    break;
            }
        }
    }
}
