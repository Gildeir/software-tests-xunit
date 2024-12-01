using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertsNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {
            //Arrange & Act
            var funcionario = new Funcionario("", 1000);

            Assert.False(string.IsNullOrEmpty(funcionario.Nome));
        }
        
        [Fact]
        public void Funcionario_Apelido_NaoDeveTerApelido()
        {
            //Arrange & Act
            var funcionario = new Funcionario("", 1000);

            Assert.Null(funcionario.Apelido);

            // Assert Bool
            Assert.True(string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(funcionario.Apelido?.Length > 0);

            
        }
    }
}
