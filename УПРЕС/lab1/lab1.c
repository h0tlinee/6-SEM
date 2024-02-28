#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <dirent.h>
#include <sys/stat.h>
#include <time.h>
#include <linux/limits.h>

int cmp(const void *a, const void *b) // функция сравнения для qsort, в алфавитном порядке
{
    const struct dirent *first_dirent = *(const struct dirent **)a;
    const struct dirent *second_dirent = *(const struct dirent **)b;
    return strcmp(first_dirent->d_name, second_dirent->d_name);
}

void print_directory_contents(const char *path)
{
    DIR *dir;                // дескриптор каталога
    struct dirent *entry;    // структура для работы с каталогами
    struct stat file_info;   // структура для помещения информации stat
    struct dirent *buf[100]; // буфер для файлов, для посл. сортировки
    int i = 0;
    // Открытие каталога
    dir = opendir(path);
    if (dir == NULL) // если каталог не получилось открыть
    {
        perror("Ошибка открытия каталога");
        exit(EXIT_FAILURE);
    }

    // Печать имен каталогов
    while ((entry = readdir(dir)) != NULL)
    {
        char file_path[PATH_MAX];
        snprintf(file_path, sizeof(file_path), "%s/%s", path, entry->d_name);

        if (stat(file_path, &file_info) == 0 && strcmp(entry->d_name, ".") != 0 && strcmp(entry->d_name, "..")) // исключаем . и ..
        {
            if (S_ISDIR(file_info.st_mode)) // если каталог, то печатаем
            {
                printf("%s (Каталог)\n", entry->d_name);
            }
            else // если остальное, то в буфер
            {
                buf[i] = entry;
                i++;
            }
        }
    }
    qsort(buf, i, sizeof(struct dirent *), cmp); // сортировка
    for (int j = 0; j < i; j++)
    {
        stat(buf[j]->d_name, &file_info); // получаем информацию
        time_t modified_time = file_info.st_mtime;
        struct tm *modified_tm = localtime(&modified_time);
        char modified_date[30];
        strftime(modified_date, sizeof(modified_date), "%d.%m.%Y %H:%M:%S", modified_tm); // сложное форматирование времени
        printf("%s (Файл, Длина: %ld, Дата изменения: %s, Ссылок: %lu)\n",
               buf[j]->d_name, file_info.st_size, modified_date, file_info.st_nlink);
    }

    rewinddir(dir); // Сброс указателя чтения каталога
    closedir(dir);  // Закрытие каталога
}

int main(int argc, char *argv[])
{
    if (argc != 2)
    {
        printf("Использование: %s <каталог>\n", argv[0]);
        return -1;
    }

    const char *directory = argv[1];
    print_directory_contents(directory);

    return 0;
}
