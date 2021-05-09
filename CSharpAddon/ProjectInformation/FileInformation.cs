using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VoiceControl
{
    public class FileInformation : IFileInformation
    {
        CompilationUnitSyntax root;
        Dictionary<SyntaxKind, HashSet<string>> pairs = new Dictionary<SyntaxKind, HashSet<string>>();
        public FileInformation(string path)
        {
            var programText = File.ReadAllText(path);
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            root = tree.GetCompilationUnitRoot();
            UsedNamespaces = root.Usings.Select(x=>x.Name.ToString());
            //Console.WriteLine($"The tree is a {root.Kind()} node.");
            //Console.WriteLine($"The tree has {root.Members.Count} elements in it.");
            //Console.WriteLine($"The tree has {root.Usings.Count} using statements. They are:");
            //foreach (UsingDirectiveSyntax element in root.Usings)
            //    Console.WriteLine($"\t{element.Name}");
            
            foreach (var item in root.DescendantTokens().Where(m => m.Kind() == SyntaxKind.IdentifierToken))
            {
                SyntaxKind type = item.Parent.Kind();
                if (type == SyntaxKind.IdentifierName) type = item.Parent.Parent.Kind();
                HashSet<string> list;
                if (!pairs.TryGetValue(type, out list))
                {
                    list = new HashSet<string>();
                    pairs.Add(type, list);
                }
                list.Add(item.Text);
            }
            
            List<Type> types = root.DescendantNodes().Where(m => m.Kind() == SyntaxKind.IdentifierName).Select(x => x.Parent.GetType()).ToList();

            UsedMembers = pairs.Select(x => x.Value).SelectMany(x => x);

            UsedAttributes = Consume(SyntaxKind.Attribute);
            DefinedClasses = Consume(SyntaxKind.ClassDeclaration);
            DefinedFunctions = Consume(SyntaxKind.MethodDeclaration, SyntaxKind.ConstructorDeclaration);
            DefinedVariables = Consume(SyntaxKind.VariableDeclarator, SyntaxKind.Parameter, SyntaxKind.PropertyDeclaration, SyntaxKind.ForEachStatement, SyntaxKind.Argument, SyntaxKind.SimpleAssignmentExpression);

            var nodes = root.DescendantNodes().Where(m => m.Kind() == SyntaxKind.Parameter);
            var available = nodes.SelectMany(x =>
            {
                var children = x.ChildNodes();
                if (children == null || children.FirstOrDefault() == null) return new List<SyntaxNode>();
                return new List<SyntaxNode>{ children.First() };
            });
            UsedTypes = available.Select(x => x.ToString().RemoveSpecialCharacters());
            UsedTypes = UsedTypes.Concat(Consume(SyntaxKind.ObjectCreationExpression, SyntaxKind.VariableDeclaration, SyntaxKind.SimpleBaseType, SyntaxKind.ClassDeclaration));
            UsedFunctions = Consume(SyntaxKind.MethodDeclaration, SyntaxKind.ConstructorDeclaration,SyntaxKind.InvocationExpression, SyntaxKind.Parameter, SyntaxKind.PropertyDeclaration, SyntaxKind.ForEachStatement);
            UsedGenerics = Consume(SyntaxKind.GenericName);

           
            

        }
        public void ListMissing()
        {
            IEnumerable<SyntaxKind> unusedKinds = pairs.Select(x => x.Key).Where(x => !usedKinds.Contains(x));
            
            foreach (var pair in pairs)
            {
                var type = pair.Key;
                var list = pair.Value;
                Console.WriteLine(type);
                foreach (var item in list)
                {
                    Console.WriteLine("\t" + item);
                }
            }
        }
        HashSet<SyntaxKind> usedKinds = new HashSet<SyntaxKind>();
        public IEnumerable<string> Consume(params SyntaxKind[] kinds)
        {
            IEnumerable<string> list = new List<string>();
            
            foreach (var kind in kinds)
            {
                if (!pairs.ContainsKey(kind)) continue;
                list = list.Concat(pairs[kind]);
                usedKinds.Add(kind);
            }
            return list;
        }
        public IEnumerable<string> DefinedClasses { get; }
        public IEnumerable<string> DefinedFunctions { get; }
        public IEnumerable<string> DefinedVariables { get; }
        public IEnumerable<string> UsedTypes { get; }
        public IEnumerable<string> UsedFunctions { get; }
        public IEnumerable<string> UsedGenerics{ get; }
        public IEnumerable<string> UsedMembers{ get; }
        public IEnumerable<string> UsedInterfaces{ get; }

        public IEnumerable<string> AllIdentifiers => root.DescendantNodes().Where(m => m.Kind() == SyntaxKind.IdentifierName).Select(x => ((IdentifierNameSyntax)x).Identifier.Text);

        public IEnumerable<string> UsedNamespaces { get; }
        public IEnumerable<string> UsedAttributes { get; }
        
    }
}