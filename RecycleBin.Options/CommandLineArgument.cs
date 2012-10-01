using System.Collections;
using System.Collections.Generic;

namespace RecycleBin.Options
{
   /// <summary>
   /// Represents command-line arguments.
   /// </summary>
   /// <typeparam name="TOption"></typeparam>
   public class CommandLineArgument<TOption> : IEnumerable<string>
   {
      private readonly string[] rest;
      /// <summary>
      /// Gets the arguments that are unused to construct <see cref="Option" />.
      /// </summary>
      public string this[int index]
      {
         get { return this.rest[index]; }
      }

      /// <summary>
      /// Gets the number of arguments that are unsed to construct <see cref="Option" />.
      /// </summary>
      public int Length
      {
         get { return this.rest.Length; }
      }

      private readonly TOption option;
      /// <summary>
      /// Gets the option arguments.
      /// </summary>
      public TOption Option
      {
         get { return this.option; }
      }

      internal CommandLineArgument(TOption option, string[] rest)
      {
         this.option = option;
         this.rest = rest;
      }

      /// <inheritDoc />
      public IEnumerator<string> GetEnumerator()
      {
         return ((IEnumerable<string>)this.rest).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
         return this.GetEnumerator();
      }
   }
}
