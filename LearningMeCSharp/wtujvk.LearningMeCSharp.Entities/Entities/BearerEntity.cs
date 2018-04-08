using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.Entities
{
    /// <summary>
    /// Bearer实体
    /// </summary>
    [Table("BearerEntity")]
    public class BearerEntity:IEntity
    {
        public BearerEntity()
        {
            Access_token_Expires = Access_token_Expires.CheckTimeForSql(() => DateTime.Now);
            Refresh_Token_Expires = Refresh_Token_Expires.CheckTimeForSql(() => DateTime.Now);
            Access_token = string.IsNullOrWhiteSpace(Access_token) ? Guid.NewGuid().ToString("N") : Access_token;
        }
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 请求token
        /// </summary>
        [MaxLength(64)]
        public string Access_token { get; set; }
        /// <summary>
        /// 刷新的token，access_token有问题再比较这个，然后同时生成新的access_token
        /// </summary>
        [MaxLength(64)]
        public string Refresh_token { get; set; }
        /// <summary>
        /// access_token过期时间
        /// </summary>
        public DateTime Access_token_Expires { get; set; }
        /// <summary>
        /// refresh_token过期时间
        /// </summary>
        public DateTime Refresh_Token_Expires { get; set; }
        /// <summary>
        /// 被授权的用户名
        /// </summary>
        [MaxLength(64)]
        public string UserName { get; set; }
        /// <summary>
        /// 被收取的用户编号 应用程序id
        /// </summary>
        [MaxLength(64)]
        public string UserId { get; set; }
        //public bool Equals(int other)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        ///开发者密码 不在网络中传输 但是参与加密运算
        /// 用户可以修改
        /// </summary>
        [MaxLength(250)]
        public string AppSecret { get; set; }

        /// <summary>
        /// 状态码，1-可用 2-禁用
        /// </summary>
        [Display(Name = "状态", Description = "1-可用，2-禁用，默认禁用")]
        public short StateCode { get; set; }
        /// <summary>
        /// app应用名称或者项目解决方案名称
        /// </summary>
        [MaxLength(120)]
        [Display(Name = "app应用名称", Description = "用途描述")]
        public string AppName { get; set; } = "";
    }
}
