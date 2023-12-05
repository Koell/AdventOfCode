using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day05 {

        public static void Solver() {
            string fileName = "05";
            string inputpath = Helper.GetInputPath();

            List<string> testInput = Helper.ExtractList($"{inputpath}tinput_{fileName}");
            List<string> realInput = Helper.ExtractList($"{inputpath}input_{fileName}");
            if (testInput.Count == 0) {
                Console.WriteLine("MISSING DATA!!!");
            }
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
            List<long> seeds = new List<long>();
            List<long> parsed_seeds = new List<long>();
            List<long> tmp_seeds = new List<long>();
            Dictionary<int, Dictionary<long, long>> mapping = new Dictionary<int, Dictionary<long, long>>();
            int count = -1;
            Regex rx_number = new Regex(@"(\d+)");

            foreach ((string line, int i) in lines.Select((value, i) => (value, i))) {
                if (i == 0) {
                    foreach (Match match in rx_number.Matches(line)) {
                        seeds.Add(Int64.Parse(match.Value));
                    }
                    continue;
                }
                if (line == "") {
                    count++;
                    seeds.AddRange(parsed_seeds);
                    parsed_seeds.Clear();
                    continue;
                } else if (!rx_number.IsMatch(line)) {
                    continue;
                } else {
                    var matches = rx_number.Matches(line);
                    var dest = Int64.Parse(matches[0].Value);
                    var source = Int64.Parse(matches[1].Value);
                    var step = Int64.Parse(matches[2].Value) - 1;

                    foreach (var seed in seeds) {
                        if (source <= seed && seed <= source + step) {
                            parsed_seeds.Add(seed - source + dest);
                        } else {
                            tmp_seeds.Add(seed);
                        }
                    }
                    seeds.Clear();
                    seeds.AddRange(tmp_seeds);
                    tmp_seeds.Clear();
                }
            }
            seeds.AddRange(parsed_seeds);
            seeds.Sort();
            result = (int)seeds[0];


            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }


        private static IEnumerable<long> CreateRange(long start, long count) {
            var limit = start + count;

            while (start < limit) {
                yield return start;
                start++;
            }
        }

        private static int Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 2 logic here
            List<(long, long)> seeds = new List<(long, long)>();
            List<(long, long)> parsed_seeds = new List<(long, long)>();
            List<(long, long)> tmp_seeds = new List<(long, long)>();
            Dictionary<int, Dictionary<long, long>> mapping = new Dictionary<int, Dictionary<long, long>>();
            int count = -1;
            Regex rx_number = new Regex(@"(\d+)");
            Regex rx_pair = new Regex(@"(\d+ \d+)");

            foreach ((string line, int i) in lines.Select((value, i) => (value, i))) {
                if (i == 0) {
                    foreach (Match match in rx_pair.Matches(line)) {
                        var range = match.Value.Split(" ");
                        seeds.Add((Int64.Parse(range[0]), Int64.Parse(range[1])));
                    }
                    continue;
                }
                if (line == "") {
                    count++;
                    seeds.AddRange(parsed_seeds);
                    parsed_seeds.Clear();
                    continue;
                } else if (!rx_number.IsMatch(line)) {
                    continue;
                } else {
                    var matches = rx_number.Matches(line);
                    var dest = Int64.Parse(matches[0].Value);
                    var source = Int64.Parse(matches[1].Value);
                    var step = Int64.Parse(matches[2].Value) - 1;
                    //TODO needs improvement 
                    foreach (var seed in seeds) {
                        if (source <= seed.Item1 && seed.Item1 + seed.Item2 <= source + step) {
                            parsed_seeds.Add((seed.Item1 - source + dest, seed.Item2));
                        } else if (source <= seed.Item1 && seed.Item1 <= source + step) {
                            parsed_seeds.Add((seed.Item1 - source + dest, long.Abs(seed.Item1 - (source + step))));
                            tmp_seeds.Add(((source + step), long.Abs((seed.Item1 + seed.Item2) - (source + step))));
                        } else if (source <= seed.Item2 && seed.Item2 <= source + step) {
                            parsed_seeds.Add((dest, long.Abs((seed.Item1 + seed.Item2) - (source + step))));
                            tmp_seeds.Add((seed.Item1, long.Abs(seed.Item1 - source)));
                        } else if (source >= seed.Item1 && seed.Item2 >= source + step) {
                            parsed_seeds.Add((dest, step));
                            tmp_seeds.Add((seed.Item1, long.Abs(seed.Item1 - source)));
                            tmp_seeds.Add((source + step, long.Abs((seed.Item1 + seed.Item2) - (source + step))));
                        } else {
                            tmp_seeds.Add(seed);
                        }
                    }
                    seeds.Clear();
                    seeds.AddRange(tmp_seeds);
                    tmp_seeds.Clear();
                }
            }
            seeds.AddRange(parsed_seeds);
            seeds.Sort();
            result = (int)seeds[0].Item1;

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }
    }
}
