using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day05 : Day {

        protected override int Case1(List<string> lines) {
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


        private  IEnumerable<long> CreateRange(long start, long count) {
            var limit = start + count;

            while (start < limit) {
                yield return start;
                start++;
            }
        }

        protected override int Case2(List<string> lines) {
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
                        var start = Int64.Parse(range[0]);
                        var stop = start + Int64.Parse(range[1]) - 1;
                        seeds.Add((start, stop));
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
                    var dest_end = dest + Int64.Parse(matches[2].Value) - 1;
                    var source = Int64.Parse(matches[1].Value);
                    var source_end = source + Int64.Parse(matches[2].Value) - 1;
                    foreach (var seed in seeds) {
                        if (source <= seed.Item1 && seed.Item2 <= source_end) {
                            parsed_seeds.Add((seed.Item1 - source + dest, seed.Item2 - source_end + dest_end));
                        } else if (source <= seed.Item1 && seed.Item1 <= source_end && seed.Item2 > source_end) {
                            parsed_seeds.Add((seed.Item1 - source + dest, source_end - source + dest));
                            tmp_seeds.Add((source_end +1, seed.Item2));
                        } else if (seed.Item1 < source && source <= seed.Item2 && seed.Item2 <= source_end ) {
                            parsed_seeds.Add((dest, seed.Item2 - source_end + dest_end));
                            tmp_seeds.Add((seed.Item1, source - 1));
                        } else if (source > seed.Item1 && seed.Item2 > source_end) {
                            parsed_seeds.Add((dest, dest_end));
                            tmp_seeds.Add((seed.Item1, source -1));
                            tmp_seeds.Add((source_end +1, seed.Item2));
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
