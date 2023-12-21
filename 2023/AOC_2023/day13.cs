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

                result += FindMirror(map) * 100;
                result += FindMirror(map_flipped);

                return result;
            }
            public long CalculatePoints2() {
                FlipMap();
                long result = 0;

                result += FindMirrorWithSmudge(map) * 100;
                result += FindMirrorWithSmudge(map_flipped);

                return result;
            }

            private long FindMirror(List<string> current_map) {
                long result = 0;
                List<int> mirror_starts = new List<int>();

                for (var i = 1; i < current_map.Count; i++) {
                    var prev_line = current_map[i - 1];
                    var current_line = current_map[i];
                    if (prev_line == current_line) {
                        mirror_starts.Add(i - 1);
                    }
                }

                foreach (var start in mirror_starts) {
                    var end = start + 1;
                    for (var i = start; i >= 0; i--) {
                        if (current_map[i] == current_map[end]) {
                            if (i - 1 == -1 || end + 1 == current_map.Count) {
                                result = start + 1;
                                break;
                            }
                            end++;
                        } else {
                            break;
                        }
                    }
                    if (result != 0) {
                        break;
                    }
                }

                return result;
            }
            
            private long FindMirrorWithSmudge(List<string> current_map) {
                long result = 0;
                List<int> mirror_starts = new List<int>();

                for (var i = 1; i < current_map.Count; i++) {
                    var prev_line = current_map[i - 1];
                    var current_line = current_map[i];
                    var comp = SmudgeDiff(prev_line, current_line);
                    if (comp.Item1) {
                        mirror_starts.Add(i - 1);
                    }
                }

                foreach (var start in mirror_starts) {
                    var end = start + 1;
                    var smudge_count = 0;
                    for (var i = start; i >= 0; i--) {
                        var comp = SmudgeDiff(current_map[i], current_map[end]);
                        smudge_count += comp.Item2;
                        if (comp.Item1) {
                            if (i - 1 == -1 || end + 1 == current_map.Count) {
                                if (smudge_count == 1) {
                                    result = start + 1;
                                }
                                break;
                            }
                            end++;
                        } else {
                            break;
                        }
                    }
                    if (result != 0) {
                        break;
                    }
                }

                return result;
            }

            private (bool, int) SmudgeDiff(string a, string b) {
                int smudge_count = 0;
                for (int i = 0; i < a.Length; i++) {
                    if (a[i].CompareTo(b[i]) != 0) {
                        smudge_count++;
                    }
                }

                return (smudge_count <= 1, smudge_count);
            }
        }
        
        

        protected override Solution Case1(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;

            // Your case 1 logic here
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

            return new Solution(result.ToString(), DateTime.Now - start_time);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime start_time = DateTime.Now;
            long result = 0;

            // Your case 2 logic here
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

            maps.ForEach(m => result += m.CalculatePoints2());

            return new Solution(result.ToString(), DateTime.Now - start_time);
        }
    }
}
