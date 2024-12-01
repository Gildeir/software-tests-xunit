using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertStringTest
    {
        [Fact]
        public void StringsTools_UnirNomes_RetornarNomeCompleto()
        {
            //Arrange

            var nome = new StringsTools();


            //Act

            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert

            Assert.Equal("Gildeir Rodrigues", nomeCompleto);
        }
        
        [Fact]
        public void StringsTools_UnirNomes_IgnoraCase()
        {
            //Arrange

            var nome = new StringsTools();


            //Act

            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert

            Assert.Equal("Gildeir RODRIGUES", nomeCompleto, true);
        }
        
        [Fact]
        public void StringsTools_UnirNomes_DeveConterTreco()
        {
            //Arrange

            var nome = new StringsTools();


            //Act

            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert

            Assert.Contains("deir", nomeCompleto);
        }
        
        [Fact]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            //Arrange
            var nome = new StringsTools();


            //Act
            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert
            Assert.StartsWith("Gil", nomeCompleto);
        }

        [Fact]
        public void StringsTools_UnirNomes_DeveTerminarCom()
        {
            //Arrange
            var nome = new StringsTools();


            //Act
            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert
            Assert.EndsWith("gues", nomeCompleto);
        }
        
        [Fact]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            //Arrange
            var nome = new StringsTools();


            //Act
            var nomeCompleto = nome.Unir("Gildeir", "Rodrigues");

            //Assert
            Assert.Matches(@"[A-Z][a-z]+\s+[A-Z][a-z]+", nomeCompleto);


        }

    }
}
