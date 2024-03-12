#!/bin/bash

# Компилируем программу на C
gcc -o test lab2.c

# Функция для вычисления факториала числа
factorial() {
  if [ $1 -eq 0 ] || [ $1 -eq 1 ]; then
    echo 1
  else
    echo "$(($1 * $(factorial $(($1 - 1)))))"
  fi
}

# Запускаем тесты
for m in {0..5}; do
  for n in {1..6}; do
    result=$(./test $m $n)
    factorial_n=$(factorial $n)
    factorial_nm=$(factorial $(($n - $m)))
    
    expected=$((factorial_n / factorial_nm))
    
    if [ $result -eq $expected ]; then
      echo "Тест для m=$m, n=$n пройден успешно. Ожидаемый результат: $expected, Фактический результат: $result"
    else
      echo "Тест для m=$m, n=$n не пройден. Ожидаемый результат: $expected, Фактический результат: $result"
    fi
  done
done