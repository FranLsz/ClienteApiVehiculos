using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BaseServicios
{
    class Serializacion<T>
    {
        public static T Deserializar(String obj)
        {
            var ser = new JavaScriptSerializer();
            var data = ser.Deserialize<T>(obj);
            return data;
        }

        public static String Serializar(T obj)
        {
            var ser = new JavaScriptSerializer();
            var data = ser.Serialize(obj);
            return data;
        }
    }
}
