# RequestHelper
NetCore.RequestHelper is reflection based tool that simplifies the proccess of passing data to API methods

[Sample of using RequestHelper](https://github.com/EuFindlay/RequestHelper/tree/master/Samples/RequestHelperSample "Sample")

### Features
- Supports various data types: Lists, nullable types, custom models, etc
- Supports embeded models
- Has set of custom attributes for better configuration

# Documentation 

### Serialization of data objects to an URL encoded string
#### 1. Directly from an object
```csharp
string requestUrl = "http://test-url.com";

 // model is an object that should be converted to an URL string
RequestParameters parametersCollection = RequestParameters.CreateFromModel(model);
parametersCollection.AddParametersToUrl(ref requestUrl);
```

#### 2. With multiple parameters / objects
```csharp
int value = 1;
string text = "test";
string requestUrl = "http://test-url.com";

RequestParameters parametersCollection = new RequestParameters();
parametersCollection.Add(value);
parametersCollection.Add(text);

parametersCollection.AddParametersToUrl(ref requestUrl);
````

### Posting files to API

#### A test model with a file property
```csharp
    public class PhotoModel
    {
        public int StudentId { get; set; }
		
        [FileParameter] // required attribute for File property
        public IFormFile Photo { get; set; }  //file property
    }
````

#### Posting a model to API from MVC Controller
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
