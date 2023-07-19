using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMVMTAssessment.Data;
using RMVMTAssessment.Models;
using RMVMTAssessment.Repository;

namespace RMVMTAssessment.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public IndexModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAsync()
        {
            DbInitializer.Reset(_dbContext);
            TempData["Message"] = "Database reseted successful";
            return Page();
        }
    }
}