﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mailfa.CleanArch.Core.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Mailfa.CleanArch.Core.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to حساب کاربری شما غیرفعال شده است.
        /// </summary>
        internal static string AccountIsDisabled {
            get {
                return ResourceManager.GetString("AccountIsDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to دامنه {0} مربوط به به این سامانه نمی باشد.
        /// </summary>
        internal static string DomainIsNotRelatedSystem {
            get {
                return ResourceManager.GetString("DomainIsNotRelatedSystem", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to آدرس ایمیل وارد نشده است.
        /// </summary>
        internal static string EmailIsEmpty {
            get {
                return ResourceManager.GetString("EmailIsEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to آدرس ایمیل وارد شده در سیستم ثبت نشده است.
        /// </summary>
        internal static string EmailNotExist {
            get {
                return ResourceManager.GetString("EmailNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to کد کپچا نامعتبر است.
        /// </summary>
        internal static string InvalidCaptch {
            get {
                return ResourceManager.GetString("InvalidCaptch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to داده های دریافتی نامعتبر است.
        /// </summary>
        internal static string InvalidData {
            get {
                return ResourceManager.GetString("InvalidData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ارسال درخواست بیش از حد مجاز.
        /// </summary>
        internal static string MaxRequestCountPassed {
            get {
                return ResourceManager.GetString("MaxRequestCountPassed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to هیچ یک از روش های بازیابی ایمیل براش شما فعال نیست.
        /// </summary>
        internal static string NotRecoveryMethodsExist {
            get {
                return ResourceManager.GetString("NotRecoveryMethodsExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to پوشه ای برای این کاربر موجود نیست.
        /// </summary>
        internal static string UserFolderNotExist {
            get {
                return ResourceManager.GetString("UserFolderNotExist", resourceCulture);
            }
        }
    }
}
