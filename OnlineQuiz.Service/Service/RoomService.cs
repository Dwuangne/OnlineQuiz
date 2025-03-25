using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.UnitOfWork;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Service.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Room?> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.RoomRepository.GetAsync(
               filter: q => q.RoomId == id,
               orderBy: null,
               includeProperties: "Players",
               pageIndex: 1,
               pageSize: 1
           );
            var entity = entities.Item1.FirstOrDefault();
            return entity;
        }

        public async Task<Room> InsertAsync(RoomBusiness roomBusiness)
        {
            var roomEntity = new Room
            {
                QuizId = roomBusiness.QuizId,
                HostId = roomBusiness.HostId,
                RoomCode = roomBusiness.RoomCode,
                IsActive = true,
                StartedAt = null,
                EndedAt = null,
            };
            roomEntity = await _unitOfWork.RoomRepository.InsertAsync(roomEntity);
            await _unitOfWork.SaveChangesAsync();

            return roomEntity;
        }

        public async Task<Room?> UpdateAsync(int id, RoomBusiness roomBusiness)
        {
            var room = await GetByIdAsync(id);
            if (room == null)
            {
                return null;
            }

            room.HostId = roomBusiness.HostId;
            room.QuizId = roomBusiness.QuizId;
            room.RoomCode = roomBusiness.RoomCode;
            room.IsActive = roomBusiness.IsActive;
            room.StartedAt = roomBusiness.StartedAt;
            room.EndedAt = roomBusiness.EndedAt;

            var roomEntity = await _unitOfWork.RoomRepository.UpdateAsync(room);
            await _unitOfWork.SaveChangesAsync();

            return roomEntity;
        }
    }
}
