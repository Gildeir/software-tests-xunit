using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        
        [Fact(DisplayName = "Adicionar item pedido vazio")]
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido")]
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
        [Trait("Categoria", "Vendas - Pedido Item")]
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



        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido Item")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);

            //Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));

        }

        // ObjetoEmTeste_MetodoComportamentoEmTeste_ComportamentoEsperado
        

        
        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido Item")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            // Configurações iniciais

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(productId, "Produto Teste", 2, 100);
            var pedidoItemAtualizado = new PedidoItem(productId, "Produto Teste", 5, 100);
            pedido.AdicionarItem(pedidoItem);
            var novaQuantidade = pedidoItemAtualizado.Quantidade;

            // Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Assert
            // Validação do resultado
            Assert.Equal(novaQuantidade, pedido.PedidoItems?.FirstOrDefault(p => p.ProductId == productId)?.Quantidade);
 
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValortotal()
        {
            // Arrange
            // Configurações iniciais

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var pedidoItemExistente1 = new PedidoItem(Guid.NewGuid(), "Produto Xpto", 2, 100);
            var pedidoItemExistente2 = new PedidoItem(productId, "Produto Teste", 3, 15);
            
            pedido.AdicionarItem(pedidoItemExistente1);
            pedido.AdicionarItem(pedidoItemExistente2);

            var pedidoItemAtualizado = new PedidoItem(productId, "Produto Teste", 5, 15);

            pedido.AtualizarItem(pedidoItemAtualizado);
            var totalPedido = pedidoItemExistente1.Quantidade * pedidoItemExistente1.ValorUnitario
                              + pedidoItemAtualizado.Quantidade * pedidoItemAtualizado.ValorUnitario;

            // Act
            pedido.AtualizarItem(pedidoItemAtualizado);
            
            // Assert
            // Validação do resultado
            Assert.Equal(totalPedido, pedido.ValorTotal);

        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade Acima do Permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadeAcimadoPedido_DeveRetornarExcpetion()
        {
            // Arrange
            // Configurações iniciais acima do pedido

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var pedidoItemExistente1 = new PedidoItem(productId, "Produto Teste", 3, 15);
            var pedidoItemAtualizado = new PedidoItem(productId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 1500);

            pedido.AdicionarItem(pedidoItemExistente1);
            pedido.AdicionarItem(pedidoItemAtualizado);

            // Act

            // Assert
            // Validação do resultado
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));

        }


        //3 - Remoção de Item
        //3.1 - O item precisa estar na lista para ser removido
        //3.2 - Ao remover um item é necessário calcular o valor total do pedido


        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPeido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange

            // Configurações iniciais acima do pedido

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var productId = Guid.NewGuid();
            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 3, 15);

            //pedido.AdicionarItem(pedidoItem1);

            // Act

            // Assert

            // Validação do resultado
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem1));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPeido_ItemNaoExisteNaLista_DeveRemoverPedido()
        {
            // Arrange

            // Configurações iniciais acima do pedido

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            
            var productId = Guid.NewGuid();
            
            var productId2 = Guid.NewGuid();
            
            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 3, 15);
            var pedidoItem2 = new PedidoItem(productId2, "Produto Teste 2", 6, 1500);

            pedido.AdicionarItem(pedidoItem1);

            pedido.AdicionarItem(pedidoItem2);

            pedido.RemoverItem(pedidoItem2);

            var newList = pedido.PedidoItems.FirstOrDefault(p => p.ProductId == pedidoItem2.ProductId);

            // Act

            // Assert

            // Validação do resultado

            Assert.Null(newList);
        }

    }
}
