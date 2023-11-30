using System.Collections.Generic;
using System.IO;

namespace AOC_2023 {
    public class HelperFunctions {

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
    }
}
