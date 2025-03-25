using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service.IService
{
    public interface IRoomService
    {
        Task<Room> InsertAsync(RoomBusiness roomBusiness);
        Task<Room?> GetByIdAsync(int id);
        Task<Room?> UpdateAsync(int id, RoomBusiness roomBusiness);
    }
}
