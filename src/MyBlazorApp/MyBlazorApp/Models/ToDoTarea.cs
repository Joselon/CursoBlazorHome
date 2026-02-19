namespace MyBlazorApp.Models;

public enum TipoTarea
{
    Urgente,
    Normal,
    NoUrgente
}

public record ToDoTarea(int Id, string Titulo, TipoTarea TipoTarea = TipoTarea.Normal)
{
    public DateTime? FechaRealizado { get; set; } = null;
    public bool Realizada => FechaRealizado is not null;
    public void MarcarRealizada() => FechaRealizado = DateTime.UtcNow;
}
