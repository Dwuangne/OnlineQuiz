using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineQuiz.Service.BusinessModel
{
    public class RoomBusiness
    {
        public int QuizId { get; set; }
        public int HostId { get; set; }
        public string RoomCode { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        [Required(ErrorMessage = "Start time is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start Time")]
        [CustomValidation(typeof(RoomBusiness), nameof(ValidateStartDate))]
        public DateTime? StartedAt { get; set; }

        [Required(ErrorMessage = "End time is required")]
        [DataType(DataType.DateTime)]
        [Display(Name = "End Time")]
        [CustomValidation(typeof(RoomBusiness), nameof(ValidateDates))]
        public DateTime? EndedAt { get; set; }

        // Validation method for start date
        public static ValidationResult ValidateStartDate(DateTime? startDate)
        {
            if (startDate == null)
                return new ValidationResult("Start time cannot be empty");

            if (startDate.Value < DateTime.Now)
                return new ValidationResult("Start time must be greater than or equal to the current time");

            return ValidationResult.Success;
        }

        // Validation method for comparing start and end dates
        public static ValidationResult ValidateDates(DateTime? endDate, ValidationContext context)
        {
            var instance = context.ObjectInstance as RoomBusiness;

            if (endDate == null)
                return new ValidationResult("End time cannot be empty");

            if (instance?.StartedAt == null)
                return new ValidationResult("Start time cannot be empty");

            if (endDate.Value <= instance.StartedAt.Value)
                return new ValidationResult("End time must be greater than start time");

            // Limit maximum time span (e.g., 24 hours)
            if ((endDate.Value - instance.StartedAt.Value).TotalHours > 24)
                return new ValidationResult("Time span between start and end time cannot exceed 24 hours");

            return ValidationResult.Success;
        }
    }
}