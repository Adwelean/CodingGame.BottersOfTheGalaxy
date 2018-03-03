using System;

namespace SourceCombiner
{
    class Program
    {
        static void Main(string[] args)
        {
            string projectFilePath = @"C:\Users\quent\source\repos\CodingGame.BottersOfTheGalaxy\Core";
            string outputFilePath = @"C:\Users\quent\source\repos\CodingGame.BottersOfTheGalaxy\Build\CompiledClass.cs";

            SourceCombiner.Build(projectFilePath, outputFilePath);
        }
    }
}
