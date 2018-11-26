using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsPrettyName.TypeExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsPrettyName.TypeExtension.Tests
{
    [TestClass()]
    public class PrettyNameExtensionTests
    {
        enum Hoge { A,B,}

        [TestMethod()]
        public void PrettyNameTest()
        {
            Assert.AreEqual("int", typeof(int).PrettyName());
            Assert.AreEqual("int?", typeof(int?).PrettyName());
            Assert.AreEqual("int[]", typeof(int[]).PrettyName());
            Assert.AreEqual("string", typeof(string).PrettyName());
            Assert.AreEqual("string[]", typeof(string[]).PrettyName());
            Assert.AreEqual("System.Collections.Generic.IEnumerable<double?[]>", typeof(IEnumerable<double?[]>).PrettyName());
            Assert.AreEqual("CsPrettyName.TypeExtension.Tests.Hoge", typeof(Hoge).PrettyName());
            Assert.AreEqual("System.Tuple", typeof(Tuple).PrettyName());
            Assert.AreEqual("System.Tuple<CsPrettyName.TypeExtension.Tests.Hoge, System.Collections.Generic.IEnumerable<double?[]>>", typeof(Tuple<Hoge, IEnumerable<double?[]>>).PrettyName());
        }
    }
}