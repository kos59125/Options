using System;

namespace RecycleBin.Options
{
   /// <summary>
   /// Represents an option argument.
   /// </summary>
   [Serializable]
   [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
   public class OptionAttribute : ValueAttribute
   {
      private readonly string name;
      /// <summary>
      /// Gets the name of this option.
      /// </summary>
      public string Name
      {
         get { return this.name; }
      }

      /// <summary>
      /// Initializes a new instance.
      /// </summary>
      /// <param name="name">The option name.</param>
      public OptionAttribute(string name)
      {
         if (name == null)
         {
            throw new ArgumentNullException("name");
         }
         if (name.Length == 0)
         {
            throw new ArgumentException("Empty name.", "name");
         }
         this.name = name;
      }
   }
}
