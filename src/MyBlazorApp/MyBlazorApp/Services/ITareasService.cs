using MyBlazorApp.Models;

namespace MyBlazorApp.Services;

public interface ITareasService
{
    Task<IEnumerable<ToDoTarea>> GetAll();
}

class TareasEnMemoria : ITareasService
{
    private List<ToDoTarea> _todos = [];

    public TareasEnMemoria()
    {
        _todos = [
            new ToDoTarea(1, "Tarea 1"),
            new ToDoTarea(2, "Tarea 2", TipoTarea.Urgente),
            new ToDoTarea(3, "Tarea 3", TipoTarea.NoUrgente)
            ];
    }

    public async Task<IEnumerable<ToDoTarea>> GetAll()
    {
        //Este metodo es el que accederia a BD por lo que lo hacemos async y devolvemos un Task.FromResult
        return _todos;
    }
}