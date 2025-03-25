using Microsoft.EntityFrameworkCore;
using OnlineQuiz.Repository.Entities;
using OnlineQuiz.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineQuizSystemContext _context; // Thay bằng DbContext cụ thể của bạn, ví dụ: OnlineQuizDbContext
        private bool _disposed = false;

        // Các repository
        public IGenericRepository<Player> PlayerRepository { get; private set; }
        public IGenericRepository<PlayerAnswer> PlayerAnswerRepository { get; private set; }
        public IGenericRepository<Question> QuestionRepository { get; private set; }
        public IGenericRepository<QuestionInRoom> QuestionInRoomRepository { get; private set; }
        public IGenericRepository<Quiz> QuizRepository { get; private set; }
        public IGenericRepository<Room> RoomRepository { get; private set; }
        public IGenericRepository<User> UserRepository { get; private set; }

        public UnitOfWork(OnlineQuizSystemContext context) // Thay DbContext bằng context cụ thể của bạn
        {
            _context = context;

            // Khởi tạo các repository
            PlayerRepository = new GenericRepository<Player>(_context);
            PlayerAnswerRepository = new GenericRepository<PlayerAnswer>(_context);
            QuestionRepository = new GenericRepository<Question>(_context);
            QuestionInRoomRepository = new GenericRepository<QuestionInRoom>(_context);
            QuizRepository = new GenericRepository<Quiz>(_context);
            RoomRepository = new GenericRepository<Room>(_context);
            UserRepository = new GenericRepository<User>(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}
