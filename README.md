RecycleBin.Options
==================

This is a .NET Framework library providing a method dynamic typing of command-line arguments.

Examples
--------

Suppose you want to construct an instance of OptionArgument below from command-line arguments like
"program.exe -seed=12345"

```csharp
public class OptionArgument
{
   public int RandomSeed { get; set; }
}
```

You just need to add OptionAttribute onto the property.

```csharp
public class OptionArgument
{
   [Option("seed")]
   public int RandomSeed { get; set; }
}
```

And you can retreive the RandomSeed with the following code.

```csharp
var parser = new CommandLineParser('-', '=');
var argument = parser.Parse<OptionArgument>();
int seed = argument.Option.RandomSeed;
```

For flag option (requires Boolean value), you can use OptionFlagAttribute.

```csharp
public class OptionArgument
{
   [OptionFlag("?")]
   [OptionFlag("h")]
   [OptionFlag("help")]
   public bool ShowHelp { get; set; }
}
```

OptionFlagAttribute can take an argument only when the argument is combined by a separator in such form like "/help:true"; besides, it allows input with suffix of "+" (for true) or "-" (for false) like "/help+" or "/help-".
