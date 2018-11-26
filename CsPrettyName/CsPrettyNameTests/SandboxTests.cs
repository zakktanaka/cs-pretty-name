using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsPrettyName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsPrettyName.Tests
{
    [TestClass()]
    public class SandboxTests
    {
        private static string PrettyName(Type type)
        {
            if (type.GetGenericArguments().Length == 0)
            {
                return string.Format($"{type.Namespace}.{type.Name}");
            }
            var typeDefeninition = string.Format($"{type.Namespace}.{type.Name}");
            var unmangledName = typeDefeninition.Substring(0, typeDefeninition.IndexOf("`"));
            var genericArguments = type.GetGenericArguments();
            return unmangledName + "<" + String.Join(",", genericArguments.Select(PrettyName)) + ">";
        }

        enum Hoge { A,B,}
        interface IHoge { }

        [TestMethod()]
        public void PrettyNameTest()
        {
            {
                var type = typeof(double[]);
                Console.WriteLine(PrettyName(type));
            }
            {
                var type = typeof(long?);
                Console.WriteLine(PrettyName(type));
            }
            {
                var type = typeof(Hoge);
                Console.WriteLine(PrettyName(type));
            }
            {
                var type = typeof(IHoge);
                Console.WriteLine(PrettyName(type));
            }
            {
                var type = typeof(IEnumerable<SandboxTests>);
                Console.WriteLine(PrettyName(type));
            }
            {
                var type = typeof(Tuple<string, int?, IEnumerable<SandboxTests>>);
                Console.WriteLine(PrettyName(type));
            }
            Assert.Fail();
        }
    }
}