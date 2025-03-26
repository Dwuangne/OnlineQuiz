using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service.IService
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string emailAddress, string password);
        Task<List<UserBusiness?>> GetUsersAsync();
    }
}
