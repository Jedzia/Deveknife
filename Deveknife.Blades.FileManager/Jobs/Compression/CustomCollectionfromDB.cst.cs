using System.ComponentModel;
using CodeSmith.Engine;
using System.Text;
using System.Data;
using System;
using SchemaExplorer;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

public class CustomCollectionfromDBTemplate : CodeTemplate
{
	private bool _propertyFromCodeBehind = false;
	private string _nase="Hohoho";
	private SchemaExplorer.TableSchema _sourcetable;

	public void setCustomCollectionfromDBTemplate(SchemaExplorer.TableSchema sourcetable)
	{
		this._sourcetable = sourcetable;
	}

	//[CategoryAttribute("Options")]
	//[DescriptionAttribute("This property is inherited from the code behind file.")]
	//[DefaultValueAttribute(true)]
	//public bool PropertyFromCodeBehind
	//{
	//	get {return _propertyFromCodeBehind;}
	//	set {_propertyFromCodeBehind = value;}
	//}


	//[CategoryAttribute("Test")]
	//[DescriptionAttribute("This property is inherited from the code behind file.")]
	//public string Nase
	//{
	//	get {return _nase;}
	//	set {_nase = value;}
	//}



	public string GetSomething()
	{
		return "Something";
	}


	public string GetPropertyName(ColumnSchema column)
	{
	string propertyName = column.Name;

	if (propertyName == column.Table.Name + "Name") return "Name";
	if (propertyName == column.Table.Name + "Description") return "Description";

	if (propertyName.EndsWith("TypeCode")) propertyName = propertyName.Substring(0, propertyName.Length - 4);

	return propertyName;
	}

	public string GetMemberVariableDeclarationStatement(ColumnSchema column)
{
	return GetMemberVariableDeclarationStatement("protected", column);
}

public string GetMemberVariableDeclarationStatement(string protectionLevel, ColumnSchema column)
{
	string statement = protectionLevel + " ";
	statement += GetCSharpVariableType(column) + " " + GetMemberVariableName(column);

	string defaultValue = GetMemberVariableDefaultValue(column);
	if (defaultValue != "")
	{
		statement += " = " + defaultValue;
	}

	statement += ";";

	return statement;
}

public string GetCSharpVariableType(ColumnSchema column)
{
	//if (column.Name == "published") //.NativeType == "Bit"
	 //Debugger.Break();
	if (column.Name.EndsWith("TypeCode")) return column.Name;
	if (column.NativeType.ToString() == "bit") return "bool";
	if (column.NativeType.ToString() == "adSingle") return "float";
	if (column.NativeType.ToString() == "adDouble") return "double";
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		default:
		{
			return "__UNKNOWN__" + column.NativeType;
		}
	}
}



public string GetMemberVariableDefaultValue(ColumnSchema column)
{

	switch (column.DataType)
	{
		case DbType.Guid:
		{
			return "Guid.Empty";
		}
		case DbType.AnsiString:
		case DbType.AnsiStringFixedLength:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "String.Empty";
		}
		default:
		{
			return "";
		}
	}
}



	public string GetMemberVariableName(ColumnSchema column)
{
	string propertyName = GetPropertyName(column);
	string memberVariableName = "_" + GetCamelCaseName(propertyName);

	return memberVariableName;
}



	public string GetFormatArgs()
	{
			if (_sourcetable.Columns.Count == 0 )
			return string.Empty;

		StringBuilder builder = new StringBuilder();
		for (int x = 0; x < _sourcetable.Columns.Count; x++)
		{
			builder.AppendFormat("_{0}, ", StringUtil.ToCamelCase(GetPropertyName(_sourcetable.Columns[x])));
		}
		if (builder.Length > 2)
			builder.Remove(builder.Length - 2, 2);

		return builder.ToString();
	}

	public string GetCamelCaseName(string value)
{
	return value.Substring(0, 1).ToLower() + value.Substring(1);
}




public string GetReaderAssignmentStatement(ColumnSchema column, int index)
{
	string statement = "if (!reader.IsDBNull(" + index.ToString() + ")) ";
	statement += GetMemberVariableName(column) + " = ";

	if (column.Name.EndsWith("TypeCode")) statement += "(" + column.Name + ")";

	statement += "reader." + GetReaderMethod(column) + "(" + index.ToString() + ");";

	return statement;
}






