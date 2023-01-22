using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace DataFileAccessLayer
{
    public class DataFileService<T> : IDataFileService<T>
    {
       
        string _basePath;
        public DataFileService()
        {
             _basePath = Environment.CurrentDirectory;
        }
        public List<T> Read(string path)
        {
            
            if (!String.IsNullOrEmpty(path))
            {
                _basePath = path;
            }         
           return Read();
        }
        public List<T> Read()
        {
            
            string text = File.ReadAllText(_basePath + ".text", Encoding.UTF8);
            List<T> data = JsonSerializer.Deserialize<List<T>>(text);
            return data;
        }
        public void Write(List<T> obj)
        {                       
            string text = JsonSerializer.Serialize(obj);
            File.WriteAllText(_basePath + ".text", text);
        }     
        public void Write(T ?obj)
        {        
            if (obj==null)
            {
                return;
            }
            string text = JsonSerializer.Serialize(obj);
           
           File.AppendAllText(_basePath + ".text", text + "\n");                      
        }
    }
}
