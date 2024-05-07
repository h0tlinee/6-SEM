#include <signal.h>
#include <wait.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>

int pipefd[2], PID1, PID2, PID_CUR, p_number; // pipefd-дескрипторы чтения-записи,PID-1,2 процесс и текущий,p_number-номер процесса для вывода
char str_cur[40];                             // буффер для чтения
FILE *file, *out;                             // дескриптор файла с данными

int main(int argc, char *argv[])
{

  if (argc != 2) // неправильное кол-во аргументов
  {
    printf("Неправильное кол-во аргументов(введите одно число N)\n");
    exit(1);
  }

  if (pipe(pipefd) == 0) // создание программного канала
  {
    printf("K1 was created\n");
    printf("PID P0: %d\n", getpid());
  }
  else
  {
    exit(1);
    printf("Error: unable to create a channel!\n"); // возвращаем ошибку, если канал не получилось создать
  }

  PID1 = fork(); // создание первого процесса

  if (PID1 == -1) // ошибка при создании P1
  {
    exit(1);
    printf("Error: unable to create P1!\n");
  }
  // P1
  if (PID1 == 0) // код первого потомка P1(возвращается 0)
  {

    char pipes0[40]; // буфер для дескрипторов канала
    char pipes1[40];
    sprintf(pipes0, "%d", pipefd[0]); // перевод дескрипторов из int в char*
    sprintf(pipes1, "%d", pipefd[1]);
    execl("pid1", "pid1", pipes0, pipes1, argv[1], NULL); // вызов P1, передаем дескрипторы канала и N переданное в argv[1]
  }

  // P0
  close(pipefd[1]); // в основном процессе сразу закрываем канал для записи

  read(pipefd[0], &p_number, sizeof(int));
  read(pipefd[0], &str_cur, sizeof(str_cur));
  printf("%s (%d)\n", str_cur, p_number); // чтение информационных сообщений

  read(pipefd[0], &p_number, sizeof(int));
  read(pipefd[0], &str_cur, sizeof(str_cur));
  printf("%s (%d)\n", str_cur, p_number); // чтение информационных сообщений

  ssize_t bytes_read; // кол-во прочитанных байт
  char buffer[40];    // буфер

  out = fopen("out.txt", "w");
  fprintf(out, "(%d)\n", p_number);                                   // в начало файла выводим PID процесса
  while ((bytes_read = read(pipefd[0], &buffer, sizeof(buffer))) > 0) // пока чтение из канала успешно
  {
    if (bytes_read == 0) // при конце данных
    {
      printf("EOF\n");
      exit(1);
    }
    if (bytes_read == -1) // при ошибке
    {
      printf("ERR\n");
      exit(1);
    }
    read(pipefd[0], &p_number, sizeof(int)); // читаем PID процесса
    // printf("%s (%d)\n", buffer, p_number);
    // printf("%s", buffer);
    fprintf(out, "%s", buffer); // выводим прочитанные данные в файл
  }
  fprintf(out, "(%d)", p_number); // записываем PID P2 в конец вывода
  wait(NULL);                     // ожидание завершения процессов
  wait(NULL);
  fclose(out); // закрываем все дескрипторы
  close(pipefd[0]);
  printf("reading finished\n");
  printf("P0 process finished\n");
  printf("Received data in out.txt\n");
  return 0; // завершение программы
}