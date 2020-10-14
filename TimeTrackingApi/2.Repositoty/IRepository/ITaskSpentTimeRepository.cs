using System.Collections.Generic;
using TimeTrackingApi._0.Models;


namespace TimeTrackingApi._2.Repositoty.IRepository
{
    public interface ITaskSpentTimeRepository
    {   
        ICollection<SpentTimeRegistry> GetListByUserId(string userId);

        ICollection<SpentTimeRegistry> GetListByTaskId(string taskId);   

        SpentTimeRegistry Create(SpentTimeRegistry taskSpentTime);
        
    }
}
