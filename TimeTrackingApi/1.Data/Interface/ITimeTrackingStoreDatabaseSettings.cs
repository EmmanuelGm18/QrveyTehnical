using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeTrackingApi._1.Data.Interface
{
    public interface ITimeTrackingStoreDatabaseSettings
    {
        string UsersCollectionName { get; set; }

        string ProjectsCollectionName { get; set; }

        string TaskCollectionName { get; set; }

        string TaskSpentTimeProjectsCollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
