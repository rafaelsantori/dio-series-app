using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace series.interfaces
{
    public interface iRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int id);
        void Insere(T entidade);
        void Exclui(int id);
        void Reativa(int id);
        void Atualiza(int id, T entidade);
        int ProximoId();
    }
}
