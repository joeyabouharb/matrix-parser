/**
 * Was Bored so I wrote an app that parses data from the command line
 * 
 * It also follows a challenge to store and transform Matrices passed into the command line
 * arguments
 * consoleapp.exe <Row/> <Column/> <Data> <Command/>
 * 
 */
namespace ConsoleApp2
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    partial class Program
    {
        public class Matrix
        {
            public IEnumerable<IEnumerable<int?>> Vectors { get; private set; }
            public int RowCount { get; private set; }
            public int ColCount { get; private set; }

            public Matrix(IEnumerable<IEnumerable<int?>> vectors, int rowCount, int colCount)
            {
                Vectors = vectors;
                RowCount = rowCount;
                ColCount = colCount;
            }
            public static Matrix TryCreate(int row, int column, IEnumerable<Argument> arguments, out string err)
            {
                err = string.Empty;
                var data = arguments
                    .Where((arg) => arg.Name.Contains("Data", StringComparison.InvariantCultureIgnoreCase))
                    .Select(arg => arg.Value as int?)
                    .ToArray<int?>();
                int len = row * column;
                if (data.Any((v) => v == null) || (data.Length != len))
                {
                    err = $"Data input was not valid Data Parameters requires {len} values";
                    return null;
                }
                var results = Enumerable.Range(0, row)
                    .Select((current) => new ArraySegment<int?>(data, current * column, column).AsEnumerable());

                return new Matrix(results, row, column);
            }
            public Matrix TryTransform(string @operator, out string err)
            {
                err = string.Empty;
                var dict = new TransformFunctionCollection();

                var result = dict[@operator]?.Invoke(this) as Matrix;
                if (result == null)
                {
                    err = "Invalid operator supplied";
                }
                return result;
            }
        }
    }
}
