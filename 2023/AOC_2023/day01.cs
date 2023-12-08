using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day01 : Day{


        protected override Solution Case1(List<string> lines) {
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
            
            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // a reverse sort would have been more elegant 
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
            

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        private string ReplaceIntStrings(string value) {
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
