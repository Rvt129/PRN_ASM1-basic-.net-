using System;
using System.IO;
using Newtonsoft.Json;

namespace Asm1.Presentation
{
    public class Configuration
    {
        public AdminAccount AdminAccount { get; set; }
       
    }

    public class AdminAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class Connection
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public static class ConfigLoader
    {
        public static Configuration LoadConfig(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
    }
}
