namespace WS.Gen.ObjectMother;

public record ConstructorGenerator(
	string ClassName,
	ConstructorInformation? Constructor,
	List<ConstParm> Parameters,
	List<IClassMemberInfo> ConstructorParameters,
	List<IClassMemberInfo> Fields)
{
	private readonly StringBuilder sb = new (10000);


	public override string ToString()
	{
		if (Constructor == null)
			return GetUnknownConstructor(ClassName);

		return Constructor!.Accessibility switch
		{
			Accessibility.Public => GetPublicConstructor(),
			Accessibility.Private => GetPrivateConstructor(),
			_ => GetPrivateConstructor()
		};
	}


	private static string GetUnknownConstructor(string className)
	{
		return $@"
	#if SomethingRandom
			
	// Can't find constructor

	// Please Override Construct() in the non generated Builder class

	// You will need something like the following

	protected override {className} Construct()
	{{
		var obj = CreateInstance(_parm1(), _parm2(), _parm3());

		obj.Parm4 = _parm4();
		obj.Parm5 = _parm5();

		return obj;
	}}

	#endif
";
	}


	private string GetPublicConstructor()
	{
		var p = GetObjectInitializationMembers(3);
		var f = GetMemberAssignment(3, Fields);
		var semiColon = string.IsNullOrWhiteSpace(p) && string.IsNullOrWhiteSpace(f) ? ";" : string.Empty;

        return $@"	// Public

	protected override {ClassName} Construct()
	{{		
		var obj = new {ClassName}{GetInstantionParameters()}{p}{f}{semiColon}
		return obj;
	}}
";
	}


	private string GetPrivateConstructor()
	{
		var p = GetMemberAssignment(3, ConstructorParameters);
		var f = GetMemberAssignment(3, Fields);

		return $@"	// Private

	protected override {ClassName} Construct()
	{{		
		var obj = CreateInstance({GetInstantionParameters()});{p}{f}
		return obj;
	}}
";
	}


	private string GetInstantionParameters()
	{
		var parameters = Parameters.OrderBy(p => p.ConstructorParameter.Order);

		var text =  string.Join(", ", parameters.Select(p => $"{p.ClassMemberInfo.FieldName}()"));

		return string.IsNullOrWhiteSpace(text)
			? string.Empty
			: $"({text})";
	}


	private string GetObjectInitializationMembers(int tabCount)
	{
		if (!ConstructorParameters.Any())
			return string.Empty;

		return
		$@"
		{{
{GetMemberAssignment(false, tabCount, ConstructorParameters)}
		}};
";
	}


	private string GetMemberAssignment(int tabCount, IEnumerable<IClassMemberInfo> members)
		=> !members.Any()
			? string.Empty
			: $@"
{GetMemberAssignment(true, tabCount, members)}";


	private string GetMemberAssignment(bool includeObjectName, int tabCount, IEnumerable<IClassMemberInfo> members)
	{
		sb.Clear();

		foreach (var item in members)
		{
			if (item.IsReadOnly || item.Accessibility != Accessibility.Public)
				continue;

			sb.AppendLine(GetMemberAssignment(includeObjectName, tabCount, item));
		}

		// Take newline and optionally last comma
		if (!includeObjectName && sb.Length > 3)
			sb.Length -= Utility.NewlineLength + 1;

		return sb.ToString();
	}


	private static string GetMemberAssignment(bool includeObjectName, int tabCount, IClassMemberInfo member)
	{
		return includeObjectName
			? $"{Utility.Tabs(tabCount - 1)}obj.{member.Name} = {member.FieldName}();"
			: $"{Utility.Tabs(tabCount)}{member.Name} = {member.FieldName}(),";
	}




	//
	//	Code required to create an instance of ConstructorGenerator
	//

	public static string Generate(InterfaceInformation interfaceInformation)
	{
		var classMembers = interfaceInformation.GetMembers()
			.Where(m => m.Accessibility == Accessibility.Public && m.IsReadOnly == false)
			.Distinct()
			.GroupBy(c => c.Name.ToLower()).ToDictionary(grp => grp.Key, grp => grp.ToList());

		var constructor = FindConstructor(interfaceInformation, classMembers);
		var constAll = CreateConstructorData(interfaceInformation.ClassName, constructor, classMembers);

		return constAll.ToString();
	}


	private static ConstructorInformation FindConstructor(InterfaceInformation interfaceInformation, Dictionary<string, List<IClassMemberInfo>> classMemberInfo)
		=> interfaceInformation.Constructors
			.FirstOrDefault(c => c.IsParameterless || MatchedAllParameters(c, classMemberInfo));


	private static bool MatchedAllParameters(ConstructorInformation c, Dictionary<string, List<IClassMemberInfo>> classMemberInfo)
		=> c.ConstructorParameters.All(kvp => classMemberInfo.TryGetValue(kvp.Key, out var member) && member.Any(m => m.DataType == kvp.Value.DataType));


	private static ConstructorGenerator CreateConstructorData(string className, ConstructorInformation constructor, Dictionary<string, List<IClassMemberInfo>> classMembers)
	{
		// Match Constructor Parameter to Property
		var parameters = constructor?.ConstructorParameters
			.Select(c => new ConstParm(c.Value, classMembers.First(m => m.Key == c.Key).Value.First(m => m.DataType == c.Value.DataType)))
			.ToList();

		parameters ??= [];

		// Exclude Constructor Parameters from being set again
		var exclusions = parameters.Select(p => p.ClassMemberInfo);
		var variables = classMembers.Values.SelectMany(m => m).Except(exclusions).ToList();

		var properties = variables.Where(v => v.ClassMemberType == ClassMemberType.Property).ToList();
		var fields = variables.Where(v => v.ClassMemberType == ClassMemberType.Field).ToList();

		return new ConstructorGenerator(className, constructor, parameters, properties, fields);
	}
}


public record ConstParm(ConstructorParameters ConstructorParameter, IClassMemberInfo ClassMemberInfo);
