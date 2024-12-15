using NerdStore.Vendas.Application.Command;
using static NerdStore.Vendas.Application.Command.AdicionarItemPedidoCommand;

namespace Teste.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("Categoria", "Vendas - Pedido Command")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePasssarNaValidacao()
        {
            ////Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 1, 20);

            ////Act
            var result = pedidoCommand.EhValido().IsValid;

            ////Assert
            Assert.True(result);
        }
        
        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("Categoria", "Vendas - Pedido Command")]
        public void AdicionarItemPedidoCommand_ComandoEstaInvalido_NaoDevePasssarNaValidacao()
        {
            ////Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 60, -10);

            ////Act
            var result = pedidoCommand.EhValido();
             
            ////Assert
            Assert.False(pedidoCommand.EhValido().IsValid);
            Assert.Equal(5, result.Errors.Count);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, result.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }


        [Fact(DisplayName = "Adicionar Item Command Unidade Abaixo do Permitido")]
        [Trait("Categoria", "Vendas - Pedido Command")]
        public void AdicionarItemPedidoCommand_QuantidadeAdicionadaIneriorAoPermitido_NaoDevePasssarNaValidacao()
        {
            ////Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", -60, 10);

            ////Act
            var result = pedidoCommand.EhValido();

            ////Assert
            Assert.False(pedidoCommand.EhValido().IsValid);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, result.Errors.Select(c => c.ErrorMessage));
        }

    }
}
