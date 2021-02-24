using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>,IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAcessor;
        public PedidoRepository(ApplicationContext contexto, IHttpContextAccessor contextAcessor) : base(contexto)
        {
            this.contextAcessor = contextAcessor;
        }

        public void AddItem(string codigo)
        {
            var produto = contexto.Set<Produto>().Where(p => p.Codigo == codigo).SingleOrDefault();

            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            var pedido = GetPedido();

            var itemPedido = contexto.Set<ItemPedido>().Where(i => i.Produto.Codigo == codigo && i.Pedido.Id == pedido.Id).SingleOrDefault();

            if (itemPedido == null)
            {
                itemPedido = new ItemPedido(pedido, produto, 1, produto.Preco);

                contexto.Set<ItemPedido>().Add(itemPedido);

                contexto.SaveChanges();
            }
        }

        public Pedido GetPedido()
        {
            var pedidoId = GetPedidoId();
            var pedido = dbSet.Include(p => p.Itens).ThenInclude(i => i.Produto)
                .Where(p => p.Id == pedidoId).SingleOrDefault();//Estamos consultando o DB e selecionando se o  pedido que o usuário esta fazendo pelo id já foi feito ou ainda não, o método "SingleOrDefault()" é responsável por retornar se encontramos este produto, então nos retorna o produto, caso contrário nosso retorno é null 

            if (pedido == null)//Se o pedido for nulo, criamos um novo pedido
            {
                pedido = new Pedido();
                dbSet.Add(pedido);
                contexto.SaveChanges();
                SetPedidoId(pedido.Id);
            }

            return pedido;
        }

        //Lê o id do pedido 
        private int? GetPedidoId()
        {
            return contextAcessor.HttpContext.Session.GetInt32("pedidoId");
        }

        //Grava o id do pedido na sessão de nossa aplicação ASP.NET Core
        private void SetPedidoId(int pedidoId)
        {
            contextAcessor.HttpContext.Session.SetInt32("pedidoId", pedidoId);
        }
    }
}
