using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace RecycleBin.Options
{
   public class Flag
   {
      [OptionFlag("Help")]
      public bool FlagValue { get; set; }
   }

   [TestFixture]
   internal class OptionFlagTest
   {
      [Test]
      public void TestWithoutArgument()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Help", "Unused1" };
         var actual = parser.Parse<Flag>(args);
         Assert.That(actual.Option.FlagValue, Is.True);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1" }));
      }

      [Test]
      public void TestWithArgumentTrue()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Help:true", "Unused1" };
         var actual = parser.Parse<Flag>(args);
         Assert.That(actual.Option.FlagValue, Is.True);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1" }));
      }

      [Test]
      public void TestWithArgumentFalse()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Help:false", "Unused1" };
         var actual = parser.Parse<Flag>(args);
         Assert.That(actual.Option.FlagValue, Is.False);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1" }));
      }

      [Test]
      public void TestWithFlagSuffixTrue()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Help+", "Unused1" };
         var actual = parser.Parse<Flag>(args);
         Assert.That(actual.Option.FlagValue, Is.True);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1" }));
      }

      [Test]
      public void TestWithFlagSuffixFalse()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Help-", "Unused1" };
         var actual = parser.Parse<Flag>(args);
         Assert.That(actual.Option.FlagValue, Is.False);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1" }));
      }
   }
}
