using System;

namespace RecycleBin.Options
{
   /// <summary>
   /// Takes no arguments. Only used for Boolean member.
   /// </summary>
   [Serializable]
   [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
   public class OptionFlagAttribute : OptionAttribute
   {
      /// <summary>
      /// Initializes a new instance.
      /// </summary>
      /// <param name="name">The option name.</param>
      public OptionFlagAttribute(string name)
         : base(name)
      {
      }

      /// <summary>
      /// Converts the specified value as a Boolean value.
      /// </summary>
      /// <param name="value">The value.</param>
      /// <param name="memberType">Should be Boolean type.</param>
      /// <returns><c>True</c> if the specified value indicates <c>True</c>; otherwise, <c>False</c>.</returns>
      public override object Parse(string value, Type memberType)
      {
         bool parseResult;
         if (Boolean.TryParse(value, out parseResult))
         {
            return parseResult;
         }
         return value != "-";
      }
   }
}
