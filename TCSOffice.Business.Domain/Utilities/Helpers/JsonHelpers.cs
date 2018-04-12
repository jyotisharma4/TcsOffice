using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCSOffice.Business.Domain.Utilities.Helpers
{
    public static class JsonUtilities
    {
        public static T CreateFromJsonStream<T>(this Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            T data;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                data = (T)serializer.Deserialize(streamReader, typeof(T));
            }
            return data;
        }

        public static T CreateFromJsonString<T>(this String json) where T : new()
        {
            T data;
            if (json == null) return new T();

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(json)))
            {
                data = CreateFromJsonStream<T>(stream);
            }
            return data;
        }

        public static T CreateFromJsonFile<T>(this String fileName)
        {
            T data;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                data = CreateFromJsonStream<T>(fileStream);
            }
            return data;
        }

        //public static ExpandoObject CreateFromJsonString(this String json)
        public static object CreateFromJsonStream(this Stream stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            object data;
            using (StreamReader streamReader = new StreamReader(stream))
            {
                data = serializer.Deserialize(new JsonTextReader(streamReader));
            }
            return data;
        }

        public static object CreateFromJsonString(this String json)
        {
            object data;
            if (json == null) return new object();

            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.Default.GetBytes(json)))
            {
                data = CreateFromJsonStream(stream);
            }
            return data;
        }

        public static object CreateFromJsonFile(this String fileName)
        {
            object data;
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                data = CreateFromJsonStream(fileStream);
            }
            return data;
        }

        public static string ConvertObjectToJson(this object item)
        {
            return JsonConvert.SerializeObject(item);
        }
    }
}
