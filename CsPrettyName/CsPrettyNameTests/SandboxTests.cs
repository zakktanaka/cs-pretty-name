using Microsoft.VisualStudio.TestTools.UnitTesting;
using CsPrettyName;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace CsPrettyName.Tests
{
    [TestClass()]
    public class SandboxTests
    {
        class PrettyNameRepository
        {
            static ConcurrentDictionary<Type, string> prettyNames;

            static PrettyNameRepository()
            {
                prettyNames = new ConcurrentDictionary<Type, string>();
                prettyNames.TryAdd(typeof(bool), "bool");
                prettyNames.TryAdd(typeof(bool?), "bool?");
                prettyNames.TryAdd(typeof(bool?[]), "bool?[]");
                prettyNames.TryAdd(typeof(bool[]), "bool[]");
                prettyNames.TryAdd(typeof(byte), "byte");
                prettyNames.TryAdd(typeof(byte?), "byte?");
                prettyNames.TryAdd(typeof(byte?[]), "byte?[]");
                prettyNames.TryAdd(typeof(byte[]), "byte[]");
                prettyNames.TryAdd(typeof(char), "char");
                prettyNames.TryAdd(typeof(char?), "char?");
                prettyNames.TryAdd(typeof(char?[]), "char?[]");
                prettyNames.TryAdd(typeof(char[]), "char[]");
                prettyNames.TryAdd(typeof(decimal), "decimal");
                prettyNames.TryAdd(typeof(decimal?), "decimal?");
                prettyNames.TryAdd(typeof(decimal?[]), "decimal?[]");
                prettyNames.TryAdd(typeof(decimal[]), "decimal[]");
                prettyNames.TryAdd(typeof(double), "double");
                prettyNames.TryAdd(typeof(double?), "double?");
                prettyNames.TryAdd(typeof(double?[]), "double?[]");
                prettyNames.TryAdd(typeof(double[]), "double[]");
                prettyNames.TryAdd(typeof(float), "float");
                prettyNames.TryAdd(typeof(float?), "float?");
                prettyNames.TryAdd(typeof(float?[]), "float?[]");
                prettyNames.TryAdd(typeof(float[]), "float[]");
                prettyNames.TryAdd(typeof(int), "int");
                prettyNames.TryAdd(typeof(int?), "int?");
                prettyNames.TryAdd(typeof(int?[]), "int?[]");
                prettyNames.TryAdd(typeof(int[]), "int[]");
                prettyNames.TryAdd(typeof(long), "long");
                prettyNames.TryAdd(typeof(long?), "long?");
                prettyNames.TryAdd(typeof(long?[]), "long?[]");
                prettyNames.TryAdd(typeof(long[]), "long[]");
                prettyNames.TryAdd(typeof(object), "object");
                prettyNames.TryAdd(typeof(object[]), "object[]");
                prettyNames.TryAdd(typeof(sbyte), "sbyte");
                prettyNames.TryAdd(typeof(sbyte?), "sbyte?");
                prettyNames.TryAdd(typeof(sbyte?[]), "sbyte?[]");
                prettyNames.TryAdd(typeof(sbyte[]), "sbyte[]");
                prettyNames.TryAdd(typeof(short), "short");
                prettyNames.TryAdd(typeof(short?), "short?");
                prettyNames.TryAdd(typeof(short?[]), "short?[]");
                prettyNames.TryAdd(typeof(short[]), "short[]");
                prettyNames.TryAdd(typeof(string), "string");
                prettyNames.TryAdd(typeof(string[]), "string[]");
                prettyNames.TryAdd(typeof(uint), "uint");
                prettyNames.TryAdd(typeof(uint?), "uint?");
                prettyNames.TryAdd(typeof(uint?[]), "uint?[]");
                prettyNames.TryAdd(typeof(uint[]), "uint[]");
                prettyNames.TryAdd(typeof(ulong), "ulong");
                prettyNames.TryAdd(typeof(ulong?), "ulong?");
                prettyNames.TryAdd(typeof(ulong?[]), "ulong?[]");
                prettyNames.TryAdd(typeof(ulong[]), "ulong[]");
                prettyNames.TryAdd(typeof(ushort), "ushort");
                prettyNames.TryAdd(typeof(ushort?), "ushort?");
                prettyNames.TryAdd(typeof(ushort?[]), "ushort?[]");
                prettyNames.TryAdd(typeof(ushort[]), "ushort[]");
            }

            private static string ResolvePName(Type type)
            {
                string pname;
                if(!prettyNames.TryGetValue(type, out pname))
                {
                    lock(prettyNames)
                    {
                        if (!prettyNames.TryGetValue(type, out pname))
                        {
                            pname = BuildPName(type);
                            prettyNames.TryAdd(type, pname);
                        }
                    }
                }
                return pname;
            }

            private static string BuildPName(Type type)
            {
                if( type.IsArray)
                {
                    return string.Format($"{BuildPName(type.GetElementType())}[]");
                }

                var tp = string.Format($"{type.Namespace}.{type.Name}");
                if (type.GetGenericArguments().Length == 0)
                {
                    return tp;
                }
                else
                {
                    var gpname = tp.Substring(0, tp.IndexOf("`"));
                    var gargs = type.GetGenericArguments();
                    return gpname + "<" + string.Join(",", gargs.Select(ResolvePName)) + ">";
                }
            }

            public string ResolvePrettyName(Type type)
            {
                return ResolvePName(type);
            }
        }

        enum Hoge { A,B,}
        interface IHoge { }

        [TestMethod()]
        public void PrettyNameTest()
        {
            var r = new PrettyNameRepository();
            {
                var type = typeof(double[]);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(long?);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(long?);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(Hoge);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(IHoge);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(IEnumerable<SandboxTests>);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(Tuple<string, int?, IEnumerable<SandboxTests>>);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(Tuple<string, int?, IEnumerable<SandboxTests>>);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            {
                var type = typeof(Tuple<string, int?, IEnumerable<SandboxTests>>[]);
                Console.WriteLine(r.ResolvePrettyName(type));
            }
            Assert.Fail();
        }
    }
}