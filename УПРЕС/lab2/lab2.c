#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

#define N 100

unsigned long long factorial(unsigned long long n)
{
    if (n == 0 || n == 1)
        return 1;
    else
        return n * factorial(n - 1);
}

int main(int argc, char *argv[])
{
    if (argc != 3)
    {
        printf("Использование: %s <число m> <число n>\n", argv[0]);
        return -1;
    }
    FILE *finter;
    char buff_of_string[N];
    finter = fopen("data_for_parent.txt", "w");
    unsigned long long m = atoi(argv[1]);
    unsigned long long n = atoi(argv[2]);

    if (m > n)
    {
        printf("Ошибка ввода данных. m не может быть больше n.\n");
        return 20;
    }

    pid_t pid1, pid2;
    pid1 = fork();

    if (pid1 == 0)
    {
        // Код первого потомка
        unsigned long long res1 = factorial(n);
        fprintf(finter, "process: %d parent process: %d result: %llu\n", getpid(), getppid(), res1);
        return 0;
    }
    else if (pid1 > 0)
    {
        pid2 = fork();

        if (pid2 == 0)
        {
            // Код второго потомка
            unsigned long long res2 = factorial(n - m);
            fprintf(finter, "process: %d parent process: %d result: %llu\n", getpid(), getppid(), res2);
            return 0;
        }
        else if (pid2 > 0)
        {
            // Код родительского процесса
            wait(NULL);
            wait(NULL);

            fclose(finter);

            finter = fopen("data_for_parent.txt", "r");

            unsigned long long n_factorial, n_m_factorial, result;

            while (fgets(buff_of_string, sizeof(buff_of_string), finter))
            {
                if (sscanf(buff_of_string, "process: %*d parent process: %*d result: %llu", &n_factorial) == 1)
                {
                    fgets(buff_of_string, sizeof(buff_of_string), finter);
                    if (sscanf(buff_of_string, "process: %*d parent process: %*d result: %llu", &n_m_factorial) == 1)
                    {
                        result = n_factorial / n_m_factorial;
                        printf("Результат вычисления числа размещений из %llu элементов по %llu: %llu\n", n, m, result);
                    }
                }
            }

            fclose(finter);
        }
        else
        {
            printf("Ошибка при создании второго потомка.\n");
            return 21;
        }
    }
    else
    {
        printf("Ошибка при создании первого потомка.\n");
        return 22;
    }

    return 0;
}