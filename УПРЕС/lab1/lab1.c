#define _GNU_SOURCE
#include <sys/stat.h>
#include <sys/types.h>
#include <dirent.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <errno.h>

int found = 0;                        // для отслеживания успеха, т.к. поиск рекурсивный и возвращать правильное значение тяжело
int cmp(const void *a, const void *b) // функция сравнения для qsort, в алфавитном порядке
{
    const struct dirent *first_dirent = *(const struct dirent **)a;
    const struct dirent *second_dirent = *(const struct dirent **)b;
    return strcmp(first_dirent->d_name, second_dirent->d_name);
}

void listFilesRecursively(char *basePath, char *goal) // рекурсивный поиск, принимает каталог откуда поиск начинается и цель поиска
{

    char path[1000];              // буфер для того, чтобы формировать путь к каталогу
    struct dirent *dp;            // структура для работы с каталогами
    DIR *dir = opendir(basePath); // дескриптор каталога

    // если каталог не получилось открыть, то возращаемся на уровень выше
    if (!dir)
    {
        return;
    }

    while ((dp = readdir(dir)) != NULL) // пока каталог получилось успешно прочитать
    {
        if (strcmp(dp->d_name, goal) == 0) // если мы нашли заданный каталог
        {
            found = 1;               // цель найдена
            struct dirent *buf[100]; // буфер для файлов, для посл. сортировки
            struct stat st;          // структура для помещения информации stat
            struct tm *timeinfo;     // структура для получения времени изменения файла
            char buff[20];           // буфер для преобразования tm в строку и форматирования

            strcat(basePath, "/");
            strcat(basePath, dp->d_name); // прибавляем к пути найденный каталог
            strcat(basePath, "/");

            printf("%s  %s\n", "Заданный каталог:", dp->d_name);
            printf("%s  %s\n\n", "Путь:", basePath); // вывод информации
            dir = opendir(basePath);                 // открыли найденный каталог
            printf("%s\n", "Файлы в каталоге:");
            int i = 0;
            while ((dp = readdir(dir)) != NULL)
            {
                if (strcmp(dp->d_name, ".") != 0 && strcmp(dp->d_name, "..") != 0) // исключаем . и ..
                {
                    char a[100];
                    strcpy(a, basePath);
                    strcat(a, dp->d_name); // в этих 3 строках мы получаем полный путь, иначе, если файл не в одном каталоге, мы его не прочитаем
                    if (stat(a, &st) == 0) // если удалось получить информацию о файле
                    {
                        if ((st.st_mode & S_IFDIR) == S_IFDIR) // смотрим, является ли файл каталогом по битовой маске
                        {
                            printf("%s\n", dp->d_name);
                        }
                        else
                        {
                            buf[i] = dp; // если не каталог, то заносим в буфер, для сортировки по имени
                            i++;
                        }
                    }
                    else
                    {
                        printf("%s\n", strerror(errno));
                        printf("%s\n", "Не удалось получить информацию о файле! Возможно, необходимы права поиска во всех каталогах в полном пути к файлу");
                    }
                }
            }
            qsort(buf, i, sizeof(struct dirent *), cmp); // сортировка

            for (int j = 0; j < i; j++) // вывод информации о файлах
            {
                stat(buf[j]->d_name, &st);
                timeinfo = localtime(&st.st_mtim);
                strftime(buff, 20, "%b %d %H:%M", timeinfo);
                printf("%s  %lu  %lu %s\n", (buf[j]->d_name), st.st_size, st.st_nlink, buff);
                // printf("%s\n", buf[j]->d_name);
            }
            printf("\n");
            closedir(dir);
            return;
        }

        if (strcmp(dp->d_name, ".") != 0 && strcmp(dp->d_name, "..") != 0)
        {
            strcpy(path, basePath); // если не нашли каталог, то формируем новый путь и ищем дальше рекурсией
            strcat(path, "/");
            strcat(path, dp->d_name);
            listFilesRecursively(path, goal);
        }
    }
    closedir(dir);
    return;
}

int main(int argc, char *argv[])
{

    if (argc != 2)
    {
        printf("Программа принимает только 1 аргумент(искомый каталог)");
        return 1;
    }
    char *goal = argv[1];
    listFilesRecursively("/home", goal);
    // printf("%d\n", found);
    if (found != 1)
    {
        printf("%s\n", "Заданный каталог не был найден!");
        return 1;
    }
    return 0;
}
