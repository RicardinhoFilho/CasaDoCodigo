﻿using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        void SaveProdutos(List<ProdutoRepository.Livro> livros);
        IList<Produto> GetProdutos();//Método responsável por pegar Produtos dentro do nosso banco de dados

         Produto GetProduto();
    }


}