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

## (resto)

## Clase 38 Javascript

-
- Javascript wrapper en **wwwroot/js** para indicar los metodos que necesitemos de javascript como el uso de localstorage (setItem, etc...) en `window.localStorageWrapper = {...}`

```js
window.localStorageWrapper = {
    setItem: function (key, value) {
        localStorage.setItem(key, value);
    },
    getItem: function (key) {
        return localStorage.getItem(key);
    },
    removeItem: function (key) {
        localStorage.removeItem(key);
    },
    clear: function () {
        localStorage.clear();
    }
};
```

- Luego ya en una carpeta Services (por ejemplo) lo usamos en **LocalStorageService.cs**, que tiene un atributo de la clase IJSRuntime que se asocia en el constructor de este service.

```cs
namespace BlazorAndJS;


using Microsoft.JSInterop;
using System.Threading.Tasks;

public class LocalStorageService
{
    private readonly IJSRuntime _jsRuntime;

    public LocalStorageService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SetItemAsync(string key, string value)
    {
        await _jsRuntime.InvokeVoidAsync("localStorageWrapper.setItem", key, value);
    }

    public async Task<string> GetItemAsync(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("localStorageWrapper.getItem", key);
    }

    public async Task RemoveItemAsync(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorageWrapper.removeItem", key);
    }

    public async Task ClearAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorageWrapper.clear");
    }
}
```

- Centralizamos en esta clase la interaccion con JS para desacoplarlo.

- En el componente página ya lo usamos así:

```cs
@page "/jsinterop"
@inject LocalStorageService LocalStorage
@inject IHttpContextAccessor HttpContextAccessor
@rendermode InteractiveServer

<PageTitle>Local Storage Demo!!!</PageTitle>

<h3>Local Storage Demo!!!</h3>

<button @onclick="SetItem">Set Item</button>
<button @onclick="GetItem">Get Item</button>
<button @onclick="RemoveItem">Remove Item</button>
<button @onclick="Clear">Clear</button>


<p></p>

<input type="text" @bind="localValue" />

@code {

    private string localValue = "";
    [CascadingParameter] private HttpContext? HttpContext { get; set; }
    private bool isPreRendering;

    protected override async Task OnInitializedAsync()
    {
        isPreRendering = HttpContext?.RequestServices.GetService(typeof(IHttpContextAccessor)) != null;
        if (!isPreRendering)
        {
            await GetItem();
        }
        await base.OnInitializedAsync();

    }
    

    private async Task SetItem()
    {
        if (!string.IsNullOrEmpty(localValue))
        {
            await LocalStorage.SetItemAsync("exampleKey", localValue);
        }
    }

    private async Task GetItem()
    {
        var value = await LocalStorage.GetItemAsync("exampleKey");
        localValue = value;
    }

    private async Task RemoveItem()
    {
        await LocalStorage.RemoveItemAsync("exampleKey");
        localValue = "";
    }

    private async Task Clear()
    {
        await LocalStorage.ClearAsync();
        localValue = "";
    }
}
```

- Tenemos que usar e inyectar también el httpcontext para poder ejecutar js antes del onRendered ya que si no no veriamos los datos nunca, se quedarían en las variables. Para poder usar ese localstorage en el onInitializedAsync tenemos que comprobar si se ha cargado con: `isPreRendering = HttpContext?.RequestServices.GetService(typeof(IHttpContextAccessor)) != null;`

## Clase 39 Gestion de usuarios Seguridad

- Con using AspNetCore.Identity tenemos toda la gestión ya hecha.
- No es necesario que se haga con SqlServer, se puede usar cualquier base de datos instalando el paquete correspondiente.
- Habrá que modificar lo que tenemos en program.cs para la conexión a bd.
- Si queremos extender la estructura de usuarios y cuentas que nos da, tenemos la clase ApplicationDbContext que viene vacia para incluir esas nuevas tablas o relaciones que no estén en IdentityDbContext (que representa todas las tablas de base de datos en clase de c#)

## Notas Git

Nuevo repo:

- Después de git remote add origin `https://github.com/Joselon/CursoBlazorHome.git` antes de push: `git push --set-upstream origin master`

[Repositorio de ejemplos](https://github.com/EscuelaIt/blazor-ejemplos-curso.git)
