namespace TES.Domain.Common;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string entity, object key)
        : base($"{entity} com identificador '{key}' não foi encontrado.") { }
}
