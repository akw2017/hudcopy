using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Principal;

namespace AIC.Core
{
    public class RoleBasedModuleInitializer : IModuleInitializer
    {
        private readonly IServiceLocator serviceLocator;
        private readonly ILoggerFacade loggerFacade;

        /// <summary>
        /// Initializes a new instance of <see cref="ModuleInitializer"/>.
        /// </summary>
        /// <param name="serviceLocator">The container that will be used to resolve the modules by specifying its type.</param>
        /// <param name="loggerFacade">The logger to use.</param>
        public RoleBasedModuleInitializer(IServiceLocator serviceLocator, ILoggerFacade loggerFacade)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }

            if (loggerFacade == null)
            {
                throw new ArgumentNullException("loggerFacade");
            }

            this.serviceLocator = serviceLocator;
            this.loggerFacade = loggerFacade;
        }

        /// <summary>
        /// Initializes the specified module.
        /// </summary>
        /// <param name="moduleInfo">The module to initialize</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Catches Exception to handle any exception thrown during the initialization process with the HandleModuleInitializationError method.")]
        public void Initialize(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null) throw new ArgumentNullException("moduleInfo");

            IModule moduleInstance = null;
            try
            {
                if (ModuleIsInUserRole(moduleInfo))
                {
                    moduleInstance = this.CreateModule(moduleInfo);
                    if (moduleInstance != null)
                        moduleInstance.Initialize();
                }
            }
            catch (Exception ex)
            {
                this.HandleModuleInitializationError(
                    moduleInfo,
                    moduleInstance != null ? moduleInstance.GetType().Assembly.FullName : null,
                    ex);
            }
        }

        private bool ModuleIsInUserRole(ModuleInfo moduleInfo)
        {
            bool isInRole = false;

            var roles = GetModuleRoles(moduleInfo);
            if(roles!=null)
            {
                foreach (var role in roles)
                {
                    if (WindowsPrincipal.Current.IsInRole(role))
                    {
                        isInRole = true;
                        break;
                    }
                }
            }
            return isInRole;
        }

        private IEnumerable<string> GetModuleRoles(ModuleInfo moduleInfo)
        {
            var type = Type.GetType(moduleInfo.ModuleType);

            foreach (var attr in GetCustomAttribute<RolesAttribute>(type))
            {
                return attr.Roles.AsEnumerable();
            }

            return null;
        }

        private IEnumerable<T> GetCustomAttribute<T>(Type type)
        {
            return type.GetCustomAttributes(typeof(T), true).OfType<T>();
        }

        /// <summary>
        /// Handles any exception occurred in the module Initialization process,
        /// logs the error using the <see cref="ILoggerFacade"/> and throws a <see cref="ModuleInitializeException"/>.
        /// This method can be overridden to provide a different behavior. 
        /// </summary>
        /// <param name="moduleInfo">The module metadata where the error happenened.</param>
        /// <param name="assemblyName">The assembly name.</param>
        /// <param name="exception">The exception thrown that is the cause of the current error.</param>
        /// <exception cref="ModuleInitializeException"></exception>
        public virtual void HandleModuleInitializationError(ModuleInfo moduleInfo, string assemblyName, Exception exception)
        {
            if (moduleInfo == null) throw new ArgumentNullException("moduleInfo");
            if (exception == null) throw new ArgumentNullException("exception");

            Exception moduleException;

            if (exception is ModuleInitializeException)
            {
                moduleException = exception;
            }
            else
            {
                if (!string.IsNullOrEmpty(assemblyName))
                {
                    moduleException = new ModuleInitializeException(moduleInfo.ModuleName, assemblyName, exception.Message, exception);
                }
                else
                {
                    moduleException = new ModuleInitializeException(moduleInfo.ModuleName, exception.Message, exception);
                }
            }

            this.loggerFacade.Log(moduleException.ToString(), Category.Exception, Priority.High);

            throw moduleException;
        }

        /// <summary>
        /// Uses the container to resolve a new <see cref="IModule"/> by specifying its <see cref="Type"/>.
        /// </summary>
        /// <param name="moduleInfo">The module to create.</param>
        /// <returns>A new instance of the module specified by <paramref name="moduleInfo"/>.</returns>
        protected virtual IModule CreateModule(ModuleInfo moduleInfo)
        {
            if (moduleInfo == null) throw new ArgumentNullException("moduleInfo");
            return this.CreateModule(moduleInfo.ModuleType);
        }

        /// <summary>
        /// Uses the container to resolve a new <see cref="IModule"/> by specifying its <see cref="Type"/>.
        /// </summary>
        /// <param name="typeName">The type name to resolve. This type must implement <see cref="IModule"/>.</param>
        /// <returns>A new instance of <paramref name="typeName"/>.</returns>
        protected virtual IModule CreateModule(string typeName)
        {
            Type moduleType = Type.GetType(typeName);
            if (moduleType == null)
            {
                throw new ModuleInitializeException(string.Format(CultureInfo.CurrentCulture, "Failed To Get Type", typeName));
            }

            return (IModule)this.serviceLocator.GetInstance(moduleType);
        }
    }
}
