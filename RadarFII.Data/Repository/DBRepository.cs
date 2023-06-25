using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RadarFII.Business.Interfaces.Data;

namespace RadarFII.Data.Repository
{
    public class DBRepository : IDBRepository, IDisposable
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

        public void Dispose()
        {
            DBConexao.Dispose();
        }

        async Task<IEnumerable<T>> IDBRepository.RealizaConsulta<T>(string expressaoConsulta)
        {
            if (DBConexao == null) { ConectaDB(); }
            return await DBConexao.QueryAsync<T>(expressaoConsulta);
        }

        async Task ExecutaInsert(string expressaoInsert)        {
            if (DBConexao == null) { ConectaDB(); }
            await DBConexao.QueryAsync(expressaoInsert);
        }
    }
}