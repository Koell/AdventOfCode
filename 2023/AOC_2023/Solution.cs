namespace AOC_2023 {
    public class Solution {
        public string Result { get; set; }
        public TimeSpan TimeStamp { get; set; }

        public Solution() {
            Result = "Nothing was solved";
            TimeStamp = TimeSpan.Zero;
        }

        public Solution(string result, TimeSpan timeStamp) {
            Result = result;
            TimeStamp = timeStamp;
        }

        public override string ToString() {
            return $"Time needed: {TimeStamp.TotalSeconds} seconds\nResult: {Result}";
        }
    }
}
