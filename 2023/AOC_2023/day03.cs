using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace AOC_2023 {
    class Day03 {

        public static void Solver() {
            string fileName = "03";
            string inputpath = Helper.GetInputPath();

            List<string> testInput = Helper.ExtractList($"{inputpath}tinput_{fileName}");
            List<string> realInput = Helper.ExtractList($"{inputpath}input_{fileName}");
            Console.WriteLine("Testrun:");
            Solve(testInput);
            Console.WriteLine("\nSolution:");
            Solve(realInput);
        }

        private static void Solve(List<string> input) {
            int sol1 = Case1(input);
            int sol2 = Case2(input);

            Console.WriteLine($"case 1: {sol1}");
            Console.WriteLine($"case 2: {sol2}");
        }

        private static int Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 1 logic here
            var rx = new Regex(@"\d+");
            var rx_symb = new Regex(@"[-!$%^&*()_+|~@=`{}\[\]:"";'<>?,\\\/#]");
            foreach (var item in lines.Select((value, i) => (value, i))) {
                foreach (Match number in rx.Matches(item.value)) {
                    var search_length = number.Index + number.Length + 1 <= lines[item.i].Length
                        ? number.Length + 1
                        : number.Length;
                    var search_start = 0;
                    if (number.Index > 0) {
                        search_start = number.Index - 1;
                        search_length += 1;
                    } else {
                        search_start = number.Index;
                    }

                    if (item.i > 0) {
                        if (rx_symb.IsMatch(lines[item.i - 1].Substring(search_start, search_length))) {
                            result += Int32.Parse(number.Value);
                            continue;
                        }
                    }
                    if (rx_symb.IsMatch(lines[item.i].Substring(search_start, search_length))) {
                        result += Int32.Parse(number.Value);
                        continue;
                    }
                    if (item.i < lines.Count) {
                        if (rx_symb.IsMatch(lines[item.i + 1].Substring(search_start, search_length))) {
                            result += Int32.Parse(number.Value);
                            continue;
                        }
                    }
                }
            }

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }

        private static int Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 2 logic here
            var rx = new Regex(@"\d+");
            var rx_symb = new Regex(@"\*");
            foreach (var item in lines.Select((value, i) => (value, i))) {
                foreach (Match symbol in rx_symb.Matches(item.value)) {
                    int search_start = symbol.Index - 1;
                    int search_end = symbol.Index + 1;
                    List<int> found_numbers = new List<int>();

                    if (item.i > 0) {
                        foreach (Match found in rx.Matches(lines[item.i - 1])) {
                            if ((found.Index >= search_start && found.Index <= search_end) ||
                                (found.Index + found.Length -1 >= search_start && found.Index + found.Length -1 <= search_end)) {
                                found_numbers.Add(Int32.Parse(found.Value));
                            }
                        }
                    }
                    foreach (Match found in rx.Matches(lines[item.i])) {
                        if ((found.Index >= search_start && found.Index <= search_end) ||
                            (found.Index + found.Length -1 >= search_start && found.Index + found.Length -1 <= search_end)) {
                            found_numbers.Add(Int32.Parse(found.Value));
                        }
                    }
                    if (item.i < lines.Count) {
                        foreach (Match found in rx.Matches(lines[item.i+1])) {
                            if ((found.Index >= search_start && found.Index <= search_end) ||
                                (found.Index + found.Length -1 >= search_start && found.Index + found.Length -1 <= search_end)) {
                                found_numbers.Add(Int32.Parse(found.Value));
                            }
                        }
                    }
                    if (found_numbers.Count == 2) {
                        result += found_numbers[0] * found_numbers[1];
                    }
                }
            }


            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }
    }
}