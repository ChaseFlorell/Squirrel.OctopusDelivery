﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Squirrel.OctopusDelivery.App.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Squirrel.OctopusDelivery.App.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You&apos;re in the {0} environment..
        /// </summary>
        internal static string CurrentEnvironment {
            get {
                return ResourceManager.GetString("CurrentEnvironment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You&apos;re on version {0}..
        /// </summary>
        internal static string CurrentVersion {
            get {
                return ResourceManager.GetString("CurrentVersion", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Update Found, checking again in {0} seconds..
        /// </summary>
        internal static string NoUpdateFound {
            get {
                return ResourceManager.GetString("NoUpdateFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version {0} has been applied, press [X] to relaunch, or any other key to continue..
        /// </summary>
        internal static string Relaunch {
            get {
                return ResourceManager.GetString("Relaunch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Version {0} is available. Press [U] to update..
        /// </summary>
        internal static string UpdateAvailable {
            get {
                return ResourceManager.GetString("UpdateAvailable", resourceCulture);
            }
        }
    }
}