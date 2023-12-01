using System.Text.RegularExpressions;

namespace AOC_2023 {
    class Day01 {

        public static void Solver() {
            string fileName = "01";
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
            Regex rx = new Regex(@"(\d)");
            foreach (var line in lines) {
                var matches = rx.Matches(line);
                if (matches.Count > 0) {
                    int tmp = 0;
                    string group1 = matches[0].Value;
                    string group2 = matches.Last().Value;
                    if (group2 == "") {
                        group2 = group1;
                    }
                    tmp = Int32.Parse($"{group1}{group2}");
                    result += tmp;
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
            
            Regex rx = new Regex(@"(\d)");
            foreach (var line in lines) {
                var reduced = ReplaceIntStrings(line);
                var matches = rx.Matches(reduced);
                if (matches.Count > 0) {
                    int tmp = 0;
                    string group1 = matches[0].Value;
                    string group2 = matches.Last().Value;
                    if (group2 == "") {
                        group2 = group1;
                    }
                    tmp = Int32.Parse($"{group1}{group2}");
                    result += tmp;
                }
            }
            

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }

        private static string ReplaceIntStrings(string value) {
            var result = value;
            var regex = new Regex(@"(one|two|three|four|five|six|seven|eight|nine)");
            while(regex.IsMatch(result)) {
                Match match = regex.Match(result);
                var replacement = "";
                switch (match.Groups[1].Value) {
                    case "one":
                        replacement = "o1e";
                        break;
                    case "two":
                        replacement = "t2o";
                        break;
                    case "three":
                        replacement = "thr3ee";
                        break;
                    case "four":
                        replacement = "fo4ur";
                        break;
                    case "five":
                        replacement = "fi5ve";
                        break;
                    case "six":
                        replacement = "s6x";
                        break;
                    case "seven":
                        replacement = "se7ven";
                        break;
                    case "eight":
                        replacement = "ei8ght";
                        break;
                    case "nine":
                        replacement = "ni9ne";
                        break;
                }
                
                result = regex.Replace(result, replacement, 1);
            }
            
            return result;
        }
    }
}
