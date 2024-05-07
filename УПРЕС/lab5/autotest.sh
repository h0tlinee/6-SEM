#!/bin/bash

# Сравнение исходного файла data.txt и файла с результатом выполнения программы output.txt
file1=data.txt
file2=output.txt

# Проверка существования файлов
if [ ! -f $file1 ] || [ ! -f $file2 ]; then
  echo "Один из файлов не существует"
  exit 1
fi

# Сравнение файлов построчно
diff -q $file1 $file2

# Получение кода выхода diff
diff_result=$?

# Вывод результата
if [ $diff_result -eq 0 ]; then
  echo "Автотест пройден"
else
  echo "Ошибка. Автотест не пройден!!!"
fi