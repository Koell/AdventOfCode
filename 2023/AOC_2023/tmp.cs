
    using System.Text.RegularExpressions;

    namespace AOC_2023 {
        public class Temp {

            public static void Solver() {
                string fileName = "tmp";
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

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }

            private static int Case2(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 2 logic here

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }
        }
    }

