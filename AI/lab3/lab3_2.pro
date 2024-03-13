domains
	item,gender=symbol
	max_size,min_size,price=integer
	list=integer*
database
	item_name(symbol)
	min_size(integer)
	max_size(integer)
	price(integer)
	gender(symbol)
	clothing(item,min_size,max_size,price,gender).
predicates
	nondeterm choice(integer)
	menu
	nondeterm repeat
	nondeterm task1(symbol,integer,integer,symbol)
	nondeterm task2(symbol,integer,integer,symbol)
	nondeterm task3(integer)
	nondeterm task4(symbol,integer)
	nondeterm task5(symbol)
	nondeterm max(list,integer)
	nondeterm task6(symbol,symbol)	
	nondeterm task7(symbol,integer)
	nondeterm task8(integer,symbol)
clauses
	task1(Item,ReqSize,ReqPrice,ReqGender):-
		clothing(Item,Min_Size,Max_Size,Price,Gender),
		Price<=ReqPrice,
		Gender=ReqGender,
		Max_Size>=ReqSize,
		Min_Size<=ReqSize.
	task2(Item,ReqSize1,ReqSize2,ReqGender):-
		clothing(Item,Min_Size,Max_Size,_,Gender),
		Gender=ReqGender,
		ReqSize1<=Max_Size,
		ReqSize1>=Min_Size,
		ReqSize2<=Max_Size,
		ReqSize2>=Min_Size.
	task3(Price):-
		clothing(black_shirt,_,_,Price1,_),
		clothing(yellow_shirt,_,_,Price2,_),
		clothing(red_shirt,_,_,Price3,_),
		clothing(blue_shirt,_,_,Price4,_),
		Price=(Price1+Price2+Price3+Price4).
	task4(Name,ReqPrice):-
		findall(N,clothing(jeans,_,_,N,man),L),
		L=[H|_],
		clothing(Name,_,_,Price,man),
		H+Price<=ReqPrice,
		"jeans"<>Name.
	task5(Name):-
		findall(Price,clothing(_,_,_,Price,_),L),
		
		max(L,Max),
		clothing(Name,_,_,Price,_),
		(Max-Price)<=400.
	max([Head|Tail],Result):-
		max(Tail,Result),Result>Head,!.
		max([Head|_],Head).
	task6(Name,Req_Gender):-
		clothing(Name,_,_,_,Gender),
		Req_Gender=Gender.
	task7(Name,Req_Price):-
		clothing(Name,_,_,Price,_),
		Price<=Req_Price.
	task8(Max_Size,Req_Gender):-
		findall(Size,clothing(_,_,Size,_,Req_Gender),L),
		max(L,Max_Size).
	choice('a'):-
		write("Name of adding item:"),
		readln(Name),
		write("Minimal size of an item:"),
		readint(Min_Size),
		write("Maximal size of an item:"),
		readint(Max_Size),
		write("Price of an item:"),
		readint(Price),
		write("Gender:"),
		readln(Gender),
		assert(clothing(Name,Min_Size,Max_Size,Price,Gender)),
		fail.
	choice('v'):-
		write("Presence of clothing in database:\n"),
		clothing(Name,Min_Size,Max_Size,Price,Gender),
		write("Name: ",Name,";Minimal size: ",Min_Size,";Maximum Size: ",Max_Size,";Price: ",Price,";Gender: ",Gender),nl.
	choice('1'):-
		write("Enter required size:"),
		readint(Req_Size),
		write("Enter required price:"),
		readint(Req_Price),
		write("Enter required gender(man of woman):"),
		readln(Req_Gender),
		task1(_,Req_Size,Req_Price,Req_Gender).
	choice('2'):-
		write("Enter interval of sizes:"),
		readint(Req_Size1),
		readint(Req_Size2),
		write("Enter required gender:"),
		task2(_,Req_Size1,Req_Size2,woman).
	choice('3'):-
		task3(_).
	choice('4'):-
		write("Enter required price"),
		readint(Req_Price),
		task4(_,Req_Price).
	choice('5'):-
		task5(_).
	choice('6'):-
		write("Enter required gender"),
		readln(ReqGender),
		task6(_,ReqGender).
	choice('7'):-
		write("Enter required price"),
		readint(ReqPrice),
		task7(_,ReqPrice).
	choice('8'):-
		write("Enter required gender"),
		readln(ReqGender),
		task8(_,ReqGender).
	choice('s'):-
		save("D:\data2.txt"),
		write("Information saved succesfully!\n").
	choice('l'):-
		existfile("D:\data2.txt"),!,
		consult("D:\data2.txt").
	choice('0'):-
		!.
	menu:-
		repeat,
		write("-----------------------------\n"),
		write("Please, choose option:\n"),
		write("1 - task 1\n"),
		write("2 - task 2\n"),
		write("3 - task 3\n"),
		write("4 - task 4\n"),
		write("5 - task 5\n"),
		write("6 - task 6\n"),
		write("7 - task 7\n"),
		write("8 - task 8\n"),
		write("a - add information about clothing\n"),
		write("v - view all information\n"),
		write("s - save database in file\n"),
		write("l - load database from file\n"),
		write("0 - exit\n"),
		readchar(Choice),
		choice(Choice),
		Choice='0',
		!.
	repeat.
	repeat :- repeat.

	goal
    menu.
