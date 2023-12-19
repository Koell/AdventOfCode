using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day13: Day {

        private class Map {
            private List<string> map { get; set; }
            private List<string> map_flipped { get; set; }

            public Map(List<string> map) {
                this.map = map;
            }

            private void FlipMap() {
                map_flipped = new List<string>();
                foreach (var c in map[0]) {
                    map_flipped.Add("");
                }
                foreach (var line in map) {
                    int j = map_flipped.Count - 1;
                    for (int i = line.Length - 1; i >= 0; i--) {
                        map_flipped[j] = line[i] + map_flipped[j];
                        j--;
                    }
                }
            }

            public long CalculatePoints() {
                FlipMap();
                long result = 0;

                result += TopDown(map) * 100;
                result += DownTop(map) * 100;
                result += TopDown(map_flipped);
                result += DownTop(map_flipped);

                return result;
            }

            private long TopDown(List<string> current_map) {
                long result = 0;
                int start = 0;
                int end = current_map.Count - 1;
                bool match = false;
                for (; start < end; start++) {
                    if (current_map[start] == current_map[end]) {
                        match = true;
                        end--;
                    } else if (match) {
                        match = false;
                    }
                }
                if (match) {
                    result = start;
                }

                return result;
            }

            private long DownTop(List<string> current_map) {
                long result = 0;
                int start = 0;
                int end = current_map.Count - 1;
                bool match = false;
                for (; start < end; end--) {
                    if (current_map[start] == current_map[end]) {
                        match = true;
                        start++;
                    } else if (match) {
                        match = false;
                    }
                }
                if (match) {
                    result = start;
                }

                return result;
            }


        }

        protected override Solution Case1(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;
            var new_map = new List<string>();
            var maps = new List<Map>();

            foreach (var line in lines) {
                if (line.Length == 0) {
                    maps.Add(new Map(new_map));
                    new_map = new List<string>();
                } else {
                    new_map.Add(line);
                }
            }
            maps.Add(new Map(new_map));

            maps.ForEach(m => result += m.CalculatePoints());
            // Your case 1 logic here

            return new Solution(result.ToString(), DateTime.Now - start_time);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;

            // Your case 2 logic here

            return new Solution(result.ToString(), DateTime.Now - start_time);
        }
    }
}
