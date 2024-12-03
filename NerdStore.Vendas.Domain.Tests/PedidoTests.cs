using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {

        [Fact(DisplayName = "Adicionar item pedido vazio")]
        [Trait("Categoria", "Pedido teste")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            // Configurações iniciais
            var pedido = new Pedido();
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            // Execução do método
            pedido.AdicionarItem(pedidoItem);

            // Assert
            // Validação do resultado
            Assert.Equal(200, pedido.ValorTotal);
        }
        
        
    }
}
