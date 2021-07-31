using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sesak
{
    public class AppHelper
    {
        /// <summary>
        /// Create config path if not exist
        /// </summary>
        public static void InitializeConfigPath()
        {
            string path = GetConfigPath();
            Directory.CreateDirectory(path);
        }


        /// <summary>
        /// Get application config path
        /// </summary>
        /// <returns>Configuration path</returns>
        public static string GetConfigPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(path, "Sesak");

            return path;
        }

        /// <summary>
        /// Get application config path
        /// </summary>
        /// <param name="filename">Combine filename with result</param>
        /// <returns>Configuration path with filename</returns>
        public static string GetConfigPath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            path = System.IO.Path.Combine(path, "Sesak", filename);

            return path;
        }
    }
}
