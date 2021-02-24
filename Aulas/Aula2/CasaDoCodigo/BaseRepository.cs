using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class BaseRepository<T> where T: BaseModel//eSPECIFICANDO QUE O T SERÁ O TIPO DO MODELO, NA CLASSE PRODUTO POR EXEMPLO O T SERÁ PRODUTO
    {
        protected readonly ApplicationContext contexto;
        protected readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSet;

        public BaseRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
            dbSet = contexto.Set<T>();
        }
    }
}
