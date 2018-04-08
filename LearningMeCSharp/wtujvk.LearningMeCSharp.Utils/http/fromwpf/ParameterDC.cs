using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.Utils.http
{
    public class ParameterDC
    {
        /// <summary>
        /// 参数类型限定名。是反射的目标。
        /// </summary>
        public string TypeQualifiedName { get; set; }

        /// <summary>
        /// 参数Json值。
        /// </summary>
        public string JsonValue { get; set; }
    }
}
