using A_ThrowAwayApp.Models;

namespace A_ThrowAwayApp;

internal partial class Program
{
    static void Main(string[] args)
    {
		var text = Run();
		Trace.WriteLine(text);
    }
}


internal partial class Program
{
	public static string Run()
	{
		GeneratorInformation generatorInformation = new GeneratorInformation
		{
			ClassInformation = new ClassInformation
			{
				Namespace = "RandomTestDataApp.Generator",
				ClassName = "PostalAddressBuilder"
			},
			InterfaceInformation = new ClassInformation
			{
				Namespace = "RandomTestDataApp",
				ClassName = "PostalAddress",

				Properties = new()
				{
					new PropertyInformation { DataType = "string", PropertyName = "Address1"   },
					new PropertyInformation { DataType = "string", PropertyName = "Address2"   },
					new PropertyInformation { DataType = "string", PropertyName = "City"       },
					new PropertyInformation { DataType = "int",    PropertyName = "StateId"    },
					new PropertyInformation { DataType = "string", PropertyName = "PostalCode" },
					new PropertyInformation { DataType = "List<string>", PropertyName = "People", IsCollection = true }
				}
			}
		};


		return FileGenerator.GetTemplate(generatorInformation, 1);
	}
}