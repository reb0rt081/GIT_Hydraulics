using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScienceAndMaths.MachineLearning.Tools
{
    public static class CsvFileConverter
    {
        /// <summary>
        /// Converts an input CSV file into a new CSV file with only certain columns
        /// </summary>
        /// <param name="inputFile">@"C:\test.csv"</param>
        /// <param name="outputFile"></param>
        /// <param name="columnsToCopy"></param>
        /// <param name="separator"></param>
        public static void ConvertCsvToCsv(string inputFile, string outputFile, List<int> columnsToCopy, char separator = ',')
        {
            Dictionary<int, List<string>> entryTable = new Dictionary<int, List<string>>();
            foreach (int columnId in columnsToCopy)
            {
                entryTable.Add(columnId, new List<string>());
            }

            using (var reader = new StreamReader(inputFile))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(separator);

                    foreach (KeyValuePair<int, List<string>> entry in entryTable)
                    {
                        entry.Value.Add(values[entry.Key]);
                    }
                }
            }

            var csv = new StringBuilder();
            int length = entryTable[0].Count;
            for (int i = 0; i < length; i++)
            {
                string newLine = "";
                bool firstInRowElement = true;
                foreach (KeyValuePair<int, List<string>> entry in entryTable)
                {
                    
                    newLine += firstInRowElement ? entry.Value[i] : $",{entry.Value[i]}";
                    firstInRowElement = false;
                }
                csv.AppendLine(newLine);
            }

            File.WriteAllText(outputFile, csv.ToString());
        }

        public static void ConvertToCsv(string outputFile, string[][] data)
        {
            var csv = new StringBuilder();
            foreach (string[] row in data)
            {
                string newLine = "";
                bool firstInRowElement = true;
                foreach (string cell in row)
                {
                    newLine += firstInRowElement ? cell : $",{cell}";
                    firstInRowElement = false;
                }
                csv.AppendLine(newLine);
            }

            File.WriteAllText(outputFile, csv.ToString());
        }
    }
}
