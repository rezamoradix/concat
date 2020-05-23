using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace concat
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentNullException();

            List<string> argsList = args.ToList();
            List<string> inputFiles = new List<string>();
            var outputFile = argsList.Last();

            if (File.Exists(outputFile)) File.Delete(outputFile);

            for (int i = 0; i < args.Length - 1; i++)
                if (args[i].IndexOf("*") != -1)
                    inputFiles.AddRange(Directory.GetFiles(new FileInfo(args[i]).DirectoryName, args[i]));
                else
                    inputFiles.Add(args[i]);


            foreach (var f in inputFiles)
                if (!File.Exists(f))
                    throw new FileNotFoundException(f);


            using (Stream outStream = File.Open(outputFile, FileMode.Create, FileAccess.Write, FileShare.Read))
                foreach (var inputFile in inputFiles)
                    using (Stream inStream = File.OpenRead(inputFile))
                        inStream.CopyTo(outStream);
        }
    }
}
