namespace Weight_Lifting_Tracker.Models
{
    public class Lift
    {
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public string? Name { get; set; }
        public Workout? Workout { get; set; }
        public ICollection<Set>? Sets { get; set; } = default!;
    }
}
