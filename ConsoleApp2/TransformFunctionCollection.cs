
namespace ConsoleApp2
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using System.Reflection;
    public class TransformFunctionCollection : TransformFunction
    {
        public TransformFunction this[string key]
        {
            get
            {
                funcs.TryGetValue(key, out var item);
                return item;
            }
        }
        private readonly IDictionary<string, TransformFunction> funcs;
        public TransformFunctionCollection()
        {
            var type = typeof(TransformFunction);
            try
            {
                funcs = type.GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Select(bla => bla.GetValue(type) as TransformFunction).ToImmutableDictionary(p => p.Operator, p => p);
            }
            catch { }
        }
    }
}

