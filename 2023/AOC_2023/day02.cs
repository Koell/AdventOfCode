using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day02 : Day {
        

        protected override int Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;
            
            // Your case 1 logic here
            Dictionary<int, Dictionary<string, int>> game = new Dictionary<int, Dictionary<string, int>>();
            
            var blue = 14;
            var red = 12;
            var green = 13;
            
            foreach (var line in lines) {
                char[] delimiter = { ':', ';' };
                var split = line.Split(delimiter);
                Regex cube_rx = new Regex(@"(\d+) (red|blue|green)");
                if (split.Length > 0) {
                    var index = Int32.Parse(split[0].Replace("Game ", "").Trim());
                    game.Add(index, new Dictionary<string, int> {
                        {"blue", 0},
                        {"red", 0},
                        {"green", 0}
                    });
                    for (int i = 1; i < split.Length; i++) {
                        var cubes = split[i].Split(',');
                        foreach (var cube in cubes) {
                            var col = cube_rx.Matches(cube);
                            var color = col[0].Groups[2].Value;
                            var amount = Int32.Parse(col[0].Groups[1].Value);
                            
                            game[index][color] = int.Max(game[index][color], amount);
                            
                        }
                    }
                }
            }


            foreach (var subgame in game) {
                var subblue = subgame.Value["blue"];
                var subred = subgame.Value["red"];
                var subgreen = subgame.Value["green"];
                if (blue >= subblue && red >= subred && green >= subgreen) {
                    result += subgame.Key;
                }

            }


            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }

        protected override int Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;
            // Your case 2 logic here
            Dictionary<int, Dictionary<string, int>> game = new Dictionary<int, Dictionary<string, int>>();
            
            foreach (var line in lines) {
                char[] delimiter = { ':', ';' };
                var split = line.Split(delimiter);
                Regex cube_rx = new Regex(@"(\d+) (red|blue|green)");
                if (split.Length > 0) {
                    var index = Int32.Parse(split[0].Replace("Game ", "").Trim());
                    game.Add(index, new Dictionary<string, int> {
                        {"blue", 0},
                        {"red", 0},
                        {"green", 0}
                    });
                    for (int i = 1; i < split.Length; i++) {
                        var cubes = split[i].Split(',');
                        foreach (var cube in cubes) {
                            var col = cube_rx.Matches(cube);
                            var color = col[0].Groups[2].Value;
                            var amount = Int32.Parse(col[0].Groups[1].Value);
                            
                            game[index][color] = int.Max(game[index][color], amount);
                            
                        }
                    }
                }
            }


            foreach (var subgame in game) {
                var subblue = subgame.Value["blue"];
                var subred = subgame.Value["red"];
                var subgreen = subgame.Value["green"];
                result += subblue * subred * subgreen;

            }

            TimeSpan elapsedTime = DateTime.Now - startTime;
            Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
            return result;
        }
    }
}
