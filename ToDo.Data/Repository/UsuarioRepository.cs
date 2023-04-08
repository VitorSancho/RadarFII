using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RadarFII.Business.Intefaces;
using RadarFII.Business.Models;
using RadarFII.Data.Context;

namespace RadarFII.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MeuDbContext context) : base(context) { }
        
    }
}