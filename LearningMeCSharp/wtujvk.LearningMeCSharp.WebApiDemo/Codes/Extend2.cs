using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Codes
{
    /// <summary>
    /// 
    /// </summary>
    public static  class Extend2
    {
        /// <summary>
        /// 获取错误信息
        /// </summary>
        /// <param name="ModelStates"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetErrorMsg(this Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ModelStates)
        {
            List<string> errorMsgLst = new List<string>();
            //获取所有错误的Key
            var Keys = ModelStates.Keys.ToList();
            //获取每一个key对应的ModelStateDictionary
            foreach (var key in Keys)
            {
                var errors = ModelStates[key].Errors.ToList();
                //将错误描述添加到sb中
                foreach (var error in errors)
                {
                    errorMsgLst.Add(error.ErrorMessage);
                }
            }
            return errorMsgLst;
        }
    }
}
