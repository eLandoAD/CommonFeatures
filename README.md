# Package for Azure Service Bus

use nuget.org: 


This package provides an easy configuration Azure Service Bus.

## Topics:
For both sides you will need start-up configuration
 
### Publisher

#### 1. Add Service Bus Connection string in to appsettings.json
```json
{
    "ConnectionStrings": {
          "DefaultConnection": ...,
          "ServiceBusConnection": "AzureServiceBus.Conection.String"
  },
  ... 
}
````

#### 2. Add Configuration on start up ( Priogaram.cs) 
````csharp
builder.Services.AddAzureClients(opt =>
{
    opt.AddServiceBusClient(builder.Configuration.GetConnectionString("ServiceBusConnection"));
});
...
builder.services.AddSingleton<ServiceBusPublisher>();
...
````

#### 3. Add into code
````csharp
// add as field
private readonly ServiceBusPublisher _publisher;

//inject into ctor
 ...
````
#### Use it:

````csharp
... YourMethod(){
    ...
    await serviceBusPublisher.Publish<MyClass>(objectOfMyClass);
}
````
##### OR include traceId
````csharp
... YourMethod(){
    ...
    await serviceBusPublisher.Publish(new MessageWithTraceId (objectOfMyClass, requestId));
}
````
### Subscriber 
