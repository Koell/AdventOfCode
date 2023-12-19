using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day11: Day {
        private class Galaxy {
            public long x { get; set; }
            public long y { get; set; }

            public Galaxy(long x, long y) {
                this.x = x;
                this.y = y;
            }

            public long Distance(Galaxy other) {
                return Int64.Abs(this.x - other.x) + Int64.Abs(this.y - other.y);
            }

        }

        protected override Solution Case1(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;

            // Your case 1 logic here
            var empty_width = Enumerable.Range(0, lines[0].Length).ToList();
            var empty_height = Enumerable.Range(0, lines.Count).ToList();
            var galaxies = new List<Galaxy>();

            foreach ((string line, int y) in lines.Select((value, y) => (value, y))) {
                foreach ((char symbol, int x) in line.Select((value, y) => (value, y))) {
                    if (symbol == '#') {
                        galaxies.Add(new Galaxy(x, y));
                        empty_width.Remove(x);
                        empty_height.Remove(y);
                    }
                    ;
                }
            }
            empty_width.Reverse();
            empty_height.Reverse();

            foreach (long x in empty_width) {
                foreach (Galaxy galaxy in galaxies) {
                    if (galaxy.x > x) {
                        galaxy.x += 1;
                    }
                }
            }

            foreach (long y in empty_height) {
                foreach (Galaxy galaxy in galaxies) {
                    if (galaxy.y > y) {
                        galaxy.y += 1;
                    }
                }
            }

            foreach ((Galaxy galaxy, var i) in galaxies.Select((value, index) => (value, index))) {
                for (var j = i + 1; j < galaxies.Count; j++) {
                    result += galaxy.Distance(galaxies[j]);
                }
            }


            return new Solution(result.ToString(), DateTime.Now - start_time);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;

            // Your case 2 logic here
            
            var empty_width = Enumerable.Range(0, lines[0].Length).ToList();
            var empty_height = Enumerable.Range(0, lines.Count).ToList();
            var galaxies = new List<Galaxy>();
            long increase = 999999;

            foreach ((string line, int y) in lines.Select((value, y) => (value, y))) {
                foreach ((char symbol, int x) in line.Select((value, y) => (value, y))) {
                    if (symbol == '#') {
                        galaxies.Add(new Galaxy(x, y));
                        empty_width.Remove(x);
                        empty_height.Remove(y);
                    }
                    ;
                }
            }
            empty_width.Reverse();
            empty_height.Reverse();

            foreach (long x in empty_width) {
                foreach (Galaxy galaxy in galaxies) {
                    if (galaxy.x > x) {
                        galaxy.x += increase;
                    }
                }
            }

            foreach (long y in empty_height) {
                foreach (Galaxy galaxy in galaxies) {
                    if (galaxy.y > y) {
                        galaxy.y += increase;
                    }
                }
            }

            foreach ((Galaxy galaxy, var i) in galaxies.Select((value, index) => (value, index))) {
                for (var j = i + 1; j < galaxies.Count; j++) {
                    result += galaxy.Distance(galaxies[j]);
                }
            }

            return new Solution(result.ToString(), DateTime.Now - start_time);
        }
    }
}
