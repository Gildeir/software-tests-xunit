using MediatR;
using NerdStore.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace NerdStore.Vendas.Application.Events
{
    public abstract class EventForget : Message, INotification
    {
        public DateTime Timestamp { get; set; }

        public ValidationResult ValidationResult { get; set; }

        protected EventForget()
        {
            Timestamp = DateTime.Now;
        }
    }
}
