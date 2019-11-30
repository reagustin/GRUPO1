# GRUPO1

# Ultima modificacion del 30-11-2019
Se agrego validacion del formato a los ingresos de stock.

# Modificacion del 27-11-2019
Se corregieron algunos aspectos esteticos de mensajes y validaciones de negocio.

# Modificacion 25-11-2019, se controla que todos los productos del pedido tengan stock, caso contrario el pedido no se procesa
Tampoco se elimina, solo que se "saltea", de forma tal que si se procesa otro lote de produccion que aumenta el stock en el siguiente envio a logistica se vuelve a procesar. Como los pedidos tienen orden cronologico se va a procesar primero.

***NUEVA FUNCIONALIDAD***
El programa va a controlar si la carpeta TP existe, de lo contrario va a crear la misma en el raiz del C.


***ATENCION***
LOS SIGUIENTES ARCHIVOS SON OBLIGATORIOS PARA EL CORRECTO FUNCIONAMIENTO DEL SISTEMA, LOS MISMOS TIENEN QUE ESTAR DENTRO DE LA CARPETA "TP" EN EL RAIZ DEL C.

Stock.txt
pedidos.txt
pedidoscodigohistorico.txt
DevolucionesProcesadas.txt


NOTA:
Para adicionar Stock, solo basta con armar un archivo con igual estructura que el stock, y poner los productos y cantidades, luego seleccionar el primer option boton de la aplicacion llamado "Recepcion linea de produccion" y procesarlo.

P60;500
P124;100
P1200;400
Siendo el primer valor el codigo de producto y a continuacion del ";" la cantidad del mismo.


NOTA:
Aquellos productos que no se encuentre en el stock, son agregados como nuevos productos.

Mi archivo de Stock.txt tiene el siguiente formato, ej:
P60;500
P124;100
P1200;400


ATENCION: En caso de que el archivo no exista el sistema le dara la opcion de crear un archivo stock vacio al inicio de la aplicacion, para continuar con el funcionamiento, en caso de no aceptar el sistema se cerrara.


NOTA:
En caso de recibir un pedido que tiene un producto que no existe en stock, o que existe pero la cantidad no es suficiente, ese producto no se considera para cuando se confeccione el LOTE.


--------------------------------------------------------------------------------------------------------------------------------------------

El archivo pedidoscodigohistorico.txt se compone del siguiente formato, ejemplo:

A332
A333
A335
A336

Como se observa, unicamente tiene guardado el codigo de pedido que viene del comercio, el formato del nombre de archivo del pedido proveniente del comercio era el siguiente, ejemplo: Pedido_A332.txt

El software toma del nombre de archivo del pedido del comercio el codigo y lo guarda, para verificar contra el mismo si alguna vez proceso ese pedido y que no se vuelva a repetir.

ATENCION: En caso de que el archivo no exista el sistema le dara la opcion de crear el archivo pedidoscodigohistorico.txt al inicio de la aplicacion, para continuar con el funcionamiento, en caso de no aceptar el sistema se cerrara.

--------------------------------------------------------------------------------------------------------------------------------------------

El archivo DevolucionesProcesadas.txt se compone del siguiente formato:

A332
A335
A336
A337

Como se observa, unicamente tiene guardado el codigo de pedido que viene dentro del archivo de "Reporte de entrega" de logistica, este ultimo tiene el siguiente formato:

A332;false
A335;true

El software toma el codigo del pedido de cada linea dentro del "Reporte de entrega" proveniente de logistica, para que la devolucion no se vuelva a procesar de manera erronea, una vez que esta en esta lista, no se vuelve a procesar.

ATENCION: En caso de que el archivo no exista el sistema le dara la opcion de crear el archivo DevolucionesProcesadas.txt al inicio de la aplicacion, para continuar con el funcionamiento, en caso de no aceptar el sistema se cerrara.


-------------------------------------------------------------------------------------------------------------------------------------------

El archivo Pedidos.txt se compone del siguiente formato:

A335;C18624;Jorgito SRL;20-25797888-5;ObiWan 2674 Quilmes;True
P325;30
P900;100
P2100;450

La primera linea de 6 elementos separados por ";" corresponden con el pedido enviado por el comercio. El ultimo elemento booleano determina si el pedido fue despachado a logistica o no. Luego abajo se listan productos separados por ";" y luego las cantidades.

ATENCION: En caso de que el archivo no exista el sistema le dara la opcion de crear el archivo pedidos.txt al inicio de la aplicacion, para continuar con el funcionamiento, en caso de no aceptar el sistema se cerrara.


--------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------



Mi archivo de envio a logistica tiene el siguiente formato:

Ponzio SRL;20-12774982-2;Hortiguera 600
---
A338;ObiWan 2674 Quilmes
P124;10

Igual a como fue indicado en la consigna:

[Rte. Razón Social];[Rte. CUIT];[Dirección devolución]
---
[Cod. Referencia];[Dirección entrega]
[Cod. De producto];[Cantidad]
[Cod. De producto];[Cantidad]

Este archivo nos indica nuestro datos en una linea como remitentes, y luego por debajo de la linea punteada la referencia de pedido y destino, y finalmente por debajo se listan codigos de productos y cantidades separados por ";".
