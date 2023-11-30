
    using System.Text.RegularExpressions;

    namespace AOC_2023 {
        class Temp {

            public static void Solver() {
                string fileName = "tmp";
                string filePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                filePath = Regex.Replace(filePath, "bin.*", "");


                List<string> testInput = HelperFunctions.ExtractList($"{filePath}inputs/tinput_{fileName}");
                List<string> realInput = HelperFunctions.ExtractList($"{filePath}inputs/input_{fileName}");
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

            static int Case1(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 1 logic here

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }

            static int Case2(List<string> lines) {
                DateTime startTime = DateTime.Now;
                int result = 0;

                // Your case 2 logic here

                TimeSpan elapsedTime = DateTime.Now - startTime;
                Console.WriteLine($"Time needed: {elapsedTime.TotalSeconds} seconds");
                return result;
            }
        }
    }

