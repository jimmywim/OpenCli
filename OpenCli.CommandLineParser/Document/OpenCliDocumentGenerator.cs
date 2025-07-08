using CommandLine;
using OpenCli.Document;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCli.CommandLineParser.Document
{
    /// <summary>
    /// A class for generating an OpenCli Specification Document from a specified Assembly. 
    /// This scans the assembly for types and attributes used by CommandLineParser to define command line verbs and options
    /// </summary>
    public class OpenCliDocumentGenerator
    {
        /// <summary>
        /// Create an OpenCli Specification Document from the given Assembly
        /// </summary>
        /// <param name="assembly">An assembly that contains types that use CommandLineParser Attributes</param>
        /// <returns>An OpenCliDocument representing the assembly by its use of CommandLineParser Attributes</returns>
        public static OpenCliDocument CreateFromAssembly(Assembly assembly)
        {
            var document = new OpenCliDocument();

            var allTypes = assembly.GetTypes();
            foreach (var type in allTypes)
            {
                var verbAttributes = type.GetCustomAttributes(typeof(VerbAttribute), false);
                foreach (VerbAttribute verbAttribute in verbAttributes)
                {
                    OpenCliCommand command = new OpenCliCommand
                    {
                        Description = verbAttribute.HelpText,
                        Hidden = verbAttribute.Hidden,
                    };

                    if (document.Commands == null)
                    {
                        document.Commands = new Dictionary<string, OpenCliCommand>();
                    }

                    document.Commands.Add(verbAttribute.Name, command);


                    var verbOptions = verbAttribute.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.PropertyType == typeof(OptionAttribute));
                    foreach(PropertyInfo verbProp in verbOptions)
                    {
                        verbProp.GetType().GetCustomAttribute<OptionAttribute>();
                        OptionAttribute verbOption = verbProp.GetType().GetCustomAttribute<OptionAttribute>();
                        if (verbOption != null)
                        {
                            OpenCliOption option = new OpenCliOption
                            {
                                Aliases = new List<string> { $"-{verbOption.ShortName}"},
                                Description = verbOption.HelpText,
                                Hidden =verbOption.Hidden,
                                Required = verbOption.Required,
                                Group = verbOption.Group
                            };

                            if (command.Options == null)
                            {
                                command.Options = new Dictionary<string, OpenCliOption>();
                            }

                            command.Options.Add($"--{verbOption.LongName}", option);
                        }
                    }
                }

                // Todo: This method will likely duplicate options that were found when scanning for options inside Verb classes
                // CommandLineParser lets you add options to a class without a verb attribute, so this will currently duplicate options
                // into the top-level Document, which were found when adding options to the verb classes from before
                var typeProps = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach(var typeProp in typeProps)
                {
                    var optionAttributes = typeProp.GetCustomAttributes(typeof(OptionAttribute), false);
                    foreach (OptionAttribute attribute in optionAttributes)
                    {
                        OpenCliOption option = new OpenCliOption
                        {
                            Required = attribute.Required,
                            Description = attribute.HelpText,
                            Aliases = new List<string>() { $"-{attribute.ShortName}" },
                            Group = attribute.Group,
                            Hidden = attribute.Hidden,
                        };

                        if (document.Options == null)
                        {
                            document.Options = new Dictionary<string, OpenCliOption>();
                        }

                        document.Options.Add($"--{attribute.LongName}", option);
                    }
                }
              
            }

            return document;
        }
    }
}
