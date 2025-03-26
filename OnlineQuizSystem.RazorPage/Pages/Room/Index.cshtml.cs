using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OnlineQuiz.Repository;
using OnlineQuiz.Repository.Entities;

namespace OnlineQuizSystem.RazorPage.Pages.Room
{
    public class IndexModel : PageModel
    {
        private readonly OnlineQuiz.Repository.OnlineQuizSystemContext _context;

        public IndexModel(OnlineQuiz.Repository.OnlineQuizSystemContext context)
        {
            _context = context;
        }

        public IList<OnlineQuiz.Repository.Entities.Room> Room { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Room = await _context.Rooms
                .Include(r => r.Host)
                .Include(r => r.Quiz).ToListAsync();
        }
    }
}
