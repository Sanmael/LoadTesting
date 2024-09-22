using BusinessLogic;

namespace Loadtesting
{
    [TestClass]
    public class PersonTests
    {
        [TestMethod]
        public async Task GetListPerson()
        {
            //Arrange
            PersonBus personBus = new PersonBus();

            //Act
            bool success = await personBus.GetPersonsAsync(nameof(GetListPerson));

            //Assert
            Assert.IsTrue(success);
        }
    }
}