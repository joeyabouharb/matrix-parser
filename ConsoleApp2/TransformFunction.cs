
namespace ConsoleApp2
{
    using System;

    partial class Program
    {
        public class TransformFunction
        {
            public string @Operator { get; set; }
            private Delegate @Delegate { get; set; }
            public object Invoke(params object[] args)
            {
                return Delegate.DynamicInvoke(args);
            }

            public static TransformFunction Increase = new TransformFunction
            {
                Operator = "I",
                Delegate = FuncUtilities.Increase,
            };
            public static TransformFunction Decrease = new TransformFunction
            {
                Operator = "D",
                Delegate = FuncUtilities.Decrease,
            };
            public static TransformFunction Transpose = new TransformFunction
            {
                Operator = "T",
                Delegate = FuncUtilities.Transpose,
            };
        }
    }
}
