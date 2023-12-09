using System.Text.RegularExpressions;
using System.Linq;

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
                result += calculateNextNumber(numbers);
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
                result += calculateNextNumberReverse(numbers);
            }

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }


        private long calculateNextNumber(List<long> input) {
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
        
        private long calculateNextNumberReverse(List<long> input) {
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
            numbers.Last().Insert(0, 0);

            for (int i = numbers.Count - 2; i >= 0; i--) {
                numbers[i].Insert(0,numbers[i].First() - numbers[i+1].First());
            }
            return numbers.First().First();
        }

        [GeneratedRegex("(-*\\d+)")]
        private static partial Regex NumberRegex();
    }
}
