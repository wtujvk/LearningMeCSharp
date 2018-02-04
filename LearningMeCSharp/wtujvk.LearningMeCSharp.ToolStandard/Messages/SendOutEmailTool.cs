using Demo.LindAgile.Standard.SerializingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using wtujvk.LearningMeCSharp.ToolStandard.Conf;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.ToolStandard.Messages
{
    public class SendOutEmailTool: ThreadSafeLazyBaseSingleton<SendOutEmailTool>
    {
        static EmailMessage emailMessage = ConfigManager.Config.EmailMessage;
        private static object lockObj = new object();
        //private static SendOutEmailTool instance;
        public SendOutEmailTool() { }

        ///// <summary>
        ///// 单例模式的日志工厂对象
        ///// </summary>
        //public static SendOutEmailTool Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            lock (lockObj)
        //            {
        //                if (instance == null)
        //                {
        //                    instance = new SendOutEmailTool();
        //                }
        //            }
        //        }
        //        return instance;
        //    }
        //}
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="messageContext"></param>
        /// <param name="actionSuccess"></param>
        /// <returns></returns>
        public bool SendEmail(MessageContext messageContext,Action actionSuccess=null)
        {
            try
            {
                CheckMessageContext(messageContext);
                Thread.Sleep(150);
               
                using (SmtpClient smtpServer = new SmtpClient(emailMessage.Email_Host))
                {
                    smtpServer.Port = emailMessage.Email_Port;
                    smtpServer.Credentials = new System.Net.NetworkCredential(emailMessage.Email_UserName,emailMessage.Email_Password);
                    smtpServer.EnableSsl = true;
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress(emailMessage.Email_Address,emailMessage.Email_DisplayName,Encoding.UTF8);//和上面的对应
                    foreach (var item in messageContext.Addresses)
                    {
                        mail.To.Add(item);
                    }
                    ////向抄送收件人地址集合添加邮件地址
                    
                    if (messageContext.MailCcArray.AnyElement())
                    {
                        foreach (var item in messageContext.MailCcArray)
                        {
                            mail.CC.Add(item);
                        }
                    }
                    //发送附件
                    if (messageContext.AttachmentsPath.AnyElement())
                    {
                        foreach (var item in messageContext.AttachmentsPath)
                        {
                            mail.Attachments.Add(new Attachment(item));
                        }
                    }
                    mail.Subject = messageContext.Subject;//标题
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.Body = messageContext.Body;
                    mail.Priority = MailPriority.High;//邮件优先级
                    mail.IsBodyHtml = true;
                   // mail.Attachments.Add(new Attachment (""))
                    smtpServer.Send(mail);
                    smtpServer.SendCompleted += (a, b) => { actionSuccess?.Invoke(); };
                 }
                
                return true;
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                throw;
            }
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="messageContext"></param>
        /// <param name="actionSuccess"></param>
        /// <returns></returns>
        public async Task<bool> SendEmailAsync(MessageContext messageContext, Action actionSuccess)
        {
            try
            {
               CheckMessageContext(messageContext);
               Thread.Sleep(150);
               
               using (SmtpClient smtpServer = new SmtpClient(emailMessage.Email_Host))
               {
                   smtpServer.Port = emailMessage.Email_Port;
                   smtpServer.Credentials = new System.Net.NetworkCredential(emailMessage.Email_UserName, emailMessage.Email_Password);
                   smtpServer.EnableSsl = true;
                   MailMessage mail = new MailMessage();
                   mail.From = new MailAddress(emailMessage.Email_Address,emailMessage.Email_DisplayName,Encoding.UTF8);//和上面的对应
                   foreach (var item in messageContext.Addresses)
                   {
                       mail.To.Add(item);
                   }
                    ////向抄送收件人地址集合添加邮件地址
                    if (messageContext.MailCcArray.AnyElement())
                    {
                        foreach (var item in messageContext.MailCcArray)
                        {
                            mail.CC.Add(item);
                        }
                    }
                    //发送附件
                    if (messageContext.AttachmentsPath.AnyElement())
                    {
                        foreach (var item in messageContext.AttachmentsPath)
                        {
                            mail.Attachments.Add(new Attachment(item));
                        }
                    }
                   mail.Subject = messageContext.Subject;//标题
                   mail.SubjectEncoding = Encoding.UTF8;
                   mail.Body = messageContext.Body;
                   mail.Priority = MailPriority.High;//邮件优先级
                   mail.IsBodyHtml = true;
                  await  smtpServer.SendMailAsync(mail);
                 smtpServer.SendCompleted += (a, b) => { actionSuccess?.Invoke(); };
               }
              return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                LoggerFactory.Instance.Logger_Error(ex);
                throw;
            }
        }
        /// <summary>
        /// 验证消息体是否合法
        /// </summary>
        /// <param name="messageContext"></param>
        void CheckMessageContext(MessageContext messageContext)
        {
            if (messageContext == null || !messageContext.Addresses.AnyElement() || new string[] {messageContext.Body,messageContext.Subject}.IsAnyNullOrWhiteSpace())
            {
                var messagecontextJson = SerializerManager.Instance.SerializeObj(messageContext ?? new MessageContext());
                throw new ArgumentNullException("参数错误:"+messagecontextJson);
            }
            else
            {
                //过滤非法邮件地址
               
                messageContext.Addresses = EmailAddressCheck(messageContext.Addresses);
                if (!messageContext.Addresses.AnyElement())
                {
                    throw new ArgumentNullException("messageContext.Addresses 邮件缺少发送人！");
                }
                messageContext.MailCcArray = EmailAddressCheck(messageContext.MailCcArray);
                messageContext.AttachmentsPath = StringLstCheck(messageContext.AttachmentsPath);
            }
        }
        /// <summary>
        /// 邮件验证
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsEmailVaild(string email)
        {
            //var regstr = @" ^ ([\w -\.] +)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w -]+\.)+))([a - zA - Z]{2,4}|[0-9]{1,3})(\]?)$";
            // var regstr = @"^[a-zA-Z0-9_-]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";
            //var regstr = @"^[A-Za-z0-9\u4e00-\u9fa5]+@[a-zA-Z0-9_-]+(\.[a-zA-Z0-9_-]+)+$";//支持中文
            //var regstr = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            //Regex regex = new Regex(regstr);
            //return email.IsVisable() && regex.IsMatch(email);
            return IsWhat.IsEamil(email);
        }

        /// <summary>
        /// 邮件地址过滤 过滤非法邮件地址
        /// </summary>
        /// <param name="addresss"></param>
        /// <returns></returns>
        public IEnumerable<string> EmailAddressCheck(IEnumerable<string> addresss)
        {
            return StringLstCheck(addresss, c => c.IsEamil());
        }
        /// <summary>
        /// 字符串集合匹配过滤
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public IEnumerable<string> StringLstCheck(IEnumerable<string> lst, Func<string, bool> func = null)
        {
            //过滤非法邮件地址
            var list = lst.ToList();
            if (func == null) func = (c) => true;
            list = list.Where(c => c.IsValuable() && func(c)).Select(c => c.Trim()).Distinct().ToList();
            return list;
        }
    }
}
