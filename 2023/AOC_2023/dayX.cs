using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class DayX: Day {

        protected override Solution Case1(List<string> lines) {
            var stopwatch = Stopwatch.StartNew();
            long result = 0;

            // Your case 1 logic here

            stopwatch.Stop();
            return new Solution(result.ToString(), stopwatch.Elapsed);
        }

        protected override Solution Case2(List<string> lines) {
            var stopwatch = Stopwatch.StartNew();
            long result = 0;

            // Your case 2 logic here

            stopwatch.Stop();
            return new Solution(result.ToString(), stopwatch.Elapsed);
        }
    }
}
