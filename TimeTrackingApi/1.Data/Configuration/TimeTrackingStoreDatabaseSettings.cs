using TimeTrackingApi._1.Data.Interface;

namespace TimeTrackingApi._1.Data.Configuration
{
    public class TimeTrackingStoreDatabaseSettings : ITimeTrackingStoreDatabaseSettings
    {
        public string UsersCollectionName { get; set; }

        public string ProjectsCollectionName { get; set; }
        
        public string TaskCollectionName { get; set; }

        public string TaskSpentTimeProjectsCollectionName { get; set; }

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}
