using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;

namespace ConsoleApp2
{
    public abstract class Parser : IEnumerable, IEnumerator
    {

        public int Count { get; protected set; }
        protected IList Elements { get; set; }
        private int Cursor { get; set; }
        public object Current => Elements[Cursor];

        public Parser()
        {
            Elements = new List<Argument>();
        }

        public IEnumerator GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        public bool MoveNext()
        {
            Cursor += 1;
            return Cursor < Elements.Count;
        }
        public void Reset()
        {
            throw new System.NotImplementedException();
        }
        public static Func<string, int?> AsInt = (value) =>
        {
            return int.TryParse(value, out int success)
            ? success as int?
            : null;
        };
        public Argument this[int position]
        {
            get => this.Elements[position] as Argument;
        }
        public Argument this[string name]
        {
            get => (this.Elements as IEnumerable<Argument>).FirstOrDefault((arg) => arg.Name == name);
        }
        public static Func<string, string> AsString = (value) => value;
    }

    public class ParseCaster
    {
        private static Func<string, int?> _asInt = (value) =>
        {
            return int.TryParse(value, out int success)
            ? success as int?
            : null;
        };
        private static Func<string, string> _asStr = (value) => value;
        private Delegate Action { get; set; }

        public object Invoke (params object[] args)
        {
            return Action.DynamicInvoke(args);
        }
        public static ParseCaster AsString = new ParseCaster(_asStr);
        public static ParseCaster AsInt = new ParseCaster(_asInt);
        private ParseCaster(Delegate @delegate)
        {
            Action = @delegate;
        }
    }
}
