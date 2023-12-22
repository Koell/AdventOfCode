using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day14: Day {

        

        private void Tilt(List<char[]> map, int row, int col, int rowChange, int colChange) {
            int i = row + rowChange;
            int j = col + colChange;

            while (i >= 0 && i < map.Count && j >= 0 && j < map[i].Length && map[i][j] == '.') {
                map[i - rowChange][j - colChange] = '.';
                map[i][j] = 'O';
                i += rowChange;
                j += colChange;
            }
        }

        protected override Solution Case1(List<string> lines) {
            var stopwatch = Stopwatch.StartNew();
            long result = 0;

            var map = lines.Select(line => line.ToCharArray()).ToList();

            for (int i = 1; i < map.Count; i++) {
                for (int j = 0; j < lines[i].Length; j++) {
                    if (lines[i][j] == 'O') {
                        Tilt(map, i, j, -1, 0);
                    }
                }
            }

            for (int i = 0; i < map.Count; i++) {
                for (int j = 0; j < map[i].Length; j++) {
                    if (map[i][j] == 'O') {
                        result += map.Count - i;
                    }
                }
            }

            stopwatch.Stop();
            return new Solution(result.ToString(), stopwatch.Elapsed);
        }

        protected override Solution Case2(List<string> lines) {
            Stopwatch stopwatch = Stopwatch.StartNew();
            long result = 0;

            var map = lines.Select(line => line.ToCharArray()).ToList();

            const int IterationCount = 1000000000;
            for (int count = 0; count < IterationCount; count++) {
                for (int i = 0; i < map.Count; i++) {
                    for (int j = 0; j < lines[i].Length; j++) {
                        if (map[i][j] == 'O') {
                            Tilt(map, i, j, -1, 0);
                        }
                    }
                }

                // Tilt West logic
                for (int i = 0; i < map.Count; i++) {
                    for (int j = 0; j < lines[i].Length; j++) {
                        if (map[i][j] == 'O') {
                            Tilt(map, i, j, 0, -1);
                        }
                    }
                }

                // Tilt South logic
                for (int i = map.Count - 1; i >= 0; i--) {
                    for (int j = 0; j < lines[i].Length; j++) {
                        if (map[i][j] == 'O') {
                            Tilt(map, i, j, 1, 0);
                        }
                    }
                }

                // Tilt East logic
                for (int i = 0; i < map.Count; i++) {
                    for (int j = lines[i].Length - 1; j >= 0; j--) {
                        if (map[i][j] == 'O') {
                            Tilt(map, i, j, 0, 1);
                        }
                    }
                }
            }

            for (int i = 0; i < map.Count; i++) {
                for (int j = 0; j < map[i].Length; j++) {
                    if (map[i][j] == 'O') {
                        result += map.Count - i;
                    }
                }
            }

            stopwatch.Stop();
            return new Solution(result.ToString(), stopwatch.Elapsed);
        }
    }
}
