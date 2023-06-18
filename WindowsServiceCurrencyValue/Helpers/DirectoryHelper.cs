using System;
using System.IO;

namespace WindowsServiceCurrencyValue.Helpers
{
    public class DirectoryHelper
    {
        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
