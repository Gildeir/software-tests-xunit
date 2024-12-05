using System;
using NerdStore.Core.DomainObjects;

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
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            // Execução do método
            pedido.AdicionarItem(pedidoItem);

            // Assert
            // Validação do resultado
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Acrescentar item pedido vazio")]
        [Trait("Categoria", "Pedido teste")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            // Configurações iniciais

            var productId = Guid.NewGuid();
            var pedido = new Pedido();
            var pedidoItem = new PedidoItem(productId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);


            var pedidoItem2 = new PedidoItem(productId, "Produto Teste", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            // Validação do resultado
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido?.PedidoItems.Count);
            Assert.Equal(3, pedido?.PedidoItems?.FirstOrDefault(p => p?.ProductId == productId)?.Quantidade);
            Assert.Equal(0, (int)PedidoStatus.Rascunho);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima do Permitido")]
        [Trait("Categoria", "Pedido Tests")]
        public void AdicionarItemPedido_ItemAcimaDoPermitidoUnidades_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            
            //Act & Assert
            
            Assert.Throws<DomainException>(() =>
                new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100));


        }

        [Fact(DisplayName = "Adicionar Item Pedido Abaixo do Permitido")]
        [Trait("Categoria", "Pedido Item Tests")]
        public void AdicionarItemPedido_ItemAbaixoDoPermitidoUnidades_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();

            //Act & Assert
            Assert.Throws<DomainException>(() =>
                new PedidoItem(produtoId, "Produto Teste", Pedido.MIN_UNIDADES_ITEM - 1, 100));
        }


        /*
          2 - Atualização de Item
          2.1 - O item precisa estar na lista para ser atualizado
          2.2 - Um item pode ser atualizado contendo mais ou menos unidades do que anteriormente
          2.3 - Ao atualizar um item é necessário calcular o valor total do pedido
          2.4 - Um item deve permanecer entre 1 e 15 unidades do produto
         */

        //[Fact(DisplayName = "Atualizar item ")]
        //[Trait("Categoria", "Pedido teste")]
        //public void AtualizarItemPedido_NovoPedido_DeveAtualizarValor()
        //{
        //    // Arrange
        //    // Configurações iniciais
        //    // O item precisa estar na lista para ser atualizado

        //    var productId = Guid.NewGuid();

        //    var pedido = new Pedido();
        //    var pedidoItem = new PedidoItem(productId, "Produto Teste", 2, 100);

        //    // Act
        //    // Execução do método
        //    pedido.AdicionarItem(pedidoItem);

        //    pedidoItem.AtualizarPedido(pedido, pedidoItem);
        //    // Assert
        //    // Validação do resultado
        //    Assert.Equal(200, pedido.ValorTotal);
        //}



    }
}
