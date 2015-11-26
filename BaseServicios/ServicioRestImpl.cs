using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace BaseServicios
{
    public class ServicioRestImpl<TModel> : IServiciosRest<TModel>
    {
        private String url;
        private bool auth;
        private String user;
        private String pass;


        public ServicioRestImpl(String url, bool auth = false, String user = null, String pass = null)
        {
            this.url = url;
            this.auth = auth;
            this.user = user;
            this.pass = pass;
        }

        public Task<TModel> Add(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task Update(TModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TModel model)
        {
            throw new NotImplementedException();
        }

        public List<TModel> Get()
        {
            throw new NotImplementedException();
        }

        public List<TModel> Get(Dictionary<string, string> param)
        {
            throw new NotImplementedException();
        }
    }
}
