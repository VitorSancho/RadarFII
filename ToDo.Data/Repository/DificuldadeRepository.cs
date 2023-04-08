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
    public class DificuldadeRepository : Repository<Dificuldade>, IColetaProventosFIIBusiness
    {
        public DificuldadeRepository(MeuDbContext context) : base(context) { }
    }
}