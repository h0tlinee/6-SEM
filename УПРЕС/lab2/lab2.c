#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

unsigned long long factorial(unsigned long long n)
{
    if (n == 0 || n == 1)
        return 1;
    else
        return n * factorial(n - 1);
}

int main()
{
    unsigned long long m, n;
    printf("Введите значения m и n: ");
    scanf("%llu %llu", &m, &n);

    if (m > n)
    {
        perror("m не может быть больше n\n");
        return 21;
    }

    pid_t pid1, pid2;
    pid1 = fork();
    pid2 = fork();

    if (pid1 == 0 && pid2 == 0)
    {
        // Код первого потомка
        unsigned long long res1 = factorial(n);
        printf("Первый потомок: n! = %llu\n", res1);
        // Код второго потомка
        unsigned long long res2 = factorial(n - m);
        printf("Второй потомок: (n-m)! = %llu\n", res2);

        exit(0);
    }
    else if (pid1 > 0 && pid2 > 0)
    {
        // Код родительского процесса
        wait(NULL);
        wait(NULL);

        unsigned long long result = factorial(n) / factorial(n - m);
        printf("Число размещений A(%llu, %llu) = %llu\n", m, n, result);
    }
    else if (pid1 < 0)
    {
        perror("Ошибка при создании первого потомка.\n");
        return 22;
    }
    else if (pid2 < 0)
    {
        perror("Ошибка при создании второго потомка.\n");
        return 23;
    }

    return 0;
}