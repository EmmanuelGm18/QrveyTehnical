using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using System;
using System.Security.Authentication;
using TimeTrackingApi._1.Data.Interface;

namespace TimeTrackingApi._1.Data.Configuration
{
    public class SettingServices : ISettingsService
    {
        public MongoClient MongoSettings { get; }

        public SettingServices(Interface.ITimeTrackingStoreDatabaseSettings settings)
        {
            MongoSettings = GetClient(settings);
        }

        private MongoClient GetClient(Interface.ITimeTrackingStoreDatabaseSettings settings)
        {
            MongoClientSettings settings2 = MongoClientSettings.FromUrl(new MongoUrl(settings.ConnectionString));
            settings2.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
           
            var mongoClient = new MongoClient(settings2);            
            return mongoClient;
        }

    }
}
