using RadarFII.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Data.Interfaces
{
    public interface IDBRepository: IDisposable
    {
        void ConectaDB();

        IEnumerable<T> RealizaConsulta<T>(string expressaoConsulta);
    }
}
