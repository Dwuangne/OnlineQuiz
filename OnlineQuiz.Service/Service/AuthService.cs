using Azure;
using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.Repositories;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.BusinessModel;
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
        private readonly IGenericRepository<User?> genericRepository;

        public AuthService(IUnitOfWork unitOfWork, IGenericRepository<User?> genericRepository)
        {
            _unitOfWork = unitOfWork;
            this.genericRepository = genericRepository;
        }

        public async Task<List<UserBusiness?>> GetUsersAsync()
        {
            try
            {
                var (listUser, _, _, _) = await genericRepository.GetAsync(filter: null, orderBy: null, includeProperties: "", pageIndex: 1, pageSize: 10);
                return listUser.Select(c => new UserBusiness
                {
                    UserId = c.UserId,
                    Email = c.Email

                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
