using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMVMTAssessment.Dtos;
using RMVMTAssessment.Exceptions;
using RMVMTAssessment.Interfaces;

namespace RMVMTAssessment.Pages.SuspensionTransactions
{
    public class CreateModel : PageModel
    {
        private readonly ISuspensionTransactionRepository _suspensionTransactionRepository;

        [BindProperty]
        public SuspensionTransactionForm SuspensionTransactionForm { get; set; } = new SuspensionTransactionForm();

        public CreateModel(ISuspensionTransactionRepository suspensionTransactionRepository)
        {
            _suspensionTransactionRepository = suspensionTransactionRepository ?? throw new ArgumentNullException(nameof(suspensionTransactionRepository));
        }

        public IActionResult OnGet()
        {
            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                if (SuspensionTransactionForm.DriverLicenceNumber == null) throw new Exception("error....");

                var suspensionTransaction = await _suspensionTransactionRepository.AddAsync(
                    (int)SuspensionTransactionForm.DriverLicenceNumber,
                    SuspensionTransactionForm.StartDate,
                    SuspensionTransactionForm.EndDate);

                TempData["Message"] = $"Suspension added successfully into the #{suspensionTransaction?.Licence?.Number} driver licence";

                ModelState.Clear();
                SuspensionTransactionForm = new SuspensionTransactionForm();
            } 
            catch (InvalidPeriodException)
            {
                ModelState.AddModelError("InvalidPeriod", "Start Date is greather than or equals to the End Date");
            }
            catch (DriverLicenceNotFoundException)
            {
                ModelState.AddModelError("DriverLicenceNotFound", "Driver Licence Number not found");
            }
            catch (ExpiredDriverLicenceException)
            {
                ModelState.AddModelError("ExpiredDriverLicence", "Driver Licence is expired");
            }

            return Page();
        }
    }
}
