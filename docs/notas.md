# Notas de clase
# NOTAS DE CLASES BLAZOR

Ejemplos en [RepoEscuelaIT](https://github.com/EscuelaIt/blazor-ejemplos-curso)

## Introduccion

- Está pensado para aplicacion SPA (Single Page Application)
- Desde 2015, .NET ha cambiado los mitos desde que sacó .NET CORE
- .NET Framework está finalizado. Ultima versión 4.8 (solo sacan updates de seguridad)
- Renombraron .NET CORE (despues de sacar versiones 1 y 2) a .NET en su version 5 unificaron. Sacan una version por año. La 9 en 2024. Las versiones pares se mantienen y las impares no. Ya la 7 no se puede usar.
- Se puede plantear una migración de 4.8 con WindowsForm a .net 8 sin tener que reescribir todo como es el caso de WebForms que pasan a Blazor. Aunque si utiliza paquetes de controles externos, si esos paquetes no existen para .net 8 va a ser más complicado. No sería multiplataforma porque ya estamos diciendo que es winform que es solo windows.

## Caracteristicas de Blazor

- Sin necesidad de usar JavaScript
- Ideal para desarrolladores de dotnet y C#
- Tiene dos modelos basicos de ejecución:
    - Blazor Server - Los componentes se ejecutan en el servidor. Los componentes generan un html, un DOM y lo mandan al navegador. Y el navegador se comunica con el servidor y actualiza los componentes. Se mandan usando Websockets con SignalIR (es libreria que es una abstracción sobre websockets)
    - Blazor WebAssembly (WEBASM) - Toda la aplicación y los componentes se ejecutan en el cliente (navegador). Para comunicar con servidor tendrá que usar una API como cualquier pagina.
Actualmente (2024) los dos modelos son compatibles. Un mismo componente se puede renderizar una parte en servidor y otra en cliente.

### Ventajas y desventajas Blazor Server

Ventajas:
- Descarga pequeña.
- Componentes tiene toda la potencia de .NET (puede usar las conexiones a base de datos directmente, por ejemplo, si necesita datos puede abrir un sqlconection sin problema porque estamos en el servidor)
- Soporta clientes que no tengan soporte para WebAssembly
- El código no llega al cliente. Solo se comunica por websockets.

Desventajas:
- Tiene mayor latencia. Sin soporte offline.
- Requiere servidor web dotnet

### Ventajas y desventajas Blazor Webasm
Ventajas:
- No requiere servidor.
- La carga de trabajo se mueve al cliente.

Desventajas:
- Tiempo de descarga e inicializacion superior.
- Requiere

## Primer Proyecto Clase 1

Ver que tipos de proyectos podemos crear: `dotnet new --list`

- Este es server side o webassembly:

```sh
Aplicación web Blazor                                      blazor                      [C#]        Web/Blazor/WebAssembly
``` 
- Esta es solo webassembly

```sh
Aplicación independiente WebAssembly de Blazor             blazorwasm                  [C#]        Web/Blazor/WebAssembly/PWA
``` 


En la carpeta de los proyectos, creamos una aplicacion con nombre demowebasm: `dotnet new blazorwasm -n demowebasm` Si no le ponemos más parametros los coge por defecto (en mi caso ha cogido la 9). Para definir los parametros es mejor crearlo con VisualStudio.

Creando el proyecto desde VisualStudio: Seleccionamos Nuevo proyecto -> Blazor Web App -> Sin marcar sln en misma carpeta. Se elige la version de .NET (nosotros la 8.0). En interactive render mode si no queremos nada de webassembly(soporte offline) elegimos Server. Y dejamos por defecto Interactive location como "Por pagina/componente".

Si ponemos Auto(server y wasm) crea dos proyectos dentro de la solución, uno para server y otro cliente.

En los ejemplos tenemos la app "RenderModes" que es una aplicacion solo server.

- Para cambiar de un tipo de proyecto a otro hay recursos de migración, pero dependiendo de como sea el componente, si no tiene nada especial, podemos moverlos de un proyecto a otro, de webasm a server. Al reves tenemos que adaptar (por ejemplo usando API)

## COMPONENTES

- Blazor se orgniza por componentes blazor
- 1 componente tiene:
    - Código de UI (HTML - Razor)
    - Código de lógica
- Tienen propiedades y lanzan eventos

- Para ejecutar la aplicacion en modo debug desde consola: `dotnet watch` (activa hot reloading, blazor lo soporta en algunos casos)

- En los componentes hay una etiqueta  @code donde va todo el codigo del componente que no es más que una clase especial, pero una clase, con sus atributos y metodos.

- El resto fuera de @code es el html(razor) y podemos invocar atributos del code con @ (cambia el contexto) y metodos también, por ejemplo con @onclick="nombreMetodo" indicamos que es onclick es un evento que va a ser gestionado por nuestro metodo.

- Ventaja con respecto a javascript: al ser un lenguaje tipado tendremos errores de compilacion antes de ir al navegador.

- Para CSS tiene su propio modelo.

- Los componentes se autoredenrizan.

- Hay otra formas de renderizar:
    - **Streaming rendering**: lo renderiza en servidor pero por partes. Por ejemplo si tiene consultas a BD, primero pinta un esqueleto y un loading... y cuando termina la consulta la pone automaticamente renderizada correctamente.
    - **Interactive SSR (Server-side rendering)**
    - **CSR (Client side rendering)**
    - **Auto(SSR+ CSR)** Primera instancia SSR rapido y luego en actualizaciones CSR.

    - En la carpeta components del proyecto vemos que hay componentes pagina y otros que no son pagina, no hay diferencias, son iguales, pero los componentes página está enrutados en la tabla de rutas, y para ello se usa la directiva @page donde se pone la ruta. Los que no lo tiene solo pueden ser usados dentro de otros componentes que lo tengan.
    - *Detalle del código del proyecto RenderModes* El componente WeatherPersisted está mal escrito: WeahterPersisted y para probarlo como en clase hay que comentar la directiva con @* ... *@
    - Blazor intenta que se carguen los componentes lo más rápido posible para que el usuario vea algo cuanto antes. Hace un primer renderizado **pre-render**, en este caso hay una primera carga de los valores aleatorios que devuelve forecast, luego cuando ya está cargada toda la página, se hace un segundo renderizado (con otros valores aleatorios) y ya tendrá toda la funcionalidad. El primer renderizado no es interactivo, si hubiera botones no serán funcionales hasta la segunda carga.
    - Este pre-renderizado hace que si, por ejemplo, los datos se traen de consultar una API, se esté consultando la API dos veces. Se puede evitar si se guardan los valores de forma persistente en la primera consulta, se usarán para el segundo renderizado. `@inject PersistentComponentState ApplicationState` con esta dependencia podemos registrar la tarea Persist (donde indicamos lo que persiste) en `_subscription = ApplicationState.RegisterOnPersisting(Persist);` y asi podemos recuperar una cache de datos en un json:  `var foundInState = ApplicationState.TryTakeFromJson<WeatherForecast[]>("forecasts", out var cachedForecasts);`.
    - Otra forma de evitar la doble carga de datos de pre-render y render final, es desactivar el pre-render indicando: `<Weather @rendermode=@(new InteractiveServerRenderMode(false)) />` donde bool prerender = false. Es más cómodo para el programador pero es peor para el usuario.
    - Este @rendermode lo podemos poner dentro del componente y así no tenemos que ponerlo cada vez que lo usamos en otro componente (como hacemos en Home.razor).

- Los componentes heredan de una clase componente, podemos ver los metodos que heredan del ciclo de vida que tienen los componentes: override ... y vemos los distintos metodos que se llaman en los distintos momentos del componente. Cuando se ha cargado, cuando se ha renderizado, antes de ...
- Aunque un componente sea página (@page ruta) no quiere decir que no lo pueda incluir dentro de otros componentes.

- Tenemos otro tipo de componentes que se generan solos al crear el proyecto: **Layouts** : (`@inherits LayoutComponentBase`) Este es el componente real que se renderiza y renderiza los otros componentes. Todas las paginas tienen el mismo layout y en `@Body` es donde se renderiza ese componente pagina.
    - Podemos tener distintos layouts según la ruta.
    - NavMenu.razor tiene solo html con etiquetas que proporciona Blazor como `<NavLink ...>...</NavLink>` (sin MudBlazor)
- Desarrollar en blazor en su mayoría es desarrollar estos componentes.
- Veremos:
    - Como sabe blazor cuando se debe re-renderizar un componente, cuando no. 
    - Como hacemos los enlaces de datos. Como evitar por ejemplo que si el componente está en render mode auto (que primero se renderiza en servidor y luego en cliente) que no haga la llamada a la API ya que está en el servidor y tiene acceso directo a los datos.

El puerto usado se configura en el proyecto en Properties: launchSettings.json Donde tenemos la configuración para levantar la app en http, https y de IIS Express.

## Clase 2

- Los componentes página tienen la directiva `@page "/url/{id}"` con la ruta y ésta puede tener parámetros. Para estos parámetros hay que tener en cuenta estas restricciones:
    - Si son tipados: {id:int} -> Si en la ruta no hay un entero, si no alfanuméricos el componente ya no recibe esa ruta, es así como el sistema de rutas lo para antes de que el componente de un error por recibir otro tipo.
    - Si son opcionales {id?}
    - Catch-all: {*param} -> Todo lo que no ha sido capturado por el sistema de ruta y los parametros anteriores van aqui.

- Detalle de creacion de proyectos fuera de visualstudio2022 -> El fichero sln no hace falta, pero para facilitar que unos lo usen y otro no, podemos crear el fichero por consola con: `dotnet new sln nombreproyecto`. Si ahora ponemos `start nombreproyecto.sln` Nos abre una solucion vacia en visualStudio2022. Ahi podemos añadir el proyecto en el explorador de soluciones. O también lo podemos hacer por consola: Lo creamos `dotnet new blazorwasm -n demowebasm`(*Indicando todos los parametros que definimos en el wizard) y luego lo agregamos con `dotnet sln nombreproyecto add demowebasm`.

- Creamos uno nuevo desde el wizard de visualstudio2022 -> Template: Blazor Web App con interactive mode:Server. Borramos los componentes por defecto. (En nuestro caso hemos agregado también MudBlazor (8.15.0 ultima estable) con el Administrador de Paquetes NuGet y la version de .NET 8.0)
    - (PENDIENTE) NOTA Revisar warning al iniciar git: 
    
    ```sh
     > git add .
     warning: in the working copy of 'DemoBlazorWeb/wwwroot/bootstrap/bootstrap.min.css', LF will be replaced by CRLF the next time Git touches it
    ```
- Nos quedamos entonces solo con los componentes página Home y error. Agregamos un nuevo componente. (desde consola no hace falta más que crear un fichero de texto .razor).
- Si al nuevo componente le ponemos @page y una ruta con parametro entero, ya vimos que lo podemos usar en otro componente ignorando la ruta y el parámetro (PENDIENTE Ver que valor tiene por defecto > VISTO depende del tipo, por ejemplo para int es 0) y si navegamos a la ruta ya tendremos un 404 si el parametro no es entero o un error de excepcion del componente si es entero porque aún no hemos definido el parametro en el codigo del componente. Tenemos que poner el decorador [Parameter] y luego la declaración de la variable para recogerlo. `public int id {get; set}`
- El valor del parametro tiene que cumplir las limitaciones del tipo. Por ejemplo int en c# es de 32bits por lo que el mayor numero que se puede representar es -2,147,483,648 hasta 2,147,483,647.
- En el caso de haber incluido el componente en otra página también podemos acceder al parametro desde la etiqueta html: `<DemoComponent id="10"></DemoComponent>`. Si ponemos algo que no pueda convertir en entero ya directamente no compila. Eso que ponemos entre comillas, si es un entero internamente es la constante 10, si es alfanumerico da error si no existe en @code de la página nada declarado con ese nombre, pero si existe, p ej: 

```cs
    @page "/"

    <PageTitle>Home</PageTitle>
    ...
    <DemoComponent id="papa"></DemoComponet>

    @code
    {
    private int papa = 123
    }
```

Esto compila y nos pondrá como id 123. Si papa fuera un string que contenga 123 ya da error de compilación.

### TRABAJANDO CON EL DOM

- Creamos el componente EnlacesBinding para probar como se pueden enlazar html-razor a las variables del codigo en c#.
- Ponemos un input con `@bind="name"` y luego un párrafo que incluya la variable name. Pero el valor de la variable no cambia hasta que el input no pierde el foco, por lo que tenemos que poner algo más para que reciba el foco, por ejemplo un botón. (Además también le faltaba al componente poner @rendermode, si no lo tiene se renderiza en modo estático y no tiene en cuenta los eventos).
- También podíamos haber cambiado el evento en el cual se realiza la actualización de la variable (binding) con `@bind:event="onchange"`. (No hace caso a hotreload hay que parar y arrancar de nuevo)
- Incluso se puede hacer que se ejecute un metodo asincrono, un servicio web, etc.. con `@bind:after="method"`
- También es posible especificar que se ejecuta en cada caso cuando se establece el binding: `@bind-propiedad:get|set` (En el ejemplo anterior por defecto la propiedad es el value del input pero se puede bindear cualquier propiedad del dom de cada elemento).
    - `@bind:get` Estable el origen de la propiedad
    - `@bind:set` Establece el callbacka a llamar cuando cambia el valor


## Clase 3, 4, 5

- CLI y VSCode: Ver las versiones que tengo instaladas:

```sh
> dotnet --list-sdks
6.0.411 [C:\Program Files\dotnet\sdk]
9.0.308 [C:\Program Files\dotnet\sdk]
```

- He podido crear proyecto en .net 8 porque está embebido en visual studio 2022, lo instalo en el sistema: `winget install Microsoft.DotNet.SDK.8`
- Para crear el proyecto con los parámetros que indicamos en la plantilla vemos como ponerlos con `dotnet new blazor -h` donde blazor es la plantilla elegida.

- Con @ cambiamos de contexto, entonces podemos usar en la parte hmtl de contexto:

```cs
<ol>
 @for (var idx = 0; idx < 10; idx++)
 {
    <li> El valor es @idx </li>
 }
</ol>
```

- Tener en cuenta que cuando tenemos componentes con Interactive SSR, y usamos por ejemplo @bind:event="onchange" cada pulsacion de tecla está haciendo que el cliente se comunique con el servidor, el servidor renderice y lo mande de nuevo al cliente.
- **EVENT HANDLING** Para manejar los eventos que queramos en el DOM podemos usar `@on<evento>`, ej @onclick="nombreMetodoManejador".
    - Estos métodos pueden recibir parámetros. P ej @onmousedown="OnMouseDown" en el metodo podemos ponerle parametro: `public void OnMouseDown(MouseEventArgs param)`.
    - El metodo que definamos en `@bind:after="OnBindData"` debe ser `public async Task OnBindData` y se ejecutará cada vez que haya un binding de datos.
- Ejemplo de `bind:set y get` -> Sustituye al automático @bind="name". En set ponemos name y en get un metodo que debe recibir que parámetro se está bindeando (aunque no se use en el método). Esta forma de definir el bind también se aplica a otras propiedades con `bind-<propiedad>`

```cs
    <input @bind:set="name" @bind:get="SetName">

    @code 
    {
        private string name = "cti"

        public void SetName(string value)
        {
        name = value.ToUpper();
        }
    }
```

- Blazor nos permite no usar javaScript pero no hay problema por incluirlo.

## Clase 6

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

## Clase 9, 10, 11 EditForm

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

  - Creamos una clase que sirve de Modelo para el formulario y es candidata a una biblioteca de clases propia (DATA) donde tengamos las clases Modelo que solemos manejar.
  - En esa clase se definen las restricciones (rango, largo, si es obligatorio...) de cada propiedad/atributo.
  - En el formulario EditForm para usar la clase tendremos que importar el namespace, y con añadir `<DataAnnotationsValidator></DataAnnotationsValidator>` dentro, ya nos muestra si va a permitir submit.

## Clase 12

- Para los mensajes de validación, en el EditForm tenemos que poner `<ValidationMessage For="() => Articulo.Precio"></ValidationMessage>` la forma de referenciar el campo es con una función lambda que devuelve la propiedad que queremos. Es uno de los dos usos que tienen en c# las funciones lambda (se desambigüa por el contexto).
- Con esto nos pone los mensajes por defecto, para personalizar los mensajes se definen en la clase modelo, en la restricción añadiendo `ErrorMessage`: 
    ```cs
        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(0, 99, ErrorMessage="Debe ser un identificador de dos digitos (00-99)")]
        public int Id { get; set; }
    ```
- Si se usaran las traducciones (Culture) podemos usar las propiedades ErrorMessageResourceName y ErrorMessageResourceType, nos permitirán referenciar a los indices de una clase de traducción.
- Tambien tenemos otro componente que se pone al final del EditForm: `<ValidationSummary></ValidationSummary>` que nos muestra un resumen de las validaciones en una lista ul.

## Clase 13 

- Para crear nuestros propios validadores. Para ello habrá que crear clases (por ejemplo "EsPar") que hereden de la clase ValidationAttribute (que para cumplir la nomenclatura de C# llamaremos "EsParAttribute").
- Podemos añadir propiedades para configurar el validador, por ejemplo si permitirmos el valor 0. "public bool EsCeroValido { get; set; }"
- Deben redefinir el metodo heredado IsValid: `protected override ValidationResult? IsValid(object value?, ValidationContext validationContext)` que devuelven `return new ValidationResult($"El valor {value} debe ser par")` si no, null indicando que la validación ha funcionado.
- Ya podemos añadirlo a cada campo del modelo que queramos validar si es par: `[EsPar(EsCeroValido = true)]`
- DataAnnotations no nos sirve para validaciones cruzadas y complejas.

## Clase 14 Validaciones Cruzadas antes de enviar el formulario

- Cuando el EditForm tiene botón submit, al pulsarlo valida todo.
- Usando EditContext si podemos hacer validaciones cruzadas. Por defecto usa como contexto el modelo.
- Si lo queremos personalizar en lugar de poner Model= ponemos EditContext="nombrevariable" creando esa variable {get; set;} de tipo EditContext en el code del componente donde tenemos el EditForm, e inicializandola al cargar el componente que lo contiene (`OnInitialized()`) haciendo un `nombrevariable = new EditContext(Articulo)`.
- Si le añadimos en la incialización `nombrevariable.EnableDataAnnotationsValidation();` es equivalente a cuando el formulario de model añadimos `<DataAnnotationsValidator></DataAnnotationsValidator>`
- Hasta aquí es lo que hace por defecto al pasarle Model=... pero ahora podemos controlar el evento de validacion al pulsar submit añadiendo al editcontext.OnValidationRequested += nuestroMetodo donde colocaremos las validaciones cruzadas usando MessageStore que será un objeto de la clase ValidationMessageStore asociado a nuestro editcontext. En cada validación se hace clear y luego va añadiendo mensajes en los errores de validacion. Aquí usa de nuevo la función lambda para referenciar una propiedad del modelo.

```cs
    private EditContext EditContext { get; set; }
    private ValidationMessageStore? MessageStore;

    protected override void OnInitialized()
    {
        EditContext = new EditContext(Articulo);
        EditContext.EnableDataAnnotationsValidation();
        MessageStore = new ValidationMessageStore(EditContext);
        EditContext.OnValidationRequested += OnValidateForm;
    }

    private void OnValidateForm(object? sender, ValidationRequestedEventArgs e)
    {
        MessageStore?.Clear();

        // Validaciones cruzadas aquí

        if (Articulo.Id == 0 && !Articulo.Descripcion.StartsWith("Z"))
        {
            MessageStore?.Add(() => Articulo.Descripcion, "Must start with Z if id is Zero");
            EditContext?.NotifyValidationStateChanged();
        }
    }
```

## Clase 14 Validaciones Cruzadas al perder el foco

- Para validar al perder el foco añadimos al EditContext `EditContext.OnFieldChanged += OnFieldChangedValidate;`
- Ese metodo OnFieldChangedValidate tendrá que recibir dos parametros, el sender y un evento (object? sender, FieldChangedEventArgs e)
- El sender siempre será el editcontext y lo interesante es el evento donde nos da la información de que propiedad ha cambiado e incluye todos los valores del modelo.

## Clase 15 Eventos de submit de formulario

- EditForm tiene 3 eventos para gestionar el submit: `OnValidSubmit`, `OnInvalidSubmit` y `OnSubmit`.
- El tercero se usa para validaciones complejas que involucran varios campos. Se indican al poner el componente: `<EditForm OnSubmit="MetodoManejaEvento">` y luego definimos el metodo asociandolo al contexto `void MetodoManejaEvento(EditContext context)` Si no se ha creado el editcontext explicitamente, se crea solo asociado al model.
- Hasta aquí todo los de formularios.

## Clase 16 Ciclo de vida de los componentes

- SetParametersAsync:
    - Se ejecuta siempre cuando el padre se renderiza. Recibe los parametros en una parameterView.
    - Es el lugar para asignar los parametros por defecto (recordar que son las propiedades de clase definidas decoradas con `[Parameter]`)
- OnInitizalized{Async}:
    - Se ejecuta tras establecer los parametros.
    - Es la tipica donde ponemos nuestro código de inicializacion, ya se pueden usar las dependencias inyectadas(ya hablaremos).
    - Se puede ejectuar una o dos veces dependiendo de si tenemos activo pre-render o no (que genera html estático inicial y luego se ejecuta una segunda vez al establecer el entorno interactivo)
- OnParameterSet{Async}:
    - Se ejecuta en componentes recien creados justo despues de la anterior (OnInitizalizedAsync) y en componentes reutilizados tras SetParametersAsync.
    - Es ideal para completar la inicialización que dependan de valores de parámetros...¿? 
- StateHasChanged:
    - Este metodo indica que el estado del componente ha cambiado y fuerza renderizado. Se llama automáticamente cuando se gestionan eventos.
    - Es util para forzar renderizado cuando quieres hacer que se vuelva a renderizar cuando cambia otro componente que no forma parte del arbol de componentes donde está.
- ShouldRender:
    - Los componentes pueden redefinir este metodo para saber cuando debe renderizarse o no. Si devuelve false blazor no lo renderiza. Por ejemplo un campo del componente que no se está mostrando y cambia, podemos evitar que se vuelva a renderizar.
- OnAfterRender:
    - Solo se invoca cuando el DOM ha sido actualizado y ya se puede usar javascript para interactuar con DOM.
    - No se invoca si solo tenemos SSR, ya que no tenemos DOM.

## Clase 17 Componentes Plantillas

- Permiten crear jerarquías de componentes complejas.
- **RenderFragment** nos permite pasar cualquier porción de codigo blazor a un componente.
- Preparamos un modelo de usuario para usarlo en un componente lista de usuarios UsersList y creamos una pagina razor para mostrar ese componente que podría traer los usuarios de una api o de bd (pero los ponemos directamente a mano).

## Clase 18, 19, 20

- Ahora vemos como usar el componente UsersList decidiendo la plantilla que queremos, que no sea siempre un ul.
- El componente en el que definimos el `RenderFragment Contenido` como parametro (y lo tenemos en su html como @Contenido) recibe del padre lo que pongamos dentro de las etiquetas `<Componente> Aqui </Componente>`.
- En el caso de UsersList queremos sustituir `<li>@user.Name - @user.Surname</li>` por lo que le pasemos dentro de la etiqueta, pero no podemos referenciar el modelo usado si no indicamos a RenderFragment que tiene que usarlo: `RenderFragment<User>` y en el html pasarlo como `@Contenido(user)` siendo user el iterador del foreach que no es más que un objeto de la clase modelo user. En el padre ahora no usaremos user.propiedad si no context.propiedad.
- (El renderFragmente parece que tiene que llamarse **ChildContent** si no me da error context.propiedad).
- Para poder usar otro nombre hay que especificarlo en el padre:
    ```cs
     <Componente>
        <Contenido>
         <li>@context.Name
        </Contenido>
    </Componente>
    ```
- Así podemos tener otros RenderFragmente como HeaderTemplate y FooterTemplate y que sea el padre el que los defina. O el EmptyTemplate en el caso de que no haya usuarios.
- También podemos indicar en la etiqueta del renderFragment como queremos llamar a @context indicando `<Componente Context="user">` y así queda más claro.

## Clase 21, 22, 23 con Componentes Genéricos

- El componente UsersList en principio no depende de UserModel salvo que lo ponemos entre `< >` en los tipos de IEnumerable y RenderFragment. Así que podemos hacer la lista genérica para que nos valga para listar cualquier cosa.
- Necesitamos `@typeparam TItem` para que ese tipo lo pase el componente que llama al genérico.
- (Hay un bug que no nos permite dejar el rendermode que teniamos al compilar. Lo quitamos en la lista generica.)
- El **TItem** lo obtiene directamente al pasarle Items="_users", pero lo podemos indicar de forma explicita con TItem="User" en la etiqueta.

- Si no pasamos algún renderFragment va a dar error si no controlamos en el componente if (ItemTemplate is not null)
- Otra forma es ponerle al parameter renderfragment EditorRequiered y nos avisa el compilador con un warning. `    [Parameter, EditorRequired]`
- Le podemos poner directamente el código por defecto en la declaración del fragmento si no tiene parametros con `... = @<p>No items found</p>;`
- Para los que tienen parametros tenemos que usar una función lambda con el parámetro: `...<TItem>...= item => @<li>@item?.ToString()</li>;`
- De estos item en el componente genérico solo podremos usar metodos de object ya que aún no sabemos que va a ser.
- Si necesitamos que los TItem sean de una clase (o derivadas) podemos ponerlo en `@typeparam TItem where TItem:User`

## Clase 24 Layouts

- Son componentes razor normales salvo que heredan de una clase componente que es LayoutComponentBase
- Vemos que tienen **@Body** que no es más que un RenderFragment que se define en LayoutComponentBase.
- Este MainLayout lo genera VisualStudioCode con la plantilla Blazor, pero podemos definir más y tener más de uno.
- El componente que primero renderiza es el que tenemos en Program.cs, suele ser App.razor. Este genera el html inicial (head) e incluye el componente Routes.razor. Ese a su vez usa el componente Router que es el que parsea la url y obtiene los parametros de las distintas rutas. Tiene un renderFragment Found que se renderiza si en la aplicacion se ha encontrado un componente página con esa url, lo renderiza con el layout indicado.
- El layout lo podríamos modificar o personalizar para cada ruta en el punto anterior, o en cada página con `@layout MainLayout` añadiendo @using MyBlazorApp.Components.Layout
- Creamos un layout como un componente más y le ponemos `@inherits LayoutComponentBase`

## Clase 25, 26, 27 CSS

- Blazor permirte css por componente (css isolation)
- Debe estar en la misma carpeta que el archivo.razor `componente.razor.css`
- En tiempo de compilación se genera un bundle css final con todo. `proyecto.style.css` que está incluido en el componente App.razor
- (Al añadir el fichero css tuve un error, se añadió como componente y lo tuve que eliminar de .csproj se habia agregado a ItemGroup, hubo que recompilar dotnet clean y dotnet build)
- Si inspeccionamos la página vemos que el css tiene selectores personalizados por cada componente añadiendo un identificador.
 ```css
    /* _content/MyBlazorApp/Components/Alerta.razor.rz.scp.css */
    div[b-armudou7zp] {
        color: red;
    }
```
- Para configurar SASS o algun preprocesado de Css hay que definir como tarea de antes de compilar.

## Clase 28, 29, 30 Aplicacion TODO List

- Creamos un componente ListaDeTareas, que tendra un ienumerable de tareas a realizar (ToDoTarea) por lo que creamos la clase ToDoTarea. 
- La creamos como record : 
```cs
namespace MyBlazorApp.Models;

public enum TipoTarea
{
    Urgente,
    Normal,
    NoUrgente
}

public record ToDoTarea(int Id, string Titulo, TipoTarea TipoTarea = TipoTarea.Normal, DateTime? FechaRealizado = null)
{
    public bool Realizada => FechaRealizado is not null;
    public void MarcarRealizada() => FechaRealizado = DateTime.UtcNow;
}
```
- El problema es que si es **record** significa que es una clase inmutable y no nos permite cambiar FechaRealizado, así que lo sacamos del constructor y la declaramos en la clase con get,set, e inicializandola a null. Así podemos tener un record mutable y podemos cambiar la propiedad Fecha.
- (En la clase pinta las tareas si no tienen null en la fecha para lo que usa !Fecha en el if, a mi me da un error y lo pongo como `is not null`)
- Incluimos el componente en Home donde definimos una lista de tareas inicial y protegemos el componente para que no renderice nada si no hay lista de tareas.
- Vamos a traer las tareas a través de un servicio que sea un repositorio de tareas. Creamos la carpeta services y agregamos una clase que será el interfaz del servicio. A continuación agregamos una implementación de este interfaz.
- La implementacion la llamamos TareasEnMemoria y en su metodo GetAll() conectará con base de datos para traer las tareas, por lo que debe ser async y devolver una Task. Para esta practica creamos las tareas en el constructor.
- Lo usamos ahora el componente en home, en el metodo OnInitializedAsync creamos el servicio y le pedimos GetAll.
- El problema es que estamos inyectando una dependencia entre home y una implementacion concreta de la capa de infraestructura, queremos que reciba directamente de la capa de infraesctructura para tener una arquitectura más limpia y seguir el principio de inversión de dependencia.
- Para ver como hacerlo vamos a Program.cs donde tenemos el **builder** donde están las tuberías de la aplicación. Entonces antes de pedirle al builder que nos de la app, añadimos el servicio como singleton: `builder.Services.AddSingleton<ITareasService, TareasEnMemoria>();` así solo va a haber en toda la aplicacion un solo objeto TareasEnMemoria.
- Se podia haber hecho, en lugar de **AddSingleton**, con **AddScope** (que en una aplicacion SSR el scope es la petición que llega) o con **AddTransient** que tendrá un solo objeto instancia del servicio por cada componente (que no es lo que queremos porque cada componente trabajaría con una versión diferente).
- Con esto ya podemos hacer home con la directiva `@inject ITareasService TareasSvc` y directamente usar TareasSvc con los metodos de la interfaz y eliminamos esto: `var svc = new TareasEnMemoria();`.
- Ahora vamos a la funcionalidad del botón. Vamos a hacer que el componente ListaDeTareas no sea responsable de cambiar el estado de la tarea, si no que lance un evento.
- Para saber que tarea es la que se ha pulsado para completar usamos una función lambda en onclick. 

## Clase 31

PENDIENTE CLASE 31 MINUTO 5:00 -> Por qué lo hace con async await y task??

## Clase 32, 33, 34, 35, 36, 37 SERVER y WEBASM

- Recuperar datos de un json, tenemos que definir la politica para los campos del json, por si están en camelCase, que los lea también.
- Hay que ver curso de API para sincronizar la versión webasm y la versión server.

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
- Si queremos extender la estructura de usuarios y cuentas que nos da, tenemos la clase ApplicationDbContext que viene vacia para incluir esas nuevas tablas con `DBSet` o relaciones que no estén en IdentityDbContext (que representa todas las tablas de base de datos en clase de c#)
- En la página razor que queramos que sea solo para usuarios autenticados se incluye la directiva `@attribute [Authorize]` y antes `@using Microsoft.AspNetCore.Authorization`
- Los datos a los que se puede acceder de user identity son el nombre y como se ha autenticado. (AuthenticationType), y poco más, un true si está authenticado.
- Al atributo `@attribute [Authorize(Roles = 'Admin')]` se le pueden pasar parametros de rol. Y tenemos también la clase RoleManager para gestionarlo.
- También se le pueden pasar las politicas de validación con `Policy = "xxxx"`. Si quieres poner más campos en la cookie de authentication. Cada conjunto de clave valor (email, pepe@mail.com) es un claim y el token es un conjunto de claims. Las policies nos permiten hacer una selección dinámica de estos claims. Por ejemplo, una politica podría consultar la edad y poner como politica mayor de edad. De hecho el rol es una política más.
- En definitiva tenemos dos niveles para autenticar:
    - Sistema de autenticación AspNetCore que nos da mecanismo para emitir la cookie de autenticación, y autenticarme usando esa cookie, validar si existe la cookie.
    - AspNetCore.Identity mecanismos para validar usuarios, mail, bloqueos, asignar roles, etc...y si todo es correcto usa el primer sistema para emitir la cookie.
- Ambos son personalizables a todos los niveles. 
- Son middlewares que tenemos que configurar en `Program.cs`
## Notas Git

Nuevo repo:

- Después de git remote add origin `https://github.com/Joselon/CursoBlazorHome.git` antes de push: `git push --set-upstream origin master`

[Repositorio de ejemplos](https://github.com/EscuelaIt/blazor-ejemplos-curso.git)
