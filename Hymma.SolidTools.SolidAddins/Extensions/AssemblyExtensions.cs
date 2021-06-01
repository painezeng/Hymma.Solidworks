﻿using System;
using System.IO;
using System.Reflection;

namespace Hymma.SolidTools.SolidAddins
{
    public static class AssemblyExtensions
    {
        /// <summary>
        /// get the path to the current Assembly
        /// </summary>
        public static string GetAssemblyDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
   
    }
}
