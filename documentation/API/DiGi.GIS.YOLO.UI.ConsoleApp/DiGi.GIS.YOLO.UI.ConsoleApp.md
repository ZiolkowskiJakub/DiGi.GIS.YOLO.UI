#### [DiGi\.GIS\.YOLO\.UI\.ConsoleApp](DiGi.GIS.YOLO.UI.ConsoleApp.Overview.md 'DiGi\.GIS\.YOLO\.UI\.ConsoleApp\.Overview')

## DiGi\.GIS\.YOLO\.UI\.ConsoleApp Namespace
### Classes

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program'></a>

## Program Class

Provides the main entry point for the headless YOLO Year Built prediction runner\.

```csharp
public static class Program
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') → Program
### Methods

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.Main(string[])'></a>

## Program\.Main\(string\[\]\) Method

Executes the headless Year Built prediction pipeline from command\-line arguments\.

```csharp
public static System.Threading.Tasks.Task<int> Main(string[] args);
```
#### Parameters

<a name='DiGi.GIS.YOLO.UI.ConsoleApp.Program.Main(string[]).args'></a>

`args` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

Optional arguments\. The first argument specifies the path to the options JSON file\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32 'System\.Int32')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
An exit code indicating the result:
- 0: Pipeline executed successfully.
- 1: Configuration or argument validation error.
- 2: Preflight environment check failed.
- 3: WebAPI key or client configuration missing.
- 4: Pipeline execution failure.
- 5: Execution cancelled by user.