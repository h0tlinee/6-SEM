#!/bin/sh

#Стандартная работа программы
echo "Запуск теста 1"
./lab2 4 9 > res1_exp
if (diff res1_exp res1); then
    echo "Тест 1: успех"
else
    echo "Тест 1: ошибка"
fi

#Ошибка ввода данных
echo "Запуск теста 2"
./lab2 10 1 > res2_exp
if (diff res2_exp res2); then
    echo "Тест 2: успех"
else
    echo "Тест 2: ошибка"
fi

#Неправильное использование программы
echo "Запуск теста 3"
./lab2 > res3_exp
if (diff res3_exp res3); then
    echo "Тест 3: успех"
else
    echo "Тест 3: ошибка"
fi