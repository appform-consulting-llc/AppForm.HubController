# AppForm.HubController

The Hub Controller is an implementation of a MVC style controller pattern that you're probably familiar with for the SignlaR Hub. It makes managing socket commands much simpler when dealing with large or small projects. With the HubController you are now able to quickly and easily separate out the handling of your individual commands into their own logical controllers. 

## Get Started

#### Server Side

Getting started is quite simple. First you will want to edit your Startup.cs file to include the following.

```csharp
public void ConfigureServices(IServiceCollection services)
{
  ...
  
  services.UseHubRouter();
  
  ...
}
```

Next create a new class that inherits from the `BaseHubController` class. This uses the full dependency injection chain under the hood so feel free to include any required services you need.

```csharp
public class DemoHubController : BaseHubController
{
  private readonly MyService _myService;

  public DemoHubController(MyService myService)
  {
  	_myService = myService;
  }

  public async Task<List<string>> GetNames()
  {
  	return await _myService.GetNames();
  }
}
```

Lastly we will need to modify at least one SignalR `Hub` to enable it to use our new HubController. To do this, simply inherit from the `AppFormHub` class instead of the base SignalR `Hub` class.

```csharp
public class AppHub : AppFormHub
{
	...
}
```

#### Client Side (Javascript)

See the [AppForm.HubController Client Documentation](https://www.npmjs.com/package/@appformllc/hubcontroller).

#### Summary

Your new controller method can now be called by simply calling a single method client side now using async / promises. No more callback waiting management.

```javascript
hubConnection.execute('demo/getNames')
  .then(result => {
    console.log('success!', result);
  })
  .catch(error => {
    console.error('uh oh!', error);
  });
```
 
 or

```javascript
var result = await hubConnection.execute('demo/getNames');
````