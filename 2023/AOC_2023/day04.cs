using System.Text.RegularExpressions;

namespace AOC_2023 {
    class Day04 : Day{


        protected override int Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 1 logic here
            Dictionary<int, Dictionary<string, object>> game = new Dictionary<int, Dictionary<string, object>>();

            char[] delimiters = { ':', '|' };
            Regex rx_number = new Regex(@"(\d+)");
            foreach (var line in lines) {
                var split = line.Split(delimiters);
                var index = Int32.Parse(rx_number.Match(split[0]).Value);
                var winning_nrs = new List<int>();
                var playing_nrs = new List<int>();
                foreach (Match match in rx_number.Matches(split[1])) {
                    winning_nrs.Add(Int32.Parse(match.Value));
                }
                foreach (Match match in rx_number.Matches(split[2])) {
                    playing_nrs.Add(Int32.Parse(match.Value));
                }
                var intersect = playing_nrs.Intersect(winning_nrs);
                var points = 0;
                var count = intersect.Count();
                if (count == 1) {
                    points = 1;
                } else {
                    points = (int)Math.Pow(2,count - 1);
                }
                game.Add(index, new Dictionary<string, object> {
                    { "winning", winning_nrs },
                    { "playing", playing_nrs },
                    { "points", points }
                });
                result += points;
            }

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }

        protected override int Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 2 logic here
            Dictionary<int, Dictionary<string, object>> game = new Dictionary<int, Dictionary<string, object>>();
            var max_games = 0;
            char[] delimiters = { ':', '|' };
            Regex rx_number = new Regex(@"(\d+)");
            foreach (var line in lines) {
                var split = line.Split(delimiters);
                var index = Int32.Parse(rx_number.Match(split[0]).Value);
                max_games = index;
                var winning_nrs = new List<int>();
                var playing_nrs = new List<int>();
                foreach (Match match in rx_number.Matches(split[1])) {
                    winning_nrs.Add(Int32.Parse(match.Value));
                }
                foreach (Match match in rx_number.Matches(split[2])) {
                    playing_nrs.Add(Int32.Parse(match.Value));
                }
                var intersect = playing_nrs.Intersect(winning_nrs);
                var count = intersect.Count();
                game.Add(index, new Dictionary<string, object> {
                    { "winning", winning_nrs },
                    { "playing", playing_nrs },
                    { "count", count },
                    { "cards", 1}
                });
            }
            
            foreach (var card in game) {
                foreach (var duplicates in Enumerable.Range(1, (int)card.Value["cards"])) {
                    result += 1;
                    var range = Enumerable.Range(card.Key + 1, (int)card.Value["count"]);
                    foreach (var win in range) {
                        if (win <= max_games) {
                            game[win]["cards"] = (int)game[win]["cards"] + 1;
                        }
                    }
                }
            }

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }
    }
}
