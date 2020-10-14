using MongoDB.Driver;

namespace TimeTrackingApi._1.Data.Interface
{
    public interface ISettingsService
    {
        MongoClient MongoSettings { get; }
    }
}
