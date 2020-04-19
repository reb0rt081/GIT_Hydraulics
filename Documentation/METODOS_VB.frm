VERSION 5.00
Begin VB.Form Métodos 
   Caption         =   "MétodosNuméricos"
   ClientHeight    =   4350
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4575
   LinkTopic       =   "Form1"
   ScaleHeight     =   4350
   ScaleWidth      =   4575
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command17 
      Caption         =   "a"
      Height          =   375
      Left            =   1560
      TabIndex        =   17
      Top             =   3600
      Width           =   375
   End
   Begin VB.CommandButton Command18 
      Caption         =   "QR Francis"
      Height          =   375
      Left            =   240
      TabIndex        =   16
      Top             =   3600
      Width           =   1215
   End
   Begin VB.CommandButton Command16 
      Caption         =   "Potencia Inversa"
      Height          =   495
      Left            =   3240
      TabIndex        =   15
      Top             =   3600
      Width           =   1095
   End
   Begin VB.CommandButton Command15 
      Caption         =   "QR"
      Height          =   375
      Left            =   240
      TabIndex        =   14
      Top             =   3000
      Width           =   1215
   End
   Begin VB.CommandButton Command14 
      Caption         =   "Potencia "
      Height          =   375
      Left            =   3240
      TabIndex        =   13
      Top             =   3000
      Width           =   1095
   End
   Begin VB.CommandButton Command13 
      Caption         =   "Transporte"
      Height          =   375
      Left            =   3240
      TabIndex        =   12
      Top             =   2280
      Width           =   1095
   End
   Begin VB.CommandButton Command12 
      Caption         =   "Cholesky"
      Height          =   375
      Left            =   3240
      TabIndex        =   11
      Top             =   960
      Width           =   1095
   End
   Begin VB.CommandButton Command11 
      Caption         =   "Producto Matrices"
      Height          =   375
      Left            =   3240
      TabIndex        =   10
      Top             =   360
      Width           =   1095
   End
   Begin VB.CommandButton Command10 
      Caption         =   "Símplex"
      Height          =   375
      Left            =   3240
      TabIndex        =   9
      Top             =   1560
      Width           =   1095
   End
   Begin VB.CommandButton Command9 
      Caption         =   "Inversa.Matr"
      Height          =   375
      Left            =   240
      TabIndex        =   8
      Top             =   2280
      Width           =   1215
   End
   Begin VB.CommandButton Command8 
      Caption         =   "LDLt"
      Height          =   375
      Left            =   1800
      TabIndex        =   7
      Top             =   2280
      Width           =   1095
   End
   Begin VB.CommandButton Command7 
      Caption         =   "LU"
      Height          =   375
      Left            =   1800
      TabIndex        =   6
      Top             =   1560
      Width           =   1095
   End
   Begin VB.CommandButton Command6 
      Caption         =   "Con Pivot. y Permutacion"
      Height          =   615
      Left            =   1800
      TabIndex        =   5
      Top             =   2880
      Width           =   1095
   End
   Begin VB.CommandButton Command5 
      Caption         =   "Crout"
      Height          =   375
      Left            =   1800
      TabIndex        =   4
      Top             =   960
      Width           =   1095
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Determinante"
      Height          =   375
      Left            =   1800
      TabIndex        =   3
      Top             =   360
      Width           =   1095
   End
   Begin VB.CommandButton Command3 
      Caption         =   "Eq.Sencillas"
      Height          =   375
      Left            =   240
      TabIndex        =   2
      Top             =   1560
      Width           =   1215
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Gauss"
      Height          =   375
      Left            =   240
      TabIndex        =   1
      Top             =   960
      Width           =   1215
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Sist.Triang"
      Height          =   375
      Left            =   240
      TabIndex        =   0
      Top             =   360
      Width           =   1215
   End
End
Attribute VB_Name = "Métodos"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim n As Long
Dim m As Long

