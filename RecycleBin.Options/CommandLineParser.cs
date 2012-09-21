using System;
using System.Collections.Generic;
using System.Linq;
using RecycleBin.Commons.Reflection;

namespace RecycleBin.Options
{
   /// <summary>
   /// Provides methods to parse command-line arguments into an typed object.
   /// </summary>
   public class CommandLineParser
   {
      private readonly char optionMark;
      private readonly char[] valueDefinition;

      /// <summary>
      /// Initializes a new Windows-style parser.
      /// </summary>
      public CommandLineParser()
         : this('/', ':')
      {
      }

      /// <summary>
      /// Initializes a new parser.
      /// </summary>
      /// <param name="optionMark">The character which indicates that the argument is optional.</param>
      /// <param name="valueDefinition">The characters that separate option name and its value in an argument.</param>
      public CommandLineParser(char optionMark, char valueDefinition)
      {
         this.optionMark = optionMark;
         this.valueDefinition = new[] { valueDefinition };
      }

      /// <summary>
      /// Parses command-line arguments.
      /// </summary>
      /// <typeparam name="TOption">Optional argument type.</typeparam>
      /// <returns>Parsed object.</returns>
      public CommandLineArgument<TOption> Parse<TOption>()
         where TOption : new()
      {
         // First argument from Environment.GetCommandLineArgs() is program itself,
         // which is not passed in the arguments in the entrypoint function.
         var args = Environment.GetCommandLineArgs().Skip(1);
         return Parse<TOption>(args);
      }

      /// <summary>
      /// Parses command-line arguments.
      /// </summary>
      /// <typeparam name="TOption">Optional argument type.</typeparam>
      /// <param name="args">The command-line arguments.</param>
      /// <returns>Parsed object.</returns>
      public CommandLineArgument<TOption> Parse<TOption>(IEnumerable<string> args)
         where TOption : new()
      {
         if (args == null)
         {
            throw new ArgumentNullException("args");
         }
         using (var enumerator = args.GetEnumerator())
         {
            var dictionary = CreateDictionary(typeof(TOption));
            var rest = new List<string>();
            var option = new TOption();
            while (enumerator.MoveNext())
            {
               var argument = enumerator.Current;
               if (argument[0] == this.optionMark)
               {
                  var optionName = argument.Substring(1);
                  if (optionName.Any(this.valueDefinition.Contains))
                  {
                     var nameAndValue = optionName.Split(this.valueDefinition, 2);
                     optionName = nameAndValue[0];
                     var value = nameAndValue[1];
                     SetValue(dictionary[optionName], value, ref option);
                  }
                  else
                  {
                     if (!enumerator.MoveNext())
                     {
                        throw new InvalidOperationException("Requires option argument.");
                     }
                     var value = enumerator.Current;
                     if (value[0] == this.optionMark)
                     {
                        throw new InvalidOperationException("Requires option argument.");
                     }
                     SetValue(dictionary[optionName], value, ref option);
                  }
               }
               else
               {
                  rest.Add(argument);
               }
            }
            return new CommandLineArgument<TOption>(option, rest.ToArray());
         }
      }

      private void SetValue<TOption>(Tuple<SetValue, OptionAttribute, Type> info, string value, ref TOption option)
      {
         var setValue = info.Item1;
         var attribute = info.Item2;
         var memberType = info.Item3;
         var parseResult = attribute.Parse(value, memberType);
         setValue(option, parseResult);
      }

      private static IDictionary<string, Tuple<SetValue, OptionAttribute, Type>> CreateDictionary(Type optionType)
      {
         var properties = from property in optionType.GetProperties()
                          let attributes = property.GetCustomAttributes(typeof(OptionAttribute), true)
                          from attribute in attributes.Cast<OptionAttribute>()
                          let setProperty = attribute.GenerateSetValue(property)
                          select Tuple.Create(setProperty, attribute, property.PropertyType);
         var fields = from field in optionType.GetFields()
                      let attributes = field.GetCustomAttributes(typeof(OptionAttribute), true)
                      from attribute in attributes.Cast<OptionAttribute>()
                      let setField = attribute.GenerateSetValue(field)
                      select Tuple.Create(setField, attribute, field.FieldType);
         return properties.Concat(fields).ToDictionary(tuple => tuple.Item2.Name, StringComparer.InvariantCultureIgnoreCase);
      }
   }
}
