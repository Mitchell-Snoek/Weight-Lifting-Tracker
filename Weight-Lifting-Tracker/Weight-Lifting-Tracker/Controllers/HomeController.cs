using Weight_Lifting_Tracker.Data;
using Weight_Lifting_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Weight_Lifting_Tracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Datacontext _context;

        public HomeController(Datacontext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Begin home page
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "Date_desc" : "Date";
            ViewData["TimeSortParm"] = sortOrder == "Time" ? "Time_desc" : "Time";
            ViewData["CurrentFilter"] = searchString;
            var workouts = from s in _context.Workouts
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                workouts = workouts.Where(s => s.WorkoutTitle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Title_desc":
                    workouts = workouts.OrderByDescending(s => s.WorkoutTitle);
                    break;
                case "Time":
                    workouts = workouts.OrderBy(s => s.TimeInGym);
                    break;
                case "Time_desc":
                    workouts = workouts.OrderByDescending(s => s.TimeInGym);
                    break;
                case "Date":
                    workouts = workouts.OrderBy(s => s.Date);
                    break;
                case "Date_desc":
                    workouts = workouts.OrderByDescending(s => s.Date);
                    break;
                default:
                    workouts = workouts.OrderBy(s => s.WorkoutTitle);
                    break;
            }
            return View(await workouts.AsNoTracking().ToListAsync());
        }
        //End home page


        //Begin creates
        //Begin create workouts
        public IActionResult CreateWorkout()
        {
            var workout = _context.Workouts
                .Include(x => x.Lifts)
                    .ThenInclude(l => l.Sets)
                        .FirstOrDefault(x => x.Id == 0);

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWorkout(Workout workout)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(workout);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
        //End create workout

        //Begin create lift
        public IActionResult CreateLift(int id)
        {
            var lift = new Lift();
            lift.WorkoutId = id;
            return View(lift);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLift(Lift lift)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(lift);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
        //End create lift

        //Begin create set
        public IActionResult CreateSet(int id)
        {
            var set = new Set();
            set.LiftId = id;
            return View(set);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSet(Set set)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(set);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return RedirectToAction(nameof(Index));
        }
        //End create set
        //End creates


        //Begin Edits
        //Begin Edit workout
        public IActionResult EditWorkout(int id)
        {
            var workout = _context.Workouts
                .Include(x => x.Lifts)
                    .ThenInclude(l => l.Sets)
                        .FirstOrDefault(x => x.Id == id);

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditWorkout(Workout workout, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var workoutToUpdate = await _context.Workouts.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Workout>(workoutToUpdate,
                "",
                s => s.WorkoutTitle, s => s.Date, s => s.TimeInGym))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //End Edit workout

        //Begin Edit lift
        public IActionResult EditLift(int id)
        {
            var workout = _context.Lifts
                .Include(x => x.Sets)
                    .FirstOrDefault(x => x.Id == id);

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditLift(Lift Lifts, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var liftToUpdate = await _context.Lifts.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Lift>(liftToUpdate,
                "",
                s => s.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //End Edit lift

        //Begin Edit set
        public IActionResult EditSet(int id)
        {
            var workout = _context.Sets
                .FirstOrDefault(x => x.Id == id);

            return View(workout);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSet(Set Sets, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var setToUpdate = await _context.Sets.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Set>(setToUpdate,
                "",
                s => s.SetNumber, s => s.Reps, s => s.Weight))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return RedirectToAction(nameof(Index));
        }
        //End Edit set
        //End Edits


        //Begin details
        public IActionResult Details(int id)
        {
            // LINQ
            ViewBag.ID = id;

            var workout = _context.Workouts
                .Include(x => x.Lifts)
                    .ThenInclude(l => l.Sets)
                .FirstOrDefault(x => x.Id == id);

            return View(workout);
        }
        //End details


        //Begin deletes
        //Begin delete workout
        public IActionResult DeleteWorkout(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        public async Task<IActionResult> DeleteingWorkout(Workout Workouts, int id)
        {
            var workoutdelete = await _context.Workouts.FindAsync(id);
            if (workoutdelete == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Workouts.Remove(workoutdelete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }
        //End delete workout

        //Begin delete lift
        public IActionResult DeleteLift(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        public async Task<IActionResult> DeleteingLift(Lift Lifts, int id)
        {
            var liftdelete = await _context.Lifts.FindAsync(id);
            if (liftdelete == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Lifts.Remove(liftdelete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }
        //End delete lift

        //Begin delete set
        public IActionResult DeleteSet(int id)
        {
            ViewBag.ID = id;
            return View();
        }

        public async Task<IActionResult> DeleteingSet(Set sets, int id)
        {
            var setdelete = await _context.Sets.FindAsync(id);
            if (setdelete == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Sets.Remove(setdelete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Index), new { id = id, saveChangesError = true });
            }
        }
        //End delete set
        //End deletes


        //Error
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}