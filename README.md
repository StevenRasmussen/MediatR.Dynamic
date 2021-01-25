# MediatR.Dynamic
Extends the MediatR concept to include the ability to dynamically (runtime) add/remove `INotificationHandler`'s

# Example
``` csharp
// Setup (at time of startup)
// Assuming you are using Microsoft.DependencyInjection
services.AddMediatRDynamic(); // Extension method found in 'MediatR.Dynamic' namespace

// Also add this for each message type you want to handle dynamically
services.AddDynamicNotificationHandler<YourNotificationTypeHere>();
 
// Create a class that implements 'IDynamicNotificationHandler'
class MyDynamicNotificationHandler: IDynamicNotificationHandler<YourNotificationTypeHere>
{
    private readonly IDynamicNotificationRegistrar<YourNotificationTypeHere>> _registrar;

    // Pass in the registrar in order to register/unregister the handler
    public MyDynamicNotificationHandler(IDynamicNotificationRegistrar<YourNotificationTypeHere>> registrar)
    {
        _registrar = registrar;

        // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
        _registrar.RegisterHandler(this);
    }

    ~MyDynamicNotificationHandler()
    {
        // Un-register this class as an event handler for the notification type
        _registrar.UnRegisterHandler(this);
    }

    public Task Handle(YourNotificationTypeHere notification, CancellationToken cancellationToken) 
    {
        // Your code here
    }
}
```
