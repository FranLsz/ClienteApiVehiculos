using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<TModel> Add(TModel model)
        {
            var datos = Serializacion<TModel>.Serializar(model);
            //se crea el HttpClientHandler para poder añadir credenciales de autenticacion
            using (var handler = new HttpClientHandler())
            {

                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }

                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //el await es para que se espere a ejecutar
                    var r = await client.PostAsync(new Uri(url), contenido);

                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fail on update");

                    var objSerializado = await r.Content.ReadAsStringAsync();

                    return Serializacion<TModel>.Deserializar(objSerializado);


                }
            }
        }

        public async Task Update(TModel model)
        {
            var datos = Serializacion<TModel>.Serializar(model);
            //se crea el HttpClientHandler para poder añadir credenciales de autenticacion
            using (var handler = new HttpClientHandler())
            {

                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }

                using (var client = new HttpClient(handler))
                {
                    var contenido = new StringContent(datos);
                    contenido.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    //el await es para que se espere a ejecutar
                    var r = await client.PutAsync(new Uri(url), contenido);

                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fail on update");
                }
            }
        }

        public async Task Delete(int id)
        {
            //se crea el HttpClientHandler para poder añadir credenciales de autenticacion
            using (var handler = new HttpClientHandler())
            {

                if (auth)
                {
                    handler.Credentials = new NetworkCredential(user, pass);
                }

                using (var client = new HttpClient(handler))
                {

                    //el await es para que se espere a ejecutar
                    var r = await client.DeleteAsync(new Uri(url + "/" + id));

                    if (!r.IsSuccessStatusCode)
                        throw new Exception("Fail on update");
                }
            }
        }

        public List<TModel> Get(String paramUrl = null)
        {
            List<TModel> lista;
            var urlDest = url;
            if (paramUrl != null)
                urlDest += paramUrl;


            //para trabajar con peticiones hhtp tenemos WebRequest(trabaja con peticiones utilizando GET) y el HttpClient
            var request = WebRequest.Create(urlDest);
            if (auth)
            {
                //para autenticar
                request.Credentials = new NetworkCredential(user, pass);
            }

            request.Method = "GET";
            //obtiene la respuesta de la peticion
            var response = request.GetResponse();
            //desde la respuesta obtiene el stream(contenido en bytes) de la misma
            using (var stream = response.GetResponseStream())
            {
                //crea un lector de stream para leer el contenido
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    lista = Serializacion<List<TModel>>.Deserializar(serializado);
                }
            }

            return lista;
        }

        public TModel Get(int id)
        {
            TModel objeto;

            //para trabajar con peticiones hhtp tenemos WebRequest(trabaja con peticiones utilizando GET) y el HttpClient
            var request = WebRequest.Create(url);
            if (auth)
            {
                //para autenticar
                request.Credentials = new NetworkCredential(user, pass);
            }

            request.Method = "GET";
            //obtiene la respuesta de la peticion
            var response = request.GetResponse();
            //desde la respuesta obtiene el stream(contenido en bytes) de la misma
            using (var stream = response.GetResponseStream())
            {
                //crea un lector de stream para leer el contenido
                using (var reader = new StreamReader(stream))
                {
                    var serializado = reader.ReadToEnd();
                    objeto = Serializacion<TModel>.Deserializar(serializado);
                }
            }

            return objeto;
        }

        public List<TModel> Get(Dictionary<string, string> param)
        {
            var paramsUrl = "";

            var primero = true;

            foreach (var key in param.Keys)
            {
                if (primero)
                {
                    paramsUrl += "?";
                    primero = false;
                }
                else
                    paramsUrl += "&";
                paramsUrl += key + "=" + param[key];
            }


            return Get(paramsUrl);
        }
    }
}
