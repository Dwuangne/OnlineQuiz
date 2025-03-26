using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using OnlineQuiz.Service.BusinessModel;
using OnlineQuiz.Service.Service.IService;
using OnlineQuizSystem.RazorPage.Hubs;
using OnlineQuizSystem.RazorPage.Models.ResponseModel;

namespace OnlineQuizSystem.RazorPage.Pages.Hosts
{
    public class HostQuestionModel : PageModel
    {
        private readonly IQuestionInRoomService _questionInRoomService;
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        private readonly IHubContext<SignalRServer> _hubContext;

        public HostQuestionModel(
            IQuestionInRoomService questionInRoomService,
            IRoomService roomService,
            IMapper mapper,
            IHubContext<SignalRServer> hubContext)
        {
            _questionInRoomService = questionInRoomService;
            _roomService = roomService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public QuestionInRoomResponse CurrentQuestion { get; set; } = default!;
        public int RoomId { get; set; }
        public int CurrentOrder { get; set; }
        public int TotalQuestions { get; set; }

        public async Task<IActionResult> OnGetAsync(int roomId, int order = 1)
        {
            RoomId = roomId;

            // Lấy danh sách câu hỏi trong phòng
            var response = await _questionInRoomService.GetQuestionsByRoomIdAsync(roomId);
            var questions = response.Item1;
            if (questions == null || !questions.Any())
            {
                return RedirectToPage("/Hosts/HostEndGame", new { roomId });
            }

            TotalQuestions = response.TotalRecords;
            CurrentOrder = order;

            // Kiểm tra nếu order vượt quá số câu hỏi
            if (CurrentOrder > TotalQuestions)
            {
                var roomExisting = await _roomService.GetByIdAsync(roomId);
                if (roomExisting == null)
                {
                    return NotFound();
                }

                var roomBusiness = _mapper.Map<RoomBusiness>(roomExisting);

                roomBusiness.EndedAt = DateTime.Now;
                roomBusiness.IsActive = false;

                // Kết thúc phòng và chuyển hướng
                await _roomService.UpdateAsync(roomExisting.RoomId, roomBusiness);
                return RedirectToPage("/Hosts/HostEndGame", new { roomId });
            }

            // Lấy câu hỏi hiện tại
            var currentQuestion = questions.FirstOrDefault(q => q.DisplayOrder == CurrentOrder);
            if (currentQuestion == null)
            {
                return NotFound();
            }

            // Map to response model
            CurrentQuestion = _mapper.Map<QuestionInRoomResponse>(currentQuestion);

            // Gửi câu hỏi đến tất cả người chơi qua SignalR
            await _hubContext.Clients.Group($"Room_{roomId}").SendAsync("ReceiveQuestion", CurrentQuestion);

            return Page();
        }

        public async Task<IActionResult> OnPostNextQuestionAsync(int roomId, int currentOrder)
        {
            // Tăng order để chuyển sang câu tiếp theo
            return RedirectToPage("/Hosts/HostQuestion", new { roomId, order = currentOrder + 1 });
        }
    }
}
