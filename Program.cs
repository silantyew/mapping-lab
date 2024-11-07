using System;
using System.Collections.Generic;
using System.IO;

namespace IntegrationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Необходимо указать два CSV-файла: файл исходных данных и файл мапинга.");
                return;
            }

            string inputFile = args[0];
            string mappingFile = args[1];
            string outputFile = "output.csv";

            var mapping = LoadMapping(mappingFile);
            ProcessFile(inputFile, mapping, outputFile);
        }

        static Dictionary<string, string> LoadMapping(string mappingFile)
        {
            var mapping = new Dictionary<string, string>();
            if (File.Exists(mappingFile))
            {
                foreach (var line in File.ReadLines(mappingFile))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        mapping[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }
            return mapping;
        }

        static void ProcessFile(string inputFile, Dictionary<string, string> mapping, string outputFile)
        {
            using (var output = new StreamWriter(outputFile))
            {
                using (var reader = new StreamReader(inputFile))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            var parts = line.Split(',');
                            var mappedLine = MapData(parts, mapping);
                            output.WriteLine(mappedLine);
                        }
                    }
                }
            }
        }

        static string MapData(string[] data, Dictionary<string, string> mapping)
        {
            var mappedData = new List<string>();
            foreach (var key in mapping.Keys)
            {
                var value = data[Array.IndexOf(data, key)];
                mappedData.Add(value); // Может потребоваться более сложная логика маппинга
            }
            return string.Join(",", mappedData);
        }
    }
}
