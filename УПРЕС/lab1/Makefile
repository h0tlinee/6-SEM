all:lab1

lab1: lab1.o
	gcc lab1.o -o lab1

lab1.o: lab1.c
	gcc -Wall -Werror -Wextra -c lab1.c -o lab1.o

clean:
	rm -rf *.o lab1
