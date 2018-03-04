using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace GigHub.IntegrationTests
{
    //LD "Attribute" is a custom attribute. "ITestAction" is an "Nunit" interface
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(ITest testDetails)
        {
            _transactionScope = new TransactionScope();
        }
 
        public void AfterTest(ITest testDetails)
        {
            _transactionScope.Dispose();
        }

       

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; } //LD mean that this attribute can only be applied to test methods
        }
    }
}