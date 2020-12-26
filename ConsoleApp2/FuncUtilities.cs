
namespace ConsoleApp2
{
    using System;
    using System.Linq;
    public static class FuncUtilities
    {
        public static Matrix Increase(Matrix matrix, int? val = 1)
        {
            var newMatrix = matrix.Vectors.Select(vector => vector.Select(value =>
            {
                return value + (val ?? 1);
            }));
            return new Matrix(newMatrix, matrix.RowCount, matrix.ColCount);
        }
        public static Matrix Decrease(Matrix matrix, int? val = 1)
        {

            var newMatrix = matrix.Vectors.Select(vector => vector.Select(value =>
            {
                return value - (val ?? 1);
            }));
            return new Matrix(newMatrix, matrix.RowCount, matrix.ColCount);
        }
        public static Matrix Transpose(Matrix matrix, int? _)
        {
            var newMatrix = Enumerable.Range(0, matrix.ColCount)
            .Select(currentRow =>
            {
                return matrix.Vectors.Select(vector => vector.ElementAt(currentRow));
            });
            return new Matrix(newMatrix, matrix.ColCount, matrix.RowCount);
        }
        public static Matrix Multiply(Matrix matrix, int? val = 1)
        {
            var newMatrix = matrix.Vectors.Select(vector => vector.Select(value => value * (val ?? 1)));
            return new Matrix(newMatrix, matrix.RowCount, matrix.ColCount);
        }
    }
}
