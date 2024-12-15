using NerdStore.Vendas.Application.Command;
using MediatR;
using Moq;
using Moq.AutoMock;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Teste.Pedidos
{
    public class PedidoCommandHandlerTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {

            //Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(),
                "Produto Teste", 2, 100);

            var mocker = new AutoMocker();
            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            //Act

            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert

            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.Adicionar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

            //mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Novo Item Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_NovoItemPedidoRascunho_DeveExecutarComSucesso()
        {
            //Arrange
            var clientId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clientId);
            var pedidoItemExistente = new PedidoItem(Guid.NewGuid(), "Produto xpto", 2, 100);
            pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(clientId, Guid.NewGuid(),
                "Produto Teste", 2, 100);

            var mocker = new AutoMocker();
            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorClienteId(clientId))
                .Returns(Task.FromResult(pedido));

            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));


            //Act

            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert

            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);

            //mocker.GetMock<IMediator>().Verify(r => r.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Existente ao Pedido Rascunho com Sucesso")]
        [Trait("Categoria", "Vendas - Pedido Command Handler")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            var clientId = Guid.NewGuid();
            var produtoId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(clientId);
            var pedidoItemExistente = new PedidoItem(Guid.NewGuid(), "Produto xpto", 2, 100);
            pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(clientId, produtoId,
                "Produto Teste", 2, 100);

            var mocker = new AutoMocker();
            var pedidoHandler = mocker.CreateInstance<PedidoCommandHandler>();

            mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorClienteId(clientId))
                .Returns(Task.FromResult(pedido));

            mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));


            //Act

            var result = await pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            //Assert

            Assert.True(result);

            mocker.GetMock<IPedidoRepository>().Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);


        }
    }
}
