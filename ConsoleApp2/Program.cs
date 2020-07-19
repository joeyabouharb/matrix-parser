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
    using System.Runtime.CompilerServices;

    partial class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var commandList = new CommandArgumentList();
                commandList.Add("Rows", ParseCaster.AsInt);
                commandList.Add("Columns", ParseCaster.AsInt);
                commandList.Add("Data", ParseCaster.AsInt, args.Length - 1);

                commandList.Add("Operator", ParseCaster.AsString);
                commandList.TryParse(args, out string err);
                if (!string.IsNullOrEmpty(err))
                {
                    Console.WriteLine(err);
                    return;
                }
                int? row = commandList["Rows"].Value as int?;
                int? column = commandList["Columns"].Value as int?;
                var matrix = Matrix.TryCreate(row.Value, column.Value, commandList.Arguments, out err);
                if (!string.IsNullOrEmpty(err))
                {
                    Console.WriteLine(err);
                    return;
                }
                var @operator = commandList["Operator"].Value as string;
                var result = matrix.TryTransform(@operator, out err);
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
