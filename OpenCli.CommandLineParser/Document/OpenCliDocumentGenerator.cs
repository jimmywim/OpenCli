using CommandLine;
using OpenCli.Document;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCli.CommandLineParser.Document
{
    public class OpenCliDocumentGenerator
    {
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
                            };

                            command.Options.Add($"--{verbOption.LongName}", option);
                        }
                    }
                }

                // Todo: This method will likely duplicate options that were found when scanning for options inside Verb classes
                // CommandLineParser lets you add options to a class without a verb attribute, so this will currently duplicate options
                // into the top-level Document, which were found when adding options to the verb classes from before
                var optionAttributes = type.GetCustomAttributes(typeof(OptionAttribute), false);
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
                    
                    document.Options.Add($"--{attribute.LongName}", option);
                }
            }

            return document;
        }
    }
}
