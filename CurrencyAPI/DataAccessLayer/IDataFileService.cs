using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFileAccessLayer
{
    public interface IDataFileService<T>
    {
        List<T> Read();
        List<T> Read(string path);        
        void Write(T obj);
    }
}
