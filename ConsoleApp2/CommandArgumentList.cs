using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp2
{
    
    public class Argument
    {
        public string Name { get; private set; }
        public object Value { get; private set; }
        public int Position { get; private set; }
        private ParseCaster Converter { get; set; }

        public virtual void TrySetValue(string value, out string error)
        {
            error = string.Empty;
            try
            {
                Value = Converter?.Invoke(value);
                if (Value == null)
                    error = $"{Name} has been passed incorrect value";
            }
            catch
            {
                error = $"{Name} has been passed incorrect value";
            }
        }
        public Argument(string name, int position, ParseCaster converter)
        {
            Name = name;
            Position = position;
            Converter = converter;
        }
        public Argument(string name, int position)
        {
            Name = name;
            Position = position;
        }
    }
    public sealed class CommandArgumentList : Parser
    {
        public IEnumerable<Argument> Arguments { get => Elements as IEnumerable<Argument>; }
        public void Add(string name)
        {
            object element;
            element = new Argument(name, Count);
            Elements.Add(element);
            Count += 1;
        }
        public bool Add(string name, ParseCaster caster, int range = 0)
        {
            object element;
            if (range > 0)
                foreach (var _ in Enumerable.Range(0, range - Count))
                {
                    element = new Argument(name + _, Count, caster);
                    Elements.Add(element);
                    Count += 1;
                }
            else
            {
                element = new Argument(name, Count, caster);
                Elements.Add(element);
                Count += 1;
            }
            return true;
        }

        public void TryParse(string[] args, out string error)
        {
            error = string.Empty;
            if (args.Length < Count)
            {
                error = $"Missing Arguments";
                return;
            }
            if (args.Length > Count)
            {
                error = "To Many Arguments";
                return;
            }
            foreach (Argument argument in this)
            {
                argument.TrySetValue(args[argument.Position], out error);
                if (!string.IsNullOrEmpty(error))
                    return;
            }
        }
    }
}
