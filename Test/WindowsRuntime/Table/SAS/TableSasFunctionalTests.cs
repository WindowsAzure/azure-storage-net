﻿// -----------------------------------------------------------------------------------------
// <copyright file="TableSasFunctionalTests.cs" company="Microsoft">
//    Copyright 2013 Microsoft Corporation
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//      http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
// -----------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Microsoft.Azure.Storage.Table.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Azure.Storage.Table
{
    [TestClass]
    public class TableSasFunctionalTests : TableTestBase
#if XUNIT
, IDisposable
#endif
    {

#if XUNIT
        // Todo: The simple/nonefficient workaround is to minimize change and support Xunit,
        // removed when we support mstest on projectK
        public TableSasFunctionalTests()
        {
            MyTestInitialize();
        }
        public void Dispose()
        {
            MyTestCleanup();
        }
#endif
        #region Locals + Ctors

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #endregion

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
 
        //
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            if (TestBase.TableBufferManager != null)
            {
                TestBase.TableBufferManager.OutstandingBufferCount = 0;
            }
        }
        //
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            if (TestBase.TableBufferManager != null)
            {
                Assert.AreEqual(0, TestBase.TableBufferManager.OutstandingBufferCount);
            }
        }

        #endregion

        #region Permissions

        [TestMethod]
        [Description("Tests setting and getting table permissions.")]
        [TestCategory(ComponentCategory.Table)]
        [TestCategory(TestTypeCategory.FuntionalTest)]
        [TestCategory(SmokeTestCategory.NonSmoke)]
        [TestCategory(TenantTypeCategory.DevFabric), TestCategory(TenantTypeCategory.Cloud)]
        public async Task TableGetSetPermissionTestAsync()
        {
            CloudTableClient tableClient = GenerateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("T" + Guid.NewGuid().ToString("N"));
            try
            {
                await table.CreateAsync();

                await table.ExecuteAsync(TableOperation.Insert(new BaseEntity("PK", "RK")));

                TablePermissions expectedPermissions;
                TablePermissions testPermissions;

                // Test new table permissions.
                expectedPermissions = new TablePermissions();
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Test setting empty permissions.
                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Add a policy, check setting and getting.
                expectedPermissions.SharedAccessPolicies.Add(Guid.NewGuid().ToString(), new SharedAccessTablePolicy
                {
                    Permissions = SharedAccessTablePermissions.Query,
                    SharedAccessStartTime = DateTimeOffset.Now - TimeSpan.FromHours(1),
                    SharedAccessExpiryTime = DateTimeOffset.Now + TimeSpan.FromHours(1)
                });

                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Add a policy, check setting and getting.
                expectedPermissions.SharedAccessPolicies.Add(Guid.NewGuid().ToString(), new SharedAccessTablePolicy
                {
                    Permissions = SharedAccessTablePermissions.Delete | SharedAccessTablePermissions.Add,
                    SharedAccessStartTime = DateTimeOffset.Now + TimeSpan.FromHours(1),
                    SharedAccessExpiryTime = DateTimeOffset.Now + TimeSpan.FromDays(1)
                });

                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Add a null policy, check setting and getting.
                expectedPermissions.SharedAccessPolicies.Add(Guid.NewGuid().ToString(), new SharedAccessTablePolicy
                {
                    Permissions = SharedAccessTablePermissions.None,
                });

                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Add a policy, check setting and getting.
                expectedPermissions.SharedAccessPolicies.Add(Guid.NewGuid().ToString(), new SharedAccessTablePolicy
                {
                    Permissions = SharedAccessTablePermissions.Add | SharedAccessTablePermissions.Query | SharedAccessTablePermissions.Update | SharedAccessTablePermissions.Delete,
                    SharedAccessStartTime = DateTimeOffset.Now + TimeSpan.FromDays(0.5),
                    SharedAccessExpiryTime = DateTimeOffset.Now + TimeSpan.FromDays(1)
                });

                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);

                // Add a policy, check setting and getting.
                expectedPermissions.SharedAccessPolicies.Add(Guid.NewGuid().ToString(), new SharedAccessTablePolicy
                {
                    Permissions = SharedAccessTablePermissions.Update,
                    SharedAccessStartTime = DateTimeOffset.Now + TimeSpan.FromHours(6),
                    SharedAccessExpiryTime = DateTimeOffset.Now + TimeSpan.FromHours(6.5)
                });

                await table.SetPermissionsAsync(expectedPermissions);
                await Task.Delay(30 * 1000);
                testPermissions = await table.GetPermissionsAsync();
                AssertPermissionsEqual(expectedPermissions, testPermissions);
            }
            finally
            {
                table.DeleteIfExistsAsync().Wait();
            }
        }

        #endregion

        #region Test Helpers
        internal static void AssertPermissionsEqual(TablePermissions permissions1, TablePermissions permissions2)
        {
            Assert.AreEqual(permissions1.SharedAccessPolicies.Count, permissions2.SharedAccessPolicies.Count);

            foreach (KeyValuePair<string, SharedAccessTablePolicy> pair in permissions1.SharedAccessPolicies)
            {
                SharedAccessTablePolicy policy1 = pair.Value;
                SharedAccessTablePolicy policy2 = permissions2.SharedAccessPolicies[pair.Key];

                Assert.IsNotNull(policy1);
                Assert.IsNotNull(policy2);

                Assert.AreEqual(policy1.Permissions, policy2.Permissions);
                if (policy1.SharedAccessStartTime != null)
                {
                    Assert.IsTrue(Math.Floor((policy1.SharedAccessStartTime.Value - policy2.SharedAccessStartTime.Value).TotalSeconds) == 0);
                }

                if (policy1.SharedAccessExpiryTime != null)
                {
                    Assert.IsTrue(Math.Floor((policy1.SharedAccessExpiryTime.Value - policy2.SharedAccessExpiryTime.Value).TotalSeconds) == 0);
                }
            }
        }
        #endregion
    }
}
