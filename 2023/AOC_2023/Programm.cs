using System.Reflection;

namespace AOC_2023 {
    public class Programm {
        static void Main() {
            string day_string = "09";
            Assembly assembly = Assembly.GetExecutingAssembly();
            Day day = assembly.CreateInstance($"AOC_2023.Day{day_string}") as Day;
            day.Solver(day_string, true);
        }
    }
}
