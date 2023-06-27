namespace Weight_Lifting_Tracker.Models
{
    public class Set
    {
        public int Id { get; set; }
        public int LiftId { get; set; }
        public int SetNumber { get; set; }
        public int Reps { get; set; }
        public int Weight { get; set; }
        public Lift? Lift { get; set; }
    }
}
