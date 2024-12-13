using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        /*
        4 Aplicar voucher de desconto
           4.1 O voucher só pode ser aplicado se estiver válido, para isto:
              4.1.1 Deve possuir um código
              4.1.2 A data de validade é superior à data atual
              4.1.3 O voucher está ativo
              4.1.4 O voucher possui quantidade disponível
              4.1.5 Uma das formas de desconto devem estar preenchidas com valor acima de 0
           4.2 Calcular o desconto conforme tipo do voucher
              4.2.1 Voucher com desconto percentual
              4.2.2 Voucher com desconto em valores (reais)
           4.3 Quando o valor do desconto ultrapassa o total do pedido o pedido recebe o valor: 0
           4.4 Após a aplicação do voucher o desconto deve ser re-calculado após toda modificação de itens do pedido
        
         */

        [Fact(DisplayName = "Validar Voucher Tipo Valor Valido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherValor_DeveEstarValido()
        {
            // Arrange

            var voucher = new Voucher
            {
                Codigo = "PROMO-15-REAIS",
                ValorDesconto = 15,
                PercentualDesconto = null,
                Quantidade = 1,
                DataValidade = DateTime.Now,
                Ativo = true,
                Utilizado = false
            };


            // Act

            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Aplicar Voucher de Desconto")]
        [Trait("Categoria", "Voucher - Pedido")]
        public void AplicarVoucherPedido_AplicarVoucher_DeveAplicarVoucherEmPorcentagem()
        {
            // Arrange


            var voucherArrange = new Voucher
            {
                Codigo = "PROMO-15-REAIS",
                ValorDesconto = 0,
                PercentualDesconto = 8,
                Quantidade = 1,
                DataValidade = new DateTime(),
                Ativo = true,
                Utilizado = false
            };

            // Configurações iniciais acima do pedido

            var productId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 5, 15);

            
            //Arrange
            
            pedido.AdicionarItem(pedidoItem1);

            pedido.AplicarVoucher(voucherArrange, pedido);


            // Act


            Assert.Equal(decimal.Parse(69.ToString()), pedido.ValorTotal);

        }

        [Fact(DisplayName = "Aplicar Voucher de Desconto")]
        [Trait("Categoria", "Voucher - Pedido")]
        public void AplicarVoucherPedido_AplicarVoucher_DeveAplicarVoucherEmReais()
        {
            // Arrange

            var voucherArrange = new Voucher
            {
                Codigo = "´PROMO-15-REAIS",
                ValorDesconto = 10,
                PercentualDesconto = null,
                Quantidade = 1,
                DataValidade = new DateTime(),
                Ativo = true,
                Utilizado = false
            };

            // Configurações iniciais acima do pedido

            var productId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 5, 15);

            //Arrange

            pedido.AdicionarItem(pedidoItem1);

            pedido.AplicarVoucher(voucherArrange, pedido);

            // Act

            Assert.Equal(decimal.Parse(65.ToString()), pedido.ValorTotal);

        }
    }
}
