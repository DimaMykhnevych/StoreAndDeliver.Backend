﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreAndDeliver.BusinessLayer.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class en {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal en() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("StoreAndDeliver.BusinessLayer.Resources.en", typeof(en).Assembly);
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
        ///   Looks up a localized string similar to Confirm Account.
        /// </summary>
        internal static string ConfirmAccount {
            get {
                return ResourceManager.GetString("ConfirmAccount", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please, confirm your account by clicking the following link..
        /// </summary>
        internal static string EmailMainContent {
            get {
                return ResourceManager.GetString("EmailMainContent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Account Confirmation.
        /// </summary>
        internal static string EmailSubject {
            get {
                return ResourceManager.GetString("EmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hello.
        /// </summary>
        internal static string Hello {
            get {
                return ResourceManager.GetString("Hello", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thank You.
        /// </summary>
        internal static string ThankYou {
            get {
                return ResourceManager.GetString("ThankYou", resourceCulture);
            }
        }
    }
}
