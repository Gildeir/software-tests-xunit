using MediatR;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Core.Messages;

public abstract class Command : Message, IRequest<bool>
{
    public DateTime Timestamp { get; set; }

    public ValidationResult ValidationResult{ get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
    }


}