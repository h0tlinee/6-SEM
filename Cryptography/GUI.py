from tkinter import *
from tkinter import ttk
from p_pollard import API_for_gui

 
def delete_text():
    txt.delete(1.0, END)
    txt_inf.delete(1.0, END)
def insert_text():
    s = "Hello World"
    txt.insert(1.0, s)

def run():
    delete_text()
    arr=entry_for_number.get()
    print(arr)
    for i in range(0,len(arr)):
        if arr[i] not in ['1','2','3','4','5','6','7','8','9','0']:
            txt_inf.insert(1.0,"Вы ввели неккоректные данные!" )
            return
    solve,iter,time=API_for_gui(int(entry_for_number.get()))
    txt.insert(1.0,solve )
    print(iter)
    print(time)
    txt_inf.insert(1.0,"Кол-во иттерации "+str(iter)+"\n" )
    txt_inf.insert(END,"Время выполнения "+ str(time) )
window = Tk()
window.title("Факторизация числа")
window.resizable(False, False)
window.geometry("350x250")
#labels
lbl_for_number=ttk.Label(text='Введите число')
lbl_for_number.grid(row=1,column=1,sticky='n',padx=68)

#формы вводы данных
entry_for_number=ttk.Entry()
entry_for_number.grid(row=2,column=1,sticky='n',padx=55)

#кнопки
plot_button = ttk.Button(text="Факторизовать",command=run)
plot_button.grid(row=3, column=1,sticky='n')
#text out
txt=Text(width=42, height=5,
            fg='black', wrap=WORD)
txt.grid(row=4, column=1,sticky='n')

txt_inf=Text(width=30, height=3,
            fg='black', wrap=WORD)
txt_inf.grid(row=5, column=1,sticky='n')
window.mainloop()
