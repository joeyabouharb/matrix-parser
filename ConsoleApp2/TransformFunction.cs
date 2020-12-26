
namespace ConsoleApp2
{
    using System;
    using System.Dynamic;

    public class TransformFunction
    {
        public string @Operator { get; set; }
        private delegate Matrix action(Matrix matrix, int? value);
        private action @Delegate { get; set; }
        public Matrix Invoke(Matrix matrix, int? value)
        {
            return Delegate.Invoke(matrix, value);
        }

        public static readonly TransformFunction Increase = new TransformFunction
        {
            Operator = "I",
            Delegate = FuncUtilities.Increase,
        };
        public static readonly TransformFunction Decrease = new TransformFunction
        {
            Operator = "D",
            Delegate = FuncUtilities.Decrease,
        };
        public static readonly TransformFunction Transpose = new TransformFunction
        {
            Operator = "T",
            Delegate = FuncUtilities.Transpose,
        };
        public static readonly TransformFunction Multiply = new TransformFunction
        {
            Operator = "M",
            Delegate = FuncUtilities.Multiply,
        };
    }
}

