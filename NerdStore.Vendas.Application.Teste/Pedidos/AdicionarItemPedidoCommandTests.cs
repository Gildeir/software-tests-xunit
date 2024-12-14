using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Application.Teste.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("Categoria", "Vendas - Pedido Command")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePasssarNaValidacao()
        {
            //Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);

            //Act
            var result = pedidoCommand.EhValido();

            //Assert
            Assert.True(result);
        }

    }
}
