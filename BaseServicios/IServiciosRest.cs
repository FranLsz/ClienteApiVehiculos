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

        Task Delete(int id);

        List<TModel> Get(String paramURl = null);

        TModel Get(int id);

        //el primer string es el nombre del parametro y el segundo es el value
        List<TModel> Get(Dictionary<String, String> param);


    }
}
