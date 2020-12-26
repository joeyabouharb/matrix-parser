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
        public bool Optional { get; private set; }
        public virtual void TrySetValue(string value, out string error)
        {
            error = string.Empty;
            try
            {
                Value = Converter?.Invoke(value);
                if (Value == null && !Optional)
                    error = $"{Name} has been passed incorrect value";
            }
            catch
            {
                error = $"{Name} has been passed incorrect value";
            }
        }
        public Argument(string name, int position, ParseCaster converter, bool optional)
        {
            Name = name;
            Position = position;
            Converter = converter;
            Optional = optional;
        }
    }
    public sealed class ArgumentList : Parser
    {
        public IEnumerable<Argument> Arguments { get => Elements as IEnumerable<Argument>; }
        public bool Add(string name, ParseCaster caster, out string err, int range = 1, bool optional = false)
        {
            Argument element;
            err = string.Empty;
            if (Count > _argsList.Length)
            {
                err = $"Not enough positional arguments supplied: missing {name} at position {Count}";
                return false;
            }
            foreach (var index in Enumerable.Range(0, range))
            {
                var subVal = index == 0 ? string.Empty : index.ToString();
                element = new Argument(name + subVal, Count, caster, optional);
                element.TrySetValue(_argsList[Count], out err);
                Elements.Add(element);
                if (!string.IsNullOrEmpty(err))
                    return false;
                Count += 1;
            }
            return true;
        }
        public ArgumentList(string[] args)
        {
            _argsList = args;
        }
        private readonly string[] _argsList;
    }
}
