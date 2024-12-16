using MediatR;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Messages;
using NerdStore.Vendas.Application.Events;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Command
{
    public class PedidoCommandHandler: IRequestHandler<AdicionarItemPedidoCommand, bool>
    {
        private readonly IPedidoRepository _pedidoRepository;

        private readonly IMediator _mediator;

        public PedidoCommandHandler(IPedidoRepository pedidoRepository, IMediator mediator)
        {
            _pedidoRepository = pedidoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AdicionarItemPedidoCommand message, CancellationToken cancellationToken)
        {
            if (!message.EhValido().IsValid)
            {
                foreach (var error in message.EhValido().Errors)
                {
                   await _mediator.Publish(new DomainNotification(message.MessageType, error.ErrorMessage), cancellationToken);
                }

                return false;
            }

            var pedido = await _pedidoRepository.ObterPedidoRascunhoPorClienteId(message.ClienteId);

            var pedidoItem = new PedidoItem(message.ProdutoId, message.Nome, message.Quantidade, message.ValorUnitario);
            
            if (pedido == null) 
            { 
                pedido = Pedido.PedidoFactory.NovoPedidoRascunho(message.ClienteId);
                pedido.AdicionarItem(pedidoItem);
                _pedidoRepository.Adicionar(pedido);
            }
            else
            {
                var pedidoItemExistente = pedido.PedidoItemExistente(pedidoItem);

                if (pedidoItemExistente)
                {
                    _pedidoRepository.AtualizarItem(pedido.PedidoItems.FirstOrDefault(p => p.ProductId == pedidoItem.ProductId));
                }
                else
                {
                    _pedidoRepository.AdicionarItem(pedidoItem);
                    
                }

                //pedido.AdicionarItem(pedidoItem);
                _pedidoRepository.Atualizar(pedido);
            }


            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClientId, pedido.Id,
                message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));

            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
