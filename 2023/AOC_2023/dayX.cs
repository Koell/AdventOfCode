
    using System.Text.RegularExpressions;

    namespace AOC_2023 {
        public class DayX: Day {

            protected override int Case1(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 1 logic here

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }

            protected override int Case2(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 2 logic here

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }
        }
    }

