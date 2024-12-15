using MediatR;
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

                pedido.AdicionarItem(pedidoItem);
                _pedidoRepository.AdicionarItem(pedidoItem);
                _pedidoRepository.Atualizar(pedido);
            }


            pedido.AdicionarEvento(new PedidoItemAdicionadoEvent(pedido.ClientId, pedido.Id,
                message.ProdutoId, message.Nome, message.ValorUnitario, message.Quantidade));

            return await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
