using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQuizSystem.RazorPage.Pages.Hosts
{
    public class HostEndGameModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IPlayerAnswerService _playerAnswerService;

        public HostEndGameModel(IRoomService roomService, IPlayerAnswerService playerAnswerService)
        {
            _roomService = roomService;
            _playerAnswerService = playerAnswerService;
        }

        public int RoomId { get; set; }
        public string RoomCode { get; set; } = null!;
        public Dictionary<int, double> TeamScores { get; set; } = new Dictionary<int, double>();

        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            RoomId = roomId;

            // Lấy thông tin phòng
            var room = await _roomService.GetByIdAsync(roomId);
            if (room == null)
            {
                return NotFound();
            }

            RoomCode = room.RoomCode;

            // Lấy danh sách câu trả lời trong phòng
            var playerAnswersResult = await _playerAnswerService.GetAsync(roomId);
            var playerAnswers = playerAnswersResult.Item1;

            // Tính điểm trung bình cho từng team
            var teamGroups = playerAnswers
                .GroupBy(pa => pa.Player.TeamCode)
                .Where(g => g.Key >= 1 && g.Key <= 4); // Chỉ lấy 4 team

            foreach (var team in teamGroups)
            {
                int totalScore = team.Sum(pa => pa.Score);
                int playerCount = team.Count();
                double averageScore = playerCount > 0 ? (double)totalScore / playerCount : 0;
                TeamScores[team.Key] = averageScore;
            }

            // Đảm bảo có 4 team, nếu team chưa có người chơi thì điểm = 0
            for (int i = 1; i <= 4; i++)
            {
                if (!TeamScores.ContainsKey(i))
                {
                    TeamScores[i] = 0;
                }
            }

            return Page();
        }
    }
}