using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>,IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)//Construtor responsável por passar o contexto para nossa classe base
        {
        }

        public Produto GetProduto()
        {
            throw new NotImplementedException();
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
           
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())//A função checa se os dados do arquivo json estão em nosso banco de dados, se não estiverem estamos adicionando. Como queremos efetuar a ação quando o condição for negativa, colocamos no inicio "!", para inverter o resultado 
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));

                }
            }

            contexto.SaveChanges();
        }

        public class Livro
        {

            public string Codigo { get; set; }
            public string Nome { get; set; }
            public decimal Preco { get; set; }

        }
    }
}
