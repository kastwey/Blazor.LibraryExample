# Blazor.LibraryExample


Este pequeño proyecto de ejemplo es una simple prueba de concepto del uso de Blazor, incluyendo algunos mecanismos para mejorar la accesibilidad en una aplicación de tipo SPA.
Para más información, puedes echarle un vistazo a la documentación de Blazor en: [https://blazor.net/docs/](https://blazor.net/docs/).

## Mejoras de accesibilidad aplicadas

### 1. Focalización y gestión de los cambios
Uno de los grandes problemas de la interacción con una SPA y un lector de pantallas, es la dificultad de detectar qué cambios se han producido en el documento, y en qué lugar de la web debería focalizar su atención.
Para resolver esto, he creado un mecanismo de focalización, que permite focalizar cualquier elemento de la web tras la renderización. Así, cada vez que se navega a una nueva página, el foco se colocará en el encabezado de nivel 2, que da comienzo a la sección de dicha página.

### 2. Links sin atributo href y uso de aria role
He modificado algunos roles de enlaces sin atributo href (pues al introducir un href con valor # Blazor no realizaba la acción correctamente), y al eliminarlo, algunos lectores de pantalla no detectaban la etiqueta a como enlace. Por tanto, la única manera de conseguir una buena legibilidad con lectores de pantalla, ha sido añadir el atributo role="link" en las etiquetas "a" sin atributo href.


### 3. Ocultación de textos mediante CSS

Los lectores de pantalla tienen mecanismos para obtener listas de casi cualquier cosa, entre ellas, de enlaces. En una tabla de resultados, los enlaces cuyo texto es solo: "Editar", "Eliminar"... no tienen sentido en una lista descontextualizada. Es por ello que los enlaces necesitan tener un contexto por sí mismos. Para evitar problemas con el diseño y textos muy largos, una de las técnicas más utilizadas es la de ocultar textos mediante CSS. El usuario que utilice un lector de pantallas escuchará el texto completo (Editar Todo es eventual, por ejemplo), y el usuario no ciego verá solo la parte visible del enlace, Editar, en este caso.

### 4. Ajuste del título según la página actual

Es imprescindible que el título de la página (la etiqueta "title" de la cabecera), contenga el valor real de la página en la que nos encontremos, de lo más particular a lo más general. Así, si estamos en inicio > biblioteca > novedades, un título válido podría ser: Novedades - biblioteca - Blazor Library Example. He utilizado la característica de interoperabilidad con Javascript, para cambiar al vuelo el título de la página cada vez que se navega a una página diferente.

## Calidad del código

Soy totalmente novato con Blazor, así que seguro que he cometido un montón de errores en el código. ¡Cualquier aportación o mejora será más que bienvenida!

¡Espero que os parezca interesante! ¡Gracias!
