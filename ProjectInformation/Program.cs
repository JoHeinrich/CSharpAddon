using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectInformation
{
    class Program
    {
        static void Main(string[] args)
        {
            var info = new VoiceControl.ProjectInformation(@"C:\Users\laise\Documents\EasyVoiceCode\Addons\CSharpAddon\code\CSharpAddon\CSharpAddon.sln");
            Directory.CreateDirectory("C:/Users/laise/Desktop/info");
            File.WriteAllText("C:/Users/laise/Desktop/info/members.txt", string.Join("\n", info.UsedMembers));
            File.WriteAllText("C:/Users/laise/Desktop/info/types.txt", string.Join("\n", info.UsedTypes));
            File.WriteAllText("C:/Users/laise/Desktop/info/files.txt", string.Join("\n", info.Files));
            File.WriteAllText("C:/Users/laise/Desktop/info/fileNames.txt", string.Join("\n", info.Files.Select(x=>Path.GetFileNameWithoutExtension( x))));
            File.WriteAllText("C:/Users/laise/Desktop/info/functions.txt", string.Join("\n", info.Functions));
            File.WriteAllText("C:/Users/laise/Desktop/info/namespaces.txt", string.Join("\n", info.UsedNamespaces)); 
            File.WriteAllText("C:/Users/laise/Desktop/info/generics.txt", string.Join("\n", info.UsedGenerics)); 
        }
    }
}
