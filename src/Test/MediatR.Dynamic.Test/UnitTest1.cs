using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static MediatR.Dynamic.Test.TestDataClass;

namespace MediatR.Dynamic.Test
{
    [TestClass]
    public class RegistrationAsyncTest
    {
        DynamicNotificationRegistrar<TestObject> _TestHandler1 = new DynamicNotificationRegistrar<TestObject>(); 
        TestObjectNotificationHandler1 _Handler1_1;
        TestObjectNotificationHandler2 _Handler1_2;


        [TestMethod]
        public async Task ListTest()
        {
            _Handler1_1 = new TestObjectNotificationHandler1(_TestHandler1);
            _Handler1_2 = new TestObjectNotificationHandler2(_TestHandler1);

            Guid _testGuid1 = Guid.NewGuid();
            await _TestHandler1.Handle(new TestObject { Id = _testGuid1, Name="Test1" }, default);

            Task.Run(async () =>
            { 
                await TestInstanceRemoval();
            }) ;
            await Task.Delay(1);
            // send message with removed instance
            await _TestHandler1.Handle(new TestObject { Id = Guid.NewGuid(), Name = "Test3" }, default);
        } 
        async Task TestInstanceRemoval()
        {
            // create instance of 3rd handler
            using (var _temp = new TestObjectNotificationHandler3(_TestHandler1))
            {
                // send message
                await _TestHandler1.Handle(new TestObject { Id = Guid.NewGuid(), Name = "Test2" }, default);
            } 
        }

        #region Dynamic Filter Test

        DynamicFilteredNotificationManager<TestObject2> _TestHandler2 = new DynamicFilteredNotificationManager<TestObject2>();
        TestObject2NotificationHandler1 _Handler2_1;
        TestObject2NotificationHandler2 _Handler2_2;
        TestObject2NotificationHandler3 _Handler2_3;
        TestObject2NotificationHandlerAll _Handler2_ALL;
        [TestMethod]
        public async Task DynamicListTest()
        {
            _Handler2_1 = new TestObject2NotificationHandler1(_TestHandler2);
            _Handler2_2 = new TestObject2NotificationHandler2(_TestHandler2);
            _Handler2_3 = new TestObject2NotificationHandler3(_TestHandler2);
            _Handler2_ALL = new TestObject2NotificationHandlerAll(_TestHandler2);

            // param filter test 1
            Dictionary<string, string> _params1 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Id", "21000000-0000-0000-0000-000000000000")
            });
            Guid _testGuid1 = Guid.NewGuid();
            await _TestHandler2.Handle(new TestObject2
            {
                Id = _testGuid1,
                Name = "_params1",
                Params = _params1
            }, default);

            Dictionary<string, string> _params2 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Id", "21000000-0000-0000-0000-000000000000"),
                new KeyValuePair<string, string>("Name", "test")
            });
            await _TestHandler2.Handle(new TestObject2
            {
                Id = _testGuid1,
                Name = "_params2",
                Params = _params2
            }, default); // this should not get any results

            Dictionary<string, string> _params2_1 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Id", "21000000-0000-0000-0000-000000000000"),
                new KeyValuePair<string, string>("Name", "Test")
            });
            await _TestHandler2.Handle(new TestObject2
            {
                Id = _testGuid1,
                Name = "_params2_1",
                Params = _params2_1
            }, default);
             
            Dictionary<string, string> _params3 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Id", "22000000-0000-0000-0000-000000000000") 
            });
            await _TestHandler2.Handle(new TestObject2
            {
                Id = Guid.NewGuid(),
                Name = "_params3",
                Params = _params3
            }, default);

            Dictionary<string, string> _params4 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("Id", "22000000-0000-0000-0000-000000000000"),
                new KeyValuePair<string, string>("Name", "Test")
            });
            await _TestHandler2.Handle(new TestObject2
            {
                Id = Guid.NewGuid(),
                Name = "_params4",
                Params = _params4
            }, default);

            Dictionary<string, string> _params5 = new Dictionary<string, string>(new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>("Name", "Test")
            });
            await _TestHandler2.Handle(new TestObject2
            {
                Id = Guid.NewGuid(),
                Name = "_params5, All handlers",
                Params = _params5
            }, default);
        }


        #endregion

    }
}
