﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Tests
{
    public class AssertingRangers
    {
        [Theory]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]

        public void Funcionario_Salario_Faixas_SalariaisDevemRespeitarNivelProfissional(double salario)
        {
            //Arrange & Act
            var funcionario = new Funcionario("Eduardo", salario);

            //Assert
            if (funcionario.NivelProfissional == NivelProfissional.Junior)
                Assert.InRange(funcionario.Salario, 500, 1999);

            if (funcionario.NivelProfissional == NivelProfissional.Pleno)
                Assert.InRange(funcionario.Salario, 2000, 7999);

            if (funcionario.NivelProfissional == NivelProfissional.Senior)
                Assert.InRange(funcionario.Salario, 8000, double.MaxValue);


            Assert.NotInRange(funcionario.Salario, 0, 499);
        }

        [Theory]
        [InlineData(200)] // Salários inválidos
        [InlineData(499)]
        public void Funcionario_Salario_Invalido_DeveLancarExcecao(double salario)
        {
            // Assert
            var exception = Assert.Throws<Exception>(() => new Funcionario("Eduardo", salario));
            Assert.Equal("Salario inferior nao permitido", exception.Message);

        }


    }
}
