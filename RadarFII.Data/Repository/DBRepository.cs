using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RadarFII.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadarFII.Data.Repository
{
    public class DBRepository : IDBRepository, IDisposable
    {
        public SqlConnection DBConexao;
        private readonly IConfiguration _configuration;
        private readonly string strConexao;

        public DBRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //strConexao = _configuration.Get("ConnectionStrings:DefaultConnection") ;
        }

        public SqlConnection ConectaDB()
        {
            return new SqlConnection(strConexao);
        }

        public async Task<IEnumerable<T>> RealizaConsulta<T>(string expressaoConsulta)
        {
            return await DBConexao.QueryAsync<T>(expressaoConsulta);
        }

        public void Dispose()
        {
            DBConexao.Dispose();
        }
    }
}
