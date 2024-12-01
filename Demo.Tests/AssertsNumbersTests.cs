using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertsNumbersTests
    {
        [Fact]
        public void Calculadora_Somar_DveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Calculadora_Somar_NaoDeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1.13123123123, 2.231231123);

            // Assert
            Assert.NotEqual(3, result, 1);
        }
    }
}
