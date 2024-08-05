﻿namespace WS.Gen.ObjectMother;

internal static class GenerateBuilder
{
	public static string GetTemplate(GeneratorInformation generatorInformation, string attributeNamespace)
	{
		var classInformation = generatorInformation.ClassInformation!;
		var interfaceInformation = generatorInformation.InterfaceInformation!;
		var builderName = classInformation.ClassName;

		string fieldBackers = GetAllFieldBackers(interfaceInformation);
		string withProperties = GetAllWithProperties(builderName, interfaceInformation);
		string builderConstructor = GetConstructor(interfaceInformation);
		string typical = GetTypical(builderName, interfaceInformation);

		return $@"
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
#nullable disable

// ================================================================================
// <auto-generated>
//
//  This code was generated by a Source Code Generator
//  Changes to this file may cause incorrect behavior and will be lost if
//  the code is regenerated.
//
// </auto-generated>
// ================================================================================

using WS.Gen.ObjectMother;

namespace {classInformation!.Namespace};

public partial class {classInformation.ClassName} : Builder<{interfaceInformation!.ClassName}>
{{
{fieldBackers}

    public override {interfaceInformation.ClassName} Build()
    {{
        if (BuilderObject?.IsValueCreated != true)
        {{{builderConstructor}
		}}

        PostBuild(BuilderObject.Value);

        return BuilderObject.Value;
    }}


    //
    //  Default Constructor
    //

    public static {classInformation.ClassName} Default()
    {{
        return new {classInformation.ClassName}();
    }}


    //
    //  With Properties
    //
{withProperties}
}}

{typical}
#nullable restore
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS8669 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context. Auto-generated code requires an explicit '#nullable' directive in source.
";
	}


	public static string GetAllFieldBackers(InterfaceInformation interfaceInformation)
	{
		var sb = new StringBuilder(10000);

		foreach (ClassMember item in interfaceInformation.Properties)
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.AppendLine(GetFieldBackers(item));
		}

		if (interfaceInformation.Inherited != null)
			sb.AppendLine(GetAllFieldBackers(interfaceInformation.Inherited));

