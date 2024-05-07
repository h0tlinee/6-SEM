#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <sys/ipc.h>
#include <sys/msg.h>
#include <sys/sem.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/wait.h>

// Максимальный размер строки для стихотворения
#define MAX_SIZE 100

// Структура для сообщения
struct msgbuf
{
  long mtype;           // Тип сообщения
  char mtext[MAX_SIZE]; // Текст сообщения
};

// Структура для семафора
union semun
{
  int val;
  struct semid_ds *buf;
  unsigned short *array;
};

// Операции для работы с семафорами
struct sembuf acquire_op = {0, -1, SEM_UNDO};
struct sembuf release_op = {0, 1, SEM_UNDO};

int main()
{
  key_t key = ftok("data.txt", 'A');         // Создание IPC ключа
  int msgid = msgget(key, IPC_CREAT | 0666); // Создание новой очереди сообщений
  if (msgid == -1)
  {
    perror("Ошибка создания очереди сообщений");
    exit(20);
  }

  FILE *fp = fopen("data.txt", "r");
  if (fp == NULL)
  {
    perror("Ошибка открытия файла");
    exit(21);
  }

  int semid = semget(key, 1, IPC_CREAT | 0666); // Создание семафора
  if (semid == -1)
  {
    perror("Ошибка создания семафора");
    exit(22);
  }

  union semun sem_arg;
  sem_arg.val = 1;
  if (semctl(semid, 0, SETVAL, sem_arg) == -1)
  {
    perror("Ошибка установки начального значения семафора");
    exit(23);
  }

  for (int i = 0; i < 4; i++)
  {
    pid_t pid = fork();
    if (pid == -1)
    {
      perror("Ошибка создания дочерних процессов");
      exit(24);
    }
    if (pid == 0)
    {
      // Код для дочернего процесса
      struct msgbuf message;
      int line_number = i + 1;
      while (fgets(message.mtext, MAX_SIZE, fp) != NULL)
      {
        semop(semid, &acquire_op, 1);
        message.mtype = line_number;
        msgsnd(msgid, &message, sizeof(message.mtext), 0);
        semop(semid, &release_op, 1);
        sleep(line_number % 4 + 1); // Добавим небольшую задержку для демонстрации несбалансированности
      }
      exit(0);
    }
  }

  for (int i = 0; i < 4; i++)
  {
    wait(NULL);
  }

  struct msgbuf message;
  FILE *output = fopen("output.txt", "w");
  for (int i = 1; i <= 4; i++)
  {
    while (msgrcv(msgid, &message, sizeof(message.mtext), i, IPC_NOWAIT) > 0)
    {
      fprintf(output, "%s", message.mtext);
    }
  }

  fclose(output);
  fclose(fp);
  msgctl(msgid, IPC_RMID, NULL);
  semctl(semid, 0, IPC_RMID);

  printf("Файл output.txt успешно заполнен\n");

  return 0;
}