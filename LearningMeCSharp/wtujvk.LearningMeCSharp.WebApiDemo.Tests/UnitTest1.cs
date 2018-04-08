using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using wtujvk.LearningMeCSharp.Utils;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes;
using wtujvk.LearningMeCSharp.WebApiDemo.Codes.Ioc;

namespace wtujvk.LearningMeCSharp.WebApiDemo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //IocHelper.Register<IEnumerable<Student>, List<Student>>();
            IocHelper.Register(typeof(IA<>), typeof(AA<>));
            var isreg1 = IocHelper.IsRegistered<IA<Student>>();
            var isreg2 = IocHelper.IsRegistered(typeof(IA<>));
            var repository= IocHelper.Get<IA<Student>>();
            repository.Add(new Student() {  Name="¿Êª®",Age=4});
            repository.Add(new Student() { Name="ÀŸÀ„",Age=25});
            Assert.IsNotNull(repository);
        }

        [TestMethod]
        public void WebApiTest()
        {
            string url = "http://192.168.1.123:12591/";
            LoginInfo loginInfo = new LoginInfo() {  AddTime=DateTime.Now, AppName="appname", AppKey="key", ExceptionName="test", ExSource="Method", ExTargetSite="ddddd", HasInnerException=false, LogLevel="Test", Message="5555", ThreadId=1, StackTrace="kkkk"};
            var loginJsonstr = loginInfo.ToJson();
           // var result=  HttpClientHelper.PostResponse(url+ "api/home2/add", loginJsonstr);
            string statecode;
           var result=  HttpClientHelper.GetResponse(url + "api/home",out statecode);

            Assert.IsNotNull(result);
        }
    }
    class Student
    {
        public Student()
        {
            Id = GetHashCode();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
    interface IA<T>
    {
        void Add(T t);
        int Count();
        void DeleteByquery(Predicate<T> match);
    }
    class AA<T> : IA<T>
    {
        private List<T> plist = new List<T>();
        public void Add(T t)
        {
            plist.Add(t);
        }

        public int Count()
        {
            return plist.Count;
        }
        public void DeleteByquery(Predicate<T> match)
        {
            plist.RemoveAll(match);
        }
    }
}
