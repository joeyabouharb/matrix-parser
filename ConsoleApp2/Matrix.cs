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

        public class Matrix
        {
            private TransformFunctionCollection _transformCollection => new TransformFunctionCollection();
            public IEnumerable<IEnumerable<int?>> Vectors { get; private set; }
            public int RowCount { get; private set; }
            public int ColCount { get; private set; }

            public Matrix(IEnumerable<IEnumerable<int?>> vectors, int rowCount, int colCount)
            {
                Vectors = vectors;
                RowCount = rowCount;
                ColCount = colCount;
            }
            public Matrix (List<List<int?>> vectors)
            {
                Vectors = vectors;
                RowCount = vectors.Count;
                ColCount = vectors.FirstOrDefault()?.Count ?? 0;
            }
            public static Matrix TryCreate(int row, int column, int?[] data, out string err)
            {
                err = string.Empty;
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
            public Matrix TryTransform(string @operator, int? value, out string err)
            {
                err = string.Empty;
                var result = _transformCollection[@operator]?.Invoke(this, value);
                if (result == null)
                {
                    err = "Invalid operator supplied";
                }
                return result;
            }
    }
}
