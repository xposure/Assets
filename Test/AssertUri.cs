// <copyright file="AssetUri.cs" company="None">
//     MyCompany.com. All rights reserved.
// </copyright>
// <author>Xposure</author>

namespace AssetTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Xposure.Assets;

    [TestClass]
    public class AssetUriTest
    {
        [TestMethod]
        public void AssetUriIsNotValid()
        {
            Assert.AreEqual(false, AssetUri.Invalid.IsValid);
        }

        [TestMethod]
        public void AssetUriIsValid()
        {
            var uri = new AssetUri("module:type:name");
            Assert.AreEqual(true, uri.IsValid);
            Assert.AreEqual("module", uri.Module);
            Assert.AreEqual("type", uri.Type);
            Assert.AreEqual("name", uri.Name);
            Assert.AreEqual("module:type:name", uri.FullName);
            Assert.AreEqual("module:type:name".GetHashCode(), uri.GetHashCode());
        }

        [TestMethod]
        public void AssertUriOperators()
        {
            var a = new AssetUri("module:type:name");
            var b = new AssetUri("module:type:name");
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
            Assert.IsTrue(a == "module:type:name");
            Assert.IsTrue("module:type:name" == a);
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals((object)b));
        }

        [TestMethod]
        public void AssertUriCompare()
        {
            var a = new AssetUri("module:type:name1");
            var b = new AssetUri("module:type:name2");
            Assert.IsTrue(b.CompareTo(a) > 0);
            Assert.IsTrue(a.CompareTo(b) < 0);
            Assert.IsTrue(a.CompareTo(a) == 0);
            Assert.IsTrue(b.CompareTo(b) == 0);
        }
    }
}
