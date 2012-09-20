using System;

namespace RecycleBin.Options
{
   [Serializable]
   [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
   public class OptionAttribute : ValueAttribute
   {
      private readonly string name;
      public string Name
      {
         get { return this.name; }
      }

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
