using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.Utils.http
{
    
    public sealed class MethodDC
    {
        /// <summary>
        /// Api的映射标识。
        /// </summary>
        public string ApiConfigKey { get; set; }

        /// <summary>
        /// 调用的远程方法所在的接口全名而非限定名,因为他并不是反射的目标。
        /// </summary>
        public string InterfaceName { get; set; }

        /// <summary>
        /// 调用的远程方法的方法名。
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 调用远程方法的参数。
        /// </summary>
        public List<ParameterDC> ParameterList { get; set; }

        /// <summary>
        /// 返回结果。
        /// </summary>
        public ApiResultDC Result { get; set; }
       

        /// <summary>
        /// 泛型方法的类型实参
        /// </summary>
        public string[] TypeArguments { set; get; }

        /// <summary>
        /// 构造函数。
        /// </summary>
        public MethodDC()
        {

        }
    }
}
