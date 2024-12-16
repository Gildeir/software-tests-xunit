using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NerdStore.Vendas.Domain;

namespace NerdStore.Vendas.Application.Command
{
    public class AdicionarItemPedidoCommand : Core.Messages.Command, IRequest<bool>, IRequest
    {
        public Guid ClienteId { get; set; }

        public Guid ProdutoId { get; set; }

        public string Nome { get; set; }

        public int Quantidade { get; set; }

        public decimal ValorUnitario { get; set; }

        public AdicionarItemPedidoCommand(Guid clienteId, Guid produtoId, string nome, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            ProdutoId = produtoId;
            Nome = nome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        public ValidationResult EhValido()
        {
            return new AdicionarItemPedidoValidation().Validate(this);
        }

        public class AdicionarItemPedidoValidation : AbstractValidator<AdicionarItemPedidoCommand>
        {
            public static string IdClienteErroMsg => "Invalid client ID";
            public static string IdProdutoErroMsg => "Invalid product ID";
            public static string NomeErroMsg => "Product name not informed";
            public static string QtdMaxErroMsg => $"Maximum quantity per item is {Pedido.MAX_UNIDADES_ITEM}";
            public static string QtdMinErroMsg => "Minimum quantity per item is 1";
            public static string ValorErroMsg => "Item value must be greater than 0";
        
            public AdicionarItemPedidoValidation()
            {
                RuleFor(c => c.ClienteId)
                    .NotEmpty()
                    .WithMessage(IdClienteErroMsg);

                RuleFor(c => c.ProdutoId)
                    .NotEmpty()
                    .WithMessage(IdProdutoErroMsg);

                RuleFor(c => c.Nome)
                    .NotEmpty()
                    .WithMessage(NomeErroMsg);

                RuleFor(c => c.Quantidade)
                    .GreaterThan(0)
                    .WithMessage(QtdMinErroMsg)
                    .LessThanOrEqualTo(Pedido.MAX_UNIDADES_ITEM)
                    .WithMessage(QtdMaxErroMsg);
                
                RuleFor(c => c.ValorUnitario)
                    .GreaterThan(0)
                    .WithMessage(ValorErroMsg);
            }
        }
    }
}