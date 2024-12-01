using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertingExceptionsTests
    {
        [Fact]
        public void Calculadora_Divisao_ErroDivisaoPorZero()
        {
            // Arrange
            var calculadora = new Calculadora();

            var result = calculadora.Dividir(10, 0);

            Assert.Equal(double.PositiveInfinity, result);

        }

        [Fact]
        public void Funcionario_Salario_DeveRetornarErroSalarioInferiorPermitido()
        {
            var exception = Assert.Throws<Exception>(() => FuncionarioFactory.Criar("Eduardo", 0));
            Assert.Equal("Salario inferior nao permitido", exception.Message);

        }
    }
}
