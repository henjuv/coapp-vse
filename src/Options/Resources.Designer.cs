﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoGet.Options {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CoGet.Options.Resources", typeof(Resources).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select a Package Source Folder.
        /// </summary>
        public static string BrowseFolderDialogDescription {
            get {
                return ResourceManager.GetString("BrowseFolderDialogDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select.
        /// </summary>
        public static string BrowseFolderDialogSelectButton {
            get {
                return ResourceManager.GetString("BrowseFolderDialogSelectButton", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All packages have been cleared from the cache..
        /// </summary>
        public static string ShowInfo_ClearPackageCache {
            get {
                return ResourceManager.GetString("ShowInfo_ClearPackageCache", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All items have been cleared from the recent packages list..
        /// </summary>
        public static string ShowInfo_ClearRecentPackages {
            get {
                return ResourceManager.GetString("ShowInfo_ClearRecentPackages", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The source specified is invalid. Please provide a valid source..
        /// </summary>
        public static string ShowWarning_InvalidSource {
            get {
                return ResourceManager.GetString("ShowWarning_InvalidSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name and source specified cannot be empty. Please provide a valid name and source..
        /// </summary>
        public static string ShowWarning_NameAndSourceRequired {
            get {
                return ResourceManager.GetString("ShowWarning_NameAndSourceRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name specified cannot be empty. Please provide a valid name..
        /// </summary>
        public static string ShowWarning_NameRequired {
            get {
                return ResourceManager.GetString("ShowWarning_NameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified source&apos;s name is an official package source name, but there is already an official package source. Please provide a different name..
        /// </summary>
        public static string ShowWarning_OfficialSourceName {
            get {
                return ResourceManager.GetString("ShowWarning_OfficialSourceName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The source specified cannot be empty. Please provide a valid source..
        /// </summary>
        public static string ShowWarning_SourceRequried {
            get {
                return ResourceManager.GetString("ShowWarning_SourceRequried", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CoApp Package Manager Options.
        /// </summary>
        public static string ShowWarning_Title {
            get {
                return ResourceManager.GetString("ShowWarning_Title", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The name specified has already been added to the list of available package sources. Please provide a unique name..
        /// </summary>
        public static string ShowWarning_UniqueName {
            get {
                return ResourceManager.GetString("ShowWarning_UniqueName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The source specified has already been added to the list of available package sources. Please provide a unique source..
        /// </summary>
        public static string ShowWarning_UniqueSource {
            get {
                return ResourceManager.GetString("ShowWarning_UniqueSource", resourceCulture);
            }
        }
    }
}
