using System;
using System.IO;

namespace WindowsServiceCurrencyValue.Helpers
{
    //Classe responsável por utilitátios referentes a Diretórios
    public class DirectoryHelper
    {
        //Cria o diretório caso não exista
        public static void CreateDirectoryIfNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
