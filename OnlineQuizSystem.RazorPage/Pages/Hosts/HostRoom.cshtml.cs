using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Hubs;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;

namespace OnlineQuizSystem.RazorPage.Pages.Hosts
{
    public class HostRoomModel : PageModel
    {
        private readonly IRoomService _roomService;
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        private readonly IHubContext<SignalRServer> _hubContext;

        public HostRoomModel(
            IRoomService roomService,
            IPlayerService playerService,
            IMapper mapper,
            IHubContext<SignalRServer> hubContext)
        {
            _roomService = roomService;
            _playerService = playerService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public RoomResponse Room { get; set; } = default!;
        public Dictionary<int, List<PlayerResponse>> Teams { get; set; } = new Dictionary<int, List<PlayerResponse>>();

        public async Task<IActionResult> OnGetAsync(int roomId)
        {
            // Lấy thông tin phòng
            var response = await _roomService.GetByIdAsync(roomId);
            if (response == null || !(bool)response.IsActive)
            {
                return NotFound();
            }
            Room = _mapper.Map<RoomResponse>(response);

            // Khởi tạo 4 team
            for (int i = 1; i <= 4; i++)
            {
                Teams[i] = new List<PlayerResponse>();
            }

            // Lấy danh sách người chơi và phân vào các team
            var players = await _playerService.GetPlayersByRoomIdAsync(roomId);
            foreach (var player in players)
            {
                if (Teams.ContainsKey(player.TeamCode) && Teams[player.TeamCode].Count < 5)
                {
                    Teams[player.TeamCode].Add(_mapper.Map<PlayerResponse>(player));
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostStartQuizAsync(int roomId)
        {
            // Bắt đầu quiz và thông báo qua SignalR
            await _hubContext.Clients.Group($"Room_{roomId}").SendAsync("QuizStarted", roomId);
            return RedirectToPage("/Host/HostQuestion", new { roomId, order = 1 });
        }
    }
}
