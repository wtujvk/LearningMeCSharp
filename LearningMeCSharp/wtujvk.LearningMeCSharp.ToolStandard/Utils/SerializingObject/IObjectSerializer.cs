using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.ToolStandard.Utils.SerializingObject
{
    public interface IObjectSerializer
    {
        string SerializeObj(object obj);

        T DeserializeObj<T>(string value) where T : class, new();

        object DeserializeObj(string value, Type t);
    }
}
