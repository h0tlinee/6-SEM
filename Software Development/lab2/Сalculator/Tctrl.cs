using System;

namespace Сalculator
{
    internal class TCtrl
    {
        /// Набор состояний калькулятора
        public enum TCtrlState { cStart, cEditing, cExpDone, cOpDone, cValDone, cOpChange, cFuncDone, cError }

        /// Свойство для получения и установки значений состояний калькулятора
        public TCtrlState CtrlSt { get; set; }

        /// Объект класса Редактор
        private TEditor editor;

        /// Объект класса Процессор
        private TProc proc;

        /// Объект класса Память
        private TMemory memory;

        ///Объект класса История
        public History history=new History();

        /// Объект класса p-ичное число
        internal TPNumber pNumber;

        /// Максимальная длина целой части числа
        private const int MAXINTLEN = 12;

        /// Максимальная длина дробной части числа
        private const int MAXFRACTLEN = 8;

        /// Конструктор
        public TCtrl()
        {
            CtrlSt = TCtrlState.cStart;
            editor = new TEditor();
            proc = new TProc();
            memory = new TMemory();
            pNumber = new TPNumber(0, 10, 8);
        }


        /// Выполнить команду калькулятора
        public void DoCommandCalculator(int command)
        {
            string editorResult;
            TPNumber procResult;

            // Обработка ввода и редактирования числа
            if (command <= 19)
            {
                editorResult = DoCommandEditor(command);
                if (CheckLenNumber(editorResult))
                {
                    throw new Exception("Превышена максимально возможная длина числа");
                }
                else
                {
                    CtrlSt = TCtrlState.cValDone;
                    pNumber.StrNumber = editorResult;
                }
            }
            // Обработка команды для кнопки C
            else if (command == 20)
            {
                ResetTCtrl();
            }
            // Обработка взаимодействий с памятью
            else if (command >= 21 && command <= 24)
            {
                DoCommandMemory(command);
                if (CheckLenNumber(pNumber.StrNumber))
                {
                    throw new Exception("Превышена максимально возможная длина числа");
                }
            }
            // Обратобка ввода операций
            else if (command >= 25 && command <= 28)
            {
                procResult = DoProcOper(command);
                if (CtrlSt == TCtrlState.cError)
                {
                    throw new Exception("Деление на ноль невозможно");
                }
                else if (CheckLenNumber(procResult.StrNumber))
                {
                    throw new Exception("Превышена максимально возможная длина числа");
                }
                //else if (isZero(procResult))
                //{
                    //throw new Exception("Слишком маленькое число");
                //}
                else
                {
                    pNumber = procResult;
                }
            }
            // Обратобка ввода функций
            else if (command == 29 || command == 30)
            {
                procResult = DoProcFunc(command);
                if (CtrlSt == TCtrlState.cError)
                {
                    throw new Exception("Деление на ноль невозможно");
                }
                else if (CheckLenNumber(procResult.StrNumber))
                {
                    throw new Exception("Превышена максимально возможная длина числа");
                }
                else if (isZero(procResult))
                {
                    throw new Exception("Слишком маленькое число");
                }
                else
                {
                    CtrlSt = TCtrlState.cFuncDone;
                    pNumber = procResult;
                }
            }
            // Обработка вычисления всего выражения
            else if (command == 31)
            {
                procResult = CalcExpr();
                if (CtrlSt == TCtrlState.cError)
                {
                    throw new Exception("Деление на ноль невозможно");
                }
                else if (CheckLenNumber(procResult.StrNumber))
                {
                    throw new Exception("Превышена максимально возможная длина числа");
                }
                //else if (isZero(procResult))
                //{
                    //throw new Exception("Слишком маленькое число");
                //}
                else
                {
                    CtrlSt = TCtrlState.cExpDone;
                    pNumber = procResult;
                }
            }
        }


        /// Выполнить команду редактора
        private string DoCommandEditor(int command)
        {
            // Уставнавливаем состояние калькулятора на редактирование числа
            CtrlSt = TCtrlState.cEditing;
            string result = editor.DoEdit(command);
            return result;
        }


