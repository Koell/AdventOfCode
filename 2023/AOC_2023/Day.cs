using System.Text.RegularExpressions;

namespace AOC_2023 {
    public abstract class Day {

        public void Solver(string fileName) {

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

        private void Solve(List<string> input) {
            Solution sol1 = Case1(input);
            Solution sol2 = Case2(input);

            Console.WriteLine($"case 1: \n{sol1}\n");
            Console.WriteLine($"case 2: \n{sol2}\n");
            Console.WriteLine($"Total time needed: {(sol1.TimeStamp + sol2.TimeStamp).TotalSeconds}\n");
        }

        protected virtual Solution Case1(List<string> lines) => new Solution();

        protected virtual Solution Case2(List<string> lines) => new Solution();
    }
}
