# Nettle
Nettle is a .NET Templating Language (.**_Net_**-**_T_**emplating-**_L_**anguag**_E_**) designed as a lightweight solution to solving content rendering problems faced in .NET applications.

Using Nettle is simple, there are just 5 core areas to learn:

# Pointers
These are essentially the names of properties contained in the model. A string representation of the properties value will replace the pointer. The syntax for a pointer is as follows:

```
{{Name}}
```

Nested properties are also supported:

```
{{User.Identity.Name}}
```

# Iteration

For each loops are supported with any pointer that is of type IEnumerable. The syntax of a for each loop is as follows:

```
{{foreach RoleAssignments}}
	<p>Role {{RoleName}}</p>
{{endfor}}
```

Which would generate something like:

```
<p>Role Admin<p>
<p>Role Customer<p>
```

Nested for loops are also supported:

```
{{foreach Users}}
	<p>User {{UserName}}</p>

	{{foreach RoleAssignments}}
		<p>Role {{RoleName}}</p>
	{{endfor}}
{{endfor}}
```

# Selection

If statements are supported with any pointer that is either of type bool or can be resolved as true or false. The syntax for an if statement is as follows:

```
{{if Active}}
	<p>Currently Active</p>
{{endif}}
```

Nested if statements are also supported:

```
{{if Active}}
	<p>Currently Active</p>
	
	{{if HasProfile}}
		<p>The user has a profile</p>
	{{endif}}
{{endif}}
```

# Functions

Functions can take zero or more parameters, which can be a string, number, pointer or variable. The syntax for a function is as follows:

```
@Truncate("Hello World!", 5)
```

Which would generate:

```
Hello
```

There are various built in functions, these are:

- @Truncate(Text, Length)
- @Replace(OriginalText, FindText, ReplaceText)
- @HtmlEncode(Text)

# Variables

The value of a pointer or the result of a function can be assigned to a variable. This gets added to the model and can then be used later as another pointer. The syntax for a variable declaration and assignment is as follows:

```
{{var truncatedText = @Truncate("Here is some text to truncate.", 10)}}

{{truncatedText}}
```

Which would generate:

```
Here is so
```
