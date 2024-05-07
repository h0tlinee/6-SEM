#include <signal.h>
#include <wait.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>

int PID1, PID2, pipefd[2], N;
int main(int argc, char *argv[])
{
  pipefd[0] = atoi(argv[1]); // перевод дескрипторов канала из char в int
  pipefd[1] = atoi(argv[2]);
  close(pipefd[0]); // закрываем канал для чтения
  PID1 = getpid();  // получить PID P1

  char str_cur1[40] = "P1 was created";
  write(pipefd[1], &PID1, sizeof(int));
  write(pipefd[1], &str_cur1, sizeof(str_cur1)); // в канал отправляем инф.сообщение
  printf("Data was sent from P1 to channel\n");

  PID2 = fork(); // создаем процесс P2

  if (PID2 == -1) // ошибка при создании P2
  {
    exit(1);
    printf("Error: unable to create P2!\n");
  }

  // код второго потомка(P2)
  if (PID2 == 0)
  {
    char pipes0[40]; // буферы для дескрипторов канала
    char pipes1[40];

    sprintf(pipes0, "%d", pipefd[0]); // перевод дескрипторов канала в char из int
    sprintf(pipes1, "%d", pipefd[1]);

    execl("pid2", "pid2", pipes0, pipes1, NULL); // вызываем выполнение P2
  }
  else
  // P1(родительский процесс)
  {
    N = atoi(argv[3]); // получаем задержку сигнала из argv
    close(pipefd[1]);  // закрываем канал для записи
    int i;
    sleep(N);                          // задерживаем выполнение P1 на N
    if ((i = kill(PID2, SIGUSR2)) < 0) // отправить сигнал на прекращение работы P2
    {
      printf("ERROR");
    };
    wait(NULL); // ожидаем завершения процесса P2

    printf("P1 was finished\n");
    exit(0);
  }
  return 0;
}