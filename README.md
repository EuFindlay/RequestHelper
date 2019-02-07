# RequestHelper
NetCore.RequestHelper is reflection based tool that simplifies the proccess of passing data to API methods

### Serialization of an data objects to an URL encoded string
#### 1. Direct from object
```csharp
 // model is an object that should be converted to an URL string
RequestParameters parametersCollection = RequestParameters.CreateFromModel(model);
string urlParameters = parametersCollection.ToString();
```

#### 2. With multiple parameters / object
```csharp
int value = 1;
string text = "test";

RequestParameters parametersCollection = new RequestParameters();
parametersCollection.Add(value);
parametersCollection.Add(text);
string urlParameters = parametersCollection.ToString();
````
### Features
- Supports various data types: Lists, nullable types, custom models, etc
- Supports embeded models
- Has set of custom attributes for better configuration
