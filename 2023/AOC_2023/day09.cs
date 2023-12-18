using System.Text.RegularExpressions;

namespace AOC_2023 {
    public partial class Day09: Day {

        protected override Solution Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            long result = 0;
            // Your case 1 logic here
            Regex rx_number = NumberRegex();
            
            foreach (var line in lines) {
                var numbers = new List<long>();
                foreach (Match match in rx_number.Matches(line)) {
                    numbers.Add(long.Parse(match.Value));
                }
                result += CalculateNextNumber(numbers);
            }

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            long result = 0;

            // Your case 2 logic here
            Regex rx_number = NumberRegex();
            
            foreach (var line in lines) {
                var numbers = new List<long>();
                foreach (Match match in rx_number.Matches(line)) {
                    numbers.Add(long.Parse(match.Value));
                }
                numbers.Reverse();
                result += CalculateNextNumber(numbers);
            }

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }


        private long CalculateNextNumber(List<long> input) {
            var numbers = new List<List<long>> {
                input
            };
            while (!numbers.Last().TrueForAll(val => val == 0)) {
                var last = numbers.Last();
                var new_list = new List<long>();
                for (int i= 0; i < last.Count -1; i++) {
                    new_list.Add(last[i+1]-last[i]);
                }
                numbers.Add(new_list);
            }
            numbers.Last().Add(0);

            for (int i = numbers.Count - 2; i >= 0; i--) {
                numbers[i].Add(numbers[i].Last() + numbers[i+1].Last());
            }
            return numbers.First().Last();
        }

        [GeneratedRegex(@"(-?\d+)")]
        private static partial Regex NumberRegex();
    }
}
