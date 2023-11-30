using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC_2023 {
    public class Helper {

        public static List<string> ExtractList(string fileName) {
            List<string> lines = new List<string>();
            try {
                using (StreamReader reader = new StreamReader(fileName)) {
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        lines.Add(line);
                    }
                }
            }
            catch { }

            return lines;
        }

        public static string GetInputPath() {
            string inputPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            inputPath = Regex.Replace(inputPath, "bin.*", "");
            inputPath = $"{inputPath}inputs{inputPath[^1]}";

            return inputPath;
        }
    }
}
