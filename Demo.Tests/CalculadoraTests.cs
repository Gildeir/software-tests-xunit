using Xunit;

namespace Demo.Tests
{
    public class CalculadoraTests
    {
        [Fact]
        public void Calcularoda_Somar_RetornaValorSoma()
        {
            //Arrange

            var calculadora = new Calculadora();

            //Act

            var result = calculadora.Somar(10, 5);


            //Assert

            Assert.Equal(15, result);

        }
        
        [Theory]
        [InlineData(0, 5, 5)]
        [InlineData(1, 5, 6)]
        [InlineData(3, 5, 8)]
        [InlineData(10, 5, 15)]
        [InlineData(1579, 37846,39425)]
        [InlineData(0, 0, 0)]

        public void Calcularoda_Somar_RetornaValoresSomaCorreto(double a, double b, double total)
        {
            //Arrange

            var calculadora = new Calculadora();

            //Act

            var result = calculadora.Somar(10, 5);


            //Assert

            Assert.Equal(15, result);

        }
    }
}