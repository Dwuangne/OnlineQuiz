using Azure;
using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User?> LoginAsync(string emailAddress, string password)
        {
            var user = await _unitOfWork.UserRepository.GetAsync(
                   filter: x => x.Email == emailAddress && x.Password == password,
                   orderBy: null,
                   includeProperties: "",
                   pageIndex: 1,
                   pageSize: 1
                   );
            var entity = user.Item1.FirstOrDefault();
            return entity;
        }
    }
}
