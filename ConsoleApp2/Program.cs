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
    using System;
    using System.Dynamic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string err;
                var commandList = new ArgumentList(args);
                if (!commandList.Add("Rows", ParseCaster.AsInt, out err))
                {
                    Console.WriteLine(err);
                    return;
                }
                if (!commandList.Add("Columns", ParseCaster.AsInt, out err))
                {
                    Console.WriteLine(err);
                    return;
                }
                int? row = commandList["Rows"]?.Value as int? ?? 0;
                int? column = commandList["Columns"]?.Value as int? ?? 0;
                if (!commandList.Add("Data", ParseCaster.AsInt, out err, row.Value * column.Value))
                {
                    Console.WriteLine(err);
                    return;
                }

                if (!commandList.Add("Operator", ParseCaster.AsString, out err))
                {
                    Console.WriteLine(err);
                    return;
                }
                //commandList.Add("Value", ParseCaster.AsInt, 0, true);

                int?[] data = commandList.Arguments
                    .Where((arg) => arg.Name.Contains("Data", StringComparison.InvariantCultureIgnoreCase))
                    .Select(arg => arg.Value as int?)
                    .ToArray<int?>();
                var matrix = Matrix.TryCreate(row.Value, column.Value, data, out err);
                if (!string.IsNullOrEmpty(err))
                {
                    Console.WriteLine(err);
                    return;
                }
                var operatorPart = commandList["Operator"]?.Value as string;
                if (operatorPart == null)
                {
                    return;
                }
                var operatorSplit = operatorPart.Split(',');
                var @operator = operatorSplit.FirstOrDefault() ?? string.Empty;
                var valuePart = operatorSplit.LastOrDefault();
                if(!int.TryParse(valuePart, out int value))
                {
                    value = 1;
                }
                
                var result = matrix.TryTransform(@operator, value, out err);
                if (!string.IsNullOrEmpty(err))
                {
                    Console.WriteLine(err);
                    return;
                }
                foreach (var item in result.Vectors)
                {
                    Console.WriteLine(string.Join(" ", item));
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
