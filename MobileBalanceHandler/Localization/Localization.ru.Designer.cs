﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MobileBalanceHandler.Localization {
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
    internal class Localization_ru {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Localization_ru() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MobileBalanceHandler.Localization.Localization.ru", typeof(Localization_ru).Assembly);
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
        ///   Looks up a localized string similar to Возникла ошибка, запрос не был отправлен успешно.
        /// </summary>
        internal static string failedRequest {
            get {
                return ResourceManager.GetString("failedRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Оператора по такому префиксу не существует.
        /// </summary>
        internal static string prefixError {
            get {
                return ResourceManager.GetString("prefixError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Приносим извинения, произошла ошибка при отправке запроса.
        /// </summary>
        internal static string reqTypeError {
            get {
                return ResourceManager.GetString("reqTypeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Запрос не был отправлен, произошла ошибка на сервере.
        /// </summary>
        internal static string responseError {
            get {
                return ResourceManager.GetString("responseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Платеж по номеру number на сумму sum тенге принят в обработку.
        /// </summary>
        internal static string success {
            get {
                return ResourceManager.GetString("success", resourceCulture);
            }
        }
    }
}