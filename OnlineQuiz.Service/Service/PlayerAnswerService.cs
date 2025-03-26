using OnlineQuiz.Repository.Entities;
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
    public class PlayerAnswerService : IPlayerAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayerAnswerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(IEnumerable<PlayerAnswer>, int PageIndex, int TotalPages, int TotalRecords)> GetAsync(int roomId, int? pageIndex = null, int? pageSize = null)
        {
            return await _unitOfWork.PlayerAnswerRepository.GetAsync(
                filter: p => p.QuestionInRoom.RoomId == roomId,
                orderBy: null,
                includeProperties: "Player",
                pageIndex: 1,
                pageSize: 1
            );
        }

        public async Task<PlayerAnswer?> GetByIdAsync(int id)
        {
            var entities = await _unitOfWork.PlayerAnswerRepository.GetAsync(
               filter: p => p.PlayerAnswerId == id,
               orderBy: null,
               includeProperties: "Player",
               pageIndex: 1,
               pageSize: 1
           );
            var entity = entities.Item1.FirstOrDefault();
            return entity;
        }

        public async Task<PlayerAnswer> InsertAsync(PlayerAnswerBusiness PlayerAnswerBusiness)
        {
            var questionInRoom = await _unitOfWork.QuestionInRoomRepository.GetAsync(
                filter: p => p.QuestionInRoomId == PlayerAnswerBusiness.QuestionInRoomId,
               orderBy: null,
               includeProperties: "Question",
               pageIndex: 1,
               pageSize: 1
            );
            var questionInRoomEntity = questionInRoom.Item1.FirstOrDefault();
            if (questionInRoomEntity == null || questionInRoomEntity.Question == null)
            {
                throw new InvalidOperationException("QuestionInRoom or Question cannot be null");
            }
            var answerCorrect = questionInRoomEntity.Question.AnswerCorrect;

            var score = (answerCorrect == PlayerAnswerBusiness.SelectedAnswer) ? (int)Math.Round(PlayerAnswerBusiness.ResponseTime * 10) : 0; // Max 100 score / question ; Max ResponseTime = 10s

            var playerAnswerEntity = new PlayerAnswer
            {
                QuestionInRoomId = PlayerAnswerBusiness.QuestionInRoomId,
                PlayerId = PlayerAnswerBusiness.PlayerId,
                SelectedAnswer = PlayerAnswerBusiness.SelectedAnswer,
                ResponseTime = PlayerAnswerBusiness.ResponseTime,
                AnsweredAt = PlayerAnswerBusiness.AnsweredAt,
                Score = score
            };
            playerAnswerEntity = await _unitOfWork.PlayerAnswerRepository.InsertAsync(playerAnswerEntity);
            await _unitOfWork.SaveChangesAsync();

            return playerAnswerEntity;

        }
    }
}