        /// Выполнение команды процессора
        private TPNumber DoProcOper(int operation)
        {
            TPNumber result = new TPNumber(0, 10, 8);
            // Если на предыдущем шаге не определяли операцию
            if (CtrlSt != TCtrlState.cOpChange)
            {
                // Если операция не задана или на предыдушем шаге было вычислено выражение 
                if (proc.Oper == TProc.TOper.None || CtrlSt == TCtrlState.cExpDone)
                {
                    proc.LopRes = pNumber;
                    DoCommandEditor(20);
                    proc.Oper = (TProc.TOper)operation;
                    CtrlSt = TCtrlState.cOpChange;
                }
                else
                {
                    // Если для правого ввели значение или на предыдущем шаге была вычислена функция
                    if (CtrlSt == TCtrlState.cValDone || CtrlSt == TCtrlState.cFuncDone)
                    {
                        proc.Rop = pNumber;
                        DoCommandEditor(20);

                        if (CheckDivZero(proc.Rop))
                        {
                            return new TPNumber(0, 10, 8);
                        }
                        
                        var number1 = proc.LopRes.StrNumber;
                        var p1 = proc.LopRes.StrP;
                        var oper = proc.Oper;
                        proc.CalcOper();
                        
                        history.AddRecord(p1, proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.LopRes.StrNumber, oper.ToString());

                        proc.Oper = (TProc.TOper)operation;
                        CtrlSt = TCtrlState.cOpChange;
                    }
                }
            }
            else
            {
                proc.Oper = (TProc.TOper)operation;
                CtrlSt = TCtrlState.cOpChange;
            }

            result = proc.LopRes;
            return result;
        }


        // Переменная для хранения в каком операнде хранится результат функции
        static string operResFunc = "";


        /// Вычисление функции процессора
        private TPNumber DoProcFunc(int func)
        {
            TPNumber defaultValue = new TPNumber(0, 10, 8);
            TPNumber result = new TPNumber(0, 10, 8);
            proc.Func = (TProc.TFunc)func;

            // Если в левом операнде установлено значение по умолчанию
            if (proc.LopRes == defaultValue)
            {
                proc.LopRes = pNumber;

                if (CheckDivZero(proc.LopRes))
                {
                    return new TPNumber(0, 10, 8);
                }

                swapOperands();
               
                Console.WriteLine(proc.Rop.StrNumber);
                Console.WriteLine(proc.Func);
                var number1 = proc.Rop.StrNumber;
               
                var operation = proc.Oper;
                proc.CalcFunc();
                history.AddRecordOne(proc.Rop.StrP,number1,proc.Rop.StrNumber,proc.Func.ToString());
                Console.WriteLine(proc.Rop.StrNumber);
                swapOperands();
                operResFunc = "LopRes";
                result = proc.LopRes;
            }
            else
            {
                // Если в левом и в правом операнде установлено некоторое значение
                if (proc.Rop != defaultValue)
                {
                    // Если ввели новое значение для правого операнда
                    if (CtrlSt == TCtrlState.cValDone)
                    {
                        proc.Rop = pNumber;
                        DoCommandEditor(20);

                        if (CheckDivZero(proc.Rop))
                        {
                            return new TPNumber(0, 10, 8);
                        }
                        var number1 = proc.Rop.StrNumber;
                        var operation = proc.Oper;
                        proc.CalcFunc();
                        history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                        operResFunc = "Rop";
                        result = proc.Rop;
                    }

                    // Если было вычислено выражение, и в левом операнде хранится результат
                    if (CtrlSt == TCtrlState.cExpDone)
                    {
                        if (CheckDivZero(proc.LopRes))
                        {
                            return new TPNumber(0, 10, 8);
                        }

                        swapOperands();
                        var number1 = proc.Rop.StrNumber;
                        var operation = proc.Oper;
                        proc.CalcFunc();
                        history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                        swapOperands();

                        operResFunc = "LopRes";
                        result = proc.LopRes;
                    }

                    // Если было вычислена функция, и в левом / правом операнде хранится результат
                    if (CtrlSt == TCtrlState.cFuncDone)
                    {
                        // Если результат вычисления функции сохранен в левом операнде
                        if (operResFunc == "LopRes")
                        {
                            if (CheckDivZero(proc.LopRes))
                            {
                                return new TPNumber(0, 10, 8);
                            }

                            swapOperands();
                            var number1 = proc.Rop.StrNumber;
                            var operation = proc.Oper;
                            proc.CalcFunc();
                            history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                            swapOperands();

                            operResFunc = "LopRes";
                            result = proc.LopRes;
                        }
                        // Если результат вычисления функции сохранен в правом операнде
                        else
                        {
                            if (CheckDivZero(proc.LopRes))
                            {
                                return new TPNumber(0, 10, 8);
                            }
                            var number1 = proc.Rop.StrNumber;
                            var operation = proc.Oper;
                            proc.CalcFunc();
                            history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                            operResFunc = "Rop";
                            result = proc.Rop;
                        }
                    }
                }
                // Если в левом операнде установлено некоторое значение, а в правом - значение по умолчанию
                else
                {
                    // Если ввели значение для правого операнда
                    if (CtrlSt == TCtrlState.cValDone)
                    {
                        proc.Rop = pNumber;
                        DoCommandEditor(20);

                        if (CheckDivZero(proc.Rop))
                        {
                            return new TPNumber(0, 10, 8);
                        }
                        var number1 = proc.Rop.StrNumber;
                        var operation = proc.Oper;
                        proc.CalcFunc();
                        history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                        operResFunc = "Rop";
                        result = proc.Rop;
                    }
                    else
                    {
                        // Если а в правом установлено значение по умолчанию
                        if (CheckDivZero(proc.LopRes))
                        {
                            return new TPNumber(0, 10, 8);
                        }

                        swapOperands();
                        var number1 = proc.Rop.StrNumber;
                        var operation = proc.Oper;
                        proc.CalcFunc();
                        history.AddRecordOne(proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.Func.ToString());
                        swapOperands();

                        operResFunc = "LopRes";
                        result = proc.LopRes;
                    }
                }
            }
            return result;
        }


