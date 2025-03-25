using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Player> PlayerRepository { get; }
        IGenericRepository<PlayerAnswer> PlayerAnswerRepository { get; }
        IGenericRepository<Question> QuestionRepository { get; }
        IGenericRepository<QuestionInRoom> QuestionInRoomRepository { get; }
        IGenericRepository<Quiz> QuizRepository { get; }
        IGenericRepository<Room> RoomRepository { get; }
        IGenericRepository<User> UserRepository { get; }

        //Phương thức lưu thay đổi vào database
        Task<int> SaveChangesAsync();
    }
}
