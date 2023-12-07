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
            int sol1 = Case1(input);
            int sol2 = Case2(input);

            Console.WriteLine($"case 1: {sol1}");
            Console.WriteLine($"case 2: {sol2}");
        }

        protected virtual int Case1(List<string> lines) => 0;

        protected virtual int Case2(List<string> lines) => 0;
    }
}