public string GetReaderMethod(ColumnSchema column)
{

	switch (column.DataType)
	{
		case DbType.Byte:
		{
			return "GetByte";
		}
		case DbType.Int16:
		{
			return "GetInt16";
		}
		case DbType.Int32:
		{
			return "GetInt32";
		}
		case DbType.Int64:
		{
			return "GetInt64";
		}
		case DbType.AnsiStringFixedLength:
		case DbType.AnsiString:
		case DbType.String:
		case DbType.StringFixedLength:
		{
			return "GetString";
		}
		case DbType.Boolean:
		{
			return "GetBoolean";
		}
		case DbType.Guid:
		{
			return "GetGuid";
		}
		case DbType.Currency:
		case DbType.Decimal:
		{
			return "GetDecimal";
		}
		case DbType.DateTime:
		case DbType.Date:
		{
			return "GetDateTime";
		}
		case DbType.Binary:
		{
			return "GetBytes";
		}
		default:
		{
			return "__SQL__" + column.DataType;
		}
	}
}

public string GetClassName(TableSchema table)
{
	String name = table.Name;
	name = name.Substring(name.IndexOf("_")+1);

	if (table.Name.EndsWith("s"))
	{
		return name.Substring(0, name.Length - 1);
	}
	else
	{
		return name;
	}
/*
		if (table.Name.EndsWith("s"))
	{
		return table.Name.Substring(0, table.Name.Length - 1);
	}
	else
	{
		return table.Name;
	}
*/
}

public string GetSqlDbType(ColumnSchema column)
{

	switch (column.NativeType)
	{
		case "bigint": return "BigInt";
		case "binary": return "Binary";
		case "bit": return "Bit";
		case "char": return "Char";
		case "datetime": return "DateTime";
		case "decimal": return "Decimal";
		case "float": return "Float";
		case "image": return "Image";
		case "int": return "Int";
		case "money": return "Money";
		case "nchar": return "NChar";
		case "ntext": return "NText";
		case "numeric": return "Decimal";
		case "nvarchar": return "NVarChar";
		case "real": return "Real";
		case "smalldatetime": return "SmallDateTime";
		case "smallint": return "SmallInt";
		case "smallmoney": return "SmallMoney";
		case "sql_variant": return "Variant";
		case "sysname": return "NChar";
		case "text": return "Text";
		case "timestamp": return "Timestamp";
		case "tinyint": return "TinyInt";
		case "uniqueidentifier": return "UniqueIdentifier";
		case "varbinary": return "VarBinary";
		case "varchar": return "VarChar";
		default: return "__UNKNOWN__" + column.NativeType;
	}
}

