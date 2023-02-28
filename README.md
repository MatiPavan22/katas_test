# katas_test

Este proyecto esta compuesto por 2 partes:
  1. App desarrollada en React con JS.
  2. Backend desarrollado en .NET.

Para ejecutar el backend simplemente hay que hacer el build y el run de dotnet.

Para ejecutar la app simplemente hay que ejecutar:
  1. yarn (para instalar los paquetes)
  2. yarn dev (para ejecutar)
  
  El backend consiste unicamente en un endpoint el cual se encarga de generar una nueva tirada pasandole en el body lo siguiente:
    username -> Nombre del usuario que tira.
    initialSquare -> cuadrado actual donde se encuentra la ficha del usuario
    spacesToMoved -> los espacios a mover seria el numero del dado que tiro el usuario al azar (esto debe ser pasado en el body y es un numero del 1 -6)
    gameIsStarted -> establece si el juego esta por empezar o no
