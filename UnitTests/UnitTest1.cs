using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;


namespace IntegrationApp.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void LoadMapping_ValidFile_ReturnsMapping()
        {
            var content = "key1,value1\nkey2,value2";
            var filePath = "mapping.csv";

            File.WriteAllText(filePath, content);
            var mapping = Program.LoadMapping(filePath);

            Assert.Equal("value1", mapping["key1"]);
            Assert.Equal("value2", mapping["key2"]);
            File.Delete(filePath);
        }

        [Fact]
        public void LoadMapping_NonExistentFile_ReturnsEmptyMapping()
        {
            var mapping = Program.LoadMapping("nonexistent.csv");
            Assert.Empty(mapping);
        }

        [Fact]
        public void MapData_ValidDataAndMapping_ReturnsMappedData()
        {
            var data = new string[] { "1", "2", "3", "4" };
            var mapping = new Dictionary<string, string>
            {
                { "1", "key1" },
                { "2", "key2" }
            };

            var result = Program.MapData(data, mapping);

            Assert.Equal("1,2", result);
        }
    }
}
