using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.Dynamic.Test
{
    public static class TestDataClass
    {
        #region Test Object 1
        public class TestObject : INotification
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class TestObjectNotificationHandler1 : IDynamicNotificationHandler<TestObject>
        {
            private readonly IDynamicNotificationManager<TestObject> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("11000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }
            public TestObjectNotificationHandler1(IDynamicNotificationManager<TestObject> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id} - Nmae: {notification.Name}");
            }

            ~TestObjectNotificationHandler1()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }

        public class TestObjectNotificationHandler2 : IDynamicNotificationHandler<TestObject>
        {
            private readonly IDynamicNotificationManager<TestObject> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("12000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }
            public TestObjectNotificationHandler2(IDynamicNotificationManager<TestObject> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id} - Nmae: {notification.Name}");
            }

            ~TestObjectNotificationHandler2()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }

        public class TestObjectNotificationHandler3 
            : IDynamicNotificationHandler<TestObject>, IDisposable
        {
            private readonly IDynamicNotificationManager<TestObject> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("13000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }
            public TestObjectNotificationHandler3(IDynamicNotificationManager<TestObject> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id} - Nmae: {notification.Name}");
            }

            public void Dispose()
            { 
            }

            ~TestObjectNotificationHandler3()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }
        #endregion


        #region Test Object 2
        public class TestObject2 : IDynamicFilteredNotification
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public Dictionary<string, string> Params { get; set; } = null;

        }

        public class TestObject2NotificationHandler1 : IDynamicFilteredNotificationHandler<TestObject2>
        {
            private readonly IDynamicFilteredNotificationManager<TestObject2> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("21000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }

            public Dictionary<string, string> Params { get; set; }
                = new Dictionary<string, string>(
                    new List<KeyValuePair<string,string>>()
                        { new KeyValuePair<string, string>("Id","21000000-0000-0000-0000-000000000000"),
                          new KeyValuePair<string, string>("Name","Test"), } 
                    );

            public TestObject2NotificationHandler1(IDynamicFilteredNotificationManager<TestObject2> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject2 notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id}, Name: {notification.Name}");
            }

            ~TestObject2NotificationHandler1()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }
        public class TestObject2NotificationHandler2 : IDynamicFilteredNotificationHandler<TestObject2>
        {
            private readonly IDynamicFilteredNotificationManager<TestObject2> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("22000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }

            public Dictionary<string, string> Params { get; set; }
                = new Dictionary<string, string>(
                    new List<KeyValuePair<string, string>>()
                        { new KeyValuePair<string, string>("Id","22000000-0000-0000-0000-000000000000"),
                          new KeyValuePair<string, string>("Name","Test"), }
                    );

            public TestObject2NotificationHandler2(IDynamicFilteredNotificationManager<TestObject2> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject2 notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id}, Name: {notification.Name}");
            }

            ~TestObject2NotificationHandler2()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }
        public class TestObject2NotificationHandler3 : IDynamicFilteredNotificationHandler<TestObject2>
        {
            private readonly IDynamicFilteredNotificationManager<TestObject2> _registrar;
            public Guid Id
            {
                get
                {
                    return Guid.Parse("23000000-0000-0000-0000-000000000000"); // {54F3C1CE-03E2-4BC2-94B6-B75AC77F02F4}
                }
            }

            public Dictionary<string, string> Params { get; set; }
                = new Dictionary<string, string>(
                    new List<KeyValuePair<string, string>>()
                        { new KeyValuePair<string, string>("Id","22000000-0000-0000-0000-000000000000"),
                          new KeyValuePair<string, string>("Name","Test"), }
                    );

            public TestObject2NotificationHandler3(IDynamicFilteredNotificationManager<TestObject2> registrar)
            {
                _registrar = registrar;

                // Dynamically add this class as a handler for the notification type 'YourNotificationTypeHere'
                _registrar.RegisterHandler(this);
            }
            public async Task Handle(TestObject2 notification, CancellationToken cancellationToken)
            {
                Debug.WriteLine($"{DateTime.Now}: {notification.Id} fired for {this.Id}, Name: {notification.Name}");
            }

            ~TestObject2NotificationHandler3()
            {
                // Un-register this class as an event handler for the notification type
                _registrar.UnRegisterHandler(this);
            }
        }

        #endregion 
    }
}
