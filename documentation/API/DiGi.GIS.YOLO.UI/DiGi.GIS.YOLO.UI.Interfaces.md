#### [DiGi\.GIS\.YOLO\.UI](DiGi.GIS.YOLO.UI.Overview.md 'DiGi\.GIS\.YOLO\.UI\.Overview')

## DiGi\.GIS\.YOLO\.UI\.Interfaces Namespace
### Interfaces

<a name='DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject'></a>

## IGISYOLOUIObject Interface

Represents an object within the GIS YOLO UI domain\.

```csharp
public interface IGISYOLOUIObject : DiGi.Core.Interfaces.IObject
```

Derived  
↳ [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions')  
↳ [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult')  
↳ [IGISYOLOUISerializableObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUISerializableObject')

Implements [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject')

<a name='DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUISerializableObject'></a>

## IGISYOLOUISerializableObject Interface

Represents a serializable object within the GIS YOLO UI domain\.

```csharp
public interface IGISYOLOUISerializableObject : DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject, DiGi.Core.Interfaces.IObject, DiGi.Core.Interfaces.ISerializableObject, DiGi.Core.Interfaces.ICloneableObject<DiGi.Core.Interfaces.ISerializableObject>, DiGi.Core.Interfaces.ICloneableObject
```

Derived  
↳ [YearBuiltPredictionPipelineOptions](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionPipelineOptions 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionPipelineOptions')  
↳ [YearBuiltPredictionResult](DiGi.GIS.YOLO.UI.Classes.md#DiGi.GIS.YOLO.UI.Classes.YearBuiltPredictionResult 'DiGi\.GIS\.YOLO\.UI\.Classes\.YearBuiltPredictionResult')

Implements [IGISYOLOUIObject](DiGi.GIS.YOLO.UI.Interfaces.md#DiGi.GIS.YOLO.UI.Interfaces.IGISYOLOUIObject 'DiGi\.GIS\.YOLO\.UI\.Interfaces\.IGISYOLOUIObject'), [DiGi\.Core\.Interfaces\.IObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iobject 'DiGi\.Core\.Interfaces\.IObject'), [DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject'), [DiGi\.Core\.Interfaces\.ICloneableObject&lt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1')[DiGi\.Core\.Interfaces\.ISerializableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.iserializableobject 'DiGi\.Core\.Interfaces\.ISerializableObject')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject-1 'DiGi\.Core\.Interfaces\.ICloneableObject\`1'), [DiGi\.Core\.Interfaces\.ICloneableObject](https://learn.microsoft.com/en-us/dotnet/api/digi.core.interfaces.icloneableobject 'DiGi\.Core\.Interfaces\.ICloneableObject')