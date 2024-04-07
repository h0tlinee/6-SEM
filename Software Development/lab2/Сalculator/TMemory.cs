namespace Сalculator
{
    class TMemory
    {
        TPNumber fNumber;//поле числа

        public enum FState { On, Off };//состояния памяти
        public FState St { get; set; }//сво-во для получения/задания состояния памяти

        //конструктор
        public TMemory()
        {
            fNumber = new TPNumber(0, 10, 5);
            St = FState.Off;
        }

        //сво-во для получения/задания числа
        public TPNumber FNumber
        {
            set
            {
                fNumber = value.Copy();
                St = FState.On;
            }
            get
            {
                St = FState.On;
                return fNumber.Copy();
            }
        }

        //сложение переданнгого числа и числа находящегося в памяти
        public void AddTPNumber(TPNumber number)
        {
            if (St == FState.Off)
            {
                FNumber = number;
                St = FState.On;
            }
            else
            {
                FNumber = fNumber + number;
            }
        }

        // Очистка памяти
        public void Clear()
        {
            fNumber = new TPNumber(0, 10, 5);
            St = FState.Off;
        }
    }
}
