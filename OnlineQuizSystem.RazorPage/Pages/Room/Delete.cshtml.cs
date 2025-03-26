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
    public class DeleteModel : PageModel
    {
        private readonly OnlineQuiz.Repository.OnlineQuizSystemContext _context;

        public DeleteModel(OnlineQuiz.Repository.OnlineQuizSystemContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OnlineQuiz.Repository.Entities.Room Room { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FirstOrDefaultAsync(m => m.RoomId == id);

            if (room == null)
            {
                return NotFound();
            }
            else
            {
                Room = room;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room != null)
            {
                Room = room;
                _context.Rooms.Remove(Room);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
