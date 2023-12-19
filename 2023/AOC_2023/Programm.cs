using System.Reflection;

namespace AOC_2023 {
    public class Programm {
        static void Main() {
            string day_string = "13";
            Assembly assembly = Assembly.GetExecutingAssembly();
            try {
                Day day = assembly.CreateInstance($"AOC_2023.Day{day_string}") as Day;
                day.Solver(day_string, true);
            } catch (Exception e) {
              Console.WriteLine(e.Message);  
            }

        }
    }
}
