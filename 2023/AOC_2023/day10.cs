using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Day10: Day {
        private class Pipe {
            // (y,x)
            public (int, int) con_a = (0, 0);
            public (int, int) con_b = (0, 0);
            public long distance = 0;

            public Pipe((int, int) coordinates ,((int, int), (int, int)) shift) {
                con_a = AddTuple(coordinates, shift.Item1);
                con_b = AddTuple(coordinates, shift.Item2);;
            }

            public bool IsConnected((int, int) coordinates) {
                bool result = (con_a == coordinates || con_b == coordinates);
                return result;
            }
            
            public (int,int) Next((int, int) from) {
                (int, int) result = con_a == from ? con_b : con_a;
                return result;
            }

        }

        private (int, int) NORTH = (-1, 0);
        private (int, int) EAST = (0, 1);
        private (int, int) SOUTH = (1, 0);
        private (int, int) WEST = (0, -1);

        private ((int, int), (int, int)) CalculateNeighbours(char symbol) {
            ((int, int), (int, int)) result = ((0, 0), (0, 0)); 
            switch (symbol) {
                case '|':
                    result = (NORTH, SOUTH);
                    break;
                case '-':
                    result = (EAST, WEST);
                    break;
                case 'L':
                    result = (NORTH, EAST);
                    break;
                case 'J':
                    result = (NORTH, WEST);
                    break;
                case '7':
                    result = (SOUTH, WEST);
                    break;
                case 'F':
                    result = (SOUTH, EAST);
                    break;
            }
            
            return result;
        }

        private static (int, int) AddTuple((int, int) a, (int, int) b) {
            return (a.Item1 + b.Item1, a.Item2 + b.Item2);
        }


        protected override Solution Case1(List<string> lines) {
            DateTime startTime = DateTime.Now;
            long result = 0;
            

            // Your case 1 logic here
            (int, int) start = (0, 0);
            var pipes = new List<List<Pipe>>();
            
            foreach ((string line, int y) in lines.Select((value, y) => (value, y))) {
                var pipe_line = new List<Pipe>();
                foreach ((char symbol, int x) in line.Select((value, y) => (value, y))) {
                    if (symbol == 'S') {
                        start = (y, x);
                    }
                    pipe_line.Add(new Pipe((y,x), CalculateNeighbours(symbol)));
                }
                pipes.Add(pipe_line);
            }

            var neighbours = new List<(int, int)>();

            var north = AddTuple(start, NORTH);
            var east = AddTuple(start, EAST);
            var south = AddTuple(start, SOUTH);
            var west = AddTuple(start, WEST);

            if (pipes[north.Item1][north.Item2].IsConnected(start)) {
                pipes[north.Item1][north.Item2].distance = 1;
                neighbours.Add(north);
            }
            if (pipes[east.Item1][east.Item2].IsConnected(start)) {
                pipes[east.Item1][east.Item2].distance = 1;
                neighbours.Add(east);
            }
            if (pipes[south.Item1][south.Item2].IsConnected(start)) {
                pipes[south.Item1][south.Item2].distance = 1;
                neighbours.Add(south);
            }
            if (pipes[west.Item1][west.Item2].IsConnected(start)) {
                pipes[west.Item1][west.Item2].distance = 1;
                neighbours.Add(west);
            }
            
            result += 1;
            var from_a = start;
            var next_a = neighbours[0];
            var from_b = start;
            var next_b = neighbours[1];
            
            bool searching = true;
            while (searching) {
                result += 1;
                var tmp_a = pipes[next_a.Item1][next_a.Item2].Next(from_a);
                from_a = next_a;
                next_a = tmp_a;
                if (pipes[next_a.Item1][next_a.Item2].distance != 0) {
                    result = pipes[next_a.Item1][next_a.Item2].distance;
                    break;
                } 
                
                pipes[next_a.Item1][next_a.Item2].distance = result;
                
                
                var tmp_b = pipes[next_b.Item1][next_b.Item2].Next(from_b);
                from_b = next_b;
                next_b = tmp_b;
                if (pipes[next_b.Item1][next_b.Item2].distance != 0) {
                    result = pipes[next_b.Item1][next_b.Item2].distance;
                    break;
                }
                
                pipes[next_b.Item1][next_b.Item2].distance = result;
            }
            
            return new Solution(result.ToString(), DateTime.Now - startTime);
        }

        protected override Solution Case2(List<string> lines) {
            DateTime startTime = DateTime.Now;
            long result = 0;

            // Your case 2 logic here

            return new Solution(result.ToString(), DateTime.Now - startTime);
        }
    }
}
