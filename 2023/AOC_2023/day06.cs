
    using System.Text.RegularExpressions;

    namespace AOC_2023 {
        public class Day06 {

            public static void Solver() {
                string fileName = "06";
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
                List<List<int>> setup = new List<List<int>>();
                Regex rx_number = new Regex(@"(\d+)");
                foreach (var line in lines) {
                    List<int> tmp = new List<int>();
                    foreach (Match match in rx_number.Matches(line)) {
                        tmp.Add(int.Parse(match.Value));
                    }
                    setup.Add(tmp);
                }

                result = 1;
                if (setup[0].Count == setup[1].Count) {
                    for (int i = 0; i < setup[0].Count(); i++) {
                        var time = setup[0][i];
                        var record = setup[1][i];
                        var lower_bound = 0;
                        var upper_bound = 0;
                        for (int j = 1; j < time; j++) {
                            if ((time - j) * j > record) {
                                lower_bound = j;
                                break;
                            }
                        }

                        for (int k = time - 1; k >= lower_bound; k--) {
                            if ((time - k) * k > record) {
                                upper_bound = k;
                                break;
                            }
                        }
                        result *= (int.Abs(upper_bound - lower_bound) +1);
                    }
                    
                }

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }

            private static int Case2(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 2 logic here

                List<long> setup = new List<long>();
                Regex rx_number = new Regex(@"(\d+)");
                foreach (var line in lines) {
                    Match match = rx_number.Match(line.Replace(" ",""));
                    long number = (long.Parse(match.Value));
                    setup.Add(number);
                }
                
                
                var time = setup[0];
                var record = setup[1];
                long lower_bound = 0;
                long upper_bound = 0;
                for (long j = 1; j < time; j++) {
                    if ((time - j) * j > record) {
                        lower_bound = j;
                        break;
                    }
                }

                for (long k = time - 1; k >= lower_bound; k--) {
                    if ((time - k) * k > record) {
                        upper_bound = k;
                        break;
                    }
                }
                result = (int)(long.Abs(upper_bound - lower_bound) +1);
                    
                
                
                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }
        }
    }

