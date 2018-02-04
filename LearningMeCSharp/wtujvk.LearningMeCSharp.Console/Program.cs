using System;
using wtujvk.LearningMeCSharp.ToolStandard.Messages;
using wtujvk.LearningMeCSharp.ToolStandard.Utils;

namespace wtujvk.LearningMeCSharp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");
            EmailRun();
            System.Console.ReadLine();
        }

        static void EmailRun()
        {
            var recipient = "1170971516@qq.com";
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var body = string.Format("这是收到的一条消息。<img src='{0}' alt='花间提壶方大厨' />", "http://img1.imgtn.bdimg.com/it/u=2162087863,686892888&fm=27&gp=0.jpg");
            MessageFactory.GetService(MessageType.Email).Send(recipient, "wtujvk.LearningMeCSharp.Console发送email消息", body,()=> { PrintLine("发送成功"); },()=> { PrintLine("发送失败"); });
        }

        static void PrintLine(string str)
        {
            if (str.IsVisable())
            {
                str= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+","+str;
                System.Console.WriteLine(str);
            }
        }
    }
}
