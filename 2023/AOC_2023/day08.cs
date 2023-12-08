using System.Text.RegularExpressions;

namespace AOC_2023 {
    public partial class Day08: Day {

        protected override Solution Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            int result = 0;

            // Your case 1 logic here
            string directions = "";
            Regex rx_string = StringRegex();
            string initialKey = "";
            Dictionary<string, List<string>> nodes = new Dictionary<string, List<string>>();


            foreach ((string line, int idx) in lines.Select((line, idx) => (line, idx))) {
                if (line != "") {
                    if (idx == 0) {
                        directions = line.Trim();
                    } else {
                        MatchCollection matches = rx_string.Matches(line);
                        if (matches.Count == 3) {
                            nodes.Add(matches[0].Value, new List<string>{matches[1].Value, matches[2].Value});
                            
                            if (matches[0].Value[2] == 'A') {
                                initialKey = matches[0].Value;
                            }
                        }

                    }
                }
            }

            List<string> next_node = nodes[initialKey];
            bool searching = true;
            while (searching) {
                for (int i = 0; i < directions.Length; i++) {
                    result++;
                    var next_key = "";
                    if (directions[i] == 'L') {
                        next_key = next_node[0];
                    } else {
                        next_key = next_node[1];
                    }
                    if (next_key[2] == 'Z') {
                        searching = false;
                        break;
                    }
                    next_node = nodes[next_key];
                }
            }
            
            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            long result = 0;

            // Your case 2 logic here
            
            string directions = "";
            Regex rx_string = StringRegex();
            var nodes = new Dictionary<string, List<string>>();
            var next_keys = new List<string>();


            foreach ((string line, int idx) in lines.Select((line, idx) => (line, idx))) {
                if (line != "") {
                    if (idx == 0) {
                        directions = line.Trim();
                    } else {
                        MatchCollection matches = rx_string.Matches(line);
                        if (matches.Count == 3) {
                            nodes.Add(matches[0].Value, new List<string>{matches[1].Value, matches[2].Value});
                            if (matches[0].Value[2] == 'A') {
                                next_keys.Add(matches[0].Value);
                            }
                        }

                    }
                }
            }

            
            var next_nodes = new List<List<string>>();
            var found = new List<long>();
            foreach (var key in next_keys) {
                next_nodes.Add(nodes[key]);
                found.Add(0);
            }
            int steps = 0;
            bool searching = true;
            while (searching) {
                for (int i = 0; i < directions.Length; i++) {
                    steps++;
                    var choose = 0;
                    if (directions[i] == 'L') {
                        choose = 0;
                    } else {
                        choose = 1;
                    }
                    List<List<string>> tmp_n_nodes = new List<List<string>>();
                    tmp_n_nodes.AddRange(next_nodes);
                    
                    foreach (var (node, idx) in tmp_n_nodes.Select((node, idx) => (node, idx))) {
                        var key = node[choose];
                        next_nodes[idx] = nodes[key];
                        if (found[idx] == 0 && key[2] == 'Z') {
                            found[idx] = steps;
                        }
                    }
                    if (!found.Contains(0)) {
                        break;
                    }
                }
                if (!found.Contains(0)) {
                    searching = false;
                }
            }
            result = lcm_multi(found.ToArray());
            
            return new Solution(result.ToString(), DateTime.Now - startTime);
        }
        
        static long gcd(long n1, long n2)
        {
            if (n2 == 0)
            {
                return n1;
            }
            else
            {
                return gcd(n2, n1 % n2);
            }
        }

        public static long lcm_multi(long[] numbers)
        {
            return numbers.Aggregate((S, val) => S * val / gcd(S, val));
        }

        [GeneratedRegex("(\\w+)")]
        private static partial Regex StringRegex();
    }
    
    
}
