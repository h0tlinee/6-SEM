import random
import math

import time



iteration=0

def R_M_test(n):
    k=50
    if n == 1:
        return False
    if n <= 3:
        return True
    s = 0
    t = n-1
    while t % 2 == 0:
        s += 1
        t //= 2
    for _ in range(15):
        a = random.randint(2, n - 1)
        if math.gcd(a, n) != 1:
            return False
        z = pow(a, t, n)
        if z == 1 or z == n-1:
            continue
        for _ in range(s - 1):
            z = pow(z, 2, n)
            if z == n-1:
                break
        else:
            return False
    return True  
def pollard(n):
    x_prev=random.randrange(0,n-1)
    y=x_prev
    k=2
    i=0
    global iteration
    while True:
        i+=1
        iteration+=1
        x_next=(x_prev*x_prev+1)%n
        x_prev=x_next
        d=abs(math.gcd(y-x_next,n))
        if (1<d<n ):
            return d
        elif(i<k):
            continue
        elif(i==k):
            y=x_next
            k=2*k




def factorizatiton(n):
    dividers=[]
    global iteration
    start_time = time.time()
    if  (n==0 or n==1):
        return ["эту цифру не имеет смысла факторизовать", 0, 0]
    elif R_M_test(n) or n==0 or n==1:
        return [f"{n} - число простое",0,0]
    
    while (n%2 == 0):
        n=n//2
        dividers.append(2)
        iteration+=1

    while (n%5 == 0):
        n=n//5
        dividers.append(5)
        iteration+=1    
    if (n!=1 and R_M_test(n) == True and dividers!=[]):
        dividers.append(n)
        end_time = time.time()
        elapsed_time = end_time - start_time
        return [dividers,iteration,elapsed_time]
    if n!=1:      
        while(not R_M_test(n)):
            d=pollard(n)
            dividers.append(d)
            n=n//d
        dividers.append(n)

    end_time = time.time()
    elapsed_time = end_time - start_time
    
    return [dividers,iteration,elapsed_time]

def API_for_gui(n):
    solve=factorizatiton(n)
    dividers=solve[0]
    iteration=solve[1]
    elapsed_time=solve[2]
  
    out =""
    
    if (type(dividers)==str):
        return dividers,0,0
    dividers2=dividers
    for i in range (len(dividers2)):
        check = dividers2[i]
        if R_M_test(check) != True:
            dividers.pop(i)
            while R_M_test(check) != True:
                inp2 = pollard(check)
             
                
                dividers.append(  inp2)
                check = check //   inp2
            dividers.append(check)
    if(dividers!=[]):
        sorted(dividers)
        div = set(dividers)
        for i in div:
            out += str(i) + "^" + str(dividers.count(i)) + " * "
    return out[:-3],iteration,elapsed_time



   
