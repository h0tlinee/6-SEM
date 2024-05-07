#include <signal.h>
#include <wait.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>

int pipefd[2], PID_CUR;
FILE *file, out;
void handelr(int s) // обработчик сигнала kill
{
  printf("P2 was terminated\n"); // сообщаем о предварительном завершении P2 и закрываем файл и дескриптор записи
  fclose(file);
  close(pipefd[1]);
  exit(0);
}
int main(int argc, char *argv[])
{
  signal(SIGUSR2, handelr); // устанавливаем обработчик сигнала для SUGUSR2
  pipefd[0] = atoi(argv[1]); // перевод дескрипторов канала из char в int
  pipefd[1] = atoi(argv[2]);
  close(pipefd[0]);  // закрываем канал для чтения
  PID_CUR = getpid(); // получаем текущий PID

  file = fopen("data.txt", "r"); // открываем файл с данными
  if (file == NULL)       // ошибка при открытии файла
  {
    printf("Error: unable to open a file\n");
    exit(1);
  }

  char str_cur2[40] = "P2 was created"; // сообщение о создании потока
  write(pipefd[1], &PID_CUR, sizeof(int));
  write(pipefd[1], &str_cur2, sizeof(str_cur2));
  printf("Data was sent from P2 to channel\n"); // отправляем сообщение в канал

  char buffer[40];                  // буфер для данных
  while (fgets(buffer, sizeof(buffer), file) != NULL) // пока успешно читаем файл
  {
    write(pipefd[1], buffer, sizeof(buffer)); // отправляем текст в канал
    // printf("%s", buffer);

    write(pipefd[1], &PID_CUR, sizeof(int)); // также отправляем PID процесса
  }
  close(pipefd[1]);      // закрываем дескриптор для записи
  printf("P2 was finished\n"); // если времени хватило и P2 завершился сам, то сообщаем об этом
  exit(0);
}
