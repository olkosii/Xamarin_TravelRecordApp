using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;

namespace TravelRecordApp.Helpers.Interfaces
{
    public interface IFirestoreService
    {
        bool Create(Post post);
        Task<bool> Update(Post post);
        Task<bool> Delete(Post post);
        Task<List<Post>> GetAll();
    }
}
