using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseServicios
{
    //El TModelo es la clase para la cual va a funcionar (Tipo, Vehiculo, etc)
    public interface IServiciosRest<TModel>
    {
        //Operacion asincrona
        Task<TModel> Add(TModel model);

        Task Update(TModel model);

        Task Delete(TModel model);

        List<TModel> Get();

        List<TModel> Get(Dictionary<String, String> param);


    }
}
