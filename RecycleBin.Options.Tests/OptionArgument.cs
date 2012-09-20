namespace RecycleBin.Options
{
   public class OptionArgument
   {
      [Option("Name")]
      public string StringValue { get; set; }

      [Option("Value")]
      public int Int32Value { get; set; }

      [Option("Flag")]
      public bool BooleanValue { get; set; }
   }
}
