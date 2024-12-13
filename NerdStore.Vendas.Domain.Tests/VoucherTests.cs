using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

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
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1,
                DateTime.Now.AddDays(15),
                true,
                false

            );


            // Act

            var result = voucher.ValidarSeAplicavel();

            //Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar Voucher Tipo Valor Invalido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Valor, 0,
                DateTime.Now.AddDays(-1), false,true

            );

            // Act

            var result = voucher.ValidarSeAplicavel();

             //Assert
            Assert.False(result.IsValid);

            Assert.Equal(6, result.Errors.Count);

            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(c => c.ErrorMessage));

            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(c => c.ErrorMessage));

            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));

            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(c => c.ErrorMessage));

            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(c => c.ErrorMessage));

            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(c => c.ErrorMessage));

        }

        [Fact(DisplayName = "Aplicar Voucher Porcentagem Válido")]
        [Trait("Categoria", "Voucher - Pedido")]
        public void AplicarVoucherPedido_AplicarVoucher_DeveAplicarVoucherEmPorcentagem()
        {
            // Arrange


            var voucher = new Voucher("PROMO-15-REAIS", null,15, TipoDescontoVoucher.Valor,1,
              DateTime.Now.AddDays(15),
              true,
              false
              
              );

            // Configurações iniciais acima do pedido

            var productId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 5, 15);

            
            //Arrange
            
            pedido.AdicionarItem(pedidoItem1);

            pedido.AplicarVoucher(voucher, pedido);


            // Act


            Assert.Equal(decimal.Parse(69.ToString()), pedido.ValorTotal);

        }

        [Fact(DisplayName = "Aplicar Voucher de Desconto")]
        [Trait("Categoria", "Voucher - Pedido")]
        public void AplicarVoucherPedido_AplicarVoucher_DeveAplicarVoucherEmReais()
        {
            // Arrange

            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1,
                DateTime.Now.AddDays(15),
                true,
                false

            );

            // Configurações iniciais acima do pedido

            var productId = Guid.NewGuid();

            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var pedidoItem1 = new PedidoItem(productId, "Produto Teste 1", 5, 15);

            //Arrange

            pedido.AdicionarItem(pedidoItem1);

            pedido.AplicarVoucher(voucher, pedido);

            // Act

            Assert.Equal(decimal.Parse(65.ToString()), pedido.ValorTotal);

        }
    }
}
