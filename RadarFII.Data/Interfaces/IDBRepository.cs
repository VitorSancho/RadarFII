
using Microsoft.Data.SqlClient;
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
        SqlConnection ConectaDB();

        Task<IEnumerable<T>> RealizaConsulta<T>(string expressaoConsulta);
    }
}
