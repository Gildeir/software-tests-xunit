using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertingObjectTypesTests
    {
        [Fact]
        public void Funcionario_Criar_DeveRetornarTipoFuncionario()
        {
            //Arrange & Act
            var objFuncriona = FuncionarioFactory.Criar("Eduardo", 3500);



            //Assert
            Assert.IsType<Funcionario>(objFuncriona);
        } 
        
        [Fact]
        public void Funcionario_Criar_DeveRetornarTipoDerivado()
        {
            //Arrange & Act
            var objFuncriona = FuncionarioFactory.Criar("Eduardo", 8500);



            //Assert
            Assert.IsAssignableFrom<Pessoa>(objFuncriona);
        }
    }
}
