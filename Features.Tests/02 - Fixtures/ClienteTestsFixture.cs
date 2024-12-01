using Features.Clientes;
using System;
using Xunit;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Matheus",
                "Oliveira",
                DateTime.Now.AddYears(-20),
                "mathreis1999@gmail.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInValidoByName()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "gildeirlopes@gmail.com",
                true,
                DateTime.Now);

            return cliente;
        } 
        
        public Cliente GerarClienteInValidoByEmail()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "gildeirlopes@gmail.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
        }
    }
}