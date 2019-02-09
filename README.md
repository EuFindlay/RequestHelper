# RequestHelper
NetCore.RequestHelper is reflection based tool that simplifies the proccess of passing data to API methods

[Sample of using RequestHelper](https://github.com/EuFindlay/RequestHelper/tree/master/Samples/RequestHelperSample "Sample")

### Features
- Supports various data types: Lists, nullable types, custom models, etc
- Supports embeded models
- Has set of custom attributes for better configuration

# Documentation 

### Serialization of an data objects to an URL encoded string
#### 1. Direct from object
```csharp
 // model is an object that should be converted to an URL string
RequestParameters parametersCollection = RequestParameters.CreateFromModel(model);
string urlParameters = parametersCollection.ToString();
```

#### 2. With multiple parameters / objects
```csharp
int value = 1;
string text = "test";

RequestParameters parametersCollection = new RequestParameters();
parametersCollection.Add(value);
parametersCollection.Add(text);
string urlParameters = parametersCollection.ToString();
````

### Posting files to API

#### Test model with file property
```csharp
    public class PhotoModel
    {
        public int StudentId { get; set; }
		
        [FileParameter] // required attribute for File property
        public IFormFile Photo { get; set; }  //file property
    }
````

#### Posting model to API from MVC Controller
```csharp
        public async Task<IActionResult> LoadStudentPhoto(PhotoModel request)
        {
            var httpClient = new HttpClient();
        
            // converting model to MultipartFormDataContent
            var dataModel = MultipartFormDataBuilder.ConvertModelToFormData(request);
            await httpClient.PostAsync("https://api-url.com", dataModel); // posting model to API
			
            return Ok();
        }
```
