#!/bin/bash

# Компилируем программу на C
gcc lab2.c -o test

# Функция для вычисления факториала числа
factorial() {
  if [ $1 -eq 0 ]; then
    echo 1
  else
    echo "$(($1 * $(factorial $(($1 - 1)))))"
  fi
}

# Запускаем тесты
for n in {1..5}; do
  for m in {0..4}; do
    result=$(./test $m $n)
    factorial_n=$(factorial $n)
    factorial_n_m=$(factorial $(($n - $m)))
    expected=$((factorial_n / factorial_n_m))

    echo "Testing A($m,$n): Expected $expected, Got $result"

    if [ $result -eq $expected ]; then
      echo "Test Passed!"
    else
      echo "Test Failed!"
    fi
  done
done

# Очищаем после себя скомпилированный файл
rm test