public string GetPrimaryKeyType(TableSchema table)
{
	if (table.PrimaryKey != null)
	{
		if (table.PrimaryKey.MemberColumns.Count == 1)
		{
			return GetCSharpVariableType(table.PrimaryKey.MemberColumns[0]);
		}
		else
		{
			throw new ApplicationException("This template will not work on primary keys with more than one member column.");
		}
	}
	else
	{
		throw new ApplicationException("This template will only work on tables with a primary key.");
	}
}


		public enum AccessibilityEnum
	{
		Public,
		Protected,
		Internal,
		ProtectedInternal,
		Private
	}



	public string GetAccessModifier(AccessibilityEnum accessibility)
	{
		switch (accessibility)
		{
			case AccessibilityEnum.Public: return "public";
			case AccessibilityEnum.Protected: return "protected";
			case AccessibilityEnum.Internal: return "internal";
			case AccessibilityEnum.ProtectedInternal: return "protected internal";
			case AccessibilityEnum.Private: return "private";
			default: return "public";
		}
	}

	public string GetConstructorArgs()
	{
			if (_sourcetable.Columns.Count == 0 )
			return string.Empty;

		StringBuilder builder = new StringBuilder();
		for (int x = 0; x < _sourcetable.Columns.Count; x++)
		{
			builder.AppendFormat("{0} {1}, ", GetCSharpVariableType(_sourcetable.Columns[x]), StringUtil.ToPascalCase(GetPropertyName(_sourcetable.Columns[x])));
		}
		if (builder.Length > 2)
			builder.Remove(builder.Length - 2, 2);

		return builder.ToString();
	}

	public string GetPrimaryKey()
	{
	if( _sourcetable.HasPrimaryKey )
		return _sourcetable.PrimaryKey.MemberColumns[0].Name;

	return "ID";
	}

	public string GetPrimaryKeyType()
	{
	if( _sourcetable.HasPrimaryKey )
		return GetCSharpVariableType( _sourcetable.PrimaryKey.MemberColumns[0]);

	return "int";
	}

	public string GetPrimaryName()
	{
	if( _sourcetable.HasPrimaryKey ) {
		String name = _sourcetable.PrimaryKey.MemberColumns[0].Name;
		name = name.Substring(0,name.LastIndexOf("ID"));
		name = name + "Name";
	return name;
	}

	return "Name";
	}

	public string GetPrimaryName_c()
	{
		return "_"+StringUtil.ToCamelCase(  GetPrimaryName() );
	}
	public string GetPrimaryName_p()
	{
		return StringUtil.ToPascalCase(  GetPrimaryName() );
	}

	public string GetPrimary_c()
	{
		return "_"+StringUtil.ToCamelCase(  GetPrimaryKey() );
	}
	public string GetPrimary_p()
	{
		return StringUtil.ToPascalCase(  GetPrimaryKey() );
	}

	private readonly static DbType[] aIntegerDbTypes = new DbType[] {DbType.Byte, DbType.Int16, DbType.Int32, DbType.Int64 };

	/// <summary>
	/// Check a table for enum eligibility
	/// </summary>
	/// <param name="table">the table instance to check.</param>
	/// <exception name="ApplicationException"/>
	private void ValidForEnum(TableSchema table)
	{
		#region Primary key validation

		// No primary key
		if (!table.HasPrimaryKey)
		{
			throw new ApplicationException("The selected table has no primary key.");
		}

		// Multiple column in primary key
		if (table.PrimaryKey.MemberColumns.Count != 1)
		{
			throw new ApplicationException("table primary key contains more than one column.");
		}

		// Primary key column is not an integer
		if (!IsInteger(table.PrimaryKey.MemberColumns[0]))
		{
			throw new ApplicationException("table primary key column is not an integer. (used for enum value)");
		}

		#endregion

		#region Second column must be a string

		// The table must have 2 columns at least
		if (table.Columns.Count < 2)
		{
			throw new ApplicationException("Table must at least contains two columns, an integer or small int as the primary key, and a string.");
		}

		// The second column must be a string (char, varchar)
		if (table.Columns[1].SystemType != typeof(string))
		{
			throw new ApplicationException("Invalid format for Enum, 2nd column must be a string.");
		}
		#endregion
	}

	/// <summary>
	///	Indicates if a column is a Number type.
	/// </summary>
	private bool IsInteger(ColumnSchema column)
	{
		bool result = false;

		for(int i = 0; i < aIntegerDbTypes.Length; i++)
		{
			if (aIntegerDbTypes[i] == column.DataType)
			{
				if (column.DataType == DbType.Byte
					&& column.NativeType.ToLower() != "smallint")
					continue;

				result=true;
			}
		}

		return result;
	}

	///<summary>
	///  Clean the string of the row.
	///</summary>
	public string CleanValue(string val)
	{
		if (string.IsNullOrEmpty(val))
			return "NullOption";

		//Initial Formatting - trim whitespace
		val = val.Trim();

		//Replace non-word characters
		val = Regex.Replace(val,@"[\W]", "_");

		//Replace duplicate _ with a single instance
		val = Regex.Replace(val,@"_+","_");

		//Remove digit characters from start
		val = Regex.Replace(val,@"^[0-9]+","");

		//Strip any leading and trailing underscores
		val = val.TrimStart(new char[] {'_'}).TrimEnd(new char[] {'_'});

		//If completely numeric value, prefix with Option_
		if ( Regex.IsMatch(val,@"^[0-9]+$"))
			val = "Option_" + val;

		return val;
	}

	public string GetTableDescription(TableSchema table)
	{
		if (!string.IsNullOrEmpty(table.Description))
			return table.Description;
		else
			return string.Concat("This enumeration contains the items contained in the table " , table.Name);
	}

	public string GetMyTableData(SchemaExplorer.TableSchema table,string classn)
	{
		NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

		string returner ="";

		String summary ="";
		for(int j=0; j < table.Columns.Count; j++)
		{
			summary += String.Format("{0}, ",table.Columns[j].Name);

		}
		returner += "// Definitions: " + summary.Substring(0,summary.Length-2) + "\r\n\r\n";

		DataTable data = table.GetTableData();
		for(int i=0; i<data.Rows.Count; i++)
		{
			DataRow row = data.Rows[i];
			//string enumValue = CleanValue(row[1] as string);
			string enumValue =  "Add(new " +classn+"(";


		string rowline ="";

			for(int j=0; j < table.Columns.Count-1; j++)
			{
				string enumValue2="";
				//object xrow = myrow.ItemArray[i];
				object aaa = row[table.Columns[j].Name];
				
				object mxx = GetCSharpVariableType(table.Columns[j]);
				object mxxName = row.Table.Columns[j].ColumnName;
				//Debugger.Break();
				
				//Debug.WriteLine(row.Table.Columns[j].ColumnName + "  " + aaa.GetType() + "\r\n");
				if(aaa.GetType().ToString()=="System.String")
				{
					enumValue2 = string.Format("\"{0}\", ",row[table.Columns[j].Name]);
				}
				else if(GetCSharpVariableType(table.Columns[j]) == "decimal")
				{
					//enumValue2 ="moo";
					string fckw2 = row[table.Columns[j].Name].ToString();
					if(string.IsNullOrEmpty(fckw2))
						fckw2 = "0";
					decimal dez = decimal.Parse(fckw2);
					enumValue2 = string.Format("{0}M, ",dez.ToString(nfi));
					//Debug.WriteLine("AAAAAAAAAAAAAAAAH \r\n");
				}
				else if(GetCSharpVariableType(table.Columns[j]) == "double")
				{
					//Debugger.Break();
					string fckw = row[table.Columns[j].Name].ToString();
					if(string.IsNullOrEmpty(fckw))
						fckw = "0";
					//enumValue2 ="moo";
					double dbl = double.Parse(fckw);
					enumValue2 = string.Format("{0}d, ",dbl.ToString(nfi));
					//Debug.WriteLine("AAAAAAAAAAAAAAAAH \r\n");
				}
				else if(GetCSharpVariableType(table.Columns[j]) == "float")
				{
					//enumValue2 ="moo";
					float floa = float.Parse(row[table.Columns[j].Name].ToString());
					enumValue2 = string.Format("{0}f, ",floa.ToString(nfi));
					//Debug.WriteLine("AAAAAAAAAAAAAAAAH \r\n");
				}
				else if(GetCSharpVariableType(table.Columns[j])=="bool")
				{

					string boolstring2 = "false";
					if( row[table.Columns[j].Name].ToString() == "1")
						boolstring2 = "true";
					enumValue2 = string.Format("{0}, ",boolstring2);

				}
				else
				{
					enumValue2 = string.Format("{0}, ",row[table.Columns[j].Name]);
					if(enumValue2 ==", ")
						enumValue2 = "0, ";
				}
				rowline += enumValue2 ;

				if(false){
					object ble  = table.Columns[j];
					object blu  = row[table.Columns[j].Name];
					string test = GetCSharpVariableType(table.Columns[j]);
					Debug.WriteLine("vartype: " + test + "\r\n");
					Debugger.Break();
				}

				//if(row[table.Columns[j].Name].ToString() == "1"){
					//Debugger.Break();
				//}

			}

			int l = row.ItemArray.Length-1;


				if(false){
					object ble  = table.Columns[l];
					object blu  = row[table.Columns[l].Name];
					string test = GetCSharpVariableType(table.Columns[l]);
					Debug.WriteLine("vartype: " + test + "\r\n");
					Debugger.Break();
				}

				//if(GetCSharpVariableType(table.Columns[l])=="adDouble")
				object bla = GetCSharpVariableType(table.Columns[l]);

				string enumValue3="";
				//object xrow = myrow.ItemArray[i];
				if(GetCSharpVariableType(table.Columns[l])=="string")
					enumValue3 = string.Format("\"{1}\"",row.Table.Columns[l].ColumnName,row.ItemArray[l]);
				else if(GetCSharpVariableType(table.Columns[l])=="bool")
				{

					string boolstring = "false";
					if(row.ItemArray[l].ToString() == "1")
						boolstring = "true";


					enumValue3 = string.Format("{0} ",boolstring);

					object ble  = table.Columns[l];
					object blu  = row[table.Columns[l].Name];
					string test = GetCSharpVariableType(table.Columns[l]);
					//Debug.WriteLine("TEST: " + row.ItemArray[l].ToString() + "\r\n");
					//Debugger.Break();


				}
				else if(GetCSharpVariableType(table.Columns[l])=="float")
				{

					float floa = float.Parse(row.ItemArray[l].ToString());
					enumValue3 = string.Format("{0}f ",floa.ToString(nfi));
				}
				else
				{
				enumValue3 = string.Format("{1}",row.Table.Columns[l].ColumnName,row.ItemArray[l]);
				if(enumValue3 ==", ")
					enumValue3 = "0, ";
				}
				rowline += enumValue3 ;



			returner += enumValue + rowline + "));\r\n" ;
		}
		returner += "";
		return returner;
	}

	public string GetMyRowData(DataRow row)
	{
		DataRow myrow = row;
		string rowline ="";

		for(int i=0; i < myrow.ItemArray.Length; i++)
		{
			object xrow = myrow.ItemArray[i];
			//string enumValue2 = string.Format("{0}->{1} {2} | ",row.Table.Columns[i].ColumnName,xrow,
			//	GetCSharpVariableType(row.Table.Columns[i]));
			//rowline += enumValue2 ;
		}
		return rowline;
	}


}