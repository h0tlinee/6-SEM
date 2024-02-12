import math



def dichotomy_method(func, a, b, eps,delta):
    while(abs(b-a)>=eps):
        x1=(a+b-delta)/2.0
        x2=(a+b+delta)/2.0
        f1=func(x1)
        f2=func(x2)
        if(f1<f2):
            b=x2
        else:
            a=x1
            
    return (a+b)/2


def golden_section(func, a, b, eps):
    phi = (1 + 5**0.5) / 2  # Золотое сечение
    c = b - (b - a) / phi
    d = a + (b - a) / phi
    while abs(c - d) > eps:
        if func(c) < func(d):
            b = d
        else:
            a = c
        # Мы пересчитываем только ту точку, которую мы сдвинули в последний раз
        c = b - (b - a) / phi
        d = a + (b - a) / phi
    return (a + b) / 2
def fib_search(func,a,b,eps):
    def fibonachi(n):
        if n==0:
            return 0
        elif n==1:
            return 1
        else:
            return fibonachi(n-1)+fibonachi(n-2)
    def n_search(a,b,eps):
        n=0
        while((b-a)/eps)>fibonachi(n):
            n+=1
        return n 
    n=n_search(a,b,eps)
    x1=a+(fibonachi(n)/fibonachi(n+2))*(b-a)
    x2=a+(fibonachi(n+1)/fibonachi(n+2))*(b-a)
    i=0
    while abs(b-a)>eps:
        if(func(x1)<func(x2)):
            b=x2
        else:
            a=x1
        x1=a+(fibonachi(n-i)/fibonachi(n+2-i))*(b-a)
        x2=a+(fibonachi(n+1-i)/fibonachi(n+2-i))*(b-a)
        ++i
    return (a+b)/2

def function(x):
    return 0.5-(x/2)*math.exp(-(math.pow(x/2,2)))


print("Метод дихотомии:")
print(dichotomy_method(function,0,5,0.0001,0.00001))
print("Метод золотого сечения:")
print(golden_section(function,0,5,0.0001))
print("Метод Фибоначчи:")
print(fib_search(function,0,5,0.0001))
