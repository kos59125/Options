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

