﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace SemanticsCS
{
    class Program
    {
        static void Main(string[] args)
        {
            var programText =
@"using System;
using System.Collections.Generic;
using System.Text;
 
namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }
    }
}";
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            Console.WriteLine($"The tree is a {root.Kind()} node.");
            Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
            Console.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            foreach (UsingDirectiveSyntax element in root.Usings)
                Console.WriteLine($"\t{element.Name}");
        }


    }
}
