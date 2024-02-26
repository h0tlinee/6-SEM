#!/bin/sh
if [ "$#" -lt 1 ]; then #проверяем на кол-во переданных аргментов, мы же ищем директорию, значит надо передать имя
    echo "Недостаточно аргументов. Пожалуйста, передайте в качестве аргумента имя."
    exit 2
fi
fname=$(find / -type d -name "$1" 2>/dev/null)
if [ "$fname" = "" ]; then
    echo "Каталог не найден!"
    exit 2
fi
if [ "$(find / -type d -name "$1" 2>/dev/null | wc -l)" -ne 1 ]; then
    echo "Больше одного каталога с заданным именем!"
    exit 2
fi

echo "----КАТАЛОГИ----"
#echo "$fname"
ls -d -1 $fname/*/
echo "----ФАЙЛЫ----"
find $fname -type f -printf "%f %s %Cd-%Cb-%CY-%CH:%CM %n\n" | sort
