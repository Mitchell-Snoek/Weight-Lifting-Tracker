
namespace Weight_Lifting_Tracker.Models
{
    public class Workout
    {
        public int Id { get; set; }     
        public DateTime Date { get; set; }
        public string? WorkoutTitle { get; set; }
        public int TimeInGym { get; set; }

        public ICollection<Lift>? Lifts { get; set; } = default!;
    }
}