using System;
using System.Reflection;

namespace Squirrel.OctopusDelivery.App.Extensions
{
    static class AssemblyExtensions
    {
        public static Version GetAssemblyFileVersion(this Assembly assembly)
        {
            return (Version.Parse(
                    ((AssemblyFileVersionAttribute)
                        Attribute.GetCustomAttribute(assembly,
                            typeof(AssemblyFileVersionAttribute), true)).Version));
        }
    }
}
