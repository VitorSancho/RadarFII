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
    public class DBRepository : IDBRepository
        //, IDisposable
    {
        public SqlConnection DBConexao;
        public IConfiguration _configuration;
        private readonly string strConexao;

        public DBRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            strConexao = _configuration["ConnectionStrings:DefaultConnection"];
        }

        private void ConectaDB()
        {
            DBConexao = new SqlConnection(strConexao);
        }

        //public void Dispose()
        //{
        //    DBConexao.Dispose();
        //}

        async Task<IEnumerable<T>> IDBRepository.RealizaConsulta<T>(string expressaoConsulta)
        {
            if (DBConexao == null) { ConectaDB(); }
            return await DBConexao.QueryAsync<T>(expressaoConsulta);
        }
    }
}