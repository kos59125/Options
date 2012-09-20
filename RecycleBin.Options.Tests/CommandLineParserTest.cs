using NUnit.Framework;

namespace RecycleBin.Options
{
   [TestFixture]
   internal class CommandLineParserTest
   {
      [Test]
      public void TestParse()
      {
         var parser = new CommandLineParser();
         var args = new[] { "Unused0", "/Name:SomeName", "Unused1", "/vALue", "42", "Unused2", "Unused3", "/Flag:true" };
         var actual = parser.Parse<OptionArgument>(args);
         Assert.That(actual.Option.StringValue, Is.EqualTo("SomeName"));
         Assert.That(actual.Option.Int32Value, Is.EqualTo(42));
         Assert.That(actual.Option.BooleanValue, Is.True);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1", "Unused2", "Unused3" }));
      }

      [Test]
      public void TestParseWithConstructorArgument()
      {
         var parser = new CommandLineParser('-', '=');
         var args = new[] { "Unused0", "-name=SomeName", "Unused1", "-vALue", "42", "Unused2", "Unused3", "-Flag=true" };
         var actual = parser.Parse<OptionArgument>(args);
         Assert.That(actual.Option.StringValue, Is.EqualTo("SomeName"));
         Assert.That(actual.Option.Int32Value, Is.EqualTo(42));
         Assert.That(actual.Option.BooleanValue, Is.True);
         Assert.That(actual, Is.EquivalentTo(new[] { "Unused0", "Unused1", "Unused2", "Unused3" }));
      }
   }
}
