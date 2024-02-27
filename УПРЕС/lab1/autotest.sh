#!/bin/sh
echo "Запуск теста 1"
sh lab1.sh test2 >result1_r
if (diff result1_r result1); then
    echo "Тест 1: успех"
else
    echo "Тест 1: ошибка"
fi
echo "Запуск теста 2"
sh lab1.sh test1 >result2_r
if (diff result2_r result2); then
    echo "Тест 2: успех"
else
    echo "Тест 2: ошибка"
fi

echo "Запуск теста 3"
sh lab1.sh ыаы >result3_r
if (diff result3_r result3); then
    echo "Тест 3: успех"
else
    echo "Тест 3: ошибка"
fi
