using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.Resolution;

namespace wtujvk.LearningMeCSharp.ToolStandard.Ioc
{
    /// <summary>
    /// 表示用于整个IoC系统的工具类。
    /// </summary>
    public class IocUtils
    {
        /// <summary>
        /// unity参数组合
        /// </summary>
        /// <param name="overridedArguments"></param>
        /// <returns></returns>
        public static IEnumerable<ParameterOverride> GetParameterOverrides(object overridedArguments)
        {
            List<ParameterOverride> overrides = new List<ParameterOverride>();

            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new ParameterOverride(propertyName, propertyValue));
                });

            return overrides;
        }

        /// <summary>
        /// autofac参数组合
        /// </summary>
        /// <param name="overridedArguments"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> GetParameter(object overridedArguments)
        {
            List<NamedParameter> overrides = new List<NamedParameter>();

            Type argumentsType = overridedArguments.GetType();
            argumentsType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToList()
                .ForEach(property =>
                {
                    var propertyValue = property.GetValue(overridedArguments, null);
                    var propertyName = property.Name;
                    overrides.Add(new NamedParameter(propertyName, propertyValue));
                });

            return overrides;
        }
    }
}
