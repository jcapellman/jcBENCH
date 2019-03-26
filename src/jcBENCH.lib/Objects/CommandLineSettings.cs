using System.Linq;

namespace jcBENCH.lib.Objects
{
    public class CommandLineSettings
    {
        public bool EnableMT { get; set; }

        public CommandLineSettings(string[] args)
        {
            var properties = typeof(CommandLineSettings).GetProperties();

            foreach (var property in properties)
            {
                if (!args.Contains(property.Name))
                {
                    continue;
                }

                property.SetValue(this, true);
            }
        }
    }
}