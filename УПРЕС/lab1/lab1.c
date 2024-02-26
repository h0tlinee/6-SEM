#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <dirent.h>
#include <sys/stat.h>
#include <time.h>
#include <linux/limits.h>

void print_directory_contents(const char *path)
{
    DIR *dir;
    struct dirent *entry;
    struct stat file_info;

    // Открытие каталога
    dir = opendir(path);
    if (dir == NULL)
    {
        perror("Ошибка открытия каталога");
        exit(EXIT_FAILURE);
    }

    // Печать имен каталогов
    while ((entry = readdir(dir)) != NULL)
    {
        char file_path[PATH_MAX];
        snprintf(file_path, sizeof(file_path), "%s/%s", path, entry->d_name);

        if (stat(file_path, &file_info) == 0 && strcmp(entry->d_name, ".") != 0 && strcmp(entry->d_name, ".."))
        {
            if (S_ISDIR(file_info.st_mode))
            {
                printf("%s (Каталог)\n", entry->d_name);
            }
        }
    }

    rewinddir(dir); // Сброс указателя чтения каталога

    // Печать имен файлов
    while ((entry = readdir(dir)) != NULL)
    {
        char file_path[PATH_MAX];
        snprintf(file_path, sizeof(file_path), "%s/%s", path, entry->d_name);

        if (stat(file_path, &file_info) == 0)
        {
            if (S_ISREG(file_info.st_mode))
            {
                time_t modified_time = file_info.st_mtime;
                struct tm *modified_tm = localtime(&modified_time);
                char modified_date[30];
                strftime(modified_date, sizeof(modified_date), "%d.%m.%Y %H:%M:%S", modified_tm);

                printf("%s (Файл, Длина: %ld, Дата изменения: %s, Ссылок: %lu)\n",
                       entry->d_name, file_info.st_size, modified_date, file_info.st_nlink);
            }
        }
    }

    closedir(dir); // Закрытие каталога
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