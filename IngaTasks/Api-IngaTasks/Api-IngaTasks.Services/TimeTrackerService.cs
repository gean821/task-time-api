using Api_IngaTasks.Infraestructure.Interfaces;
using Api_IngaTasks.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api_IngaTasks.Services
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private readonly ITimeTrackerRepository _timeTrackerRepository;

        public TimeTrackerService(ITimeTrackerRepository timeTrackerRepository)
        {
            _timeTrackerRepository = timeTrackerRepository;
        }
    }
}
