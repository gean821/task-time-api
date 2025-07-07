using Api_IngaTasks.Infraestructure.Configuracao;
using Api_IngaTasks.Infraestructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Infraestructure.Repository
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        private readonly AppDbContext _db;

        public TimeTrackerRepository(AppDbContext db)
        {
            _db = db;
        }
    }
}