		return sb.ToString();
	}


	public static string GetFieldBackers(ClassMember property)
	{
		return $"\tprivate Lazy<{property.DataType}> {property.FieldName} = new (default({property.DataType}));";
	}


	private static string GetAllObjectAssignment(List<ClassMember> properties, bool includeObjectName = false)
	{
		StringBuilder sb = new StringBuilder(10000);

		foreach (ClassMember item in properties)
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.AppendLine(ConstructorPropertyAssignment(item, includeObjectName));
		}

		if (!includeObjectName && sb.Length > 3)
			sb.Length -= (NewlineLength() + 1);

		return sb.ToString();
	}


	private static string ConstructorPropertyAssignment(ClassMember property, bool includeObjectName = false)
	{
		return includeObjectName
			? $"\t\t\t\t\tobj.{property.PropertyName} = {property.FieldName}.Value;"
			: $"\t\t\t\t{property.PropertyName} = {property.FieldName}.Value,";
	}


	private static string GetWithProperties(string builderName, ClassMember property)
	{
		return $@"
    public {builderName} With{property.PropertyName}({property.DataType} value)
    {{
        return With{property.PropertyName}(() => value);
    }}
    
    public {builderName} With{property.PropertyName}(Func<{property.DataType}> func)
    {{
        {property.FieldName} = new Lazy<{property.DataType}>(func);
        return this;
    }}
";
	}


	private static string GetRemoveProperties(string builderName, ClassMember property)
	{
		return $@"
    public {builderName} SetDefault{property.PropertyName}({property.DataType} newValue = default)
    {{
		{property.FieldName} = new Lazy<{property.DataType}>(newValue);
        return this;
    }}
";
	}

	private static string GetAllWithProperties(string builderName, InterfaceInformation interfaceInformation)
	{
		StringBuilder sb = new StringBuilder(10000);

		foreach (ClassMember item in interfaceInformation.Properties)
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.Append(GetWithProperties(builderName, item));
			sb.Append(GetRemoveProperties(builderName, item));
		}

		if (interfaceInformation.Inherited != null)
			sb.AppendLine(GetAllWithProperties(builderName, interfaceInformation.Inherited));

		if (sb.Length > NewlineLength())
			sb.Length -= NewlineLength();

		return sb.ToString();
	}


	private static string GetConstructor(InterfaceInformation interfaceInformation)
	{
		var constructors = interfaceInformation.Constructors
			.OrderBy(c => -((int)c.Accessibility))
			.ThenBy(c => c.Parameters.Count)
			.ToList();

		foreach (var c in constructors)
		{
			if (c.IsParameterless)
				return CreateParameterlessConstructor(c, interfaceInformation);

			if (FoundMatchingParameters(c, interfaceInformation))
				return CreateParameterConstructor(c, interfaceInformation);
		}


		return $@"{GetConstructorOverride(interfaceInformation)}";
	}



	private static bool FoundMatchingParameters(ConstructorInformation c, InterfaceInformation interfaceInformation)
	{
		return c.Parameters.All(p =>
		{
			var classParameter = interfaceInformation.Properties.FirstOrDefault(
				cp => string.Equals(cp.PropertyName, p.ParameterName, StringComparison.InvariantCultureIgnoreCase));

			return classParameter != null && classParameter.DataType == p.DataType;
		});
	}



	private static string CreateParameterlessConstructor(ConstructorInformation c, InterfaceInformation interfaceInformation)
	{
		var isPrivate = c.Accessibility == Accessibility.Private;
		var properties = GetAllProperties(interfaceInformation);

		string objectAssignment = GetAllObjectAssignment(properties, isPrivate);

		if (isPrivate)
		{
			return $@"
			#if SomethingRandom

			// Please copy the following override into the non generated Builder class

			protected override Lazy<{interfaceInformation.ClassName}> Construct()
			{{
				return new Lazy<{interfaceInformation.ClassName}>(() =>
				{{
					var obj = CreateInstance();

{objectAssignment}
					return obj;
				}});
			}}

			#endif

			BuilderObject = Construct()
";
		}

		return $@"
			// Public
			BuilderObject = new Lazy<{interfaceInformation.ClassName}>(new {interfaceInformation.ClassName}
			{{
{objectAssignment}
			}});";

	}


	private static List<ClassMember> GetAllProperties(InterfaceInformation interfaceInformation)
	{
		var list = new List<ClassMember>();
		var temp = interfaceInformation;

		while (temp != null)
		{
			list.AddRange(temp.Properties);
			temp = temp.Inherited;
		}

		return list;
	}


	private static string CreateParameterConstructor(ConstructorInformation c, InterfaceInformation interfaceInformation)
	{
		var isPrivate = c.Accessibility == Accessibility.Private;

		// Match Constructor Parameter to Property
		var properties = GetAllProperties(interfaceInformation);
		var parameters = c.Parameters.Select(c =>
			new KeyValuePair<ConstructorParameters, ClassMember>(c,
				properties.First(p => string.Equals(p.PropertyName, c.ParameterName, StringComparison.CurrentCultureIgnoreCase))))
			.ToList();

		// Exclude Constructor Parameters from being set again
		var exclusions = parameters.Select(p => p.Value);
		properties = properties.Except(exclusions).ToList();

		string ctorParameters = GetConstructorParameters(parameters);
		string objectAssignment = GetAllObjectAssignment(properties, isPrivate);


		if (isPrivate)
		{
			return $@"
			#if SomethingRandom

			// Please copy the following override into the non generated Builder class

			protected override Lazy<{interfaceInformation.ClassName}> Construct()
			{{
				return new Lazy<{interfaceInformation.ClassName}>(() =>
				{{
					var obj = CreateInstance({ctorParameters});

{objectAssignment}
					return obj;
				}});
			}}

			#endif

			BuilderObject = Construct()
";
		}

		return $@"
			// Public
			BuilderObject = new Lazy<{interfaceInformation.ClassName}>(new {interfaceInformation.ClassName}({ctorParameters})
			{{
{objectAssignment}
			}});";

	}


	private static string GetConstructorParameters(List<KeyValuePair<ConstructorParameters, ClassMember>> parameters)
	{
		var sb = new StringBuilder(500);

		var count = parameters.Count;

		for (var i = 0; i < count - 1; i++)
			sb.AppendFormat("{0}.Value, ", parameters[i].Value.FieldName);

		sb.AppendFormat("{0}.Value", parameters[count - 1].Value.FieldName);

		return sb.ToString();
	}


	private static string GetConstructorOverride(InterfaceInformation interfaceInformation)
	{
		return $@"
			#if SomethingRandom
			
			// Can't find constructor

			// Please Override Construct() in the non generated Builder class

			// You will need something like the following

			protected override Lazy<{interfaceInformation.ClassName}> Construct()
			{{
				return new Lazy<{interfaceInformation.ClassName}>(() =>
				{{
					var obj = CreateInstance(_parm1.Value, _parm2.Value, _parm3.Value);

					obj.Parm4 = _parm4.Value;
					obj.Parm5 = _parm5.Value;

					return obj;
				}});
			}}

			#endif

			BuilderObject = Construct();
";


	}


	private static string GetTypical(string builderName, InterfaceInformation interfaceInformation)
	{
		if (!interfaceInformation.GenerateSample)
			return string.Empty;

		var sb = new StringBuilder(1000);

		var properties = GetAllProperties( interfaceInformation)
			.Where(p => !p.IsReadOnly && p.Accessibility == Accessibility.Public)
			.ToList();

		properties.ForEach(p => sb.AppendLine($"\t\t\t.With{p.PropertyName}(default({p.DataType}))"));
		sb.Length -= NewlineLength();


		return $@"
/*

	public static {builderName} Typical()
	{{
		return new {builderName}()
{sb}
			;
	}}

*/
";
	}





	private static int _newlineLength;

	private static int NewlineLength()
	{
		if (_newlineLength == 0)
		{
			var sb = new StringBuilder();
			sb.AppendLine("");
			_newlineLength = sb.Length;
		}

		return _newlineLength;
	}
}
