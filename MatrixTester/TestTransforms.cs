using System.Collections;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using ConsoleApp2;

namespace MatrixTester
{
    public class TestTransforms
    {
        [Theory]
        [MemberData(nameof(ArithmeticDataset))]
        public void TestArithmeticTransformationToMatrix(string @operator, int row, int col, List<List<int?>> testList, List<List<int?>> answer, int? byValue = null)
        {
            var expectedMatrixResult = new Matrix(answer);
            var matrix = new Matrix(testList, row, col);
            var actualMatrixResult = matrix.TryTransform(@operator, byValue, out string err);
            Assert.Equal(expectedMatrixResult.Vectors, actualMatrixResult.Vectors);
            Assert.Equal(expectedMatrixResult.ColCount, actualMatrixResult.ColCount);
            Assert.Equal(expectedMatrixResult.RowCount, expectedMatrixResult.RowCount);
            Assert.Equal(err, string.Empty);
        }

        [Fact]
        public void TestStringParserToInt()
        {
            var parser = new ArgumentList(new string[] { "2" });
            parser.Add("Number", ParseCaster.AsInt, out string err);
            var value = ParseCaster.AsInt.Invoke("2") as int?;
            int.TryParse("2", out int actual);
            Assert.Equal(parser["Number"].Value as int?, actual);
            //var test = parser[1].Value;
        }
        
        public static IEnumerable<object[]> ArithmeticDataset =>
        new List<object[]>
        {
            new object[] { "I", 1, 5, new List<List<int?>> {
                new List<int?> { 1, 2, 3, 4, 5 } },
                new List<List<int?>> { new List<int?> { 2, 3, 4, 5, 6 } }, 1
            },
                        new object[] { "D", 2, 3, 
                new List<List<int?>> { new List<int?> { 2, 3, 4}, new List<int?> {5, 5, 6 } },
                new List<List<int?>> { new List<int?> { 1, 2, 3}, new List<int?> {4, 4, 5 } }, 1
            },
            new object[] { "I", 1, 5, new List<List<int?>> { new List<int?> { 22, 11, 33, -2, -5 } }, new List<List<int?>> { new List<int?> { 24, 13, 35, 0, -3 } }, 2 },
        };
    }
}