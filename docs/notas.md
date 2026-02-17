# Notas de clase

## Comandos de consola

- `dotnet new` Solo te muestra las plantillas instaladas para crear un proyecto
- `dotnet new gitignore` Crea en la carpeta un fichero .gitignore con los tipicos directorios de un proyecto .net para iniciar el repositorio adecuadamente
- `dotnet new sln nombresolucion`
- `dotnet new blazor nombreapp` Blazor equivale a la plantilla que seleccionamos para web app server con parametros por defecto
- `dotnet new blazor -h` Nos da los parametros para configurar al crear el proyecto
- `dotnet sln add` Añadir proyectos a la solucion para poder facilitar ambos entornos VSCode y VS2022.
- `dotnet watch` Ejecutar en modo depuración con hotreload (algunos cambios se refrescan al guardar otros requieren reiniciar)
- `start nombresolucion.sln` Abren visualStudio2022
- `dotnet run` Ejecutar app

## Clase 7

- Paso de valores de componente página a un componente contenido: igual que paso de parametros en url, definiendo las variables bajo el decorador `[Parameters]`.
- Podemos "mapear" los eventos que definamos en el componente contenido como async await en la página a metodos de la página, pero...
- Hace clickbait del ciclo de vida de los componentes para la siguiente clase...

## Clase 8

- No podemos depender de variables que nos trae el componente que nos contiene para el ciclo de vida y el renderizado del propio componente.
- Tenemos que hacer `override` para redifinir los metodos que hereda de su clase Component para el **ciclo de vida** como `OnParametersSet`

## Clase 9

- Que es un **EditForm**. Es un componente de blazor para usar formularios html, correspondiente a `<form>`. Da soporte para validacion y submit.
- Enlace bidireccional entre el formulario y una clase Modelo.
- Echar un vistazo a los elementos que tiene blazor por defecto (sin mudBlazor):
  - InputCheckBox
  - InputDate`<TValue>`
  - InputFile
  - InputNumber`<TValue>`
  - InputRadio`<TValue>`
  - InputSelect`<TValue>`
  - ...

## Notas Git

Nuevo repo:

- Después de git remote add origin `https://github.com/Joselon/CursoBlazorHome.git` antes de push: `git push --set-upstream origin master`
