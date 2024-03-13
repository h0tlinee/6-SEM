domains
	lang, name = string
	
database - databaselanguage
	nondeterm languages(lang)
	nondeterm know(name,lang)

predicates
	nondeterm repeat
	nondeterm call(integer)
	nondeterm menu()
	nondeterm stop_menu(integer)

clauses
	languages("Russian").
	languages("Italian").
	languages("German").
	languages("Japanese").
	languages("French").
	languages("English").
	
	call(0):-stop_menu(_).
	call(1):-write("What's your name?: "), readln(Name),
		languages(Lang),write("Do you know ",Lang, " (yes or no)?: "),
		readln(X), X="yes",assert(know(Name,Lang)),fail.
	call(2):-know(Name,Lang),write(Name," knows ",Lang).
	call(3):-consult("D:\\data.txt",databaselanguage), 
		write("Downloaded from a file").
	call(4):-save("D:\\data.txt",databaselanguage), 
		write("Saved to a file").	
		
	repeat.		
	repeat:-repeat.
	stop_menu(0).
	stop_menu(_):-fail.
	menu():-repeat,
		nl, write("_________________________"),
		nl, write("Choose operation: "),
		nl, write("1) What languages do you know?"),
		nl, write("2) Show who knows which languages"),
		nl, write("3) Load database from file"),
		nl, write("4) Save database in file"),
		nl, write("0) Exit"),
		nl, write("Enter number: "),
		readint(Choice),
		Choice<5,
		call(Choice),
		readln(_),
		stop_menu(Choice).
		
goal
menu.