        /// Вычисление выражение
        private TPNumber CalcExpr()
        {
            // Если выражение не было вычисленно ранее
            if (CtrlSt != TCtrlState.cExpDone)
            {
                // Если ввели значение для правого операнда
                if (CtrlSt == TCtrlState.cValDone)
                {
                    proc.Rop = pNumber;
                    
                }
                // Если хотим сложить результат с самим собой
                else if (CtrlSt == TCtrlState.cOpChange)
                {
                    
                    proc.Rop = proc.LopRes;
                    
                }
                
                // Проверка деления на ноль
                if (CtrlSt != TCtrlState.cFuncDone && CheckDivZero(proc.Rop))
                {
                    return new TPNumber(0, 10, 8);
                }
                var number1 = proc.LopRes.StrNumber;
                var p1 = proc.LopRes.StrP;
                var operation = proc.Oper;
             
                proc.CalcOper();
                
               
                history.AddRecord(p1,proc.Rop.StrP,number1,proc.Rop.StrNumber,proc.LopRes.StrNumber,operation.ToString());
                return proc.LopRes;
            }
            else
            {
                var number1 = proc.LopRes.StrNumber;
                var p1 = proc.LopRes.StrP;
                var operation = proc.Oper;
                // Повторное выполнение последней операции

                proc.CalcOper();

                history.AddRecord(p1, proc.Rop.StrP, number1, proc.Rop.StrNumber, proc.LopRes.StrNumber, operation.ToString());
                return proc.LopRes;
            }
        }


        /// Поменять операнды местами
        private void swapOperands()
        {
            TPNumber tempPNumber = proc.LopRes;
            proc.LopRes = proc.Rop;
            proc.Rop = tempPNumber;
        }


        /// Проверка является ли число нулем
        private bool isZero(TPNumber number)
        {
            return number.DecimalNumber == 0;
        }


        /// Проверка деления на ноль
        private bool CheckDivZero(TPNumber number)
        {
            if (proc.Func == TProc.TFunc.Rev && isZero(number) ||
                proc.Oper == TProc.TOper.Dvd && isZero(number))
            {
                CtrlSt = TCtrlState.cError;
                return true;
            }

            return false;
        }


        /// Получить точность числа
        public int GetAccuracy(string number)
        {
            int indexDelim = number.IndexOf(',');

            if (indexDelim == -1)
            {
                return 0;
            }

            return number.Length - indexDelim - 1;
        }


        /// Проверка длины числа
        private bool CheckLenNumber(string number)
        {
            int fractLen = GetAccuracy(number);
            int intLen = number.Length - fractLen - 1;
            if (intLen > MAXINTLEN || fractLen > MAXFRACTLEN)
            {
                CtrlSt = TCtrlState.cError;
                return true;
            }

            return false;
        }


        /// Выполнить команду памяти
        private void DoCommandMemory(int command)
        {
            switch(command)
            {
                case 21:
                    memory.Clear();
                    break;
                case 22:
                    pNumber = memory.FNumber;
                    CtrlSt = TCtrlState.cValDone;
                    break;
                case 23:
                    memory.FNumber = pNumber;
                    break;
                case 24:
                    var number1 = memory.FNumber.StrNumber;

                    memory.AddTPNumber(pNumber);
                    history.AddRecord(memory.FNumber.StrP, pNumber.StrP, number1, pNumber.StrNumber, memory.FNumber.StrNumber, "Add");
                    break;
            }
        }


        /// Получить состояние пямяти
        public string GetMemoryState()
        {
            return memory.St.ToString();
        }


        /// Сбросить введенные параметры
        private void ResetTCtrl()
        {
            DoCommandEditor(20);
            CtrlSt = TCtrlState.cStart;
            pNumber = new TPNumber(0, 10, 8);
            proc.ResetTProc();
        }
    }
}
