using Weight_Lifting_Tracker.Models;
using System;
using System.Linq;

namespace Weight_Lifting_Tracker.Data
{
    public class DbInitializer
    {
        public static void Initialize(Datacontext context)
        {
            context.Database.EnsureCreated();

            if (context.Workouts.Any())
            {
                return;   // DB has been seeded
            }

            var model = new Workout[]
            {
                new Workout{
                    WorkoutTitle="testing",
                    TimeInGym=75,
                    Date=DateTime.Parse("2105-03-07 14:20 PM"),
                    Lifts = new List<Lift>
                    {
                        new Lift{
                            Name="Bench",
                            Sets = new List<Set>
                            {
                                new Set{SetNumber=1, Weight=55, Reps=10},
                                new Set{SetNumber=2, Weight=65, Reps=8}
                            }
                        },
                        new Lift{
                            Name="Deadlift",
                            Sets = new List<Set>
                            {
                                new Set{SetNumber=1, Weight=100, Reps=10},
                                new Set{SetNumber=2, Weight=120, Reps=8}
                            }
                        }
                    }
                },
                new Workout{
                    WorkoutTitle="test634",
                    TimeInGym=75,
                    Date=DateTime.Parse("2052-05-05 17:54 PM"),
                    Lifts = new List<Lift>
                    {
                        new Lift{
                            Name="Squat",
                            Sets = new List<Set>
                            {
                                new Set{SetNumber=1, Weight=100, Reps=10},
                                new Set{SetNumber=2, Weight=110, Reps=8}
                            }
                        },
                        new Lift{
                            Name="LegPress",
                            Sets = new List<Set>
                            {
                                new Set{SetNumber=1, Weight=240, Reps=10},
                                new Set{SetNumber=2, Weight=260, Reps=8}
                            }
                        }
                    }}
            };

            foreach (var item in model)
            {
                context.Workouts.Add(item);
            }

            context.SaveChanges();
        }
    }
}
