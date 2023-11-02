
using System.Collections.Generic;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using ScienceAndMaths.MachineLearning.Tools;
using static Tensorflow.tensorflow;

namespace ScienceAndMaths.MachineLearning.Test.Tools
{
    [TestClass]
    public class CsvFileConverterTest
    {
        [TestMethod]
        public void CreateCsvFile()
        {
            string[][] data = new string[2][];
            for (int i = 0; i < 2; ++i)
                data[i] = new string[3];

            data[0][0] = "column 1";
            data[0][1] = "column 2";
            data[0][2] = "column 3";

            data[1][0] = "data row 1 column 1";
            data[1][1] = "data row 1 column 2";
            data[1][2] = "data row 1 column 3";

            CsvFileConverter.ConvertToCsv(@"test.csv", data);
            Assert.IsTrue(File.Exists("test.csv"));

            CsvFileConverter.ConvertCsvToCsv("test.csv", "test2.csv", new List<int> {0, 2});
            Assert.IsTrue(File.Exists("test2.csv"));

            using (var reader = new StreamReader("test2.csv"))
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                
                Assert.AreEqual(2, values.Length);
            }
        }

        [TestMethod, Ignore]
        public void Test()
        {
            CsvFileConverter.ConvertCsvToCsv(@"D:\CURSOS\ABDATACHALLENGE\BBDD y DataSet ejemplo\Datos demográficos INE\C2011_ccaa09_Indicadores.csv", @"D:\CURSOS\ABDATACHALLENGE\BBDD y DataSet ejemplo\Datos demográficos INE\cat_Indicadores.csv", new List<int> { 0, 1, 2, 3, 4, 5 });
            Assert.IsTrue(File.Exists("test2.csv"));
        }
    }
}
