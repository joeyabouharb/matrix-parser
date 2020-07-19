
namespace ConsoleApp2
{
    using System;
    using System.Linq;

    partial class Program
    {
        private static class FuncUtilities
        {
            public static Func<Matrix, Matrix> Increase = (Matrix matrix) =>
            {
                var newMatrix = matrix.Vectors.Select(vector => vector.Select(value =>
                {
                    return value + 1;
                }));
                return new Matrix(newMatrix, matrix.RowCount, matrix.ColCount);
            };
            public static Func<Matrix, Matrix> Decrease = (matrix) =>
            {
                var newMatrix = matrix.Vectors.Select(vector => vector.Select(value =>
                {
                    return value - 1;
                }));
                return new Matrix(newMatrix, matrix.RowCount, matrix.ColCount);
            };
            public static Func<Matrix, Matrix> Transpose = (matrix) =>
            {
                var newMatrix = Enumerable.Range(0, matrix.ColCount)
                .Select(currentRow =>
                {
                    return matrix.Vectors.Select(vector => vector.ElementAt(currentRow));
                });
                return new Matrix(newMatrix, matrix.ColCount, matrix.RowCount);
            };
        }
    }
}