Private Sub Command1_Click()
n = InputBox("Tecle el orden del sistema")
ReDim x(1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
Dim i As Long
Dim j As Long
Dim p As Long
ReDim aux(1 To n) As Double
ReDim b(1 To n) As Long
Dim suma As Double
p = 1
For i = 1 To n
 For j = i To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
  If i = j Then
      aux(p) = a(i, j)
      p = p + 1
  End If
  a(j, i) = 0
 Next j
Next i
For i = 1 To n
 b(i) = InputBox("Teclea la entrada b(" & i & ") de la matriz")
Next i
For i = 1 To n
 a(i, i) = aux(i)
Next i
x(n) = b(n) / a(n, n)
For i = n - 1 To 1 Step -1
  suma = 0
  For j = i + 1 To n
    suma = a(i, j) * x(j) + suma
  Next j
  x(i) = (b(i) - suma) / a(i, i)
Next i


EscribirNl "La solución, x= (", x, ")"
'For i = 1 To n
'
'   MsgBox ("La solución x(" & i & ") = " & x(i))
'
'Next i
End Sub

Public Sub Command10_Click()
Dim ñ As Long
n = InputBox("Introduce el número de variables(Incluye holgura y artificiales .")

ñ = n + 1
m = InputBox("Tecle el número de restricciones")
Dim nnn As Long
Dim mmm As Long
Dim opcion As Long

nnn = n + 3
mmm = m + 3
ReDim NNmm(1 To mmm, 1 To nnn) As Double


ReDim a(1 To m, 1 To n + 1) As Double
ReDim c(1 To n) As Double
ReDim w(1 To m, 1 To 2 * m) As Double
ReDim Y(1 To m, 1 To m) As Double
Dim t As Long
Dim k As Long
Dim pivotar As Long
Dim S As Long
ReDim v(1 To m) As Double
Dim s1 As Long
ReDim auxiliar(1 To m, 1 To n + 1) As Double
Dim suma As Double
Dim cte As Double
Dim maximo As Double
Dim minimo As Double
Dim posicion As Long
ReDim b(1 To m, 1 To m) As Double
ReDim h(1 To m, 1 To m) As Double
ReDim VB(1 To m) As Long
ReDim x(1 To n) As Double
Dim vmax As Double
Dim fin As Long
Dim p As Long
ReDim z(1 To n) As Double
'opcion = InputBox("¿Pasos intermedios? si=1, no =0")

pivotar = InputBox("Si quieres usar un método mejor para el cálculo de inversas pero más complejo pulsa '1', en otro caso pulsa '0', úsala cuando un resultado con SÍMPLEX haya salido erróneo.")
For i = 1 To m
 For j = 1 To n + 1
  a(i, j) = InputBox("Introduce el valor a(" & i & "," & j & ") de la matriz de restricciones (FILAS: nº Restricciones, COLUMNAS: nº variables")
 Next j
Next i
For i = 1 To n
 c(i) = InputBox("Introduce el valor c(" & i & ") del vector de costes")
Next i
For i = 1 To m
 VB(i) = InputBox("Introduce el valor VB(" & i & ") del vector de variables básicas")
Next i
fin = 0

While fin = 0
For i = 1 To m
 For j = 1 To m
 
  b(i, j) = a(i, VB(j))
 Next j
Next i


'APLICACION DE LA INVERSA





   p = 2 * m
   
   
  
    For i = 1 To m
 For j = 1 To m
  w(i, j) = b(i, j)
 Next j
Next i
For i = 1 To m
  For j = m + 1 To p
    If i = j - m Then
      w(i, j) = 1
    ElseIf i <> j Then
      w(i, j) = 0
    End If
  Next j
Next i


'AQUÍ SE DEBE PIVOTAR SI QUIERES
If pivotar = 1 Then
 For j = 1 To m - 1
  If w(j, j) = 0 Then
    S = j
    i = j + 1
    For i = j + 1 To m
     If w(i, j) <> 0 Then
      S = i
      i = i + 10
      
      
     End If
     
    Next i
    If S <> i Then
      For k = j To p
        aux = w(j, k)
        w(j, k) = w(S, k)
        w(S, k) = aux
      Next k
    End If
  End If
  
  For i = j + 1 To m
    cte = w(i, j) / w(j, j)
    For k = 1 To p
      w(i, k) = w(i, k) - w(j, k) * cte
    Next k
    
    w(i, j) = 0
  Next i
Next j
  
ElseIf pivotar <> 1 Then


For j = 1 To m - 1
  For i = j + 1 To m
    cte = w(i, j) / w(j, j)
    For k = 1 To p
      w(i, k) = w(i, k) - w(j, k) * cte
    Next k
    
    w(i, j) = 0
  Next i
Next j
End If

'AQI FINALIZA
For i = 1 To m
 For j = 1 To m
  b(i, j) = w(i, j)
 Next j
Next i
For i = 1 To m
 For j = m + 1 To p
  Y(i, j - m) = w(i, j)
 Next j
Next i

For t = 1 To m
 h(m, t) = Y(m, t) / b(m, m)
For i = m - 1 To 1 Step -1
  suma = 0
  For j = i + 1 To m
    suma = b(i, j) * h(j, t) + suma
  Next j
  h(i, t) = (Y(i, t) - suma) / b(i, i)
Next i
Next t
For i = 1 To m
 For j = 1 To m
  b(i, j) = h(i, j)
 Next j
Next i


'FIN DE LA INVERSA

'PRODUCTO DE MATRICES


For i = 1 To m
 For j = 1 To n + 1
   suma = 0
   For k = 1 To m
    suma = suma + b(i, k) * a(k, j)
   Next k
   auxiliar(i, j) = suma
 Next j
Next i


For i = 1 To m
 For j = 1 To n + 1
  
   a(i, j) = auxiliar(i, j)
 Next j
 
 Next i

'FIN DEL PRODUCTO


For j = 1 To n
  suma = 0
  For k = 1 To m
   suma = suma + c(VB(k)) * a(k, j)
  Next k
  z(j) = c(j) - suma
Next j
suma = 0
For i = 1 To m
 suma = c(VB(i)) * a(i, ñ) + suma
Next i
vmax = suma


'BUSQUEDEA DEL MAXIMO
maximo = 0
posicion = 1
 For j = 1 To n
   If z(j) > maximo Then
    maximo = z(j)
    posicion = j
   
   End If
 Next j
 If maximo <= 0.000000000001 Then
  fin = 1
 ElseIf maximo > 0.000000000001 Then
  For i = 1 To m
    If a(i, posicion) > 0 Then
     v(i) = a(i, ñ) / a(i, posicion)
    ElseIf a(i, posicion) <= 0 Then
     v(i) = 1 / 1E-300
  
    End If
    
  Next i

  minimo = v(1)
  s1 = 1
  For i = 2 To m
   If v(i) < minimo Then
    minimo = v(i)
    s1 = i
    
   End If
  Next i
  If minimo = 1 / 1E-300 Then
   MsgBox ("El problema no tiene solución")
   fin = 1
   ElseIf minimo <> realmax Then
   
    VB(s1) = posicion
  
   End If
  End If

'FIN DE SU BUSQUEDA

  EscribirNl "A= ", a
  EscribirNl "c= ", c
  EscribirNl "VB= ", VB
  EscribirNl "w= ", z
  EscribirNl "vmax= ", vmax
  EscribirNl "===================================================================================================================="
  
Wend
'FIN DEL BUCLE
For i = 1 To m
 x(VB(i)) = a(i, ñ)
Next i
  EscribirNl "x= ", x
  EscribirNl "vmax= ", vmax
'For i = 1 To n
'MsgBox ("La solución x(" & i & ") = " & x(i))
'Next i
'MsgBox ("El valor de la función para el máximo es: " & vmax)
End Sub

Private Sub Command11_Click()
m = InputBox("Introduce el número de filas de la A (A*B)")
n = InputBox("Introduce el número de columnas de la A (A*B)")
n = InputBox("Introduce el número de filas de la B (A*B)")
Dim p As Long

p = InputBox("Introduce el número de columnas de la B (A*B)")
ReDim b(1 To n, 1 To p) As Double
ReDim result(1 To m, 1 To p)
ReDim a(1 To m, 1 To n) As Double

Dim repetir As Long
Dim hojaldre As Double
hojaldre = 0
repetir = 1
For i = 1 To m
 For j = 1 To n
 a(i, j) = InputBox("Introduce el valor a(" & i & "," & j & ") ")
 Next j
Next i
For i = 1 To n
 For j = 1 To p
  b(i, j) = InputBox("Introduce el valor b(" & i & "," & j & ") ")
 Next j
Next i
While repetir = 1


 If hohaldre = 1 Then
  p = InputBox("Introduce el número de columnas de la B (A*B)")
  For i = 1 To n
 For j = 1 To p
  b(i, j) = InputBox("Introduce el valor b(" & i & "," & j & ") ")
 Next j
 Next i
 End If
 

For i = 1 To m
 For j = 1 To p
   suma = 0
   For k = 1 To n
    suma = suma + a(i, k) * b(k, j)
   Next k
   result(i, j) = suma
 Next j
Next i
 repetir = 0
 hojaldre = 0
 'For i = 1 To m
  'For j = 1 To p
   ' MsgBox ("El resultado es c(" & i & "," & j & ") = " & result(i, j))
  'Next j
 'Next i
 EscribirNl "El resultado es :"
 EscribirNl result
 repetir = InputBox("Si quieres realizar otro producto con el resultado obtenido solo tienes que que escribir un '1' y sino un '0'")
 If repetir = 1 Then
  hojaldre = 1
  m = m
  n = p
 For i = 1 To m
  For j = 1 To p
   MsgBox ("ola1")
   a(i, j) = result(i, j)
  Next j
 Next i
 End If
Wend
End Sub

Private Sub Command12_Click()
n = InputBox("Tecle el orden del sistema")
ReDim l(1 To n, 1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
Dim suma As Double
Dim i As Long
Dim j As Long
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
 
 Next j
Next i
l(1, 1) = Sqr(a(1, 1))
For i = 2 To n
 l(i, 1) = a(i, 1) / l(1, 1)
 'MsgBox ("La entrada L" & i & 1 & ") =" & l(i, 1))
Next i
For j = 2 To n
 For i = j To n
  If i = j Then
    suma = 0
    For k = 1 To i - 1
     suma = suma + l(i, k) * l(i, k)
    Next k
    l(i, i) = Sqr(a(i, i) - suma)
   'MsgBox ("La entrada L" & i & j & ") =" & l(i, j))
  ElseIf i <> j Then
    suma = 0
    For k = 1 To j - 1
     suma = suma + l(i, k) * l(k, j)
    Next k
    l(i, j) = (a(i, j) - suma) / l(j, j)
    'MsgBox ("La entrada L" & i & j & ") =" & l(i, j))
  End If
  
  
 Next i
Next j
EscribirNl "Solución Matriz L: ", l
'For i = 1 To n
 'For j = 1 To n
  'MsgBox ("La entrada de  L(" & i & "," & j & ") = " & l(i, j))
 
 'Next j
'Next i
End Sub

Private Sub Command13_Click()
Dim ñ As Long
n = InputBox("Introduce el número de variables Xij")

ñ = n + 1
m = InputBox("Tecle el número de restricciones")
ReDim a(1 To m, 1 To n + 1) As Double
ReDim c(1 To n) As Double
ReDim w(1 To m, 1 To 2 * m) As Double
ReDim Y(1 To m, 1 To m) As Double
Dim t As Long
Dim S As Long
Dim k As Long
Dim aux As Double
ReDim v(1 To m) As Double
Dim s1 As Long
Dim d As Long
ReDim auxiliar(1 To m, 1 To n + 1) As Double
Dim suma As Double
Dim cte As Double
Dim o As Long
Dim prueb As Double
Dim maximo As Double
Dim minimo As Double
Dim azid As Long
Dim posicion As Long
ReDim b(1 To m, 1 To m) As Double
ReDim h(1 To m, 1 To m) As Double
ReDim VB(1 To m) As Long
ReDim x(1 To n) As Double
Dim vmax As Double
Dim fin As Long
Dim p As Long
ReDim z(1 To n) As Double
For i = 1 To m
 For j = 1 To n + 1
  a(i, j) = InputBox("Introduce el valor a(" & i & "," & j & ") de la matriz de restricciones (FILAS: nº Restricciones, COLUMNAS: nº variables")
 Next j
Next i
For i = 1 To n
 c(i) = InputBox("Introduce el valor c(" & i & ") del vector de costes")
Next i
For i = 1 To n
 c(i) = -c(i)
Next i
EscribirNl "DATOS "
EscribirNl "VECTOR DE COSTES "
EscribirNl c
EscribirNl "MATRIZ DE RESTRICCIONES "
EscribirNl a

'REGLA DE LA ESQUINA NOROESTE


For i = 1 To m
 For j = 1 To m
   b(i, j) = 0
 Next j
Next i
o = InputBox("Introduce el número de orígenes inicial")
d = InputBox("Introduce el número de destinos inicial ")
ReDim orig(1 To o) As Double
ReDim destin(1 To d) As Double
For i = 1 To o
 orig(i) = InputBox("Introduce el valor de la oferta para a(" & i & ")")
Next i
For i = 1 To d
 destin(i) = InputBox("Introduce el valor de la demanda para b(" & i & ")")
Next i
EscribirNl "DATOS DE LOS ORÍGENES"
EscribirNl orig
EscribirNl "DATOS DE LOS DESTINOS"
EscribirNl destin
EscribirNl "==========================================================================================================================="
i = 1
j = 1
For k = 1 To o + d - 1
 If orig(i) < destin(j) Then
   b(i, j) = orig(i)
   destin(j) = destin(j) - orig(i)
   VB(k) = (i - 1) * d + j
   
   'MsgBox ("b(" & j & ")= " & destin(j))
   'MsgBox ("vb(" & k & ")= " & VB(k))
   i = i + 1
 ElseIf orig(i) >= destin(j) Then
 b(i, j) = destin(j)
   orig(i) = orig(i) - destin(j)
   VB(k) = (i - 1) * d + j
   'MsgBox ("a(" & j & ")= " & orig(j))
   'MsgBox ("vb(" & k & ")= " & VB(k))
   j = j + 1
   
 End If
   

Next k













'FIN DE LA REGLA DE LA ESQUINA NOROESTE

'For i = 1 To m
 'VB(i) = InputBox("Introduce el valor VB(" & i & ") del vector de variables básicas")
'Next i
'fin = 0
While fin = 0
For i = 1 To m
 For j = 1 To m
 
  b(i, j) = a(i, VB(j))
 Next j
Next i


'APLICACION DE LA INVERSA





   p = 2 * m
   
   
  
    For i = 1 To m
 For j = 1 To m
  w(i, j) = b(i, j)
 Next j
Next i
For i = 1 To m
  For j = m + 1 To p
    If i = j - m Then
      w(i, j) = 1
    ElseIf i <> j Then
      w(i, j) = 0
    End If
  Next j
Next i
'CAMBIO FILA Y BUSQUEDA INCLUIDO CON GAUSS
For j = 1 To m - 1
  If w(j, j) = 0 Then
    S = j
    i = j + 1
    For i = j + 1 To m
     If w(i, j) <> 0 Then
      S = i
      i = i + 10
      
      
     End If
     
    Next i
    If S <> i Then
      For k = j To p
        aux = w(j, k)
        w(j, k) = w(S, k)
        w(S, k) = aux
      Next k
    End If
  End If
  
  For i = j + 1 To m
    cte = w(i, j) / w(j, j)
    For k = 1 To p
      w(i, k) = w(i, k) - w(j, k) * cte
    Next k
    
    w(i, j) = 0
  Next i
Next j
For i = 1 To m
 For j = 1 To m
  b(i, j) = w(i, j)
 Next j
Next i
For i = 1 To m
 For j = m + 1 To p
  Y(i, j - m) = w(i, j)
 Next j
Next i
'FIN DE PERMUTACIONES Y DE TRIANGULACIONES
For i = 1 To m
 For j = 1 To m
  b(i, j) = w(i, j)
 Next j
Next i
For i = 1 To m
 For j = m + 1 To p
  Y(i, j - m) = w(i, j)
 Next j
Next i

For t = 1 To m
 h(m, t) = Y(m, t) / b(m, m)
For i = m - 1 To 1 Step -1
  suma = 0
  For j = i + 1 To m
    suma = b(i, j) * h(j, t) + suma
  Next j
  h(i, t) = (Y(i, t) - suma) / b(i, i)
Next i
Next t
For i = 1 To m
 For j = 1 To m
  b(i, j) = h(i, j)
 Next j
Next i


'FIN DE LA INVERSA

'PRODUCTO DE MATRICES


For i = 1 To m
 For j = 1 To n + 1
   suma = 0
   For k = 1 To m
    suma = suma + b(i, k) * a(k, j)
   Next k
   auxiliar(i, j) = suma
 Next j
Next i


For i = 1 To m
 For j = 1 To n + 1
  
   a(i, j) = auxiliar(i, j)
 Next j
 
 Next i

'FIN DEL PRODUCTO


For j = 1 To n
  suma = 0
  For k = 1 To m
   suma = suma + c(VB(k)) * a(k, j)
  Next k
  z(j) = c(j) - suma
Next j
suma = 0
For i = 1 To m
 suma = c(VB(i)) * a(i, ñ) + suma
Next i
vmax = suma


'BUSQUEDEA DEL MAXIMO
maximo = 0
posicion = 1
 For j = 1 To n
   If z(j) > maximo Then
    maximo = z(j)
    posicion = j
   
   End If
 Next j
 If maximo <= 0.000000000001 Then
  fin = 1
 ElseIf maximo > 0.000000000001 Then
  For i = 1 To m
    If a(i, posicion) > 0 Then
     v(i) = a(i, ñ) / a(i, posicion)
    ElseIf a(i, posicion) <= 0 Then
     v(i) = 1 / 1E-300
  
    End If
    
  Next i

  minimo = v(1)
  s1 = 1
  For i = 2 To m
   If v(i) < minimo Then
    minimo = v(i)
    s1 = i
    
   End If
  Next i
  If minimo = 1 / 1E-300 Then
   MsgBox ("El problema no tiene solución")
   fin = 1
   ElseIf minimo <> realmax Then
   
    VB(s1) = posicion
  
   End If
  End If

'FIN DE SU BUSQUEDA

EscribirNl "A= ", a
  EscribirNl "c= ", c
  EscribirNl "VB= ", VB
  EscribirNl "w= ", z
  EscribirNl "vmax= ", vmax
  EscribirNl "===================================================================================================================="
  


Wend
'FIN DEL BUCLE
For i = 1 To m
 x(VB(i)) = a(i, ñ)
Next i
EscribirNl "x= ", x
'For i = 1 To n
'MsgBox ("La solución x(" & i & ") = " & x(i))
'Next i
vmax = -vmax

EscribirNl "vmax= ", vmax
'MsgBox ("El valor de la función para el máximo es: " & vmax)
End Sub



Private Sub Command14_Click()
Dim error As Double
  n = InputBox("Dimensión de la matriz")
  Dim i As Long
  Dim j As Long
  Dim iteracion As Long
  ReDim d(1 To n) As Double
  Dim dif As Double
  Dim p As Long
  Dim fin As Long
  Dim mu As Double
  Dim posicion0 As Long
  Dim posicion1 As Long
  ReDim a(1 To n, 1 To n) As Double
  ReDim x0(1 To n) As Double
  ReDim x1(1 To n) As Double
  ReDim Y(1 To n) As Double
  ReDim maximo(1 To n) As Double
  Dim maximo2 As Double
  Dim suma As Double
  Dim signo As Long
  error = InputBox("Introduce el error máximo de obtención")
  For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
For i = 1 To n

  x0(i) = InputBox("Teclea la entrada x(" & i & ") de la matriz")
   
 
Next i

' NORMA SUBINFINITO
posicion0 = 1
For j = 1 To n
maximo2 = 0
Next j
 For j = 1 To n
  
    If x0(j) < 0 Then
     
     x0(j) = -x0(j)
     If x0(j) > maximo2 Then
      
      maximo2 = x0(j)
     End If
     x0(j) = -x0(j)
     ElseIf x0(j) >= 0 Then
      If x0(j) > maximo2 Then
      maximo2 = x0(j)
      posicion0 = j
     End If
     
    End If
  
  
 Next j

'FIN D LA NORMA
fin = 0

While fin = 0
 'PRODUCTO DE A*x0
  For i = 1 To n
  suma = 0
   For j = 1 To n
    suma = a(i, j) * x0(j) + suma
   Next j
   Y(i) = suma
  Next i
 
 
 'FIN
 'OTRA NORMA
 For j = 1 To n
maximo2 = 0
posicion1 = 1
Next j
 For j = 1 To n
  
    If Y(j) < 0 Then
     
     Y(j) = -Y(j)
     If Y(j) > maximo2 Then
      posicion1 = j
      maximo2 = Y(j)
     End If
     Y(j) = -Y(j)
     ElseIf Y(j) >= 0 Then
      If Y(j) > maximo2 Then
      maximo2 = Y(j)
      posicion1 = j
     End If
     
    End If
  
  
 Next j

 
 
 
 'FIN DE OTRA NORMA
 'FINAL DE LOS CAULCULO
  mu = Y(posicion0)
  For i = 1 To n
   x1(i) = Y(i) / Y(posicion1)
  
  Next i
  For i = 1 To n
   d(i) = x1(i) - x0(i)
  Next i
 
 
 'FIN
 'OTRA NORMA
 
  For j = 1 To n
dif = 0
p = 1
Next j
 For j = 1 To n
  
    If d(j) < 0 Then
     
     d(j) = -d(j)
     If d(j) > dif Then
      p = j
      dif = d(j)
     End If
     d(j) = -d(j)
     ElseIf d(j) >= 0 Then
      If d(j) > dif Then
     dif = d(j)
      p = j
     End If
     
    End If
  
  
 Next j

 
 
 
 'FIN DE LA NORMA
 EscribirNl "ITERACIÓN  ", iteracion
 EscribirNl "El VECTOR Y =", Y
 EscribirNl "P1 =  ", posicion1
 EscribirNl "EL VEXTOR X1 "
 EscribirNl x1
 EscribirNl "VALOR PROPIO OBTENIDO = ", mu
 EscribirNl "=================================================================================================================================="
 If dif > error Then
  For j = 1 To n
   x0(j) = x1(j)
   posicion0 = posicion1
  Next j
 ElseIf dif <= error Then
  fin = 1
 End If
iteracion = iteracion + 1
Wend
EscribirNl "VALOR PROPIO OBTENIDO FINAL  ", mu
EscribirNl "VEXTOR PROPIO OBTENIDO FINAL  "
EscribirNl x1

End Sub

Private Sub Command15_Click()

n = InputBox("Tecle el orden del sistema")
ReDim q(1 To n, 1 To n) As Double, r(1 To n, 1 To n) As Double, w(1 To n, 1 To 1) As Double, wt(1 To 1, 1 To n) As Double, z(1 To n, 1 To n) As Double, tuputamadre(1 To n, 1 To n) As Double, identidad(1 To n, 1 To n) As Double

Dim S As Double, m As Double, suma As Double

For i = 1 To n
  For j = 1 To n
      q(i, j) = 0
      identidad(i, j) = 0
  Next j
  q(i, i) = 1
  identidad(i, i) = 1
Next i

For i = 1 To n
  For j = 1 To n
    r(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
  Next j
Next i
  EscribirNl "R= ", r
  EscribirNl "Q= ", q
For i = 1 To n - 1
EscribirNl "Iteración ", i
  suma = 0
  For j = i To n
    suma = suma + r(j, i) * r(j, i)
  Next j
  If r(i, i) = 0 Then
   S = Sqr(suma)
  Else
  
  S = Sqr(suma) * (r(i, i) / Abs(r(i, i)))
  End If
  m = 1 / (Sqr(2 * S * (r(i, i) + S)))
  
  If i = 1 Then
    wt(1, 1) = (r(1, 1) + S) * m
  Else
    For k = 1 To i - 1
      wt(1, k) = 0
    Next k
    wt(1, i) = (r(i, i) + S) * m
  End If
  For k = i + 1 To n
    wt(1, k) = r(k, i) * m
  Next k
  For j = 1 To n
    w(j, 1) = wt(1, j)
  Next j
  
  'Producto wt, w, 1, n, n, tuputamadre
  For j = 1 To n
    For k = 1 To n
      tuputamadre(k, j) = wt(1, k) * w(j, 1)
    Next k
  Next j
  For j = 1 To n
    For k = 1 To n
      z(j, k) = identidad(j, k) - 2 * tuputamadre(j, k)
    Next k
  Next j
  Producto z, r, n, n, n, tuputamadre
  For j = 1 To n
    For k = 1 To n
      If Abs(tuputamadre(j, k)) < 10 ^ (-12) Then
        tuputamadre(j, k) = 0
      End If
    
      r(j, k) = tuputamadre(j, k)
    Next k
  Next j
  Producto q, z, n, n, n, tuputamadre
  For j = 1 To n
    For k = 1 To n
      q(j, k) = tuputamadre(j, k)
    Next k
  Next j
  

  EscribirNl "s= ", S
  EscribirNl "m= ", m
  EscribirNl "w= ", w
  EscribirNl "z= ", z
  EscribirNl "R= ", r
  EscribirNl "Q= ", q
  EscribirNl "======================================================================================== "
  
Next i


End Sub

Private Sub Command16_Click()
Dim error As Double
  n = InputBox("Dimensión de la matriz")
  Dim i As Long
  Dim j As Long
  Dim iteracion As Long
  ReDim d(1 To n) As Double
  Dim dif As Double
  Dim p As Long
  Dim fin As Long
  Dim mu As Double
  Dim posicion0 As Long
  Dim posicion1 As Long
  ReDim a(1 To n, 1 To n) As Double
  ReDim b(1 To n, 1 To n) As Double
  ReDim tuputamadre(1 To n, 1 To n) As Double
  ReDim x0(1 To n) As Double
  ReDim x1(1 To n) As Double
  ReDim Y(1 To n) As Double
  ReDim maximo(1 To n) As Double
  Dim maximo2 As Double
  Dim suma As Double
  Dim signo As Long, q As Double
  q = InputBox("Introduce el valor al que quieras que se aproxime el autovalor")
  error = InputBox("Introduce el error máximo de obtención")
  For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
  tuputamadre(i, j) = a(i, j)
 Next j
Next i
For i = 1 To n

  x0(i) = InputBox("Teclea la entrada x(" & i & ") de la matriz")
   
 
Next i

' NORMA SUBINFINITO
posicion0 = 1
For j = 1 To n
maximo2 = 0
Next j
 For j = 1 To n
  
    If x0(j) < 0 Then
     
     x0(j) = -x0(j)
     If x0(j) > maximo2 Then
      
      maximo2 = x0(j)
     End If
     x0(j) = -x0(j)
     ElseIf x0(j) >= 0 Then
      If x0(j) > maximo2 Then
      maximo2 = x0(j)
      posicion0 = j
     End If
     
    End If
  
  
 Next j

'FIN D LA NORMA
fin = 0

While fin = 0
For i = 1 To n
  For j = 1 To n
    tuputamadre(i, j) = a(i, j)
  Next j
Next i
 'PRODUCTO DE A*x0
  For i = 1 To n
   tuputamadre(i, i) = a(i, i) - q
  Next i
  Minversamej tuputamadre, n, b
  
  
  
  
  For i = 1 To n
  suma = 0
   For j = 1 To n
    suma = b(i, j) * x0(j) + suma
   Next j
   Y(i) = suma
  Next i
 
 
 'FIN
 'OTRA NORMA
 For j = 1 To n
maximo2 = 0
posicion1 = 1
Next j
 For j = 1 To n
  
    If Y(j) < 0 Then
     
     Y(j) = -Y(j)
     If Y(j) > maximo2 Then
      posicion1 = j
      maximo2 = Y(j)
     End If
     Y(j) = -Y(j)
     ElseIf Y(j) >= 0 Then
      If Y(j) > maximo2 Then
      maximo2 = Y(j)
      posicion1 = j
     End If
     
    End If
  
  
 Next j

 
 
 
 'FIN DE OTRA NORMA
 'FINAL DE LOS CAULCULO
  mu = q + 1 / Y(posicion0)
  For i = 1 To n
   x1(i) = Y(i) / Y(posicion1)
  
  Next i
  For i = 1 To n
   d(i) = x1(i) - x0(i)
  Next i
 
 
 'FIN
 'OTRA NORMA
 
  For j = 1 To n
dif = 0
p = 1
Next j
 For j = 1 To n
  
    If d(j) < 0 Then
     
     d(j) = -d(j)
     If d(j) > dif Then
      p = j
      dif = d(j)
     End If
     d(j) = -d(j)
     ElseIf d(j) >= 0 Then
      If d(j) > dif Then
     dif = d(j)
      p = j
     End If
     
    End If
  
  
 Next j

 
 
 
 'FIN DE LA NORMA
 EscribirNl "ITERACIÓN  ", iteracion
 EscribirNl "El VECTOR Y =", Y
 EscribirNl "P1 =  ", posicion1
 EscribirNl "EL VEXTOR X1 "
 EscribirNl x1
 EscribirNl "VALOR PROPIO OBTENIDO = ", mu
 EscribirNl "=================================================================================================================================="
 If dif > error Then
  For j = 1 To n
   x0(j) = x1(j)
   posicion0 = posicion1
  Next j
 ElseIf dif <= error Then
  fin = 1
 End If
iteracion = iteracion + 1
Wend
EscribirNl "VALOR PROPIO OBTENIDO FINAL  ", mu
EscribirNl "VEXTOR PROPIO OBTENIDO FINAL  "
EscribirNl x1

End Sub

Private Sub Command17_Click()
 n = InputBox("Tecle el orden de la matriz")
 
 ReDim a(1 To n, 1 To n) As Double
 ReDim mu(1 To n) As Double
 ReDim xin(1 To n, 1 To n) As Double
 ReDim x(1 To n) As Double
 ReDim b(1 To n) As Double
 ReDim auxiliar(1 To n, 1 To n) As Double
 For i = 1 To n
  For j = 1 To n
     a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
  Next j
 Next i
 For i = 1 To n
  mu(i) = InputBox("Teclea la entrada m(" & i & ") ")
 Next i
 
   
   For i = 1 To n
    For j = 1 To n
      auxiliar(i, j) = a(i, j)
    Next j
   Next i
   For i = 1 To n
    For j = 1 To n
     
     For k = 1 To n
      auxiliar(j, k) = a(j, k)
     Next k
     
    Next j
   
   For j = 1 To n
    a(j, j) = a(j, j) - mu(i)
   Next j
    
    For k = 1 To n
     a(i, k) = 0
     b(k) = 0
    Next k
     a(i, i) = 1
     b(i) = 1
  
     susinv a, b, n, x
     For j = 1 To n
      xin(j, i) = x(j)
     Next j
   EscribirNl "Para mu = ", mu(i)
   EscribirNl "Matriz A:"
   EscribirNl a
   EscribirNl "Vector b:"
   EscribirNl b
   EscribirNl "Solución x:", x
   EscribirNl a
  EscribirNl "=============================================================================================================================================="
    For j = 1 To n
     For k = 1 To n
      a(j, k) = auxiliar(j, k)
     Next k
    Next j
   
   Next i
   
  EscribirNl "VECTORES PROPIOS FINALES : "
  EscribirNl xin
  EscribirNl "VALORES PROPIOS FINALES : "
  EscribirNl mu
End Sub

Private Sub Command18_Click()
n = InputBox("Tecle el orden de la matriz")
ReDim q(1 To n, 1 To n) As Double, r(1 To n, 1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
ReDim d1(1 To n) As Double
Dim i As Long
Dim vectprop As Long
ReDim auxiliar(1 To n, 1 To n) As Double
ReDim b(1 To n) As Double
ReDim x(1 To n) As Double
ReDim xin(1 To n, 1 To n) As Double
ReDim qf1(1 To n, 1 To n) As Double
Dim iteracion As Long
Dim j As Long
ReDim mu(1 To n) As Double
Dim norma As Double
Dim error As Double
error = InputBox("Introduce el error máximo posible")
Dim suma As Double
For i = 1 To n
ReDim dif(1 To n) As Double
Dim fin As Long
ReDim qf(1 To n, 1 To n) As Double
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
EscribirNl "MATRIZ A INICIAL : "
EscribirNl a
EscribirNl "====================================================================================================================================== "
ReDim d(1 To n) As Double
vectprop = 0
For i = 1 To n
 d(i) = a(i, i)
 
Next i
For i = 1 To n
 For j = 1 To n
  qf(i, j) = 0
 Next j
 qf(i, i) = 1
Next i
iteracion = 1
While fin = 0
 QR a, n, q, r

 Producto r, q, n, n, n, a
 
 Producto qf, q, n, n, n, qf1
 
 For i = 1 To n
  For j = 1 To n
   qf(i, j) = qf1(i, j)
  Next j
 Next i
 For i = 1 To n
  d1(i) = a(i, i)
 Next i
 For i = 1 To n
  dif(i) = d(i) - d1(i)
 Next i
 ' NORMA MATRICIAL
   suma = 0
   For i = 1 To n
    suma = suma + dif(i) * dif(i)
   Next i
   norma = Sqr(suma)
 'FIN DE LA NORMA
  If norma < error Then
   fin = 1
  Else
   For i = 1 To n
    d(i) = d1(i)
    
   Next i
  
  End If
  
  
  iteracion = iteracion + 1
  EscribirNl "PARA LA ITERACIÓN", iteracion
  EscribirNl "MATRIZ Q : "
  EscribirNl q
  EscribirNl "MATRIZ R : "
  EscribirNl r
  EscribirNl "MATRIZ A : "
  EscribirNl a
  EscribirNl "MATRIZ qf : "
  EscribirNl qf
  EscribirNl "=============================================================================================================================================="
  
Wend
vectprop = InputBox("Para matrices no simétricas ahora puedes obtener valores y vectores propios. 'Si'=1, 'No'=0")
If vectprop = 1 Then
   For i = 1 To n
    For j = 1 To n
     If Abs(a(i, j)) < error Then
      a(i, j) = 0
     End If
    Next j
   
   Next i
   For i = 1 To n
   mu(i) = a(i, i)
   Next i
   For i = 1 To n
    For j = 1 To n
      auxiliar(i, j) = a(i, j)
    Next j
   Next i
   For i = 1 To n
    
   
   For j = 1 To n
     
     For k = 1 To n
      auxiliar(j, k) = a(j, k)
     Next k
     
    Next j
   
   For j = 1 To n
    a(j, j) = a(j, j) - mu(i)
   Next j
    
    For k = 1 To n
     a(i, k) = 0
     b(k) = 0
    Next k
     a(i, i) = 1
     b(i) = 1
  
     susinv a, b, n, x
     For j = 1 To n
      xin(j, i) = x(j)
     Next j
   EscribirNl "Para mu = ", mu(i)
   EscribirNl "Matriz A:"
   EscribirNl a
   EscribirNl "Vector b:"
   EscribirNl b
   EscribirNl "Solución x:", x
   EscribirNl a
  EscribirNl "=============================================================================================================================================="
    For j = 1 To n
     For k = 1 To n
      a(j, k) = auxiliar(j, k)
     Next k
    Next j
   
   Next i
   
  EscribirNl "VECTORES PROPIOS FINALES : "
  EscribirNl xin
  EscribirNl "VALORES PROPIOS FINALES : "
  EscribirNl mu
End If
End Sub

Private Sub Command2_Click()

n = InputBox("Tecle el orden del sistema")
ReDim x(1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
Dim i As Long
Dim j As Long
Dim cte As Double
'ReDim b(1 To n) As Long
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
'For i = 1 To n
' b(i) = InputBox("Teclea la entrada b(" & i & ") de la matriz")
'Next i
For j = 1 To n - 1
  For i = j + 1 To n
    cte = a(i, j) / a(j, j)
    For k = 1 To n
      a(i, k) = a(i, k) - a(j, k) * cte
    Next k
    
 '   b(i) = b(i) - b(j) * cte
    a(i, j) = 0
  Next i
Next j

EscribirNl "La Matriz A =", a
'For i = 1 To n
'  For j = 1 To n
'   MsgBox ("La entrada a(" & i & "," & j & ") = " & a(i, j))
'  Next j
'Next i
  
End Sub

Private Sub Command3_Click()
  n = InputBox("Tecle el orden del sistema")
ReDim x(1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
Dim i As Long
Dim j As Long
Dim cte As Double
ReDim b(1 To n) As Double
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
For i = 1 To n
 b(i) = InputBox("Teclea la entrada b(" & i & ") de la matriz")
Next i
For j = 1 To n - 1
  For i = j + 1 To n
    cte = a(i, j) / a(j, j)
    For k = 1 To n
      a(i, k) = a(i, k) - a(j, k) * cte
    Next k
    
    b(i) = b(i) - b(j) * cte
    a(i, j) = 0
  Next i
Next j
x(n) = b(n) / a(n, n)
For i = n - 1 To 1 Step -1
  suma = 0
  For j = i + 1 To n
    suma = a(i, j) * x(j) + suma
  Next j
  x(i) = (b(i) - suma) / a(i, i)
Next i
EscribirNl "La solución de la ecuación es =", x
'For i = 1 To n
  
 '  MsgBox ("La solución x(" & i & ") = " & x(i))
  
'Next i
End Sub

Private Sub Command4_Click()
n = InputBox("Tecle el orden del sistema")
ReDim a(1 To n, 1 To n) As Double
Dim i As Long
Dim j As Long
Dim cte As Double
ReDim b(1 To n) As Long
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
For j = 1 To n - 1
  For i = j + 1 To n
    cte = a(i, j) / a(j, j)
    For k = 1 To n
      a(i, k) = a(i, k) - a(j, k) * cte
    Next k
    
    a(i, j) = 0
  Next i
Next j
cte = 1
For i = 1 To n
  a(i, i) = a(i, i) * cte
Next i
 MsgBox ("El determinante de la matriz es" & a(n, n))
End Sub

Private Sub Command5_Click()
n = InputBox("Tecle el orden del sistema")
ReDim a(1 To n, 1 To n) As Double
ReDim q(1 To n, 1 To n) As Double
ReDim u(1 To n, 1 To n) As Double
Dim suma As Double
Dim p As Long
Dim k As Long
Dim i As Long
Dim j As Long
Dim cte As Double

For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
For i = 1 To n
 For j = 1 To n
   q(i, j) = 0
   u(i, j) = 0
 Next j
Next i
For i = 1 To n
 q(i, 1) = a(i, 1)
 u(1, i) = a(1, i) / q(1, 1)
Next i
For j = 2 To n
 For i = j To n
  suma = 0
  For k = 1 To j - 1
    suma = q(i, k) * u(k, j) + suma
  Next k
  q(i, j) = a(i, j) - suma
 Next i
 p = j
 For i = j + 1 To n
  suma = 0
  For k = 1 To p - 1
   suma = suma + u(k, i) * q(p, k)
  Next k
  u(p, i) = (a(p, i) - suma) / q(p, p)
 Next i
Next j
For i = 1 To n
  u(i, i) = 1
Next i

EscribirNl "La Matriz L"
EscribirNl q
EscribirNl "La Matriz U "
EscribirNl u
EscribirNl "================================================================================================================="
'For i = 1 To n
'  For j = 1 To n
'   MsgBox ("La entrada L(" & i & "," & j & ") = " & q(i, j))
'  Next j
'Next i
'For i = 1 To n
'  For j = 1 To n
'   MsgBox ("La entrada U(" & i & "," & j & ") = " & u(i, j))
'  Next j
'Next i

End Sub

Private Sub Command6_Click()
 n = InputBox("Tecle el orden del sistema")

ReDim x(1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
Dim i As Long
Dim j As Long
Dim S As Long
Dim prueb As Double
Dim k As Long
Dim u As Long
Dim Y As Long
Dim suma As Double
Dim otro As Double
Dim nmax As Long
Dim t As Long
Dim det As Double
Dim aux As Double
ReDim max(1 To n) As Double
Dim cte As Double
Dim solucion As Long
Dim encontrado As Long
ReDim b(1 To n) As Double
ReDim signo(1 To n, 1 To n) As Double
For i = 1 To n
  For j = 1 To n
    signo(i, j) = 0
  Next j
Next i
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i
For i = 1 To n
 b(i) = InputBox("Teclea la entrada b(" & i & ") de la matriz")
Next i


solucion = 1
i = 1
det = 1


For i = 1 To n - 1
   t = i
  If a(i, i) = 0 Then
    S = t
    j = t + 1
    encontrado = 0
    For j = t + 1 To n
      prueb = a(j, t)
      
      If prueb <> 0 Then
       S = j
       j = j + 5
       
      End If
     
     Next j
    If S <> t Then
      For k = 1 To n
        aux = a(t, k)
        a(t, k) = a(S, k)
        a(S, k) = aux
      Next k
      aux = b(i)
      b(i) = b(S)
      b(S) = aux
      det = -det
     
      ElseIf S = i Then
      solucion = 0
    
     End If
  
   
   

  End If
  
  If t <= n - 1 Then
    For Y = 1 To n
    max(Y) = 0
    Next Y
    For j = t To n
     For k = t To n
       If a(j, k) < 0 Then
         a(j, k) = -a(j, k)
         signo(j, k) = 1
       End If
       If a(j, k) > max(j) Then
         max(j) = a(j, k)
       End If
       If signo(j, k) = 1 Then
         a(j, k) = -a(j, k)
         signo(j, k) = 0
       End If
     Next k
    Next j
   
    nmax = 0
    u = t
    For j = t To n
            
      If a(j, t) < 0 Then
       a(j, t) = -a(j, t)
       signo(j, t) = 1
      End If
      otro = a(j, t) / max(j)
      If otro > nmax Then
       nmax = otro
       u = j
      End If
      If signo(j, t) = 1 Then
      
       a(j, t) = -a(j, t)
      End If
      
    Next j
    If u <> t Then
      For k = 1 To n
        aux = a(t, k)
        a(t, k) = a(u, k)
        a(u, k) = aux
      Next k
      aux = b(t)
      b(t) = b(u)
      b(u) = aux
      det = -det
  End If
   
  End If
  cte = 1
  If solucion = 1 Then
   
  
   For j = t + 1 To n
    cte = a(j, t) / a(t, t)
    For k = t + 1 To n
      a(j, k) = a(j, k) - a(t, k) * cte
    Next k
    
    b(j) = b(j) - b(t) * cte
    a(j, t) = 0
   Next j
    
  End If
  
  
 
Next i
 
If a(n, n) = 0 Then

solucion = 0
End If
For i = 1 To n
  det = det * a(i, i)
Next i
x(n) = b(n) / a(n, n)
For i = n - 1 To 1 Step -1
  
  suma = 0
  For j = i + 1 To n
    suma = a(i, j) * x(j) + suma
  Next j
  x(i) = (b(i) - suma) / a(i, i)
Next i
EscribirNl "La solución del sistema es x =", x
EscribirNl "El determinanta de la matriz es = ", det

'For i = 1 To n
  
   'MsgBox ("La solución x(" & i & ") = " & x(i))
  
'Next i
 MsgBox ("El determinante de la matriz es: Det = " & det)
If solucion = 0 Then
  MsgBox ("El sistema que has intentado resolver no tiene solución")
End If


End Sub

Private Sub Command7_Click()
n = InputBox("Tecle el orden del sistema")
ReDim l(1 To n, 1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
ReDim d(1 To n, 1 To n) As Double

Dim i As Long
Dim j As Long
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
   
 Next j
Next i

For j = 1 To n - 1
  For i = j + 1 To n
    d(i, j) = a(i, j) / a(j, j)
    For k = 1 To n
      a(i, k) = a(i, k) - a(j, k) * d(i, j)
    Next k
    
    
    a(i, j) = 0
  Next i
Next j
For i = 1 To n
 d(i, i) = 1
Next i

EscribirNl "La matriz L= ", d
EscribirNl "La matriz U= ", a
'For i = 1 To n
 'For j = 1 To n
'  MsgBox ("La matriz L(" & i & "," & j & ") = " & d(i, j))
' Next j
'Next i
'For i = 1 To n
' For j = 1 To n
'  MsgBox ("La matriz U(" & i & "," & j & ") = " & a(i, j))
' Next j
'Next i
End Sub

Private Sub Command8_Click()
  n = InputBox("Tecle el orden del sistema")
ReDim l(1 To n, 1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
ReDim d(1 To n, 1 To n) As Double
ReDim c(1 To n, 1 To n) As Double
Dim suma As Double
Dim i As Long
Dim j As Long
For i = 1 To n
 For j = 1 To n
  a(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")
  d(i, j) = 0
 Next j
Next i
For i = 1 To n
 For j = 1 To n
   c(i, j) = a(i, j)
 Next j
Next i
For j = 1 To n - 1
  For i = j + 1 To n
    l(i, j) = a(i, j) / a(j, j)
    For k = 1 To n
      a(i, k) = a(i, k) - a(j, k) * l(i, j)
    Next k
    
    
    a(i, j) = 0
  Next i
Next j
For i = 1 To n
 l(i, i) = 1
Next i


d(1, 1) = c(1, 1)

For i = 2 To n
 suma = 0
 For k = 1 To i - 1
  suma = suma + l(i, k) * l(i, k) * d(k, k)
 Next k
 d(i, i) = c(i, i) - suma
 'MsgBox ("suma" & suma)
 suma = 0
 
Next i

EscribirNl "La matriz L: ", l
EscribirNl "La matriz D: ", d

'For i = 1 To n
 'For j = 1 To n
  'MsgBox ("La matriz L(" & i & "," & j & ") = " & l(i, j))
 'Next j
'Next i
'For i = 1 To n
 'For j = 1 To n
  'MsgBox ("La matriz D(" & i & "," & j & ") = " & d(i, j))
 'Next j
'Next i
End Sub

Private Sub Command9_Click()
n = InputBox("Tecle el orden del sistema")
Dim m As Long
m = 2 * n
ReDim c(1 To n, 1 To m) As Double
ReDim b(1 To n, 1 To n) As Double
ReDim a(1 To n, 1 To n) As Double
ReDim x(1 To n, 1 To n) As Double
ReDim aux(1 To n, 1 To n) As Double
Dim suma As Double
Dim i As Long
Dim j As Long
Dim k As Long

Dim cte As Double
For i = 1 To n
 For j = 1 To n
  c(i, j) = InputBox("Teclea la entrada a(" & i & "," & j & ") de la matriz")

 Next j
Next i
For i = 1 To n
  For j = n + 1 To m
    If i = j - n Then
      c(i, j) = 1
    ElseIf i <> j Then
      c(i, j) = 0
    End If
  Next j
Next i


For j = 1 To n - 1
  For i = j + 1 To n
    cte = c(i, j) / c(j, j)
    For k = 1 To m
      c(i, k) = c(i, k) - c(j, k) * cte
    Next k
    
    c(i, j) = 0
  Next i
Next j
For i = 1 To n
 For j = 1 To n
  a(i, j) = c(i, j)
 Next j
Next i
For i = 1 To n
 For j = n + 1 To m
  b(i, j - n) = c(i, j)
 Next j
Next i

For p = 1 To n
 x(n, p) = b(n, p) / a(n, n)
For i = n - 1 To 1 Step -1
  suma = 0
  For j = i + 1 To n
    suma = a(i, j) * x(j, p) + suma
  Next j
  x(i, p) = (b(i, p) - suma) / a(i, i)
Next i
Next p
EscribirNl "La inversa de la matriz  A es  =", x
'For i = 1 To n
' For j = 1 To n
 ' MsgBox ("La matriz Inversa es 1/a(" & i & "," & j & ") = " & x(i, j))
' Next j
'Next i
'MEF n, x(), y(), m, NodoE(), p, LadoN(), q, NodoD(), u()
End Sub


Public Sub Producto(a() As Double, b() As Double, filasA As Long, columnasA As Long, filasB As Long, c() As Double)
Dim n As Long, m As Long, h As Long
m = filasA
n = columnasA
p = filasB

For i = 1 To m
 For j = 1 To p
   suma = 0
   For k = 1 To n
    suma = suma + a(i, k) * b(k, j)
   Next k
   c(i, j) = suma
 Next j
Next i

End Sub

