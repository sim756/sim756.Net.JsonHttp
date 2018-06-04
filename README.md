# sim756.Net.JsonHttp

#### **JsonHttpClient**

A simplified Generic method to GET or POST JSON using HTTP, & get it deserialized or serialized into the Object of specified type. 

###### Example:

```c#
Sample sample = new JsonHttpClient<Sample>("http://localhost:10000/api/values").Deserialize(); 
```

###### More Examples:
```c#
//1 Examples - JsonHttpClient<T>
//1.1
example = new JsonHttpClient<Example>("http://www.example.com").Deserialize();

//1.2
example = new JsonHttpClient<Example>("http://www.example.com", webClient).Deserialize();

//1.3
example = new JsonHttpClient<Example>("http://www.example.com", new WebClient()
{
    BaseAddress = "http://www.example.com",
    Encoding = Encoding.Unicode,
    Credentials = new Credential()
}).Deserialize();

//1.4
example = new JsonHttpClient<Example>().Deserialize("http://www.example.com");

//1.5
example = new JsonHttpClient<Example>().Deserialize("http://www.example.com", webClient);

//1.6
example = new JsonHttpClient<Example>()
{
    Url = "http://www.example.com",
    WebClient = webClient
}.Deserialize();

//1.7
JsonHttpClient<Example> jsonHttpClient = new JsonHttpClient<Example>("http://www.example.com");
jsonHttpClient.DeserializeInside();

int uid = jsonHttpClient.Object.Uid;
string property1 = jsonHttpClient.Object.Property1;
bool property2 = jsonHttpClient.Object.Property2;


//2 Examples - JsonHttpClient
//2.1            
example = JsonHttpClient.Deserialize<Example>("http://www.example.com");

//2.2
example = JsonHttpClient.Deserialize<Example>("http://www.example.com", webClient);

//2.3
example = JsonHttpClient.DeserializeString<Example>("....JSON....");

//2.4
json = JsonHttpClient.Get("http://www.example.com");

//2.5
json = JsonHttpClient.Get("http://www.example.com", new System.Net.WebClient()
{
    BaseAddress = "http://www.example.com",
    Encoding = Encoding.Unicode,
    Credentials = new Credential()
});

//2.6
json = JsonHttpClient.Serialize(new Example()
{
    Uid = 756,
    Property1 = "sim756",
    Property2 = true
});
```

#### Source

> [https://github.com/sim756/sim756.Net.JsonHttp](https://github.com/sim756/sim756.Net.JsonHttp)

#### Other Sources

> [https://github.com/sim756](https://github.com/sim756)

#### Author

Sadequl Islam Mithun

> [https://www.sim756.com](https://www.sim756.com)
>
> [https://facebook.com/sim756](https://facebook.com/sim756)
>
> [https://twitter.com/sim756](https://twitter.com/sim756)
>
> [https://www.linkedin.com/in/sim756](https://www.linkedin.com/in/sim756)

